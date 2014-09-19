using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Http;
using System.Web.Mvc;
using SeguimientoEleccion.Web.Models;

namespace SeguimientoEleccion.Web.Controllers
{
    [System.Web.Http.RoutePrefix("api/Estadisticas")]
    public class EstadisticasController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Central
        [System.Web.Http.Route("GetVotacion")]
        public dynamic GetVotacion()
        {
            return db.Colegios
                .Select(c => new
                {
                    Colegio = c.Nombre,
                    Punteado = c.Electores.Count(e => e.Punteado && e.Voto),
                    NoPunteado = c.Electores.Count(e => !e.Punteado && e.Voto),
                });
        }
        [System.Web.Http.Route("GetPunteoColegio")]
        public dynamic GetPunteoColegio()
        {
            return db.Colegios
                .Select(c => new
                {
                    Colegio = c.Nombre,
                    Punteado = c.Electores.Count(e => e.Punteado),
                    NoPunteado = c.Electores.Count(e => !e.Punteado),
                });
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