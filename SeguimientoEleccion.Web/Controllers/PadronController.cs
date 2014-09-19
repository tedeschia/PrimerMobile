using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading;
using System.Web.Http;
using System.Web.Http.Description;
using System.Web.Mvc;
using SeguimientoEleccion.Web.Models;

namespace SeguimientoEleccion.Web.Controllers
{
    [System.Web.Http.RoutePrefix("api/Padron")]
    public class PadronController : ApiController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: api/Padron
        [System.Web.Http.Route("{usuario}")]
        [ValidateInput(false)]
        public PadronViewModel GetPadron(string usuario)
        {
            //Thread.Sleep(10000);
            var colegio = db.Colegios.FirstOrDefault(c => c.Usuario == usuario);

            return new PadronViewModel()
                   {
                       Padron =
                           colegio != null
                               ? db.Electores.Where(e => colegio.Id == e.ColegioId)
                               .Select(e=>new PadronViewModel.ElectorViewModel()
                                          {
                                              Id=e.Id,
                                              Nombre = e.Nombre,
                                              DNI = e.DNI,
                                              Punteado = e.Voto
                                          })
                               .ToList()
                               : new List<PadronViewModel.ElectorViewModel>(),
                       Colegio = colegio != null ? colegio.Nombre : null,
                       Usuario = colegio != null ? usuario : null
                   };
        }



        // GET: api/Padron/5
        [ResponseType(typeof(Elector))]
        [System.Web.Http.Route("{id:int}")]
        public IHttpActionResult GetElector(int id)
        {
            Elector elector = db.Electores.Find(id);
            if (elector == null)
            {
                return NotFound();
            }

            return Ok(elector);
        }

        // PUT: api/Padron/5
        [ResponseType(typeof(void))]
        [System.Web.Http.Route("{id:int}")]
        public IHttpActionResult PutElector(int id, Elector elector)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != elector.Id)
            {
                return BadRequest();
            }

            db.Entry(elector).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElectorExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // PUT LIST: api/Padron/electores
        [ResponseType(typeof(void))]
        [System.Web.Http.Route("puntear")]
        [System.Web.Http.HttpPut]
        public IHttpActionResult Puntear(KeyValuePair<int,bool>[] punteos)
        {
            if (punteos == null) throw new ArgumentException("Ids requeridos");

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var ids = punteos.Select(p => p.Key);
            var electores = db.Electores.Where(e => ids.Contains(e.Id));
            foreach (var elector in electores)
            {
                elector.Voto = punteos.Single(p=>p.Key == elector.Id).Value;
            }

            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/Padron
        [ResponseType(typeof(Elector))]
        public IHttpActionResult PostElector(Elector elector)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Electores.Add(elector);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = elector.Id }, elector);
        }

        // DELETE: api/Padron/5
        [ResponseType(typeof(Elector))]
        public IHttpActionResult DeleteElector(int id)
        {
            Elector elector = db.Electores.Find(id);
            if (elector == null)
            {
                return NotFound();
            }

            db.Electores.Remove(elector);
            db.SaveChanges();

            return Ok(elector);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ElectorExists(int id)
        {
            return db.Electores.Count(e => e.Id == id) > 0;
        }
    }
}