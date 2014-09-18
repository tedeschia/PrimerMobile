using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using SeguimientoEleccion.Web.Models;

namespace SeguimientoEleccion.Web.Controllers
{
    public class FiscalController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Electores
        public ActionResult Index()
        {
            return View(db.Electores.ToList());
        }

        // GET: Electores/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elector elector = db.Electores.Find(id);
            if (elector == null)
            {
                return HttpNotFound();
            }
            return View(elector);
        }

        // GET: Electores/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Electores/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id,Nombre,Colegio")] Elector elector)
        {
            if (ModelState.IsValid)
            {
                db.Electores.Add(elector);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(elector);
        }

        // GET: Electores/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elector elector = db.Electores.Find(id);
            if (elector == null)
            {
                return HttpNotFound();
            }
            return View(elector);
        }

        // POST: Electores/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id,Nombre,Colegio")] Elector elector)
        {
            if (ModelState.IsValid)
            {
                db.Entry(elector).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(elector);
        }

        // GET: Electores/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Elector elector = db.Electores.Find(id);
            if (elector == null)
            {
                return HttpNotFound();
            }
            return View(elector);
        }

        // POST: Electores/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Elector elector = db.Electores.Find(id);
            db.Electores.Remove(elector);
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
