using Lab.Business;
using Lab.Data.Model;
using Lab.MemberShip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Web.Script.Serialization;

namespace MVCLab.Isthmus.Controllers
{
    public class CarController : Controller
    {
        [LabAuthorizeAttribute(Function = "Cars", Rights = "Read")]
        public ActionResult CarList()
        {
            return PartialView();
        }

        public JsonResult GetCarList()
        {
            var cars = Cars.GetCarList();

            if (cars == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;
            }

            return Json(cars, "application/json", JsonRequestBehavior.AllowGet);
        }

        [LabAuthorizeAttribute(Function = "Cars", Rights = "Create")]
        public ActionResult AddCar()
        {
            return PartialView("EditCar");
        }

        [LabAuthorizeAttribute(Function = "Cars", Rights = "Read")]
        public ActionResult EditCar()
        {
            return PartialView();
        }

        public JsonResult GetCar(int Id)
        {
            var car = Cars.GetCarById(Id);

            if (car == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;
            }

            return Json(car, "application/json", JsonRequestBehavior.AllowGet);
        }

        [LabAuthorizeAttribute(Function = "Cars", Rights = "Create")]
        public ActionResult CreateCar(string Data)
        {
            var serializer = new JavaScriptSerializer();

            var car = serializer.Deserialize<Car>(Data);

            var Car = Cars.SaveCar(car);

            if (Car == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;
            }

            return Json(Car, "application/json", JsonRequestBehavior.AllowGet);
        }

        [LabAuthorizeAttribute(Function = "Cars", Rights = "Update")]
        public ActionResult UpdateCar(string Data)
        {
            var serializer = new JavaScriptSerializer();

            var car = serializer.Deserialize<Car>(Data);

            var Car = Cars.SaveCar(car);

            if (Car == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;
            }

            return Json(Car, "application/json", JsonRequestBehavior.AllowGet);
        }

        [LabAuthorizeAttribute(Function = "Cars", Rights = "Delete")]
        public JsonResult DeleteCar(int id)
        {
            var result = Cars.DeleteCar(id);

            if (!result)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;
            }

            var cars = Cars.GetCarList();

            if (cars == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;
            }

            return Json(cars, "application/json", JsonRequestBehavior.AllowGet);
        }
    }
}
