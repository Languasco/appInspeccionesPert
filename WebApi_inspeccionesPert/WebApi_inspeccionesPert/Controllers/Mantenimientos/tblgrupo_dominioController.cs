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
using System.IO;
using Newtonsoft.Json;
using Negocios.Mantenimiento;

namespace WebApi_inspeccionesPert.Controllers.Mantenimientos
{
   [EnableCors("*", "*", "*")]
    public class tblgrupo_dominioController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblgrupo_dominio
        public IQueryable<tbl_grupo_dominio> Gettbl_grupo_dominio()
        {
            return db.tbl_grupo_dominio;
        }

        // GET: api/tblgrupo_dominio/5


        public object Gettbl_grupo(int opcion, string filtro)
        {
            db.Configuration.ProxyCreationEnabled = false;

            object resul = null;
            try
            {
               if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|');
                    int id_grupo = Convert.ToInt32(parametros[0].ToString());

                    resul = (from a in db.tbl_grupo_dominio
                             where a.id_grupo == id_grupo
                             orderby a.id_grupo_dominio
                             select new
                             {
                                 a.id_grupo_dominio,
                                 a.id_grupo,
                                 a.nombre_dominio,
                                 a.estado,
                                 a.usuario_creacion,
                                 a.fecha_creacion,
                                 a.usuario_edicion,
                                 a.fecha_edicion
                             }).ToList();
                }else if(opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    string dominio = parametros[0].ToString();

                    Dominio_BL validate = new Dominio_BL();

                    resul = validate.CheckValidDomain(dominio);
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

        // PUT: api/tblgrupo_dominio/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_grupo_dominio(int id, tbl_grupo_dominio obj_entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj_entidad.id_grupo_dominio)
            {
                return BadRequest();
            }

            tbl_grupo_dominio Ent_GrupoR;
            Ent_GrupoR = db.tbl_grupo_dominio.Where(g => g.id_grupo_dominio == obj_entidad.id_grupo_dominio).FirstOrDefault<tbl_grupo_dominio>();

            Ent_GrupoR.nombre_dominio = obj_entidad.nombre_dominio;

            Ent_GrupoR.estado = obj_entidad.estado;
            Ent_GrupoR.usuario_edicion = obj_entidad.usuario_creacion;
            Ent_GrupoR.fecha_edicion = DateTime.Now;

            db.Entry(Ent_GrupoR).State = EntityState.Modified;
 

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_grupo_dominioExists(id))
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

        // POST: api/tblgrupo_dominio
        [ResponseType(typeof(tbl_grupo_dominio))]
        public IHttpActionResult Posttbl_grupo_dominio(tbl_grupo_dominio tbl_grupo_dominio)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_grupo_dominio.fecha_creacion = DateTime.Now;
            db.tbl_grupo_dominio.Add(tbl_grupo_dominio);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_grupo_dominio.id_grupo_dominio }, tbl_grupo_dominio);
        }


        [ResponseType(typeof(tbl_grupo_dominio))]
        public async Task<IHttpActionResult> Deletetbl_grupo_dominio(int id, int estado)
        {
            tbl_grupo_dominio obj_entidad = await db.tbl_grupo_dominio.FindAsync(id);

            obj_entidad = db.tbl_grupo_dominio.Where(g => g.id_grupo_dominio == id).FirstOrDefault<tbl_grupo_dominio>();

            obj_entidad.estado = estado;   
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

        private bool tbl_grupo_dominioExists(int id)
        {
            return db.tbl_grupo_dominio.Count(e => e.id_grupo_dominio == id) > 0;
        }
    }
}