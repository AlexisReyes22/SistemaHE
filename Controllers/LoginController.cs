using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaHE.Models;

namespace SistemaHE.Controllers
{
    public class LoginController : Controller
    {
        // GET: Login
        public ActionResult Index()
        {
            return View();
        }


        public ActionResult Login( int? user,string contra)
        {
            try
            {
                using(SitiosWebEntities db= new SitiosWebEntities())
                {
                    var lst = from d in db.Cuentas
                              where d.Identificacion == user && d.Contrasenna == contra
                              select d;
                    if (lst.Count()>0)
                    {
                        var lst2 = from d in db.Usuarios
                                  where d.Identificacion == user 
                                   select d;

                        Session["Rol"] = lst2.First().Rol;
                  
                    }
                }
            }
            catch (Exception)
            {

                throw;
            }
            return View();
        }
    }
}