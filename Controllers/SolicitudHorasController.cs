

using System;
using System.Data.Entity;
using System.Linq;
using System.Net;

using System.Web.Mvc;
using SistemaHE.Models;

namespace SistemaHE.Controllers
{
    public class SolicitudHorasController : Controller
    {
        private SitiosWebEntities1 db = new SitiosWebEntities1();
        
        // GET: SolicitudHoras
        public ActionResult Index()
        {
            int cedula = Convert.ToInt32(Session["Cedula"]);
            if (Session["Rol"].Equals("Jefe"))
            {
                var soli = from d in db.SolicitudHoras
                           where d.JefeDestinatario == cedula || d.Remitente==cedula
                           select d;
             
                return View(soli.ToList());
            }
            else
            {
                var soli = from d in db.SolicitudHoras
                           where d.Remitente == cedula || d.Destinatario1==cedula || d.Destinatario2 == cedula || d.Destinatario3 == cedula
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


        public ActionResult SolicitudHE()
        {
            string rol = "Funcionario";

            var lista = from d in db.Usuarios
                        where d.Rol == rol
                        select d;

            ViewBag.Destinatario1 = new SelectList(lista, "Identificacion", "Nombre_Completo");
            ViewBag.Destinatario2 = new SelectList(lista, "Identificacion", "Nombre_Completo");
            ViewBag.Destinatario3 = new SelectList(lista, "Identificacion", "Nombre_Completo");
            ViewBag.ID_Tarea = new SelectList(db.Tareas, "ID_Tarea", "DetalleDeLaTarea");

            return View();
        }


        //solicitudes cuando es jefe a empleadao se usan los campos destinatarios1 2 3, de lo contrario solo remitente.

        public ActionResult NuevaSHE([Bind(Include = "CantidadDeHoras,ID_Tarea,Remitente,JefeDestinatario,Destinatario1,Destinatario2,Destinatario3")] SolicitudHoras solicitudHoras)
        {
            if (ModelState.IsValid)
            {
                int ced = Convert.ToInt32(Session["Cedula"]);
                db.SP_CrearSolicitudDeHorasExtra_EmpleadoAJefe(ced, solicitudHoras.CantidadDeHoras, solicitudHoras.ID_Tarea);



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

        public ActionResult SolicitudPE()
        {



            return View();
        }

        // GET: SolicitudHoras/Create
        public ActionResult Create()
        {
            ViewBag.Destinatario1 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo");
            ViewBag.Destinatario2 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo");
            ViewBag.Destinatario3 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo");
            ViewBag.ID_Tarea = new SelectList(db.Tareas, "ID_Tarea", "DetalleDeLaTarea");
            ViewBag.JefeDestinatario = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo");
            ViewBag.Remitente = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo");
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
            if (solicitudHoras == null)
            {
                return HttpNotFound();
            }
            ViewBag.Destinatario1 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudHoras.Destinatario1);
            ViewBag.Destinatario2 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudHoras.Destinatario2);
            ViewBag.Destinatario3 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudHoras.Destinatario3);
            ViewBag.ID_Tarea = new SelectList(db.Tareas, "ID_Tarea", "DetalleDeLaTarea", solicitudHoras.ID_Tarea);
            ViewBag.JefeDestinatario = new SelectList(db.Usuarios, "Nombre_Completo", "Nombre_Completo", solicitudHoras.JefeDestinatario);
            ViewBag.Remitente = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", solicitudHoras.Remitente);
            return View(solicitudHoras);
        }

        // POST: SolicitudHoras/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Transaccion,CantidadDeHoras,ID_Tarea,Remitente,JefeDestinatario,Destinatario1,Destinatario2,Destinatario3,Estado")] SolicitudHoras solicitudHoras)
        {
            if (ModelState.IsValid)
            {
                db.Entry(solicitudHoras).State = EntityState.Modified;
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
