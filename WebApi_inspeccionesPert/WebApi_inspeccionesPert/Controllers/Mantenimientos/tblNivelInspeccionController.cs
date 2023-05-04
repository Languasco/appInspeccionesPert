using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Description;
using Entidades;
using System.Web.Http.Cors;

namespace WebApi_inspeccionesPert.Controllers.Mantenimientos
{
    [EnableCors("*", "*", "*")]
    public class tblNivelInspeccionController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblNivelInspeccion
        public IQueryable<tbl_NivelInspeccion> Gettbl_NivelInspeccion()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_NivelInspeccion;
        }

        // GET: api/tblNivelInspeccion/5
        [ResponseType(typeof(tbl_NivelInspeccion))]
        public IHttpActionResult Gettbl_NivelInspeccion(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbl_NivelInspeccion tbl_NivelInspeccion = db.tbl_NivelInspeccion.Find(id);
            if (tbl_NivelInspeccion == null)
            {
                return NotFound();
            }

            return Ok(tbl_NivelInspeccion);
        }

        // PUT: api/tblNivelInspeccion/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_NivelInspeccion(int id, tbl_NivelInspeccion tbl_NivelInspeccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_NivelInspeccion.id_NivelInspeccion)
            {
                return BadRequest();
            }

            db.Entry(tbl_NivelInspeccion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_NivelInspeccionExists(id))
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

        // POST: api/tblNivelInspeccion
        [ResponseType(typeof(tbl_NivelInspeccion))]
        public IHttpActionResult Posttbl_NivelInspeccion(tbl_NivelInspeccion tbl_NivelInspeccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_NivelInspeccion.Add(tbl_NivelInspeccion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_NivelInspeccion.id_NivelInspeccion }, tbl_NivelInspeccion);
        }

        // DELETE: api/tblNivelInspeccion/5
        [ResponseType(typeof(tbl_NivelInspeccion))]
        public IHttpActionResult Deletetbl_NivelInspeccion(int id)
        {
            tbl_NivelInspeccion tbl_NivelInspeccion = db.tbl_NivelInspeccion.Find(id);
            if (tbl_NivelInspeccion == null)
            {
                return NotFound();
            }

            db.tbl_NivelInspeccion.Remove(tbl_NivelInspeccion);
            db.SaveChanges();

            return Ok(tbl_NivelInspeccion);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_NivelInspeccionExists(int id)
        {
            return db.tbl_NivelInspeccion.Count(e => e.id_NivelInspeccion == id) > 0;
        }
    }
}