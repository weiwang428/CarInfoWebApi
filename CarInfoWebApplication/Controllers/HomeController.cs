using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using CarInfoWebApplication.Models;

namespace CarInfoWebApplication.Controllers
{
    public class HomeController : Controller
    {
        CarRepository _repository = new CarRepository();
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


    }
}
