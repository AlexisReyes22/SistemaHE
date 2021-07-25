using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaHE.Models;
using System.Data.SqlClient;
using System.Security.Cryptography;



namespace SistemaHE.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            Session["Rol"] = "Nolog";
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
                        return Content("1");


                    }
                    else
                    {
                        Session["Rol"] = "Nolog";
                        return Content("Usuario y contraseña invalida");

                    }
                }
            }
            catch (Exception)
            {

                Session["Rol"] = "Nolog";
                return Content("Error Iniciando sesion");
            }
            
        }
    }
}