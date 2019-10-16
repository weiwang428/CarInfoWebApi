using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Script.Serialization;
using CarInfoWebApplication.CarInfoDbContext;
using Newtonsoft.Json;
using Newtonsoft.Json.Converters;

namespace CarInfoWebApplication.Models
{
    public class CarRepository : ICarRepository
    {
        private readonly CarDbContext _dbContext;
        public CarRepository(CarDbContext db)
        {
            _dbContext = db;
        }

        public string TestDb()
        {
            Description d1 = new Description();
            d1.Content = "This is just a content";
            Description d2 = new Description();
            d2.Content = "This is just a content";
            var n_car = new Car();
            n_car.Model = "BMW";
            n_car.Descriptions.Add(d1);
            n_car.Descriptions.Add(d2);
            _dbContext.Cars.Add(n_car);
            _dbContext.SaveChanges();
            return "";
        }

        public IList<Car> LoadCarInfoFromFile(string fileName)
        {
            //string JsonStrs = "[{Name:'苹果',Price:5.5},{Name:'橘子',Price:2.5},{Name:'柿子',Price:16}]";
            //JavaScriptSerializer Serializers = new JavaScriptSerializer();

            //List<object> objs = Serializers.Deserialize<List<object>>(JsonStrs);
            //return objs;

            string str = File.ReadAllText(@"d:\ascii.txt");
            JavaScriptSerializer Serializers = new JavaScriptSerializer();
            List<Car> objs = Serializers.Deserialize<List<Car>>(str);

            return objs;
        }


        public void AddCarInfoToDb(IList<Car> lst)
        {
            _dbContext.Database.Delete();
            _dbContext.Database.Create();
            _dbContext.Cars.AddRange(lst);
        
        }

        public void WriteCarInfoIntoFile(string fileName)
        {
            StreamWriter sw = new StreamWriter(@"d:\ascii.txt");
            JsonSerializer serializer = new JsonSerializer();
            serializer.Converters.Add(new JavaScriptDateTimeConverter());
            serializer.NullValueHandling = NullValueHandling.Ignore;
            JsonWriter writer = new JsonTextWriter(sw);
            serializer.Serialize(writer, _dbContext.Cars.ToList());
            //ser.Serialize(writer, ht);
            writer.Close();
            sw.Close();

        }


        public bool AddDescriptionToCar(Car car,Description des)
        {
            if (_dbContext.Cars.Where(c => c.CarId == car.CarId) == null)
                return false;

            var result = _dbContext.Cars.Where(c => c.Descriptions.Any(d => d.Id == des.Id)).FirstOrDefault();
            if (result != null)
                return false;
            result.Descriptions.Add(des);
            _dbContext.SaveChanges();
            return true;
        }

        public bool AddDescriptionToCar(int carId, string content)
        {
            var car = _dbContext.Cars.Include("Descriptions").Where(c => c.CarId == carId).FirstOrDefault();
            if ( car == null)
                return false;
            var result = car.Descriptions.Where(d => d.Content.Equals(content)).FirstOrDefault();
            if (result != null)
                return false;
            car.Descriptions.Add(new Description() { Content = content });
            _dbContext.SaveChanges();
            return true;
        }

        public IList<Car> ListCarInfo()
        {
            return _dbContext.Cars.Include("Descriptions").ToList();
            //return _dbContext.Cars.ToList();               
        }

        public Car FindCarByDes(int descriptionId)
        {
            var car = _dbContext.Cars.Where(c => c.Descriptions.Any(d => d.Id == descriptionId)).FirstOrDefault();
            return car;
        }

        public bool DeleteCar(int carId)
        {
            var car = _dbContext.Cars.Where(c => c.CarId == carId).FirstOrDefault();
            if (car == null)
                return false;
            _dbContext.Cars.Remove(car);
            _dbContext.SaveChanges();
            return true;
        }












    }
}