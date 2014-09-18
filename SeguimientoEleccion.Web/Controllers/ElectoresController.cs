using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.ModelBinding;
using System.Web.Http.OData;
using System.Web.Http.OData.Routing;
using SeguimientoEleccion.Web.Models;

namespace SeguimientoEleccion.Web.Controllers
{
    /*
    The WebApiConfig class may require additional changes to add a route for this controller. Merge these statements into the Register method of the WebApiConfig class as applicable. Note that OData URLs are case sensitive.

    using System.Web.Http.OData.Builder;
    using System.Web.Http.OData.Extensions;
    using SeguimientoEleccion.Web.Models;
    ODataConventionModelBuilder builder = new ODataConventionModelBuilder();
    builder.EntitySet<Elector>("Electores");
    config.Routes.MapODataServiceRoute("odata", "odata", builder.GetEdmModel());
    */
    public class ElectoresController : ODataController
    {
        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: odata/Electores
        [EnableQuery]
        public IQueryable<Elector> GetElectores()
        {
            return db.Electores;
        }

        // GET: odata/Electores(5)
        [EnableQuery]
        public SingleResult<Elector> GetElector([FromODataUri] int key)
        {
            return SingleResult.Create(db.Electores.Where(elector => elector.Id == key));
        }

        // PUT: odata/Electores(5)
        public IHttpActionResult Put([FromODataUri] int key, Delta<Elector> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Elector elector = db.Electores.Find(key);
            if (elector == null)
            {
                return NotFound();
            }

            patch.Put(elector);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElectorExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(elector);
        }

        // POST: odata/Electores
        public IHttpActionResult Post(Elector elector)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.Electores.Add(elector);
            db.SaveChanges();

            return Created(elector);
        }

        // PATCH: odata/Electores(5)
        [AcceptVerbs("PATCH", "MERGE")]
        public IHttpActionResult Patch([FromODataUri] int key, Delta<Elector> patch)
        {
            Validate(patch.GetEntity());

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            Elector elector = db.Electores.Find(key);
            if (elector == null)
            {
                return NotFound();
            }

            patch.Patch(elector);

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ElectorExists(key))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Updated(elector);
        }

        // DELETE: odata/Electores(5)
        public IHttpActionResult Delete([FromODataUri] int key)
        {
            Elector elector = db.Electores.Find(key);
            if (elector == null)
            {
                return NotFound();
            }

            db.Electores.Remove(elector);
            db.SaveChanges();

            return StatusCode(HttpStatusCode.NoContent);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool ElectorExists(int key)
        {
            return db.Electores.Count(e => e.Id == key) > 0;
        }
    }
}
