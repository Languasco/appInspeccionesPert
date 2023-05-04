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
using Entidades.Mantenimiento;

namespace WebApi_inspeccionesPert.Controllers.Mantenimientos
{
    [EnableCors("*", "*", "*")]
    public class tblDelegacionController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblDelegacion
        public IQueryable<tbl_Delegacion> Gettbl_Delegacion()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_Delegacion;
        }
        // GET: api/tblDelegacion/5
        //[ResponseType(typeof(tbl_Delegacion))]
        //public IHttpActionResult Gettbl_Delegacion(int id)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    tbl_Delegacion tbl_Delegacion = db.tbl_Delegacion.Find(id);
        //    if (tbl_Delegacion == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tbl_Delegacion);
        //}
        public object Gettbl_Delegacion(int opcion, string filtro)
        {
            db.Configuration.ProxyCreationEnabled = false;

            object resul = null;
            try
            {
                if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|');

                    int id_empresa = Convert.ToInt32(parametros[0].ToString());

                    resul = (from a in db.tbl_Delegacion
                             where a.id_Empresa == id_empresa && a.estado ==1
                             select new
                             {
                                 a.id_Delegacion,
                                 a.codigo_delegacion,
                                 a.nombre_delegacion
                             }).ToList();
                }
                else if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    int id_grupo = Convert.ToInt32(parametros[0].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Listando_Delegacion(id_grupo);
                }
                else if (opcion == 3)
                {
                    string[] parametros = filtro.Split('|');
                    int id_grupo = Convert.ToInt32(parametros[0].ToString());
                    int id_usuarioLogin = Convert.ToInt32(parametros[1].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Listando_Delegacion_Empresa_Usuario(id_grupo, id_usuarioLogin);
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

        // PUT: api/tblDelegacion/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Delegacion(int id, tbl_Delegacion obj_entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj_entidad.id_Delegacion)
            {
                return BadRequest();
            }

            tbl_Delegacion Ent_DelegacionR;
            // DATA ACTUAL
            Ent_DelegacionR = db.tbl_Delegacion.Where(g => g.id_Delegacion  == obj_entidad.id_Delegacion ).FirstOrDefault<tbl_Delegacion>();

            Ent_DelegacionR.id_grupo = obj_entidad.id_grupo;
            Ent_DelegacionR.codigo_delegacion = obj_entidad.codigo_delegacion;
            Ent_DelegacionR.nombre_delegacion = obj_entidad.nombre_delegacion;
            Ent_DelegacionR.id_Personal_Representante = obj_entidad.id_Personal_Representante;

            Ent_DelegacionR.estado = obj_entidad.estado;
            Ent_DelegacionR.usuario_edicion = obj_entidad.usuario_creacion;
            Ent_DelegacionR.fecha_edicion = DateTime.Now;

            db.Entry(Ent_DelegacionR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_DelegacionExists(id))
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

        // POST: api/tblDelegacion
        [ResponseType(typeof(tbl_Delegacion))]
        public IHttpActionResult Posttbl_Delegacion(tbl_Delegacion tbl_Delegacion)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_Delegacion.fecha_creacion = DateTime.Now;
            db.tbl_Delegacion.Add(tbl_Delegacion);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Delegacion.id_Delegacion }, tbl_Delegacion);
        }

        [HttpPost]
        [Route("api/SaveDelegacion")]
        public IHttpActionResult SaveInspeccion(Save_Delegacion saved, string token)
        {
            if(token == "HGG7HHD098JKSHG89")
            {
                string mensaje = Delegacion_BL.Save_Delegacion(saved);
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
        [ResponseType(typeof(tbl_Delegacion))]
        public async Task<IHttpActionResult> Deletetbl_Delegacion(int id)
        {
            tbl_Delegacion obj_entidad = await db.tbl_Delegacion.FindAsync(id);

            obj_entidad = db.tbl_Delegacion.Where(g => g.id_Delegacion == id).FirstOrDefault<tbl_Delegacion>();
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

        private bool tbl_DelegacionExists(int id)
        {
            return db.tbl_Delegacion.Count(e => e.id_Delegacion == id) > 0;
        }
    }
}