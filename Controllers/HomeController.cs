﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaHE.Models;


namespace SistemaHE.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
        
    
            return View();
        }


        public ActionResult About() 
        {
         

            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()

        {
           

            ViewBag.Message = "Your contact page.";

            return View();
        }

        public ActionResult Login(int? user, string pass)
        {

            try
            {
                using (SitiosWebEntities db = new SitiosWebEntities())
                {


                    var lst = from d in db.Cuentas
                              where d.Identificacion == user && d.Contrasenna == pass
                              select d;
                    if (lst.Count() > 0)
                    {
                        var lst2 = from d in db.Usuarios
                                   where d.Identificacion == user
                                   select d;


                        Session["Rol"] = lst2.First().Rol;
                        return View("Index");


                    }
                    else
                    {
                        Session["Rol"] = "Nolog";
                        return View("Login");

                    }
                }
            }
            catch (Exception)
            {

                Session["Rol"] = "Nolog";
                return View("Login");
            }

        }
    }
}