using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Lab.Business;
using Lab.MemberShip;
using Lab.Data.Model;
using System.Net;
using System.Web.Script.Serialization;

namespace MVCLab.Isthmus.Controllers
{
    public class UserController : BaseController
    {
        [LabAuthorizeAttribute(Function = "Users", Rights = "Read")]
        public ActionResult UserList()
        {
            return PartialView();
        }
        
        public JsonResult GetUserList()
        {
            var users = (from A in Users.GetUserList()
                         select new User { Id = A.Id, Active = A.Active, Email = A.Email, Password = A.Password, UserName = A.UserName }).ToList();

            if (users == null || users.Count == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;
            }

            return Json(users, "application/json", JsonRequestBehavior.AllowGet);
        }

        [LabAuthorizeAttribute(Function = "Users", Rights = "Read")]
        public ActionResult EditUser()
        {
            return PartialView();
        }

        public JsonResult GetUser(string username)
        {
            var user = Users.GetUserByUsername(username);

            if (user == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;
            }
            return Json(user, "application/json", JsonRequestBehavior.AllowGet);
        }

        [LabAuthorizeAttribute(Function = "Users", Rights = "Create")]
        public ActionResult AddUser()
        {
            return PartialView("EditUser");
        }

        [LabAuthorizeAttribute(Function = "Users", Rights = "Update")]
        public JsonResult UpdateUser(string Data)
        {
            var serializer = new JavaScriptSerializer();

            var user = serializer.Deserialize<User>(Data);

            var User = Users.SaveUser(user);

            if (User == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;
            }

            return Json(User, "application/json", JsonRequestBehavior.AllowGet);         
        }

        [LabAuthorizeAttribute(Function = "Users", Rights = "Create")]
        public ActionResult CreateUser(string Data)
        {
            var serializer = new JavaScriptSerializer();

            var user = serializer.Deserialize<User>(Data);

            var User = Users.SaveUser(user);

            if (User == null)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;
            }

            return Json(User, "application/json", JsonRequestBehavior.AllowGet);
        }

        [LabAuthorizeAttribute(Function = "Users", Rights = "Delete")]
        public JsonResult DeleteUser(string username)
        {
            var result = Users.DeleteUser(username);

            if (!result)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;

            }

            var users = (from A in Users.GetUserList()
                         select new User { Id = A.Id, Active = A.Active, Email = A.Email, Password = A.Password, UserName = A.UserName }).ToList();

            if (users == null || users.Count == 0)
            {
                Response.StatusCode = (int)HttpStatusCode.BadRequest;

                return null;
            }

            return Json(users, "application/json", JsonRequestBehavior.AllowGet);
        }        
    }
}
