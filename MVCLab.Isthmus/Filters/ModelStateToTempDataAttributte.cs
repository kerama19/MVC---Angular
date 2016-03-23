using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLab.Isthmus.Filters
{
    public class SetTempDataModelStateAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuted(ActionExecutedContext filterContext)
        {
            base.OnActionExecuted(filterContext);

            filterContext.Controller.TempData["Message"] = filterContext.Controller.ViewBag.Message;
            filterContext.Controller.TempData["MsgType"] = filterContext.Controller.ViewData["MsgType"];
            filterContext.Controller.TempData["Type"] = filterContext.Controller.ViewData["Type"];
        }
    }

    public class RestoreModelStateFromTempDataAttribute : ActionFilterAttribute
    {
        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {
            base.OnActionExecuting(filterContext);

            filterContext.Controller.ViewBag.Message = filterContext.Controller.TempData["Message"];
            filterContext.Controller.ViewData["MsgType"] = filterContext.Controller.TempData["MsgType"];
            filterContext.Controller.ViewData["Type"] = filterContext.Controller.TempData["Type"];
        }
    }
}