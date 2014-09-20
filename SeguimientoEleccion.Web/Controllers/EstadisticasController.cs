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
            //return db.Electores.
            //    GroupBy(e => e.Colegio.Nombre)
            //    .Select(g =>
            //        new
            //        {
            //            Colegio = g.Key,
            //            PunteadoVoto = g.Count(e => e.Punteado && e.Voto),
            //            PunteadoNoVoto = g.Count(e => e.Punteado && !e.Voto),
            //            NoPunteadoVoto = g.Count(e => !e.Punteado && e.Voto),
            //            NoPunteadoNoVoto = g.Count(e => !e.Punteado && !e.Voto),
            //        });
            var cantidad = db.Electores
                .GroupBy(e=>new
                           {
                               e.Colegio.Nombre,
                               e.Punteado,
                               e.Voto
                           },
                           e=>e,
                           (g,e)=>new
                                  {
                                      Colegio = g.Nombre,
                                      g.Punteado,
                                      g.Voto,
                                      Cantidad = e.Count()
                                  })
                                  .ToList();
            return cantidad
                .GroupBy(c => c.Colegio,
                    c => c,
                    (g, c) => new
                              {
                                  Colegio = g,
                                  PunteadoVoto = c.Where(e => e.Punteado && e.Voto).Sum(e => (int?) e.Cantidad),
                                  PunteadoNoVoto = c.Where(e => e.Punteado && !e.Voto).Sum(e => (int?) e.Cantidad),
                                  NoPunteadoVoto = c.Where(e => !e.Punteado && e.Voto).Sum(e => (int?) e.Cantidad),
                                  NoPunteadoNoVoto = c.Where(e => !e.Punteado && !e.Voto).Sum(e => (int?) e.Cantidad),
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