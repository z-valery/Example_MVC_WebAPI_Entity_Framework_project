using SmartHouse.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;

namespace SmartHouse.Controllers
{
    public class WebApiViewController : Controller
    {
        // GET: WebApiView
        public ActionResult Index()
        {
            ViewBag.MvcWebApi = "MVC";
            return View();
        }

        public ActionResult Tv(Tv tv)
        {
            if(tv == null)
            {
                RedirectToAction("Index");
            }
            ViewBag.MvcWebApi = "MVC";
            return View(tv);
        }

        [System.Web.Mvc.HttpPost]
        public PartialViewResult PartialTv(Tv tv)
        {
            return PartialView(tv);
        }


    }
}