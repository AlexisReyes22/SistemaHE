

using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;

using System.Web.Mvc;
using SistemaHE.Models;

namespace SistemaHE.Controllers
{
    public class SolicitudHorasController : Controller
    {
        private SitiosWebEntities db = new SitiosWebEntities();
        public busqueda b = new busqueda();
        
        // GET: SolicitudHoras
        public ActionResult Index()
        {
            int cedula = Convert.ToInt32(Session["Cedula"]);
            if (Session["Rol"].Equals("Jefe"))
            {
                var soli = from d in db.SolicitudHoras
                           where d.JefeDestinatario == cedula || d.Remitente==cedula && d.Estado !="Rechazado"
                           select d;
             
                return View(soli.ToList());
            }
            else
            {
                var soli = from d in db.SolicitudHoras
                           where d.Remitente == cedula || d.Destinatario1==cedula || d.Destinatario2 == cedula || d.Destinatario3 == cedula && d.Estado != "Rechazado"
                           select d;
             

                return View(soli.ToList());
            }



        }


        // GET: SolicitudHoras/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudHoras solicitudHoras = db.SolicitudHoras.Find(id);
            if (solicitudHoras == null)
            {
                return HttpNotFound();
            }
            return View(solicitudHoras);
        }







        // GET: SolicitudHoras/Create
        public ActionResult Create()
        {
            
            var destinatarios = from d in db.Usuarios
                        where d.Rol == "Funcionario"
                        select d;

            var jefes = db.ListaJefes().ToList();

            ViewBag.Destinatario1 = new SelectList(destinatarios, "Identificacion", "Nombre_Completo");
            ViewBag.Destinatario2 = new SelectList(destinatarios, "Identificacion", "Nombre_Completo");
            ViewBag.Destinatario3 = new SelectList(destinatarios, "Identificacion", "Nombre_Completo");
            ViewBag.ID_Tarea = new SelectList(db.Tareas, "ID_Tarea", "DetalleDeLaTarea");
         
            
            return View();
        }

        // POST: SolicitudHoras/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Transaccion,CantidadDeHoras,ID_Tarea,Remitente,JefeDestinatario,Destinatario1,Destinatario2,Destinatario3,Estado")] SolicitudHoras solicitudHoras)
        {
            if (ModelState.IsValid)
            {

                if (Session["Rol"].Equals("Jefe"))
                {
                    solicitudHoras.JefeDestinatario = null;


                }
                else
                {
                    solicitudHoras.JefeDestinatario = Convert.ToInt32(Session["CedulaJefe"]);
                    solicitudHoras.Destinatario1 = null;
                    solicitudHoras.Destinatario2 = null;
                    solicitudHoras.Destinatario3 = null;
                }
                solicitudHoras.Remitente = Convert.ToInt32(Session["Cedula"]);
                solicitudHoras.Estado = "Pendiente";

                db.SolicitudHoras.Add(solicitudHoras);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Destinatario1 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudHoras.Destinatario1);
            ViewBag.Destinatario2 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudHoras.Destinatario2);
            ViewBag.Destinatario3 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudHoras.Destinatario3);
            ViewBag.ID_Tarea = new SelectList(db.Tareas, "ID_Tarea", "DetalleDeLaTarea", solicitudHoras.ID_Tarea);
            ViewBag.JefeDestinatario = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudHoras.JefeDestinatario);
            ViewBag.Remitente = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudHoras.Remitente);
            return View(solicitudHoras);
        }

        // GET: SolicitudHoras/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudHoras solicitudHoras = db.SolicitudHoras.Find(id);
            Session["ID_Soli"] = id;
            if (solicitudHoras == null)
            {
                return HttpNotFound();
            }
            List<string> listOfNames = new List<string>() { "Pendiente", "Aceptado", "Rechazado" };
            ViewBag.Estado = new SelectList(listOfNames,solicitudHoras.Estado);
            ViewBag.CantidadDeHoras = solicitudHoras.CantidadDeHoras;

            ViewBag.Destinatario1 =b.buscarNom(Convert.ToInt32( solicitudHoras.Destinatario1));
            ViewBag.Destinatario2 = b.buscarNom(Convert.ToInt32(solicitudHoras.Destinatario2));
            ViewBag.Destinatario3 = b.buscarNom(Convert.ToInt32(solicitudHoras.Destinatario3));
            ViewBag.ID_Tarea = b.buscarTar(Convert.ToInt32(solicitudHoras.ID_Tarea));
            ViewBag.JefeDestinatario = b.buscarNom(Convert.ToInt32(solicitudHoras.JefeDestinatario));
            ViewBag.Remitente = b.buscarNom(Convert.ToInt32(solicitudHoras.Remitente));

            return View(solicitudHoras);
        }

    

        // POST: SolicitudHoras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit()
        {
            if (ModelState.IsValid)
            {
                SolicitudHoras solicitudHoras = db.SolicitudHoras.Find(Convert.ToInt32(Session["ID_Soli"]));
               
                solicitudHoras.Estado = Request["Estado"].ToString();


                string mensaje = Session["Nombre"] + " ha " + solicitudHoras.Estado.ToString() + " la solicitud: N° " + solicitudHoras.ID_Transaccion + ", correspondiente a la tarea: " + solicitudHoras.Tareas.DetalleDeLaTarea+"\n"+"Comentario:\n"+Request["Comentario"];

          
                    var correo = from d in db.Usuarios
                                 where d.Identificacion == solicitudHoras.Remitente
                                 select d;
                    foreach (var item in correo)
                    {
                        Correos correos = new Correos(item.Correo, item.Nombre_Completo, "Cambios en Solicitud de Horas Extras", mensaje);
    
                

                }

                db.Entry(solicitudHoras).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            SolicitudHoras solicitudHoras2 = db.SolicitudHoras.Find(Convert.ToInt32(Session["ID_Soli"]));

            ViewBag.Destinatario1 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudHoras2.Destinatario1);
            ViewBag.Destinatario2 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudHoras2.Destinatario2);
            ViewBag.Destinatario3 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudHoras2.Destinatario3);
            ViewBag.ID_Tarea = new SelectList(db.Tareas, "ID_Tarea", "DetalleDeLaTarea", solicitudHoras2.ID_Tarea);
            ViewBag.JefeDestinatario = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudHoras2.JefeDestinatario);
            ViewBag.Remitente = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudHoras2.Remitente);
            return View(solicitudHoras2);
        }

        // GET: SolicitudHoras/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudHoras solicitudHoras = db.SolicitudHoras.Find(id);
            if (solicitudHoras == null)
            {
                return HttpNotFound();
            }
            return View(solicitudHoras);
        }

        // POST: SolicitudHoras/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SolicitudHoras solicitudHoras = db.SolicitudHoras.Find(id);
            db.SolicitudHoras.Remove(solicitudHoras);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
