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
    public class tblFormatoInspeccionController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblFormatoInspeccion
        public IQueryable<tbl_FormatoInspeccion> Gettbl_FormatoInspeccion()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_FormatoInspeccion;
          
        }

        // GET: api/tblFormatoInspeccion/5
        [ResponseType(typeof(tbl_FormatoInspeccion))]
        public IHttpActionResult Gettbl_FormatoInspeccion(int id)
        {
            tbl_FormatoInspeccion tbl_FormatoInspeccion = db.tbl_FormatoInspeccion.Find(id);
            if (tbl_FormatoInspeccion == null)
            {
                return NotFound();
            }

            return Ok(tbl_FormatoInspeccion);
        }

        // PUT: api/tblFormatoInspeccion/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_FormatoInspeccion(int id, tbl_FormatoInspeccion obj_entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj_entidad.id_Formato)
            {
                return BadRequest();
            }

            tbl_FormatoInspeccion Ent_FormatoR;
            // DATA ACTUAL
            Ent_FormatoR = db.tbl_FormatoInspeccion.Where(g => g.id_Formato == obj_entidad.id_Formato).FirstOrDefault<tbl_FormatoInspeccion>();

            Ent_FormatoR.nombre_formato = obj_entidad.nombre_formato;
            Ent_FormatoR.estado = obj_entidad.estado;
            Ent_FormatoR.usuario_edicion = obj_entidad.usuario_creacion;
            Ent_FormatoR.fecha_edicion = DateTime.Now;

            db.Entry(Ent_FormatoR).State = EntityState.Modified;

            //db.Entry(tbl_FormatoInspeccion).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_FormatoInspeccionExists(id))
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

        // POST: api/tblFormatoInspeccion
        [ResponseType(typeof(tbl_FormatoInspeccion))]
        public IHttpActionResult Posttbl_FormatoInspeccion(tbl_FormatoInspeccion tbl_FormatoInspeccion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_FormatoInspeccion.fecha_creacion = DateTime.Now;
            db.tbl_FormatoInspeccion.Add(tbl_FormatoInspeccion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_FormatoInspeccion.id_Formato }, tbl_FormatoInspeccion);
        }

 
        // DELETE: api/tblTipo_Inspeccion/5
        [ResponseType(typeof(tbl_FormatoInspeccion))]
        public async Task<IHttpActionResult> Deletetbl_FormatoInspeccion(int id)
        {
            tbl_FormatoInspeccion obj_entidad = await db.tbl_FormatoInspeccion.FindAsync(id);

            obj_entidad = db.tbl_FormatoInspeccion.Where(g => g.id_Formato == id).FirstOrDefault<tbl_FormatoInspeccion>();
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

        private bool tbl_FormatoInspeccionExists(int id)
        {
            return db.tbl_FormatoInspeccion.Count(e => e.id_Formato == id) > 0;
        }
    }
}