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
                using (SitiosWebEntities1 db = new SitiosWebEntities1())
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
                        Session["Nombre"] = lst2.First().Nombre_Completo;
                        Session["Cedula"] = lst2.First().Identificacion;
                        Session["CedulaJefe"] = lst2.First().Jefe_Inmediato;

                        int cedJefe = Convert.ToInt32(lst2.First().Jefe_Inmediato);

                        if (Session["Rol"].Equals("Funcionario"))
                        {
                            var jef = from d in db.Usuarios
                                      where d.Identificacion == cedJefe
                                      select d;

                            Session["Jefe_Inmediato"] = jef.First().Nombre_Completo;
                        }
                    

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