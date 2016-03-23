using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab.Business;
using Lab.MemberShip;
using Lab.Data.Model;
using MVCLab.Isthmus.Filters;

namespace MVCLab.Isthmus.Controllers
{
    public class UserController : BaseController
    {
        [LabAuthorizeAttribute(Function = "Users", Rights = "Read")]
        [RestoreModelStateFromTempData]
        public ActionResult UserList()
        {
            var users = Users.GetUserList();

            if (users == null)
            {
                ViewBag.Message = "An Error has occured, the user list cannot be displayed";
                ViewData["MsgType"] = "Error";
            }

            if (users.Count == 0)
            {
                ViewBag.Message = "There are no users to be displayed";
                ViewData["MsgType"] = "Warning";
            }

            return View(users);
        }

        [LabAuthorizeAttribute(Function = "Users", Rights = "Read")]
        [RestoreModelStateFromTempData]
        public ViewResult EditUser(string username)
        {
            var user = Users.GetUserByUsername(username);

            if (user == null)
            {
               ViewBag.Message = "An error has occured, the user details cannot be displayed";
               ViewData["MsgType"] = "Error";
            }

            return View(user);
        }

        [LabAuthorizeAttribute(Function = "Users", Rights = "Create")]
        public ViewResult AddUser()
        {
            var user = new User();

            return View("EditUser", user);
        }

        [LabAuthorizeAttribute(Function = "Users", Rights = "Update")]
        [SetTempDataModelState]
        public ActionResult UpdateUser(User user, string returnUrl)
        {
            var User = Users.SaveUser(user);

            if (User == null)
            {
                ViewBag.Message = "An error has occured, the user could not be saved";
                ViewData["MsgType"] = "Error";

                return View("EditUser", user);
            }

            ViewBag.Message = "The user was saved succesfully";
            ViewData["MsgType"] = "Success";

            return RedirectToAction("EditUser", "User", new { username = User.UserName });         
        }

        [LabAuthorizeAttribute(Function = "Users", Rights = "Create")]
        [SetTempDataModelState]
        public ActionResult CreateUser(User user, string returnUrl)
        {
            var User = Users.SaveUser(user);

            if (User == null)
            {
                ViewBag.Message = "An error has occured, the user could not be saved";
                ViewData["MsgType"] = "Error";

                return View("EditUser", user);
            }

            ViewBag.Message = "The user was saved succesfully";
            ViewData["MsgType"] = "Success";

            return RedirectToAction("EditUser", "User", new { username = User.UserName });
        }

        [LabAuthorizeAttribute(Function = "Users", Rights = "Delete")]
        [SetTempDataModelState] 
        public ActionResult DeleteUser(string username)
        {
            var result = Users.DeleteUser(username);

            if (!result)
            {
                ViewBag.Message = "An error has occured, the user could not be deleted";
                ViewData["MsgType"] = "Error";
            }

            ViewData["MsgType"] = "Success";
            ViewBag.Message = "The user was deleted successfully";

            return RedirectToAction("UserList", "User");
        }        
    }
}
