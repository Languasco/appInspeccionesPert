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
    public class tblTipo_SancionController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblTipo_Sancion
        public IQueryable<tbl_Tipo_Sancion> Gettbl_Tipo_Sancion()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_Tipo_Sancion;
        }

        // GET: api/tblTipo_Sancion/5
        [ResponseType(typeof(tbl_Tipo_Sancion))]
        public IHttpActionResult Gettbl_Tipo_Sancion(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbl_Tipo_Sancion tbl_Tipo_Sancion = db.tbl_Tipo_Sancion.Find(id);
            if (tbl_Tipo_Sancion == null)
            {
                return NotFound();
            }

            return Ok(tbl_Tipo_Sancion);
        }

        // PUT: api/tblTipo_Sancion/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Tipo_Sancion(int id, tbl_Tipo_Sancion obj_entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj_entidad.id_TipoSancion)
            {
                return BadRequest();
            }

            tbl_Tipo_Sancion Ent_TipoSancionR;
            // DATA ACTUAL
            Ent_TipoSancionR = db.tbl_Tipo_Sancion.Where(g => g.id_TipoSancion  == obj_entidad.id_TipoSancion ).FirstOrDefault<tbl_Tipo_Sancion>();

            Ent_TipoSancionR.descripcion_TipoSancion = obj_entidad.descripcion_TipoSancion; 
            Ent_TipoSancionR.estado = obj_entidad.estado;
            Ent_TipoSancionR.usuario_edicion = obj_entidad.usuario_creacion;
            Ent_TipoSancionR.fecha_edicion = DateTime.Now;

            db.Entry(Ent_TipoSancionR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_Tipo_SancionExists(id))
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

        // POST: api/tblTipo_Sancion
        [ResponseType(typeof(tbl_Tipo_Sancion))]
        public IHttpActionResult Posttbl_Tipo_Sancion(tbl_Tipo_Sancion tbl_Tipo_Sancion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_Tipo_Sancion.fecha_creacion = DateTime.Now;
            db.tbl_Tipo_Sancion.Add(tbl_Tipo_Sancion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Tipo_Sancion.id_TipoSancion }, tbl_Tipo_Sancion);
        }

        // DELETE: api/tblTipo_Sancion/5
        [ResponseType(typeof(tbl_Tipo_Sancion))]
        public async Task<IHttpActionResult> Deletetbl_Actividad(int id)
        {
            tbl_Tipo_Sancion obj_entidad = await db.tbl_Tipo_Sancion.FindAsync(id);

            obj_entidad = db.tbl_Tipo_Sancion.Where(g => g.id_TipoSancion  == id).FirstOrDefault<tbl_Tipo_Sancion>();
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

        private bool tbl_Tipo_SancionExists(int id)
        {
            return db.tbl_Tipo_Sancion.Count(e => e.id_TipoSancion == id) > 0;
        }
    }
}