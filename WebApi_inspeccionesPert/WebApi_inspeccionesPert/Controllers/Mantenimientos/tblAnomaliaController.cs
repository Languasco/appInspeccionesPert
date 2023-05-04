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
    public class tblAnomaliaController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblAnomalia
        public IQueryable<tbl_Anomalia> Gettbl_Anomalia()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_Anomalia;
        }

        public object Gettbl_Anomalia(int opcion, string filtro)
        {
            db.Configuration.ProxyCreationEnabled = false;
            object resul = null;
            try
            { 
               if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|');
                    int id_formato = Convert.ToInt32(parametros[0].ToString());

                    resul = (from a in db.tbl_Anomalia 
                             where a.id_formato  == id_formato
                             select new
                             {
                                 a.id_Anomalia,
                                 a.descripcion_Anomalia                                

                             }).ToList();
                }

               else if (opcion == 2)
               {
                  string[] parametros = filtro.Split('|');

                    //string id_pais = (Convert.ToInt32(parametros[0].ToString()) == 0) ? "" : parametros[0].ToString();
                    //string id_grupo = (Convert.ToInt32(parametros[1].ToString()) == 0) ? "" : parametros[1].ToString();
                    //string id_Empresas = (Convert.ToInt32(parametros[2].ToString()) == 0) ? "" : parametros[2].ToString();
                    //int id_pais = Convert.ToInt32(parametros[0].ToString());
                    int id_grupo = Convert.ToInt32(parametros[1].ToString());
                    int idformato = Convert.ToInt32(parametros[2].ToString());

                   resul = (from a in db.tbl_Anomalia
                            join d in db.tbl_grupo on a.id_grupo equals d.id_grupo
                            join e in db.tbl_pais on d.id_pais equals e.id_pais
                            join f in db.tbl_FormatoInspeccion on a.id_formato equals f.id_Formato
                            where a.id_grupo == id_grupo && a.id_formato == idformato
                            select new
                            {
                                d.id_pais,
                                a.id_grupo,
                                a.id_Empresa,
                                a.id_Anomalia,
                                a.id_formato,
                                f.nombre_formato,
                                a.codigo_Anomalia,
                                a.descripcion_Anomalia,
                                a.anomalia_Critica,
                                a.anomalia_Critica_General,
                                a.anomalia_titulo,
                                a.anomalia_orden,
                                a.id_ValorInspeccion,
                                a.estado,
                                a.Descripcion_Anomalia_Espana,
                                a.usuario_creacion,
                                a.fecha_creacion,
                                a.anomalia_Valor,
                                a.anomalia_Grupo,
                                a.ver_Validacion,           
                                a.flag_personal_nuevo

                            }).ToList();
               }
               else if (opcion == 3)
               {
                   string[] parametros = filtro.Split('|');
                   int id_formato = Convert.ToInt32(parametros[0].ToString());
                   int id_empresa = Convert.ToInt32(parametros[1].ToString());

                   resul = (from a in db.tbl_Anomalia
                            where a.id_formato == id_formato && a.id_Empresa == id_empresa
                            select new
                            {
                                a.id_Anomalia,
                                a.descripcion_Anomalia,
                                a.ver_Validacion 
                            }).ToList();
               }
               else if (opcion == 4)
               {
                   string[] parametros = filtro.Split('|');
                   int id_grupo = Convert.ToInt32(parametros[0].ToString());

                   Anomalias_BL obj_negocio = new Anomalias_BL();
                   resul = obj_negocio.Exportar_Anomalias(id_grupo);
               }
                else if (opcion == 5)
                {
                    string[] parametros = filtro.Split('|');
                    string codAnomalia = parametros[0].ToString().ToUpper();

                    if (db.tbl_Anomalia.Count(e => e.codigo_Anomalia.ToUpper() == codAnomalia) > 0)
                    {
                        resul = true;
                    }
                    else
                    {
                        resul = false;
                    }
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


        // PUT: api/tblAnomalia/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Anomalia(int id, tbl_Anomalia obj_entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj_entidad.id_Anomalia)
            {
                return BadRequest();
            }

            tbl_Anomalia Ent_AnomaliaR;
            // DATA ACTUAL
            Ent_AnomaliaR = db.tbl_Anomalia.Where(g => g.id_Anomalia  == obj_entidad.id_Anomalia ).FirstOrDefault<tbl_Anomalia>();

            Ent_AnomaliaR.id_formato = obj_entidad.id_formato;
            Ent_AnomaliaR.id_Empresa  = obj_entidad.id_Empresa ;
            Ent_AnomaliaR.codigo_Anomalia = obj_entidad.codigo_Anomalia;
            Ent_AnomaliaR.descripcion_Anomalia = obj_entidad.descripcion_Anomalia;
            Ent_AnomaliaR.anomalia_Critica = obj_entidad.anomalia_Critica;
            Ent_AnomaliaR.anomalia_Critica_General = obj_entidad.anomalia_Critica_General;
            Ent_AnomaliaR.ver_Validacion = obj_entidad.ver_Validacion;

            Ent_AnomaliaR.anomalia_titulo = obj_entidad.anomalia_titulo;
            Ent_AnomaliaR.anomalia_Grupo = obj_entidad.anomalia_Grupo;
            Ent_AnomaliaR.Descripcion_Anomalia_Espana = obj_entidad.Descripcion_Anomalia_Espana;
            Ent_AnomaliaR.anomalia_orden = obj_entidad.anomalia_orden;
            Ent_AnomaliaR.anomalia_Valor = obj_entidad.anomalia_Valor;
            Ent_AnomaliaR.ver_Validacion = obj_entidad.ver_Validacion;

            Ent_AnomaliaR.estado = obj_entidad.estado;
            Ent_AnomaliaR.usuario_edicion = obj_entidad.usuario_creacion;
            Ent_AnomaliaR.fecha_edicion = DateTime.Now;

            db.Entry(Ent_AnomaliaR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_AnomaliaExists(id))
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

        // POST: api/tblAnomalia
        [ResponseType(typeof(tbl_Anomalia))]
        public IHttpActionResult Posttbl_Anomalia(tbl_Anomalia tbl_Anomalia)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_Anomalia.fecha_creacion = DateTime.Now;
            db.tbl_Anomalia.Add(tbl_Anomalia);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Anomalia.id_Anomalia }, tbl_Anomalia);
        }

        // DELETE: api/tblAnomalia/5
        [ResponseType(typeof(tbl_Anomalia))]
        public async Task<IHttpActionResult> Deletetbl_Anomalia(int id)
        {
            tbl_Anomalia obj_entidad = await db.tbl_Anomalia.FindAsync(id);

            obj_entidad = db.tbl_Anomalia.Where(g => g.id_Anomalia  == id).FirstOrDefault<tbl_Anomalia>();
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

        private bool tbl_AnomaliaExists(int id)
        {
            return db.tbl_Anomalia.Count(e => e.id_Anomalia == id) > 0;
        }
    }
}