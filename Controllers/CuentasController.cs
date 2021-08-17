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
    public class CuentasController : Controller
    {
        private SitiosWebEntities db = new SitiosWebEntities();

        // GET: Cuentas
        public ActionResult Index()
        {

            //var cuentas = db.Cuentas.Include(c => c.Usuarios);

            int id = Convert.ToInt32(Session["Cedula"]);
            var cuentas = from d in db.Cuentas
                          where d.Identificacion == id
                          select d;
            
            return View(cuentas);
        }

        // GET: Cuentas/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }

            Cuentas cuentas = db.Cuentas.Find(id);
        
            if (cuentas == null)
            {
                return HttpNotFound();
            }
            return View(cuentas);
        }

        // GET: Cuentas/Create
        public ActionResult Create()
        {
            ViewBag.Identificacion = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo");
            return View();
        }

        // POST: Cuentas/Create
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID_Cuenta,Identificacion,Contrasenna")] Cuentas cuentas)
        {
            if (ModelState.IsValid)
            {
                Encriptado encriptado = new Encriptado();
                cuentas.Contrasenna = encriptado.Encrypt(cuentas.Contrasenna);
                db.Cuentas.Add(cuentas);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.Identificacion = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", cuentas.Identificacion);
            return View(cuentas);
        }

        // GET: Cuentas/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuentas cuentas = db.Cuentas.Find(id);
            cuentas.Contrasenna = "";
            if (cuentas == null)
            {
                return HttpNotFound();
            }
            ViewBag.Identificacion = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", cuentas.Identificacion);
            return View(cuentas);
        }

        // POST: Cuentas/Edit/5
        // Para protegerse de ataques de publicación excesiva, habilite las propiedades específicas a las que quiere enlazarse. Para obtener 
        // más detalles, vea https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID_Cuenta,Identificacion,Contrasenna")] Cuentas cuentas)
        {
            if (ModelState.IsValid)
            {
                if (cuentas.Identificacion!=707770777)
                {
                    Encriptado encriptado = new Encriptado();
                    cuentas.Contrasenna = encriptado.Encrypt(cuentas.Contrasenna);
                    cuentas.Identificacion = Convert.ToInt32(Session["Cedula"]);
                }
              
                db.Entry(cuentas).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.Identificacion = new SelectList(db.Usuarios, "Identificacion", "Nombre_Completo", cuentas.Identificacion);
            return View(cuentas);
        }

        // GET: Cuentas/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Cuentas cuentas = db.Cuentas.Find(id);
            if (cuentas == null)
            {
                return HttpNotFound();
            }
            return View(cuentas);
        }

        // POST: Cuentas/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Cuentas cuentas = db.Cuentas.Find(id);
            db.Cuentas.Remove(cuentas);
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
