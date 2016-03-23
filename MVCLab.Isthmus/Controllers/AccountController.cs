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
using MVCLab.Isthmus.Filters;

namespace MVCLab.Isthmus.Controllers
{    
    public class AccountController : Controller
    {
        public IMembershipService MembershipService { get; set; }

        protected override void Initialize(System.Web.Routing.RequestContext requestContext)
        {
            if (MembershipService == null) { MembershipService = new MembershipService(); }

            base.Initialize(requestContext);
        }

        [AllowAnonymous]
        public ActionResult Login(string returnUrl)
        {
            ViewBag.ReturnUrl = returnUrl;
            return View();
        }

        [HttpPost]
        [SetTempDataModelState] 
        public ActionResult Login(User model, string returnUrl)
        {
            if(ModelState.IsValid)
            {
                var user = MembershipService.ValidateUser(model.UserName, model.Password);

                if(user != null)
                { 
                    var serializer = new JavaScriptSerializer();

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

                    ViewBag.Message = "Welcome " + user.UserName;
                    ViewData["MsgType"] = "Info";

                    return RedirectToAction("Index", "Home");
                }
            }

            ViewBag.Message = "The user name or password provided is incorrect.";
            ViewData["MsgType"] = "Error";

            return RedirectToAction("Index", "Home");
        }

        public ActionResult LogOff()
        {
            FormsAuthentication.SignOut();

            return RedirectToAction("Index", "Home");
        }

        [RestoreModelStateFromTempData]
        public ViewResult AccessDenied()
        {
            return View("Error");
        }
    }
}
