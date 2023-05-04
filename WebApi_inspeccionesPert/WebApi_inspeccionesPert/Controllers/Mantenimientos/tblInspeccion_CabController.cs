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
    public class tblInspeccion_CabController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblInspeccion_Cab
        public IQueryable<tbl_Inspeccion_Cab> Gettbl_Inspeccion_Cab()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_Inspeccion_Cab;
        }

        // GET: api/tblInspeccion_Cab/5
        [ResponseType(typeof(tbl_Inspeccion_Cab))]
        public IHttpActionResult Gettbl_Inspeccion_Cab(int id)
        {
            db.Configuration.ProxyCreationEnabled = false;
            tbl_Inspeccion_Cab tbl_Inspeccion_Cab = db.tbl_Inspeccion_Cab.Find(id);
            if (tbl_Inspeccion_Cab == null)
            {
                return NotFound();
            }

            return Ok(tbl_Inspeccion_Cab);
        }

        // PUT: api/tblInspeccion_Cab/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Inspeccion_Cab(int id, tbl_Inspeccion_Cab obj_entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj_entidad.id_Inspeccion)
            {
                return BadRequest();
            }

            tbl_Inspeccion_Cab Ent_inpeccionCab;
            // DATA ACTUAL
            Ent_inpeccionCab = db.tbl_Inspeccion_Cab.Where(g => g.id_Inspeccion == obj_entidad.id_Inspeccion).FirstOrDefault<tbl_Inspeccion_Cab>();

            Ent_inpeccionCab.id_Inspeccion = obj_entidad.id_Inspeccion;


            Ent_inpeccionCab.id_Pais = obj_entidad.id_Pais;
            Ent_inpeccionCab.id_grupo = obj_entidad.id_grupo;
            Ent_inpeccionCab.id_Empresa = obj_entidad.id_Empresa;
            Ent_inpeccionCab.id_Delegacion = obj_entidad.id_Delegacion;


            Ent_inpeccionCab.id_EmpresaColaboradora = obj_entidad.id_EmpresaColaboradora;
            Ent_inpeccionCab.id_Cliente = obj_entidad.id_Cliente;
            Ent_inpeccionCab.lugar_Inspeccion = obj_entidad.lugar_Inspeccion;
            Ent_inpeccionCab.actividadOT_Inspeccion = obj_entidad.actividadOT_Inspeccion;
            Ent_inpeccionCab.trabajoArealizar_Inspeccion = obj_entidad.trabajoArealizar_Inspeccion;
            Ent_inpeccionCab.id_Cargo = obj_entidad.id_Cargo;
            Ent_inpeccionCab.id_Personal_Inspeccionado = obj_entidad.id_Personal_Inspeccionado;
            Ent_inpeccionCab.id_Area = obj_entidad.id_Area;
            Ent_inpeccionCab.id_Personal_Coordinador = obj_entidad.id_Personal_Coordinador;
            Ent_inpeccionCab.id_Personal_JefeObra = obj_entidad.id_Personal_JefeObra;
            Ent_inpeccionCab.placa_Inspeccion = obj_entidad.placa_Inspeccion;
            Ent_inpeccionCab.id_NivelInspeccion = obj_entidad.id_NivelInspeccion;
            
            Ent_inpeccionCab.id_TipoInspeccion = obj_entidad.id_TipoInspeccion;
            
            Ent_inpeccionCab.Resultado_Inspeccion = obj_entidad.Resultado_Inspeccion;

            Ent_inpeccionCab.iniciofin_Trabajo = obj_entidad.iniciofin_Trabajo;
            Ent_inpeccionCab.accionPropuesta_Correctiva = obj_entidad.accionPropuesta_Correctiva;
            Ent_inpeccionCab.id_Personal_Responsable = obj_entidad.id_Personal_Responsable;
            Ent_inpeccionCab.fechaPropuesta_Correctiva = obj_entidad.fechaPropuesta_Correctiva;
            Ent_inpeccionCab.observacion_Correctiva = obj_entidad.observacion_Correctiva;
            Ent_inpeccionCab.paralizacion_Correctiva = obj_entidad.paralizacion_Correctiva;
            Ent_inpeccionCab.sancion_Correctiva = obj_entidad.sancion_Correctiva;
            Ent_inpeccionCab.id_TipoSancion = obj_entidad.id_TipoSancion;
            Ent_inpeccionCab.nroTrabajadores_Correctiva = obj_entidad.nroTrabajadores_Correctiva;
            Ent_inpeccionCab.Obs_Levantada = obj_entidad.Obs_Levantada;
            Ent_inpeccionCab.estado = obj_entidad.id_NivelInspeccion == 1 ? 5 : 6; 
            Ent_inpeccionCab.usuario_edicion = obj_entidad.usuario_creacion;
            Ent_inpeccionCab.fecha_edicion = DateTime.Now;

            db.Entry(Ent_inpeccionCab).State = EntityState.Modified;
            
            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_Inspeccion_CabExists(id))
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

        // POST: api/tblInspeccion_Cab
        [ResponseType(typeof(tbl_Inspeccion_Cab))]
        public IHttpActionResult Posttbl_Inspeccion_Cab(tbl_Inspeccion_Cab tbl_Inspeccion_Cab)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            ////var consulta=( from c in db.tbl_Personal
            ////              where c.id_Personal == tbl_Inspeccion_Cab.usuario_creacion 
            ////              select c).ToList();


            ////tbl_Inspeccion_Cab.id_Empresa   = consulta[0].id_Empresa;
            ////tbl_Inspeccion_Cab.id_Delegacion   = consulta[0].id_Delegacion;
            ////tbl_Inspeccion_Cab.id_Proyecto = consulta[0].id_Proyecto;

            tbl_Inspeccion_Cab.id_Anomalia = 0;
            tbl_Inspeccion_Cab.id_Personal_Inspector = tbl_Inspeccion_Cab.usuario_creacion;
            tbl_Inspeccion_Cab.estado = tbl_Inspeccion_Cab.id_NivelInspeccion == 1 ? 5 : 6; 

            tbl_Inspeccion_Cab.fecha_Inspeccion  = DateTime.Now;
            tbl_Inspeccion_Cab.fecha_creacion = DateTime.Now;
            db.tbl_Inspeccion_Cab.Add(tbl_Inspeccion_Cab);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Inspeccion_Cab.id_Inspeccion }, tbl_Inspeccion_Cab);
        }

        // DELETE: api/tblInspeccion_Cab/5
        [ResponseType(typeof(tbl_Inspeccion_Cab))]
        public IHttpActionResult Deletetbl_Inspeccion_Cab(int id)
        {
            tbl_Inspeccion_Cab tbl_Inspeccion_Cab = db.tbl_Inspeccion_Cab.Find(id);
            if (tbl_Inspeccion_Cab == null)
            {
                return NotFound();
            }

            db.tbl_Inspeccion_Cab.Remove(tbl_Inspeccion_Cab);
            db.SaveChanges();

            return Ok(tbl_Inspeccion_Cab);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_Inspeccion_CabExists(int id)
        {
            return db.tbl_Inspeccion_Cab.Count(e => e.id_Inspeccion == id) > 0;
        }
    }
}