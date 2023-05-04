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
    public class tblInspeccion_Cab_DetalleController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblInspeccion_Cab_Detalle
        public IQueryable<tbl_Inspeccion_Cab_Detalle> Gettbl_Inspeccion_Cab_Detalle()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_Inspeccion_Cab_Detalle;
        }

        // GET: api/tblInspeccion_Cab_Detalle/5
        [ResponseType(typeof(tbl_Inspeccion_Cab_Detalle))]
        public IHttpActionResult Gettbl_Inspeccion_Cab_Detalle(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbl_Inspeccion_Cab_Detalle tbl_Inspeccion_Cab_Detalle = db.tbl_Inspeccion_Cab_Detalle.Find(id);
            if (tbl_Inspeccion_Cab_Detalle == null)
            {
                return NotFound();
            }

            return Ok(tbl_Inspeccion_Cab_Detalle);
        }

        // PUT: api/tblInspeccion_Cab_Detalle/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Inspeccion_Cab_Detalle(int id, tbl_Inspeccion_Cab_Detalle obj_entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj_entidad.id_inspeccion_detalle)
            {
                return BadRequest();
            }

            tbl_Inspeccion_Cab_Detalle Ent_inspeccionDet;
            // DATA ACTUAL
            Ent_inspeccionDet = db.tbl_Inspeccion_Cab_Detalle.Where(g => g.id_inspeccion_detalle == obj_entidad.id_inspeccion_detalle).FirstOrDefault<tbl_Inspeccion_Cab_Detalle>();

            //Ent_inspeccionDet.id_inspeccion_detalle = obj_entidad.id_inspeccion_detalle;
            Ent_inspeccionDet.id_personal = obj_entidad.id_personal;
            Ent_inspeccionDet.id_anomalia = obj_entidad.id_anomalia;
            Ent_inspeccionDet.descripcion = obj_entidad.descripcion;
            Ent_inspeccionDet.id_ValorInspeccion = obj_entidad.id_ValorInspeccion;
            Ent_inspeccionDet.Resultado_Inspeccion = obj_entidad.Resultado_Inspeccion;

            Ent_inspeccionDet.disponividad_uso = "ZZ";
            Ent_inspeccionDet.accionPropuesta_Correctiva = obj_entidad.accionPropuesta_Correctiva;
            Ent_inspeccionDet.fechaPropuesta_Correctiva = obj_entidad.fechaPropuesta_Correctiva;

  

            try
            {
                db.Entry(Ent_inspeccionDet).State = EntityState.Modified;
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_Inspeccion_Cab_DetalleExists(id))
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

        // POST: api/tblInspeccion_Cab_Detalle
        [ResponseType(typeof(tbl_Inspeccion_Cab_Detalle))]
        public IHttpActionResult Posttbl_Inspeccion_Cab_Detalle(tbl_Inspeccion_Cab_Detalle tbl_Inspeccion_Cab_Detalle)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            var obj_personal = (from c in db.tbl_Personal
                                where c.id_Personal == tbl_Inspeccion_Cab_Detalle.id_personal
                                select c).ToList();


 
            tbl_Inspeccion_Cab_Detalle.id_cargo_personal = string.IsNullOrEmpty(obj_personal[0].id_Cargo.ToString()) == true ? 0 : obj_personal[0].id_Cargo;
            tbl_Inspeccion_Cab_Detalle.fecha_creacion = DateTime.Now;
            db.tbl_Inspeccion_Cab_Detalle.Add(tbl_Inspeccion_Cab_Detalle);
            db.SaveChanges();

             return CreatedAtRoute("DefaultApi", new { id = tbl_Inspeccion_Cab_Detalle.id_inspeccion_detalle }, tbl_Inspeccion_Cab_Detalle);
        }

        // DELETE: api/tblInspeccion_Cab_Detalle/5
        [ResponseType(typeof(tbl_Inspeccion_Cab_Detalle))]
        public IHttpActionResult Deletetbl_Inspeccion_Cab_Detalle(int id)
        {
            tbl_Inspeccion_Cab_Detalle tbl_Inspeccion_Cab_Detalle = db.tbl_Inspeccion_Cab_Detalle.Find(id);
            if (tbl_Inspeccion_Cab_Detalle == null)
            {
                return NotFound();
            }

            db.tbl_Inspeccion_Cab_Detalle.Remove(tbl_Inspeccion_Cab_Detalle);
            db.SaveChanges();

            return Ok(tbl_Inspeccion_Cab_Detalle);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_Inspeccion_Cab_DetalleExists(int id)
        {
            return db.tbl_Inspeccion_Cab_Detalle.Count(e => e.id_inspeccion_detalle == id) > 0;
        }
    }
}