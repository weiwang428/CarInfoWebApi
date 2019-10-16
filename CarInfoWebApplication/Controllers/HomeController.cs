﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarInfoWebApplication.Models;
using Newtonsoft.Json;

namespace CarInfoWebApplication.Controllers
{
    public class HomeController : Controller
    {
        //CarRepository _repository = new CarRepository();
        ICarRepository _repository;
        public HomeController(ICarRepository repo)
        {
            _repository = repo;
        }

        public ActionResult Index()
        {
            ViewBag.Title = "Home Page";

            return View();
        }


        public ActionResult Test()
        {
            _repository.TestDb();
            return View();
        }

        public ContentResult LoadJson()
        {
            IList<Car> ls = _repository.LoadCarInfoFromFile("");
            return Content(JsonConvert.SerializeObject(ls));

        }
        public ContentResult WriteJson()
        {
            _repository.WriteCarInfoIntoFile("");
            return Content("done");

        }
    }
}
