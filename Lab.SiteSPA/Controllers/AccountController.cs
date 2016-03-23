using System;
using System.Collections.Generic;
using System.Linq;
using System.Transactions;
using System.Web;
using System.Web.Mvc;
using System.Web.Security;
using DotNetOpenAuth.AspNet;
using Microsoft.Web.WebPages.OAuth;
using WebMatrix.WebData;
using Lab.Data.Model;
using Lab.Business;
using Lab.MemberShip;
using System.Web.Script.Serialization;
using System.Net;

namespace MVCLab.Isthmus.Controllers
{    
    public class AccountController : Controller
    {
        public IMembershipService MembershipService { get; set; }
        public JavaScriptSerializer serializer = new JavaScriptSerializer();

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (MembershipService == null) { MembershipService = new MembershipService(); }

            base.Initialize(requestContext);
        }

        [HttpPost]
        public JsonResult Login(string Data)
        {
            var currentUser = serializer.Deserialize<User>(Data);

            var user = MembershipService.ValidateUser(currentUser.UserName, currentUser.Password);

            if (user != null)
            {
                var userData = serializer.Serialize(user);

                var authTicket = new FormsAuthenticationTicket(
                                1,
                                user.UserName,
                                DateTime.Now,
                                DateTime.Now.AddMinutes(15),
                                false,
                                userData);

                var encTicket = FormsAuthentication.Encrypt(authTicket);

                var faCookie = new HttpCookie(FormsAuthentication.FormsCookieName, encTicket);

                Response.Cookies.Remove(FormsAuthentication.FormsCookieName);
                Response.Cookies.Add(faCookie);

                var permissions = Permissions.GetPermissionsByUser(user.UserName);

                return Json(new { User = user, Permissions = permissions }, "application/json", JsonRequestBehavior.AllowGet);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return null;
        }

        public JsonResult Validate()
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var user = serializer.Deserialize<LabMembershipUser>(authTicket.UserData);

                var permissions = Permissions.GetPermissionsByUser(user.CurrentUserName);

                return Json(new { User = user, Permissions = permissions }, "application/json", JsonRequestBehavior.AllowGet);
            }

            Response.StatusCode = (int)HttpStatusCode.BadRequest;

            return null;
        }

        public bool LogOff()
        {
            FormsAuthentication.SignOut();

            return true;
        }
        public ActionResult AccessDenied()
        {
            return PartialView();
        }
    }
}
