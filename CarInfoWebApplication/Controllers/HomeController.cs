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

        //public ContentResult LoadJson()
        //{
        //    var ls = _repository.LoadCarInfoFromFile("");
        //    return Content(ls.ToString());
        //}

        public ContentResult WriteJson()
        {
            var result = _repository.WriteCarInfoIntoFile();
            return Content(result.ToString());
        }

        [HttpPost]
        public ContentResult LoadJson()
        {
            var ls = _repository.LoadCarInfoFromFile("config");
            return Content(ls.ToString());
        }

        public HttpResponseBase List()
        {
            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(JsonConvert.SerializeObject(_repository.ListCarInfo()));
            Response.End();
            return Response;
        }

        public ContentResult Delete(int id)
        {
            var ls = _repository.DeleteCar(id);
            return Content(ls.ToString());
        }

        public HttpResponseBase FindCar(int id)
        {
            var ls = _repository.FindCarByDes(id);
            Response.Clear();
            Response.ContentType = "application/json; charset=utf-8";
            Response.Write(JsonConvert.SerializeObject(ls));
            Response.End();
            return Response;
        }

        public ContentResult Add(int carId, string content)
        {
            var result = _repository.AddDescriptionToCar(carId, content);
            return Content(result.ToString());
        }

        public ContentResult Update(int id)
        {
            Car newCar = new Car() { Brand = "benz", Model = "newtype"};
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
