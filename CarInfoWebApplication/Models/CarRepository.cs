﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using CarInfoWebApplication.CarInfoDbContext;
using Newtonsoft.Json;


namespace CarInfoWebApplication.Models
{
    public class CarRepository : ICarRepository
    {
        private readonly CarDbContext _dbContext;
        private string filePath = HttpContext.Current.Server.MapPath("~/App_Data/config.txt");
        public CarRepository(CarDbContext db)
        {
            _dbContext = db;
        }

        /// <summary>
        /// Build several items in database, initialize the database.
        /// </summary>
        public void InitDb()
        {
            Description d1 = new Description();
            d1.Content = "This is just a content";
            Description d2 = new Description();
            d2.Content = "This is just another content";
            var n_car = new Car();
            n_car.Model = "BMW";
            n_car.Descriptions.Add(d1);
            n_car.Descriptions.Add(d2);
            _dbContext.Cars.Add(n_car);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Client upload a json file, server load data from the json file, and use the data to update the database.
        /// </summary>
        /// <param name="fileName">The file you want to read data.</param>
        /// <returns>True if load sucessfully, otherwise false.</returns>
        public bool LoadCarInfoFromFile(string fileName)
        {
            try
            {
                HttpRequest httpRequest = HttpContext.Current.Request;
                var file = httpRequest.Files[fileName];
                if (file == null)
                    return false;
                if (File.Exists(filePath))
                {
                    File.Delete(filePath);
                }
                file.SaveAs(filePath);
                string str = File.ReadAllText(filePath);
                JsonSerializerSettings settings = new JsonSerializerSettings();
                settings.Formatting = Formatting.Indented;
                settings.NullValueHandling = NullValueHandling.Ignore;
                var cars = JsonConvert.DeserializeObject<List<Car>>(str, settings);
                AddCarInfoToDb(cars);
                return true;

                //string str = File.ReadAllText(@"d:\ascii.txt");
                //JsonSerializerSettings settings = new JsonSerializerSettings();
                //settings.Formatting = Formatting.Indented;
                //settings.NullValueHandling = NullValueHandling.Ignore;
                //var cars = JsonConvert.DeserializeObject<List<Car>>(str, settings);
                ////JavaScriptSerializer Serializers = new JavaScriptSerializer();
                ////List<Car> cars = Serializers.Deserialize<List<Car>>(str);
                //AddCarInfoToDb(cars);
                //return true;
            }
            catch (Exception e)
            {
                return false;
            }

        }

        /// <summary>
        /// Use an input car list to initialize the database.
        /// </summary>
        /// <param name="lst">A car list used to initialize the database.</param>
        public void AddCarInfoToDb(IList<Car> lst)
        {
            //_dbContext.Database.Delete();
            //_dbContext.Database.Create();
            _dbContext.Descriptions.RemoveRange(_dbContext.Descriptions);
            _dbContext.Cars.RemoveRange(_dbContext.Cars);
            _dbContext.Cars.AddRange(lst);
            _dbContext.SaveChanges();
        }

        /// <summary>
        /// Write the car information from database to a json file.
        /// </summary>
        /// <returns>True if the write sucessfully, otherwise false.</returns>
        public bool WriteCarInfoIntoFile()
        {
            try
            {
                StreamWriter sw = new StreamWriter(filePath);
                Formatting format = Formatting.Indented;
                sw.WriteLine(JsonConvert.SerializeObject(_dbContext.Cars.Include("Descriptions").ToList(), format));
                //JsonSerializer serializer = new JsonSerializer();
                //serializer.Converters.Add(new JavaScriptDateTimeConverter());
                //serializer.NullValueHandling = NullValueHandling.Ignore;
                //JsonWriter writer = new JsonTextWriter(sw);
                //serializer.Serialize(writer, _dbContext.Cars.Include("Descriptions").ToList());
                //writer.Close();
                sw.Close();
                return true;
            }
            catch (Exception)
            {
                return false;
            }
        }


        /// <summary>
        /// List car information, includes describtions.
        /// </summary>
        /// <returns>A list includes all cars information.</returns>
        public IList<Car> ListCarInfo()
        {
            return _dbContext.Cars.Include("Descriptions").ToList();
        }

        /// <summary>
        /// Give a description id, find the related car.
        /// </summary>
        /// <param name="descriptionId">A description id.</param>
        /// <returns>A car which includs the given description.</returns>
        public Car FindCarByDes(int descriptionId)
        {
            var car = _dbContext.Cars.Include("Descriptions").Where(c => c.Descriptions.Any(d => d.Id == descriptionId)).FirstOrDefault();
            return car;
        }

        /// <summary>
        /// Delete a car from database by using a give car id.
        /// </summary>
        /// <param name="carId">The car id you want to delete.</param>
        /// <returns>True if delete sucessfully, otherwise false.</returns>
        public bool DeleteCar(int carId)
        {
            var car = _dbContext.Cars.Include("Descriptions").Where(c => c.CarId == carId).FirstOrDefault();
            if (car == null)
                return false;
            _dbContext.Cars.Remove(car);
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// Update car information by using a given car object
        /// </summary>
        /// <param name="carId"></param>
        /// <param name="car"></param>
        /// <returns></returns>
        public bool UpdateCar(int carId, Car car)
        {
            if (car == null)
                return false;
            var result = _dbContext.Cars.Where(c => c.CarId == carId).FirstOrDefault();
            if (result == null)
                return false;
            result.Brand = car.Brand;
            result.Model = car.Model;
            result.Color = car.Color;
            result.Price = car.Price;
            _dbContext.SaveChanges();
            return true;
        }

        #region fuctions of description.
        /// <summary>
        /// Add description to a given car.
        /// </summary>
        /// <param name="carId">The car you want to add description.</param>
        /// <param name="content">The content of the description you want to add.</param>
        /// <returns>True if the add sucessfully, otherwise false.</returns>
        public bool AddDescriptionToCar(int carId, string content)
        {
            var car = _dbContext.Cars.Include("Descriptions").Where(c => c.CarId == carId).FirstOrDefault();
            if (car == null)
                return false;
            var result = car.Descriptions.Where(d => d.Content.Equals(content)).FirstOrDefault();
            if (result != null)
                return false;
            car.Descriptions.Add(new Description() { Content = content });
            _dbContext.SaveChanges();
            return true;
        }

        /// <summary>
        /// Delete a description from database by using a give description id.
        /// </summary>
        /// <param name="descriptionId">The description id you want to delete.</param>
        /// <returns>True if delete sucessfully, otherwise false.</returns>
        public bool DeleteDescriptionOfCar(int descriptionId)
        {
            var des = _dbContext.Descriptions.Where(d => d.Id == descriptionId).FirstOrDefault();
            if (des == null)
                return false;
            _dbContext.Descriptions.Remove(des);
            _dbContext.SaveChanges();
            return true;
        }

        #endregion

    }
}