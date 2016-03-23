using MVCLab.Isthmus.Filters;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace MVCLab.Isthmus.Controllers
{
    public class HomeController : Controller
    {
        [RestoreModelStateFromTempData]
        public ActionResult Index()
        {  
            if(ViewData["MsgType"] != null && ViewData["MsgType"].ToString().Equals("Error"))
            {
                ViewBag.Title = "Error";
            }
            return View();
        }
    }
}
