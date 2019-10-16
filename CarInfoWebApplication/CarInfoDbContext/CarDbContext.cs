using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SQLite.CodeFirst;
using System.Data.Entity;
using CarInfoWebApplication.Models;

namespace CarInfoWebApplication.CarInfoDbContext
{
    public class CarDbContext : DbContext
    {
        private static CarDbContext _instance;
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            var sqliteConnectionInitializer = new SqliteCreateDatabaseIfNotExists<CarDbContext>(modelBuilder);
            Database.SetInitializer(sqliteConnectionInitializer);
        }
        public CarDbContext() : base("db_car") {
        

        }

        public static CarDbContext CreateInstance()
        {
            if (_instance == null)
                _instance = new CarDbContext();
            return _instance;
        }

        public DbSet<Car> Cars { get; set; }
        public DbSet<Description> Descriptions { get; set; }
    }
}