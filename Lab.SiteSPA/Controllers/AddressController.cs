using Lab.Business.Classes;
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
    public class AddressController : Controller
    {
        [LabAuthorizeAttribute(Function = "Addresses", Rights = "Read")]
        public ActionResult AddressList()
        {
            return PartialView();
        }

        public JsonResult GetAddressList()
        {
            var addresses = Addresses.GetAddressList();

            if (addresses == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;
            }

            return Json(addresses, "application/json", JsonRequestBehavior.AllowGet);
        }

        [LabAuthorizeAttribute(Function = "Addresses", Rights = "Create")]
        public ActionResult AddAddress()
        {
            return PartialView("EditAddress");
        }

        [LabAuthorizeAttribute(Function = "Addresses", Rights = "Read")]
        public ActionResult EditAddress()
        {
            return PartialView();
        }

        public JsonResult GetAddress(int id)
        {
            var address = Addresses.GetAddressById(id);

            if (address == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;
            }

            return Json(address, "application/json", JsonRequestBehavior.AllowGet);
        }

        [LabAuthorizeAttribute(Function = "Addresses", Rights = "Update")]
        public ActionResult UpdateAddress(string Data)
        {
            var serializer = new JavaScriptSerializer();

            var address = serializer.Deserialize<Address>(Data);

            var Address = Addresses.SaveAddress(address);

            if (Address == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;
            }

            return Json(Address, "application/json", JsonRequestBehavior.AllowGet);
        }

        [LabAuthorizeAttribute(Function = "Addresses", Rights = "Create")]
        public ActionResult CreateAddress(string Data)
        {
            var serializer = new JavaScriptSerializer();

            var address = serializer.Deserialize<Address>(Data);

            var Address = Addresses.SaveAddress(address);

            if (Address == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;
            }

            return Json(Address, "application/json", JsonRequestBehavior.AllowGet);
        }

        [LabAuthorizeAttribute(Function = "Addresses", Rights = "Delete")]
        public ActionResult DeleteAddress(int id)
        {
            var result = Addresses.DeleteAddress(id);

            if (!result)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;
            }

            var addresses = Addresses.GetAddressList();

            if (addresses == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;
            }

            return Json(addresses, "application/json", JsonRequestBehavior.AllowGet);
        }
    }
}
