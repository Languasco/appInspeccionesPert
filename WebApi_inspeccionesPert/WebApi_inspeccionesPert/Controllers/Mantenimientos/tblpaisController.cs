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
    public class tblpaisController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblpais
        public IQueryable<tbl_pais> Gettbl_pais()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_pais;
        }
        
        public object Gettbl_pais(int opcion, string filtro)
        {
            db.Configuration.ProxyCreationEnabled = false;

            object resul = null;
            try
            {
                if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|');
                    int id_usuario = Convert.ToInt32(parametros[0].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Listando_Pais_Usuario(id_usuario);
 
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

        // PUT: api/tblpais/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_pais(int id, tbl_pais obj_entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj_entidad.id_pais)
            {
                return BadRequest();
            }

            tbl_pais Ent_PaisR;
            Ent_PaisR = db.tbl_pais.Where(g => g.id_pais == obj_entidad.id_pais).FirstOrDefault<tbl_pais>();

            Ent_PaisR.iso = obj_entidad.iso ;
            Ent_PaisR.descripcion = obj_entidad.descripcion;
            Ent_PaisR.estado = obj_entidad.estado;
            Ent_PaisR.usuario_edicion = obj_entidad.usuario_creacion;
            Ent_PaisR.fecha_edicion = DateTime.Now;

            db.Entry(Ent_PaisR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_paisExists(id))
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

        // POST: api/tblpais
        [ResponseType(typeof(tbl_pais))]
        public IHttpActionResult Posttbl_pais(tbl_pais tbl_pais)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_pais.fecha_creacion = DateTime.Now;
            db.tbl_pais.Add(tbl_pais);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_pais.id_pais }, tbl_pais);
        }

        [ResponseType(typeof(tbl_pais))]
        public async Task<IHttpActionResult> Deletetbl_usuarios(int id)
        {
            tbl_pais obj_entidad = await db.tbl_pais.FindAsync(id);

            obj_entidad = db.tbl_pais.Where(g => g.id_pais == id).FirstOrDefault<tbl_pais>();
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

        private bool tbl_paisExists(int id)
        {
            return db.tbl_pais.Count(e => e.id_pais == id) > 0;
        }
    }
}