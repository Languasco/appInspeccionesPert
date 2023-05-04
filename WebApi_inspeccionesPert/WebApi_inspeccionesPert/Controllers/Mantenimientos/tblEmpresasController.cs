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
    public class tblEmpresasController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblEmpresas
        public IQueryable<tbl_Empresas> Gettbl_Empresas()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_Empresas;
        }

        // GET: api/tblEmpresas/5
        //[ResponseType(typeof(tbl_Empresas))]
        //public IHttpActionResult Gettbl_Empresas(int id)
        //{
        //    tbl_Empresas tbl_Empresas = db.tbl_Empresas.Find(id);
        //    if (tbl_Empresas == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tbl_Empresas);
        //}

        public object Gettbl_Empresas(int opcion, string filtro)
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
                    int id_grupo = Convert.ToInt32(parametros[1].ToString());

                    resul = (from a in db.tbl_Empresas
                             join b in db.tbl_grupo on a.id_grupo equals b.id_grupo
                             where a.id_grupo == id_grupo
                             select new
                             {
                                a.id_Empresas,
                                a.id_grupo,
                                descripcion_grupo= b.descripcion,
                                b.id_pais,
                                a.codigo_interno_empresa,
                                a.ruc_empresa,
                                a.razonsocial_empresa,
                                a.direccion_empresa,
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
                    int id_grupo = Convert.ToInt32(parametros[0].ToString());
                    int id_usuarioLogin = Convert.ToInt32(parametros[1].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Listando_Empresa_Grupo_Usuario(id_grupo, id_usuarioLogin);
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


        // PUT: api/tblEmpresas/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Empresas(int id, tbl_Empresas obj_entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj_entidad.id_Empresas)
            {
                return BadRequest();
            }


            tbl_Empresas Ent_empresaR;
            // DATA ACTUAL
            Ent_empresaR = db.tbl_Empresas.Where(g => g.id_Empresas == obj_entidad.id_Empresas).FirstOrDefault<tbl_Empresas>();

            Ent_empresaR.id_grupo  = obj_entidad.id_grupo;
            Ent_empresaR.codigo_interno_empresa = obj_entidad.codigo_interno_empresa;
            Ent_empresaR.ruc_empresa = obj_entidad.ruc_empresa;
            Ent_empresaR.razonsocial_empresa = obj_entidad.razonsocial_empresa;
            Ent_empresaR.direccion_empresa = obj_entidad.direccion_empresa;
            Ent_empresaR.estado = obj_entidad.estado;
            Ent_empresaR.usuario_edicion = obj_entidad.usuario_creacion;
            Ent_empresaR.fecha_edicion = DateTime.Now;

            db.Entry(Ent_empresaR).State = EntityState.Modified;

            //db.Entry(obj_entidad).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_EmpresasExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return Ok("OK");

            //return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/tblEmpresas
        [ResponseType(typeof(tbl_Empresas))]
        public IHttpActionResult Posttbl_Empresas(tbl_Empresas tbl_Empresas)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_Empresas.fecha_creacion = DateTime.Now;
            db.tbl_Empresas.Add(tbl_Empresas);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Empresas.id_Empresas }, tbl_Empresas);
        }

 


        // DELETE: api/TblAlm_Productos_UnidadMedida/5
        [ResponseType(typeof(tbl_Empresas))]
        public async Task<IHttpActionResult> Deletetbl_Empresas(int id)
        {
            tbl_Empresas obj_entidad = await db.tbl_Empresas.FindAsync(id);

            obj_entidad = db.tbl_Empresas.Where(g => g.id_Empresas == id).FirstOrDefault<tbl_Empresas>();
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

        private bool tbl_EmpresasExists(int id)
        {
            return db.tbl_Empresas.Count(e => e.id_Empresas == id) > 0;
        }
    }
}