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
using System.Threading.Tasks;

namespace WebApi_inspeccionesPert.Controllers.Mantenimientos
{
        [EnableCors("*", "*", "*")]
    public class tblTipo_InspeccionController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblTipo_Inspeccion
        public IQueryable<tbl_Tipo_Inspeccion> Gettbl_Tipo_Inspeccion()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_Tipo_Inspeccion;
        }

        // GET: api/tblTipo_Inspeccion/5
        [ResponseType(typeof(tbl_Tipo_Inspeccion))]
        public IHttpActionResult Gettbl_Tipo_Inspeccion(int id)
        {
            tbl_Tipo_Inspeccion tbl_Tipo_Inspeccion = db.tbl_Tipo_Inspeccion.Find(id);
            db.Configuration.ProxyCreationEnabled = false;
            if (tbl_Tipo_Inspeccion == null)
            {
                return NotFound();
            }

            return Ok(tbl_Tipo_Inspeccion);
        }

        // PUT: api/tblTipo_Inspeccion/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Tipo_Inspeccion(int id, tbl_Tipo_Inspeccion obj_entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj_entidad.id_TipoInspeccion)
            {
                return BadRequest();
            }

            tbl_Tipo_Inspeccion Ent_TipoInspeccionR;
            // DATA ACTUAL
            Ent_TipoInspeccionR = db.tbl_Tipo_Inspeccion.Where(g => g.id_TipoInspeccion  == obj_entidad.id_TipoInspeccion).FirstOrDefault<tbl_Tipo_Inspeccion>();

            Ent_TipoInspeccionR.descripcion_TipoInspeccion = obj_entidad.descripcion_TipoInspeccion;
            Ent_TipoInspeccionR.estado = obj_entidad.estado;
            Ent_TipoInspeccionR.usuario_edicion = obj_entidad.usuario_creacion;
            Ent_TipoInspeccionR.fecha_edicion = DateTime.Now;

            db.Entry(Ent_TipoInspeccionR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_Tipo_InspeccionExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("OK");
        }

        // POST: api/tblTipo_Inspeccion
        [ResponseType(typeof(tbl_Tipo_Inspeccion))]
        public IHttpActionResult Posttbl_Tipo_Inspeccion(tbl_Tipo_Inspeccion tbl_Tipo_Inspeccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_Tipo_Inspeccion.fecha_creacion = DateTime.Now;
            db.tbl_Tipo_Inspeccion.Add(tbl_Tipo_Inspeccion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Tipo_Inspeccion.id_TipoInspeccion }, tbl_Tipo_Inspeccion);
        }

        // DELETE: api/tblTipo_Inspeccion/5
        [ResponseType(typeof(tbl_Tipo_Inspeccion))]
        public async Task<IHttpActionResult> Deletetbl_Tipo_Inspeccion(int id)
        {
            tbl_Tipo_Inspeccion obj_entidad = await db.tbl_Tipo_Inspeccion.FindAsync(id);

            obj_entidad = db.tbl_Tipo_Inspeccion.Where(g => g.id_TipoInspeccion  == id).FirstOrDefault<tbl_Tipo_Inspeccion>();
            obj_entidad.estado = 0;
            db.Entry(obj_entidad).State = System.Data.Entity.EntityState.Modified;
            await db.SaveChangesAsync();
            return Ok("ok");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_Tipo_InspeccionExists(int id)
        {
            return db.tbl_Tipo_Inspeccion.Count(e => e.id_TipoInspeccion == id) > 0;
        }
    }
}