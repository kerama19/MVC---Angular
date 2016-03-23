using Lab.Business;
using Lab.Data.Model;
using Lab.Data.VirtualModel;
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
    public class PermissionController : Controller
    {
        [LabAuthorizeAttribute(Function = "Permissions", Rights = "Read")]
        public ActionResult ViewPermissions(string username)
        {
            return PartialView();
        }
        public JsonResult GetPermissions(string username)
        {
            var users = Permissions.GetUsersWithTempPermissions(username);

            if (users == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;
            }

            return Json(new { Users = users }, "application/json", JsonRequestBehavior.AllowGet);
        }

        [LabAuthorizeAttribute(Function = "Permissions", Rights = "Read")]
        public ActionResult ViewPermissionsList()
        {
            return PartialView("ViewPermissions");
        }

        [LabAuthorizeAttribute(Function = "Permissions", Rights = "Read")]
        public ActionResult EditPermissions(string username, string function)
        {
            return PartialView();
        }

        public JsonResult GetPermissionsByUserFunction(string username, string currentFunction)
        {
            var permissions = Permissions.GetUserPermissionsByFucntion(username, currentFunction);

            if (permissions == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;
            }

            return Json(permissions, "application/json", JsonRequestBehavior.AllowGet);
        }

        [LabAuthorizeAttribute(Function = "Permissions", Rights = "Create")]
        [LabAuthorizeAttribute(Function = "Permissions", Rights = "Delete")]
        public JsonResult SavePermissions(string Data)
        {
            var serializer = new JavaScriptSerializer();

            var permissions = serializer.Deserialize<TempPermissions>(Data);

            var permission = Permissions.SavePermissions(permissions);

            if (permission == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;
            }

            return Json(permission, "application/json", JsonRequestBehavior.AllowGet);           
        }
    }
}
