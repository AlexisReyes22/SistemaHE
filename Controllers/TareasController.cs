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
    public class TareasController : Controller
    {
        private SitiosWebEntities db = new SitiosWebEntities();

        // GET: Tareas
        public ActionResult Index()
        {
            var tareas = db.Tareas.Include(t => t.Usuarios).Include(t => t.Usuarios1).Include(t => t.Usuarios2).Include(t => t.Usuarios3);
            return View(tareas.ToList());
        }

        // GET: Tareas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tareas tareas = db.Tareas.Find(id);
            if (tareas == null)
            {
                return HttpNotFound();
            }
            return View(tareas);
        }

        // GET: Tareas/Create
        public ActionResult Create()
        {
            ViewBag.Jefe_Asignado = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo");
            ViewBag.UsuarioAsigando1 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo");
            ViewBag.UsuarioAsigando2 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo");
            ViewBag.UsuarioAsigando3 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo");
            return View();
        }

        // POST: Tareas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Tarea,Jefe_Asignado,UsuarioAsigando1,UsuarioAsigando2,UsuarioAsigando3,DetalleDeLaTarea")] Tareas tareas)
        {
            if (ModelState.IsValid)
            {
                db.Tareas.Add(tareas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Jefe_Asignado = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", tareas.Jefe_Asignado);
            ViewBag.UsuarioAsigando1 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", tareas.UsuarioAsigando1);
            ViewBag.UsuarioAsigando2 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", tareas.UsuarioAsigando2);
            ViewBag.UsuarioAsigando3 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", tareas.UsuarioAsigando3);
            return View(tareas);
        }

        // GET: Tareas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tareas tareas = db.Tareas.Find(id);
            if (tareas == null)
            {
                return HttpNotFound();
            }
          
            ViewBag.Jefe_Asignado = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", tareas.Jefe_Asignado);
            ViewBag.UsuarioAsigando1 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", tareas.UsuarioAsigando1);
            ViewBag.UsuarioAsigando2 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", tareas.UsuarioAsigando2);
            ViewBag.UsuarioAsigando3 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", tareas.UsuarioAsigando3);
            return View(tareas);
        }

        // POST: Tareas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Tarea,Jefe_Asignado,UsuarioAsigando1,UsuarioAsigando2,UsuarioAsigando3,DetalleDeLaTarea")] Tareas tareas)
        {
            if (ModelState.IsValid)
            {
                db.Entry(tareas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Jefe_Asignado = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", tareas.Jefe_Asignado);
            ViewBag.UsuarioAsigando1 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", tareas.UsuarioAsigando1);
            ViewBag.UsuarioAsigando2 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", tareas.UsuarioAsigando2);
            ViewBag.UsuarioAsigando3 = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", tareas.UsuarioAsigando3);
            return View(tareas);
        }

        // GET: Tareas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Tareas tareas = db.Tareas.Find(id);
            if (tareas == null)
            {
                return HttpNotFound();
            }
            return View(tareas);
        }

        // POST: Tareas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Tareas tareas = db.Tareas.Find(id);
            db.Tareas.Remove(tareas);
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
