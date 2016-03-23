using Lab.Business;
using Lab.Data.Model;
using Lab.MemberShip;
using MVCLab.Isthmus.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLab.Isthmus.Controllers
{
    public class CarController : Controller
    {
        [LabAuthorizeAttribute(Function = "Cars", Rights = "Read")]
        [RestoreModelStateFromTempData]
        public ActionResult CarList()
        {
            var cars = Cars.GetCarList();

            if (cars == null)
            {
                ViewBag.Message = "An Error has occured, the car list cannot be displayed";
                ViewData["MsgType"] = "Error";
            }

            if (cars.Count == 0)
            {
                ViewBag.Message = "There are no cars to be displayed";
                ViewData["MsgType"] = "Warning";
            }

            return View(cars);
        }

        [LabAuthorizeAttribute(Function = "Cars", Rights = "Create")]
        public ViewResult AddCar()
        {
            var car = new Car();

            return View("EditCar", car);
        }

        [LabAuthorizeAttribute(Function = "Cars", Rights = "Create")]
        [SetTempDataModelState]
        public ActionResult CreateCar(Car car, string returnUrl)
        {
            var Car = Cars.SaveCar(car);

            if (Car == null)
            {
                ViewBag.Message = "An error has occured, the car could not be saved";
                ViewData["MsgType"] = "Error";

                return View("EditCar", car);
            }

            ViewData["MsgType"] = "Success";
            ViewBag.Message = "The car was saved successfully";

            return RedirectToAction("EditCar", "Car", new { id = Car.Id });
        }

        [LabAuthorizeAttribute(Function = "Cars", Rights = "Read")]
        [RestoreModelStateFromTempData]
        public ViewResult EditCar(int Id)
        {
            var car = Cars.GetCarById(Id);

            if (car == null)
            {
                ViewBag.Message = "An error has occured, the car details cannot be displayed";
                ViewData["MsgType"] = "Error";
            }

            return View(car);
        }

        [LabAuthorizeAttribute(Function = "Cars", Rights = "Update")]
        [SetTempDataModelState]
        public ActionResult UpdateCar(Car car, string returnUrl)
        {
            var Car = Cars.SaveCar(car);

            if (Car == null)
            {
                ViewBag.Message = "An error has occured, the car could not be saved";
                ViewData["MsgType"] = "Error";

                return View("EditCar", car);
            }

            ViewData["MsgType"] = "Success";
            ViewBag.Message = "The car was saved successfully";

            return RedirectToAction("EditCar", "Car", new { id = Car.Id });
        }

        [LabAuthorizeAttribute(Function = "Cars", Rights = "Delete")]
        [SetTempDataModelState]
        public ActionResult DeleteCar(int id)
        {
            var result = Cars.DeleteCar(id);

            if (!result)
            {
                ViewBag.Message = "An error has occured, the car could not be deleted";
                ViewData["MsgType"] = "Error";
            }

            ViewData["MsgType"] = "Success";
            ViewBag.Message = "The car was deleted successfully";

            return RedirectToAction("CarList", "Car");
        }
    }
}
