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
    public class tblActividadController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblActividad
        public IQueryable<tbl_Actividad> Gettbl_Actividad()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_Actividad;
        }

        public object Gettbl_Actividad(int opcion, string filtro)
        {
            db.Configuration.ProxyCreationEnabled = false;

            object resul = null;
            try
            {
                if (opcion == 1)
                {

                    string[] parametros = filtro.Split('|');

                    //string id_pais = (Convert.ToInt32(parametros[0].ToString()) == 0) ? "" : parametros[0].ToString();
                    //string id_grupo = (Convert.ToInt32(parametros[1].ToString()) == 0) ? "" : parametros[1].ToString();
                    //string id_Empresas = (Convert.ToInt32(parametros[2].ToString()) == 0) ? "" : parametros[2].ToString();
                    //string id_Delegacion = (Convert.ToInt32(parametros[3].ToString()) == 0) ? "" : parametros[3].ToString();
                    int id_Delegacion = Convert.ToInt32(parametros[2].ToString());

                    resul = (from a in db.tbl_Actividad 
                             join b in db.tbl_Delegacion on a.id_delegacion equals b.id_Delegacion
                             join d in db.tbl_grupo on b.id_grupo equals d.id_grupo
                             where a.id_delegacion == id_Delegacion
                             select new
                             {
                                d.id_pais,
                                b.id_grupo,
                                a.id_delegacion,
                                b.nombre_delegacion,
                                a.id_Actividad,
                                a.codigo_Actividad,
                                a.descripcion_Actividad,
                                a.estado,
                                a.usuario_creacion,
                                a.fecha_creacion,
                                a.usuario_edicion,
                                a.fecha_edicion,
                             }).ToList();
                } 
                else if (opcion == 2)
                {

                    string[] parametros = filtro.Split('|');
                    int id_Delegacion = Convert.ToInt32(parametros[0].ToString());

                    resul = (from a in db.tbl_Actividad
                             where a.id_delegacion == id_Delegacion && a.estado ==1
                             select new
                             {
                                 a.id_Actividad,
                                 a.codigo_Actividad,
                                 a.descripcion_Actividad,
 
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





        // PUT: api/tblActividad/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Actividad(int id, tbl_Actividad obj_entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj_entidad.id_Actividad)
            {
                return BadRequest();
            }

            tbl_Actividad Ent_ActividadR;
            // DATA ACTUAL
            Ent_ActividadR = db.tbl_Actividad.Where(g => g.id_Actividad == obj_entidad.id_Actividad).FirstOrDefault<tbl_Actividad>();

            Ent_ActividadR.id_delegacion  = obj_entidad.id_delegacion;
            Ent_ActividadR.codigo_Actividad = obj_entidad.codigo_Actividad;
            Ent_ActividadR.descripcion_Actividad = obj_entidad.descripcion_Actividad;
            Ent_ActividadR.estado = obj_entidad.estado;
            Ent_ActividadR.usuario_edicion = obj_entidad.usuario_creacion;
            Ent_ActividadR.fecha_edicion = DateTime.Now;

            db.Entry(Ent_ActividadR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_ActividadExists(id))
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

        // POST: api/tblActividad
        [ResponseType(typeof(tbl_Actividad))]
        public IHttpActionResult Posttbl_Actividad(tbl_Actividad tbl_Actividad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_Actividad.fecha_creacion = DateTime.Now;
            db.tbl_Actividad.Add(tbl_Actividad);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Actividad.id_Actividad }, tbl_Actividad);
        }

        // DELETE: api/tblActividad/5
        [ResponseType(typeof(tbl_Actividad))]
        public async Task<IHttpActionResult> Deletetbl_Actividad(int id)
        {
            tbl_Actividad obj_entidad = await db.tbl_Actividad.FindAsync(id);

            obj_entidad = db.tbl_Actividad.Where(g => g.id_Actividad  == id).FirstOrDefault<tbl_Actividad>();
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

        private bool tbl_ActividadExists(int id)
        {
            return db.tbl_Actividad.Count(e => e.id_Actividad == id) > 0;
        }
    }
}