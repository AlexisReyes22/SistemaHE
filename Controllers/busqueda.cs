using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using SistemaHE.Models;
using System.Linq;
using System.Data.Entity;
using System.Collections.Generic;


namespace SistemaHE.Controllers
{
    public class busqueda
    {
        private SitiosWebEntities db = new SitiosWebEntities();

        public string buscarNom(int ced)
        {
            string nombre;
            var res = from d in db.Usuarios
                      where d.Identificacion == ced
                      select d.Nombre_Completo;
            nombre = res.First();
            return nombre;


        }
        public string buscarTar(int id)
        {
            string nombre;
            var res = from d in db.Tareas
                      where d.ID_Tarea == id
                      select d.DetalleDeLaTarea;
            nombre = res.First();
            return nombre;


        }
    }
}