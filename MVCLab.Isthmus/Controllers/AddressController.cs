using Lab.Business.Classes;
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
    public class AddressController : Controller
    {
        [LabAuthorizeAttribute(Function = "Addresses", Rights = "Read")]
        [RestoreModelStateFromTempData]
        public ActionResult AddressList()
        {
            var addresses = Addresses.GetAddressList();

            if (addresses == null)
            {
                ViewBag.Message = "An Error has occured, the address list cannot be displayed";
                ViewData["MsgType"] = "Error";
            }

            if (addresses.Count == 0)
            {
                ViewBag.Message = "There are no addresses to be displayed";
                ViewData["MsgType"] = "Warning";
            }

            return View(addresses);
        }

        [LabAuthorizeAttribute(Function = "Addresses", Rights = "Create")]
        public ViewResult AddAddress()
        {
            var address = new Address();

            return View("EditAddress", address);
        }

        [LabAuthorizeAttribute(Function = "Addresses", Rights = "Read")]
        [RestoreModelStateFromTempData]
        public ViewResult EditAddress(int id)
        {
            var address = Addresses.GetAddressById(id);

            if (address == null)
            {
                ViewBag.Message = "An error has occured, the address details cannot be displayed";
                ViewData["MsgType"] = "Error";
            }

            return View(address);
        }

        [LabAuthorizeAttribute(Function = "Addresses", Rights = "Update")]
        [SetTempDataModelState]
        public ActionResult UpdateAddress(Address address, string returnUrl)
        {
            var Address = Addresses.SaveAddress(address);

            if (Address == null)
            {
                ViewBag.Message = "An error has occured, the address could not be saved";
                ViewData["MsgType"] = "Error";

                return View("EditAddress", address);
            }

            ViewData["MsgType"] = "Success";
            ViewBag.Message = "The address was saved successfully";

            return RedirectToAction("EditAddress", "Address", new { id = Address.Id });
        }

        [LabAuthorizeAttribute(Function = "Addresses", Rights = "Create")]
        [SetTempDataModelState]
        public ActionResult CreateAddress(Address address, string returnUrl)
        {
            var Address = Addresses.SaveAddress(address);

            if (Address == null)
            {
                ViewBag.Message = "An error has occured, the address could not be saved";
                ViewData["MsgType"] = "Error";

                return View("EditAddress", address);
            }

            ViewData["MsgType"] = "Success";
            ViewBag.Message = "The address was saved successfully";

            return RedirectToAction("EditAddress", "Address", new { id = Address.Id });
        }

        [LabAuthorizeAttribute(Function = "Addresses", Rights = "Delete")]
        [SetTempDataModelState]
        public ActionResult DeleteAddress(int id)
        {
            var result = Addresses.DeleteAddress(id);

            if (!result)
            {
                ViewBag.Message = "An error has occured, the address could not be deleted";
                ViewData["MsgType"] = "Error";
            }

            ViewData["MsgType"] = "Success";
            ViewBag.Message = "The address was deleted successfully";

            return RedirectToAction("AddressList", "Address");
        }
    }
}
