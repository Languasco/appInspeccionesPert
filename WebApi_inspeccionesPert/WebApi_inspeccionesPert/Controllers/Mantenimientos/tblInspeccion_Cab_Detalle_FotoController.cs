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
    public class tblInspeccion_Cab_Detalle_FotoController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblInspeccion_Cab_Detalle_Foto
        public IQueryable<tbl_Inspeccion_Cab_Detalle_Foto> Gettbl_Inspeccion_Cab_Detalle_Foto()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_Inspeccion_Cab_Detalle_Foto;
        }

        // GET: api/tblInspeccion_Cab_Detalle_Foto/5
        [ResponseType(typeof(tbl_Inspeccion_Cab_Detalle_Foto))]
        public IHttpActionResult Gettbl_Inspeccion_Cab_Detalle_Foto(int id)
        {
            tbl_Inspeccion_Cab_Detalle_Foto tbl_Inspeccion_Cab_Detalle_Foto = db.tbl_Inspeccion_Cab_Detalle_Foto.Find(id);
            if (tbl_Inspeccion_Cab_Detalle_Foto == null)
            {
                return NotFound();
            }

            return Ok(tbl_Inspeccion_Cab_Detalle_Foto);
        }

        // PUT: api/tblInspeccion_Cab_Detalle_Foto/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Inspeccion_Cab_Detalle_Foto(int id, tbl_Inspeccion_Cab_Detalle_Foto tbl_Inspeccion_Cab_Detalle_Foto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Inspeccion_Cab_Detalle_Foto.id_inspeccion_foto)
            {
                return BadRequest();
            }

            db.Entry(tbl_Inspeccion_Cab_Detalle_Foto).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_Inspeccion_Cab_Detalle_FotoExists(id))
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

        // POST: api/tblInspeccion_Cab_Detalle_Foto
        [ResponseType(typeof(tbl_Inspeccion_Cab_Detalle_Foto))]
        public IHttpActionResult Posttbl_Inspeccion_Cab_Detalle_Foto(tbl_Inspeccion_Cab_Detalle_Foto tbl_Inspeccion_Cab_Detalle_Foto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_Inspeccion_Cab_Detalle_Foto.fecha_creacion = DateTime.Now;
            db.tbl_Inspeccion_Cab_Detalle_Foto.Add(tbl_Inspeccion_Cab_Detalle_Foto);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Inspeccion_Cab_Detalle_Foto.id_inspeccion_foto }, tbl_Inspeccion_Cab_Detalle_Foto);
        }

        // DELETE: api/tblInspeccion_Cab_Detalle_Foto/5
        [ResponseType(typeof(tbl_Inspeccion_Cab_Detalle_Foto))]
        public IHttpActionResult Deletetbl_Inspeccion_Cab_Detalle_Foto(int id)
        {
            tbl_Inspeccion_Cab_Detalle_Foto tbl_Inspeccion_Cab_Detalle_Foto = db.tbl_Inspeccion_Cab_Detalle_Foto.Find(id);
            if (tbl_Inspeccion_Cab_Detalle_Foto == null)
            {
                return NotFound();
            }

            db.tbl_Inspeccion_Cab_Detalle_Foto.Remove(tbl_Inspeccion_Cab_Detalle_Foto);
            db.SaveChanges();

            return Ok(tbl_Inspeccion_Cab_Detalle_Foto);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_Inspeccion_Cab_Detalle_FotoExists(int id)
        {
            return db.tbl_Inspeccion_Cab_Detalle_Foto.Count(e => e.id_inspeccion_foto == id) > 0;
        }
    }
}