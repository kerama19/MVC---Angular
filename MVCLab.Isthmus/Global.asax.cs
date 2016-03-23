using Lab.Data.Model;
using Lab.MemberShip;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using System.Web.Optimization;
using System.Web.Routing;
using System.Web.Script.Serialization;
using System.Web.Security;

namespace MVCLab.Isthmus
{
    // Note: For instructions on enabling IIS6 or IIS7 classic mode, 
    // visit http://go.microsoft.com/?LinkId=9394801

    public class MvcApplication : System.Web.HttpApplication
    {
        protected void Application_Start()
        {
            AreaRegistration.RegisterAllAreas();

            WebApiConfig.Register(GlobalConfiguration.Configuration);
            FilterConfig.RegisterGlobalFilters(GlobalFilters.Filters);
            RouteConfig.RegisterRoutes(RouteTable.Routes);
            BundleConfig.RegisterBundles(BundleTable.Bundles);
            AuthConfig.RegisterAuth();
        }

        protected void Application_AuthenticateRequest(Object sender, EventArgs e)
        {
            var authCookie = Request.Cookies[FormsAuthentication.FormsCookieName];

            if (authCookie != null)
            {
                var authTicket = FormsAuthentication.Decrypt(authCookie.Value);

                var serializer = new JavaScriptSerializer();

                var serializeModel = serializer.Deserialize<LabMembershipUser>(authTicket.UserData);

                var newUser = new LabPrincipal(serializeModel.CurrentUserName);

                newUser.Email = serializeModel.Email;
                newUser.Active = serializeModel.Active;
                newUser.CurrentUserName = serializeModel.CurrentUserName;

                var permissions = Lab.Business.Permissions.GetPermissions(serializeModel.CurrentUserName);

                if (permissions != null && permissions.Count > 0)
                {
                    newUser.Permissions = (from A in permissions
                                   select new Permission() { Id = A.Id, Function = A.Function, Rights = A.Rights, UserId = A.UserId, User = null }).ToList();
                }

                HttpContext.Current.User = newUser;
            }
        }
    }
}