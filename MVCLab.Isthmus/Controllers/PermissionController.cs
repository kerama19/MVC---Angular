using Lab.Business;
using Lab.Data.Model;
using Lab.Data.VirtualModel;
using Lab.MemberShip;
using MVCLab.Isthmus.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLab.Isthmus.Controllers
{
    public class PermissionController : Controller
    {
        [LabAuthorizeAttribute(Function = "Permissions", Rights = "Read")]
        [SetTempDataModelState]
        public ViewResult ViewPermissions(string username)
        {
            var permissions = Permissions.GetPermissionsByUser(username);

            if (permissions == null)
            {
                ViewBag.Message = "An Error has occured, the user permissions list cannot be displayed";
                ViewData["MsgType"] = "Error";
            }

            ViewData["Type"] = "SingleUser";

            return View(permissions);
        }

        [LabAuthorizeAttribute(Function = "Permissions", Rights = "Read")]
        [SetTempDataModelState]
        public ViewResult ViewPermissionsList()
        {
            var permissions = Permissions.GetPermissionsList();

            if (permissions == null)
            {
                ViewBag.Message = "An Error has occured, the user permissions list cannot be displayed";
                ViewData["MsgType"] = "Error";
            }

            ViewData["Type"] = "MultiUser";

            return View("ViewPermissions", permissions);
        }

        [LabAuthorizeAttribute(Function = "Permissions", Rights = "Read")]
        [RestoreModelStateFromTempData]
        [SetTempDataModelState]
        public ViewResult EditPermissions(string username, string function)
        {
            var permissions = Permissions.GetUserPermissionsByFucntion(username, function);

            if (permissions == null)
            {
                ViewBag.Message = "An error has occured, the user permissions cannot be displayed";
                ViewData["MsgType"] = "Error";
            }

            return View(permissions);
        }

        [LabAuthorizeAttribute(Function = "Permissions", Rights = "Create")]
        [LabAuthorizeAttribute(Function = "Permissions", Rights = "Delete")]
        [RestoreModelStateFromTempData]
        [SetTempDataModelState]
        public ActionResult SavePermissions(TempPermissions permissions)
        {
            var permission = Permissions.SavePermissions(permissions);

            if (permission == null)
            {
                ViewBag.Message = "An error has occured, the user permissions could not be saved";
                ViewData["MsgType"] = "Error";

                return View("EditPermissions", permissions);
            }

            ViewBag.Message = "The user permissions were saved succesfully";
            ViewData["MsgType"] = "Success";

            return RedirectToAction("EditPermissions", "Permission", new { username = permission.UserName, function = permission.currentFunction, type = ViewData["Type"] });            
        }
    }
}
