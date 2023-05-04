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
    public class tblAreasController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblAreas
        public IQueryable<tbl_Areas> Gettbl_Areas()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_Areas;
        }

        public object Gettbl_Areas(int opcion, string filtro)
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
                    int id_Delegacion = Convert.ToInt32(parametros[3].ToString());
                                        
                    resul = (from a in db.tbl_Areas  
                             join b in db.tbl_Delegacion on a.id_delegacion  equals b.id_Delegacion
                             join d in db.tbl_grupo on b.id_grupo equals d.id_grupo  
                             join e in db.tbl_pais on d.id_pais  equals e.id_pais
                             where a.id_delegacion==id_Delegacion
                             select new
                             {
                                d.id_pais,
                                b.id_grupo,
                                b.id_Empresa,
                                a.id_delegacion,
                                b.nombre_delegacion,
                                a.id_Area,
                                a.descripcion_Area,
                                a.estado,
                                a.usuario_creacion,
                                a.fecha_creacion,
                                a.usuario_edicion,
                                a.fecha_edicion                                               
 
                             }).ToList();
                }
                else if (opcion == 2)
                {

                    string[] parametros = filtro.Split('|');
                    int id_Delegacion = Convert.ToInt32(parametros[0].ToString());

                    resul = (from a in db.tbl_Areas
                             where a.id_delegacion == id_Delegacion && a.estado==1
                             select new
                             {
                                a.id_Area,
                                a.descripcion_Area
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
 

        // PUT: api/tblAreas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Areas(int id, tbl_Areas obj_entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj_entidad.id_Area)
            {
                return BadRequest();
            }


            tbl_Areas Ent_AreaR;
            // DATA ACTUAL
            Ent_AreaR = db.tbl_Areas.Where(g => g.id_Area  == obj_entidad.id_Area).FirstOrDefault<tbl_Areas>();

            Ent_AreaR.id_delegacion = obj_entidad.id_delegacion;
            Ent_AreaR.descripcion_Area  = obj_entidad.descripcion_Area ;
            Ent_AreaR.estado = obj_entidad.estado;
            Ent_AreaR.usuario_edicion = obj_entidad.usuario_creacion;
            Ent_AreaR.fecha_edicion = DateTime.Now;

            db.Entry(Ent_AreaR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_AreasExists(id))
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

        // POST: api/tblAreas
        [ResponseType(typeof(tbl_Areas))]
        public IHttpActionResult Posttbl_Areas(tbl_Areas tbl_Areas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_Areas.fecha_creacion = DateTime.Now;
            db.tbl_Areas.Add(tbl_Areas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Areas.id_Area }, tbl_Areas);
        }

        // DELETE: api/tblAreas/5

        [ResponseType(typeof(tbl_Areas))]
        public async Task<IHttpActionResult> Deletetbl_Areas(int id)
        {
            tbl_Areas obj_entidad = await db.tbl_Areas.FindAsync(id);

            obj_entidad = db.tbl_Areas.Where(g => g.id_Area == id).FirstOrDefault<tbl_Areas>();
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

        private bool tbl_AreasExists(int id)
        {
            return db.tbl_Areas.Count(e => e.id_Area == id) > 0;
        }
    }
}