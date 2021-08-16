using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SistemaHE.Models;

namespace SistemaHE.Controllers
{
    public class SolicitudPersonalsController : Controller
    {
        private SitiosWebEntities db = new SitiosWebEntities();

        // GET: SolicitudPersonals
        public ActionResult Index()
        {
            int cedula = Convert.ToInt32(Session["Cedula"]);
            if (Session["Rol"].Equals("Jefe"))
            {
                var soli = from d in db.SolicitudPersonal
                           where d.JefeDestinatario == cedula || d.Remitente == cedula && d.Estado != "Rechazado"
                           select d;

                return View(soli.ToList());
            }
            else
            {
                var soli = from d in db.SolicitudPersonal
                           where d.Remitente == cedula || d.Destinatario1 == cedula || d.Destinatario2 == cedula || d.Destinatario3 == cedula && d.Estado != "Rechazado"
                           select d;


                return View(soli.ToList());
            }

        }

        // GET: SolicitudPersonals/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudPersonal solicitudPersonal = db.SolicitudPersonal.Find(id);
            if (solicitudPersonal == null)
            {
                return HttpNotFound();
            }
            return View(solicitudPersonal);
        }

        // GET: SolicitudPersonals/Create
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
        // POST: SolicitudPersonals/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Transaccion,CantidadDePersonal,ID_Tarea,Remitente,JefeDestinatario,Destinatario1,Destinatario2,Destinatario3,Estado")] SolicitudPersonal solicitudPersonal)
        {
            if (ModelState.IsValid)
            {

                if (Session["Rol"].Equals("Jefe"))
                {
                    solicitudPersonal.JefeDestinatario = null;


                }
                else
                {
                    solicitudPersonal.JefeDestinatario = Convert.ToInt32(Session["CedulaJefe"]);
                    solicitudPersonal.Destinatario1 = null;
                    solicitudPersonal.Destinatario2 = null;
                    solicitudPersonal.Destinatario3 = null;
                }
                solicitudPersonal.Remitente = Convert.ToInt32(Session["Cedula"]);
                solicitudPersonal.Estado = "Pendiente";

                db.SolicitudPersonal.Add(solicitudPersonal);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Destinatario1 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudPersonal.Destinatario1);
            ViewBag.Destinatario2 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudPersonal.Destinatario2);
            ViewBag.Destinatario3 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudPersonal.Destinatario3);
            ViewBag.ID_Tarea = new SelectList(db.Tareas, "ID_Tarea", "DetalleDeLaTarea", solicitudPersonal.ID_Tarea);
            ViewBag.JefeDestinatario = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudPersonal.JefeDestinatario);
            ViewBag.Remitente = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudPersonal.Remitente);
            return View(solicitudPersonal);
        }
        // GET: SolicitudPersonals/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudPersonal solicitudPersonal = db.SolicitudPersonal.Find(id);
            Session["ID_Soli"] = id;
            if (solicitudPersonal == null)
            {
                return HttpNotFound();
            }
            List<string> listOfNames = new List<string>() { "Pendiente", "Aceptado", "Rechazado" };
            ViewBag.Estado = new SelectList(listOfNames, solicitudPersonal.Estado);
            ViewBag.CantidadDePersonal = solicitudPersonal.CantidadDePersonal;

            ViewBag.Destinatario1 = solicitudPersonal.Destinatario1;
            ViewBag.Destinatario2 = solicitudPersonal.Destinatario2;
            ViewBag.Destinatario3 = solicitudPersonal.Destinatario3;
            ViewBag.ID_Tarea = solicitudPersonal.ID_Tarea;
            ViewBag.JefeDestinatario = solicitudPersonal.JefeDestinatario;
            ViewBag.Remitente = solicitudPersonal.Remitente;
            return View(solicitudPersonal);
        }

        // POST: SolicitudPersonals/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Transaccion,CantidadDePersonal,ID_Tarea,Remitente,JefeDestinatario,Destinatario1,Destinatario2,Destinatario3,Estado")] SolicitudPersonal solicitudPersonal)
        {
            if (ModelState.IsValid)
            {
                SolicitudPersonal solicitudPersonal1 = db.SolicitudPersonal.Find(Convert.ToInt32(Session["ID_Soli"]));

                solicitudPersonal1.Estado = Request["Estado"].ToString();

                string mensaje = Session["Nombre"] + " ha " + solicitudPersonal1.Estado.ToString() + " la solicitud: N° " + solicitudPersonal1.ID_Transaccion + ", correspondiente a la tarea: " + solicitudPersonal1.Tareas.DetalleDeLaTarea + "\n" + "Comentario:\n" + Request["Comentario"];


                var correo = from d in db.Usuarios
                             where d.Identificacion == solicitudPersonal1.Remitente
                             select d;
                foreach (var item in correo)
                {
                    Correos correos = new Correos(item.Correo, item.Nombre_Completo, "Cambios en Solicitud de Horas Extras", mensaje);



                }

                db.Entry(solicitudPersonal1).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            SolicitudPersonal solicitudPersonal2 = db.SolicitudPersonal.Find(Convert.ToInt32(Session["ID_Soli"]));

            ViewBag.Destinatario1 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudPersonal2.Destinatario1);
            ViewBag.Destinatario2 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudPersonal2.Destinatario2);
            ViewBag.Destinatario3 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudPersonal2.Destinatario3);
            ViewBag.ID_Tarea = new SelectList(db.Tareas, "ID_Tarea", "DetalleDeLaTarea", solicitudPersonal2.ID_Tarea);
            ViewBag.JefeDestinatario = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudPersonal2.JefeDestinatario);
            ViewBag.Remitente = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudPersonal2.Remitente);
            return View(solicitudPersonal2);
        }

        // GET: SolicitudPersonals/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            SolicitudPersonal solicitudPersonal = db.SolicitudPersonal.Find(id);
            if (solicitudPersonal == null)
            {
                return HttpNotFound();
            }
            return View(solicitudPersonal);
        }

        // POST: SolicitudPersonals/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            SolicitudPersonal solicitudPersonal = db.SolicitudPersonal.Find(id);
            db.SolicitudPersonal.Remove(solicitudPersonal);
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
