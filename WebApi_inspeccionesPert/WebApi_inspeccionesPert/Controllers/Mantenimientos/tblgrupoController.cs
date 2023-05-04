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
using Negocios.Mantenimiento;

namespace WebApi_inspeccionesPert.Controllers.Mantenimientos
{
    [EnableCors("*", "*", "*")]
    public class tblgrupoController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblgrupo
        public IQueryable<tbl_grupo> Gettbl_grupo()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_grupo;
        }


        public object Gettbl_grupo(int opcion, string filtro)
        {
            db.Configuration.ProxyCreationEnabled = false;

            object resul = null;
            try
            {
                if (opcion == 1)
                {
                    resul = (from a in db.tbl_grupo 
                             join b in db.tbl_pais on a.id_pais equals b.id_pais
                             orderby   a.id_pais,a.id_grupo
                             select new
                             {
                                a.id_grupo,
                                a.id_pais,
                                descripcion_pais = b.descripcion,
                                a.descripcion,
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
                    int id_pais = Convert.ToInt32(parametros[0].ToString());

                    resul = (from a in db.tbl_grupo
                             where a.id_pais == id_pais
                             select new
                             {
                                 a.id_grupo,
                                 a.id_pais,
                                 a.descripcion,
                                 a.estado
                             }).ToList();
                }

                else if (opcion == 3)
                {
                    string[] parametros = filtro.Split('|');
                    int id_pais = Convert.ToInt32(parametros[0].ToString());
                    int id_usuarioLogin = Convert.ToInt32(parametros[1].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Listando_Grupo_Pais_Usuario(id_pais, id_usuarioLogin);
                }
                else if (opcion == 4)
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

        // PUT: api/tblgrupo/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_grupo(int id, tbl_grupo obj_entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj_entidad.id_grupo)
            {
                return BadRequest();
            }

            tbl_grupo Ent_GrupoR;
            Ent_GrupoR = db.tbl_grupo.Where(g => g.id_grupo == obj_entidad.id_grupo).FirstOrDefault<tbl_grupo>();

            Ent_GrupoR.id_pais = obj_entidad.id_pais;
            Ent_GrupoR.descripcion  = obj_entidad.descripcion;

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
                if (!tbl_grupoExists(id))
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

        // POST: api/tblgrupo
        [ResponseType(typeof(tbl_grupo))]
        public IHttpActionResult Posttbl_grupo(tbl_grupo tbl_grupo)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_grupo.fecha_creacion = DateTime.Now;
            db.tbl_grupo.Add(tbl_grupo);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_grupo.id_grupo }, tbl_grupo);
        }
        
        [ResponseType(typeof(tbl_grupo))]
        public async Task<IHttpActionResult> Deletetbl_usuarios(int id)
        {
            tbl_grupo obj_entidad = await db.tbl_grupo.FindAsync(id);

            obj_entidad = db.tbl_grupo.Where(g => g.id_grupo  == id).FirstOrDefault<tbl_grupo>();
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

        private bool tbl_grupoExists(int id)
        {
            return db.tbl_grupo.Count(e => e.id_grupo == id) > 0;
        }
    }
}