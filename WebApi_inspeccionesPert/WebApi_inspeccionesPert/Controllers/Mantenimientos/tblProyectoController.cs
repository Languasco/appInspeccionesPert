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
using Entidades.Mantenimiento;
using Negocios.Mantenimiento;

namespace WebApi_inspeccionesPert.Controllers.Mantenimientos
{
    [EnableCors("*", "*", "*")]
    public class tblProyectoController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblProyecto
        public IQueryable<tbl_Proyecto> Gettbl_Proyecto()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_Proyecto;
        }

        // GET: api/tblProyecto/5


        public object Gettbl_Delegacion(int opcion, string filtro)
        {
            db.Configuration.ProxyCreationEnabled = false;

            object resul = null;
            try
            { 
                if (opcion ==1)
                {
                    string[] parametros = filtro.Split('|');
                    int id_delegacion = Convert.ToInt32(parametros[0].ToString());
                 
                    resul = (from a in db.tbl_Proyecto
                             join b in db.tbl_Delegacion on a.id_Delegacion equals b.id_Delegacion
                             join d in db.tbl_grupo on b.id_grupo equals d.id_grupo  
                             join c in db.tbl_Personal on a.id_Personal_JefeObra equals c.id_Personal into sinJefe
                             join e in db.tbl_Cliente on a.id_Cliente equals e.id_Cliente into sinCliente
                             from c in sinJefe.DefaultIfEmpty()
                             from e in sinCliente.DefaultIfEmpty()
                             where a.id_Delegacion == id_delegacion
                             select new
                             {
                                d.id_pais,
                                b.id_grupo,
                                a.id_Proyecto,
                                a.id_Delegacion,
                                b.nombre_delegacion,
                                a.nombre_proyecto,
                                a.estado,
                                a.usuario_creacion,
                                a.fecha_creacion,
                                a.usuario_edicion,
                                a.fecha_edicion,
                                a.id_Cliente,
                                a.id_Personal_JefeObra,
                                c.apellidos_Personal,
                                c.nombres_Personal,
                                e.nombre_Cliente
                             }).ToList();
                }
                else
                {
                    resul = "Opcion selecciona invalida";
                }
            }
            catch (Exception ex)
            {
                resul = ex.Message;
            }
            return resul;
        }

        // PUT: api/tblProyecto/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Proyecto(int id, tbl_Proyecto obj_entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj_entidad.id_Proyecto)
            {
                return BadRequest();
            }


            tbl_Proyecto Ent_ProyectoR;
            // DATA ACTUAL
            Ent_ProyectoR = db.tbl_Proyecto.Where(g => g.id_Proyecto  == obj_entidad.id_Proyecto).FirstOrDefault<tbl_Proyecto>();

            Ent_ProyectoR.id_Empresa = obj_entidad.id_Empresa;
            Ent_ProyectoR.id_Delegacion = obj_entidad.id_Delegacion;
            Ent_ProyectoR.nombre_proyecto = obj_entidad.nombre_proyecto; 

            Ent_ProyectoR.estado = obj_entidad.estado;
            Ent_ProyectoR.usuario_edicion = obj_entidad.usuario_creacion;
            Ent_ProyectoR.fecha_edicion = DateTime.Now;

            Ent_ProyectoR.id_Personal_JefeObra = obj_entidad.id_Personal_JefeObra;
            Ent_ProyectoR.id_Cliente = obj_entidad.id_Cliente;

            db.Entry(Ent_ProyectoR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_ProyectoExists(id))
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

        // POST: api/tblProyecto
        [ResponseType(typeof(tbl_Proyecto))]
        public IHttpActionResult Posttbl_Proyecto(tbl_Proyecto tbl_Proyecto)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_Proyecto.fecha_creacion = DateTime.Now;
            db.tbl_Proyecto.Add(tbl_Proyecto);
            db.SaveChanges();
            return CreatedAtRoute("DefaultApi", new { id = tbl_Proyecto.id_Proyecto }, tbl_Proyecto);
        }

        [HttpPost]
        [Route("api/SaveProyecto")]
        public IHttpActionResult SaveInspeccion(Proyecto_En saved, string token)
        {
            if (token == "HGG7HHD098JKL0978kjf")
            {
                string mensaje = Proyecto_BL.saveMaterialOCompra_Web(saved);
                if (mensaje != null)
                {
                    return Ok(mensaje);
                }
                else return NotFound();
            }
            else
            {
                return Ok("No tienes permiso!");
            }
        }

        [HttpPost]
        [Route("api/SaveProyecto_Empresa_Edicion")]
        public IHttpActionResult Save_Empresa(List<Empresa_Colaboradora> saved, string token)
        {
            if (token == "HGG7HHD098JKL0978kjfkllk")
            {
                string mensaje = Proyecto_BL.saved_Empresas_Proyecto(saved);
                if (mensaje != null)
                {
                    return Ok(mensaje);
                }
                else return NotFound();
            }
            else
            {
                return Ok("No tienes permiso!");
            }
        }

        // DELETE: api/tblDelegacion/5
        [ResponseType(typeof(tbl_Proyecto))]
        public async Task<IHttpActionResult> Deletetbl_Proyecto(int id)
        {
            tbl_Proyecto obj_entidad = await db.tbl_Proyecto.FindAsync(id);

            obj_entidad = db.tbl_Proyecto.Where(g => g.id_Proyecto == id).FirstOrDefault<tbl_Proyecto>();
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

        private bool tbl_ProyectoExists(int id)
        {
            return db.tbl_Proyecto.Count(e => e.id_Proyecto == id) > 0;
        }
    }
}