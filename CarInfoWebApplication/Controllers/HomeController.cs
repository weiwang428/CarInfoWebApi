using System;
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
            _repository.InitDb();
            return View();
        }

        public ContentResult LoadJson()
        {
            var ls = _repository.LoadCarInfoFromFile("");
            return Content(ls.ToString());

        }
        public ContentResult WriteJson()
        {
            var result = _repository.WriteCarInfoIntoFile("");
            return Content(result.ToString());
        }

        //public JsonResult List()
        //{
        //    var ls = _repository.ListCarInfo();
        //    return Json(ls);
        //}

        public ContentResult List()
        {
            var ls = _repository.ListCarInfo();
            return Content(JsonConvert.SerializeObject(ls));
        }


        public ContentResult Delete(int id)
        {
            var ls = _repository.DeleteCar(id);
            return Content(ls.ToString());

        }

        public ContentResult FindCar(int id)
        {
            var ls = _repository.FindCarByDes(id);
            return Content(JsonConvert.SerializeObject(ls));
        }

        public ContentResult Add(int carId, string content)
        {
            var result = _repository.AddDescriptionToCar(carId, content);
            return Content(result.ToString());
        }

        public ContentResult Update(int id)
        {
            Car newCar = new Car() { Brand = "benz", Model = "newtype", Descriptions = null };
            var result = _repository.UpdateCar(id,newCar);
            return Content(result.ToString());
        }

        public ContentResult DeleteDes(int id)
        {
            var result = _repository.DeleteDescriptionOfCar(id);
            return Content(result.ToString());
        }






    }
}
