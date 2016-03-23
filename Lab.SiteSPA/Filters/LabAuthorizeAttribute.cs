﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Web.Routing;
using Lab.MemberShip;

namespace Lab.MemberShip
{
    public class BaseController : Controller
    {
        protected virtual LabPrincipal CurrentUser
        {
            get
            {
                return HttpContext.User as LabPrincipal;
            }
        }
    }

    public abstract class BaseViewPage : WebViewPage
    {
        public virtual LabPrincipal CurrentUser
        {
            get { return base.User as LabPrincipal; }
        }
    }
    public abstract class BaseViewPage<TModel> : WebViewPage<TModel>
    {
        public virtual LabPrincipal CurrentUser
        {
            get { return base.User as LabPrincipal; }
        }
    }
    public class LabAuthorizeAttribute : AuthorizeAttribute
    {
        public string Rights { get; set; }
        
        public string Function { get; set; }

        protected virtual LabPrincipal CurrentUser
        {
            get
            {
                return HttpContext.Current.User as LabPrincipal;
            }
        }

        public override void OnAuthorization(AuthorizationContext filterContext)
        {
            if(filterContext.HttpContext.Request.IsAuthenticated)
            {
                if (!String.IsNullOrEmpty(Function) && !String.IsNullOrEmpty(Rights))
                {
                    if (!(CurrentUser != null && CurrentUser.IsInRole(Function, Rights)))
                    {
                        if (filterContext.HttpContext.Request.UrlReferrer == null)
                        {
                            filterContext.Result = new RedirectResult("../#!/AccessDenied");
                        }
                        else
                        {
                            filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "AccessDenied" }));
                        }
                    }
                }
                else
                {
                    if (filterContext.HttpContext.Request.UrlReferrer == null)
                    {
                        filterContext.Result = new RedirectResult("../#!/AccessDenied");
                    }
                    else
                    {
                        filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "AccessDenied" }));
                    }
                }
            }
            else
            {
                if(filterContext.HttpContext.Request.UrlReferrer == null)
                {
                    filterContext.Result = new RedirectResult("../#!/AccessDenied");                    
                }
                else
                {
                    filterContext.Result = new RedirectToRouteResult(new RouteValueDictionary(new { controller = "Account", action = "AccessDenied" }));
                }                
            }
        }
    }
}
