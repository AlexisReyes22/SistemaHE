using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SistemaHE.Controllers;
using SistemaHE.Models;
namespace SistemaHE.Filtros
{
    public class Verificar : ActionFilterAttribute
    {

        public override void OnActionExecuting(ActionExecutingContext filterContext)
        {

            var user = (Usuarios)HttpContext.Current.Session["User"];
            if (user == null)
            {
                if (filterContext.Controller is LoginController == false || filterContext.Controller is HomeController == false)
                {
                    filterContext.HttpContext.Response.Redirect("~/Login/Index");
                }
            }
            //metodo padre
            base.OnActionExecuting(filterContext);
        }
    }
}