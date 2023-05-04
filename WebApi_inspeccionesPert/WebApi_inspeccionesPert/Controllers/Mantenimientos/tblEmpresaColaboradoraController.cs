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
    public class tblEmpresaColaboradoraController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblEmpresaColaboradora
        public IQueryable<tbl_EmpresaColaboradora> Gettbl_EmpresaColaboradora()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_EmpresaColaboradora;
        }

        public object Gettbl_EmpresaColaboradora(int opcion, string filtro)
        {
            db.Configuration.ProxyCreationEnabled = false;

            object resul = null;
            try
            {
                if (opcion == 1)
                {

                    string[] parametros = filtro.Split('|');
                    int id_grupo = Convert.ToInt32(parametros[0].ToString());

                    resul = (from a in db.tbl_EmpresaColaboradora
                             where a.id_grupo == id_grupo && a.estado ==1

                             select new
                             {
                               a.id_EmpresaColaboradora,
                               a.RazonSocial_EmpresaColaboradora
                             }).ToList();
                }
                else if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    int id_grupo = Convert.ToInt32(parametros[0].ToString());

                    resul = (from a in db.tbl_EmpresaColaboradora 
                             join b in db.tbl_grupo on a.id_grupo equals b.id_grupo
                             where a.id_grupo == id_grupo && a.estado == 1
                             select new
                             {
                                 a.id_EmpresaColaboradora ,
                                 a.id_grupo,
                                 descripcion_grupo = b.descripcion,
                                 b.id_pais,
                                 a.ruc_EmpresaColaboradora ,
                                 a.RazonSocial_EmpresaColaboradora ,
                                 a.estado,
                                 a.Directo_EmpresaColaboradora, 
                                 a.usuario_creacion,
                                 a.fecha_creacion,
                                 a.usuario_edicion,
                                 a.fecha_edicion
                             }).ToList();
                }
                else if (opcion == 3)
                {
                    string[] parametros = filtro.Split('|');
                    int id_grupo = Convert.ToInt32(parametros[0].ToString());

                    resul = (from a in db.tbl_EmpresaColaboradora
                             join b in db.tbl_grupo on a.id_grupo equals b.id_grupo
                             where a.id_grupo == id_grupo  
                             select new
                             {
                                 a.id_EmpresaColaboradora,
                                 a.id_grupo,
                                 descripcion_grupo = b.descripcion,
                                 b.id_pais,
                                 a.ruc_EmpresaColaboradora,
                                 a.RazonSocial_EmpresaColaboradora,
                                 a.estado,
                                 a.Directo_EmpresaColaboradora,
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





        // PUT: api/tblEmpresaColaboradora/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_EmpresaColaboradora(int id, tbl_EmpresaColaboradora obj_entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj_entidad.id_EmpresaColaboradora)
            {
                return BadRequest();
            }

            tbl_EmpresaColaboradora Ent_empresaR;
            // DATA ACTUAL
            Ent_empresaR = db.tbl_EmpresaColaboradora.Where(g => g.id_EmpresaColaboradora == obj_entidad.id_EmpresaColaboradora).FirstOrDefault<tbl_EmpresaColaboradora>();

            Ent_empresaR.id_grupo = obj_entidad.id_grupo;
            Ent_empresaR.ruc_EmpresaColaboradora = obj_entidad.ruc_EmpresaColaboradora;
            Ent_empresaR.RazonSocial_EmpresaColaboradora = obj_entidad.RazonSocial_EmpresaColaboradora;
            Ent_empresaR.estado = obj_entidad.estado;
            Ent_empresaR.Directo_EmpresaColaboradora = obj_entidad.Directo_EmpresaColaboradora;
            Ent_empresaR.usuario_edicion = obj_entidad.usuario_creacion;
            Ent_empresaR.fecha_edicion = DateTime.Now;
            db.Entry(Ent_empresaR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_EmpresaColaboradoraExists(id))
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

        // POST: api/tblEmpresaColaboradora
        [ResponseType(typeof(tbl_EmpresaColaboradora))]
        public IHttpActionResult Posttbl_EmpresaColaboradora(tbl_EmpresaColaboradora tbl_EmpresaColaboradora)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_EmpresaColaboradora.fecha_creacion = DateTime.Now;
            db.tbl_EmpresaColaboradora.Add(tbl_EmpresaColaboradora);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_EmpresaColaboradora.id_EmpresaColaboradora }, tbl_EmpresaColaboradora);
        }




        [ResponseType(typeof(tbl_EmpresaColaboradora))]
        public async Task<IHttpActionResult> Deletetbl_Empresas(int id)
        {
            tbl_EmpresaColaboradora obj_entidad = await db.tbl_EmpresaColaboradora.FindAsync(id);

            obj_entidad = db.tbl_EmpresaColaboradora.Where(g => g.id_EmpresaColaboradora == id).FirstOrDefault<tbl_EmpresaColaboradora>();
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

        private bool tbl_EmpresaColaboradoraExists(int id)
        {
            return db.tbl_EmpresaColaboradora.Count(e => e.id_EmpresaColaboradora == id) > 0;
        }
    }
}