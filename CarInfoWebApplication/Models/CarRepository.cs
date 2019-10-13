using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using CarInfoWebApplication.CarInfoDbContext;

namespace CarInfoWebApplication.Models
{
    public class CarRepository
    {
        private readonly CarDbContext _dbContext;
        public CarRepository()
        {
            _dbContext = CarDbContext.CreateInstance();
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



    }
}