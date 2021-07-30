using System.Web;
using System.Web.Optimization;

namespace SistemaHE
{
    public class BundleConfig
    {
        // Para obtener más información sobre las uniones, visite https://go.microsoft.com/fwlink/?LinkId=301862
        public static void RegisterBundles(BundleCollection bundles)
        {
            bundles.Add(new ScriptBundle("~/bundles/jquery").Include(
                        "~/Scripts/jquery-{version}.js"));

            bundles.Add(new ScriptBundle("~/bundles/jqueryval").Include(
                        "~/Scripts/jquery.validate*"));

            bundles.Add(new ScriptBundle("~/bundles/materialize").Include(
                         "~/Scripts/materialize.min.js*"));
            bundles.Add(new ScriptBundle("~/bundles/init").Include(
                      "~/Scripts/init.js"));

            // Utilice la versión de desarrollo de Modernizr para desarrollar y obtener información. De este modo, estará
            // para la producción, use la herramienta de compilación disponible en https://modernizr.com para seleccionar solo las pruebas que necesite.
            //bundles.Add(new ScriptBundle("~/bundles/modernizr").Include(
            //            "~/Scripts/modernizr-*"));



            bundles.Add(new StyleBundle("~/Content/css").Include(
                      "~/Content/Header.css",
                      "~/Content/Delete.css",
                      "~/Content/Index.css",
                      "~/Content/Principal.css",
                      "~/Content/Usuarios.css",
                      "~/Content/materialize.css",
                      "~/Content/Create.css",
                      "~/Content/materialize.min.css",
                     
                      "~/Content/login.css",
                      "~/Content/Tarjeta.css",
                       "~/Content/Site.css"));


        }
    }
}
