using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SistemaHE.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            Session["Rol"] = "Director";
    
            return View();
        }


        public ActionResult About() 
        {
            Session["Rol"] = "Jefe";

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}