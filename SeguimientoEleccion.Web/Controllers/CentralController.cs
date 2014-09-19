using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using SeguimientoEleccion.Web.Models;

namespace SeguimientoEleccion.Web.Controllers
{
    public class CentralController : Controller
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Central
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Votacion()
        {
            return View();
        }
        public ActionResult PunteoPorColegio()
        {
            return View();
        }
        public ActionResult ListadoTotalesPorColegio()
        {
            var vm = db.Electores.
                GroupBy(e => e.Colegio)
                .Select(g => new CentralIndexViewModel()
                {
                    Colegio = g.Key.Nombre,
                    Electores = g.Count(),
                    Punteados = g.Count(e => e.Punteado)
                });
            return View(vm);
        }

        public ActionResult VerPunteados(string colegio)
        {
            ViewBag.Colegio = colegio;
            var vm = db.Electores
                .Where(e => e.Colegio.Nombre == colegio && e.Punteado)
                .OrderBy(e=>e.Nombre);
            return View(vm);
        }
        public ActionResult VerTodos(string colegio)
        {
            var vm = db.Electores
                .Where(e => e.Colegio.Nombre == colegio)
                .OrderBy(e=>e.Nombre);
            return View(vm);
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