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

            var user =HttpContext.Current.Session["Rol"];
            if (user == null )
            {
                if (filterContext.Controller is LoginController == false)
                {
                  
                    filterContext.HttpContext.Response.Redirect("~/Login/Index");
                }
            }
            else
            {
                if (filterContext.Controller is LoginController == true)
                {
                    

                    filterContext.HttpContext.Response.Redirect("~/Principal/Index");
                }
            }
            //metodo padre
            base.OnActionExecuting(filterContext);
        }
    }
}