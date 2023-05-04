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
    public class tblClienteController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblCliente
        public IQueryable<tbl_Cliente> Gettbl_Cliente()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_Cliente;
        }

        // GET: api/tblCliente/5
        public object Gettbl_Cliente(int opcion, string filtro)
        {
            db.Configuration.ProxyCreationEnabled = false;

            object resul = null;
            try
            {
                if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|');
 
                    int id_grupo = Convert.ToInt32(parametros[0].ToString());

                    resul = (from a in db.tbl_Cliente
                             join b in db.tbl_grupo on a.id_grupo equals b.id_grupo
                             join c in db.tbl_pais on b.id_pais equals c.id_pais
                             where a.id_grupo == id_grupo
                             select new
                             {
                                a.id_Cliente, 
                                a.ruc_Cliente, 
                                a.nombre_Cliente, 
                                a.estado, 
                                a.usuario_creacion, 
                                a.fecha_creacion, 
                                a.usuario_edicion, 
                                a.fecha_edicion, 
                                a.id_grupo,
                                grupo= b.descripcion,
                                b.id_pais
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


        // PUT: api/tblCliente/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Cliente(int id, tbl_Cliente obj_entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj_entidad.id_Cliente)
            {
                return BadRequest();
            }


            tbl_Cliente Ent_ClienteR;
            // DATA ACTUAL
            Ent_ClienteR = db.tbl_Cliente.Where(g => g.id_Cliente == obj_entidad.id_Cliente).FirstOrDefault<tbl_Cliente>();

            Ent_ClienteR.ruc_Cliente = obj_entidad.ruc_Cliente;
            Ent_ClienteR.nombre_Cliente = obj_entidad.nombre_Cliente;
            Ent_ClienteR.id_grupo = obj_entidad.id_grupo;

            Ent_ClienteR.estado = obj_entidad.estado;
            Ent_ClienteR.usuario_edicion = obj_entidad.usuario_creacion;
            Ent_ClienteR.fecha_edicion = DateTime.Now;

            db.Entry(Ent_ClienteR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_ClienteExists(id))
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

        // POST: api/tblCliente
        [ResponseType(typeof(tbl_Cliente))]
        public IHttpActionResult Posttbl_Cliente(tbl_Cliente tbl_Cliente)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_Cliente.fecha_creacion = DateTime.Now;
            db.tbl_Cliente.Add(tbl_Cliente);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Cliente.id_Cliente }, tbl_Cliente);
        }

        // DELETE: api/tblCliente/5
        [ResponseType(typeof(tbl_Cliente))]
        public async Task<IHttpActionResult> Deletetbl_Cliente(int id)
        {
            tbl_Cliente obj_entidad = await db.tbl_Cliente.FindAsync(id);

            obj_entidad = db.tbl_Cliente.Where(g => g.id_Cliente == id).FirstOrDefault<tbl_Cliente>();
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

        private bool tbl_ClienteExists(int id)
        {
            return db.tbl_Cliente.Count(e => e.id_Cliente == id) > 0;
        }
    }
}