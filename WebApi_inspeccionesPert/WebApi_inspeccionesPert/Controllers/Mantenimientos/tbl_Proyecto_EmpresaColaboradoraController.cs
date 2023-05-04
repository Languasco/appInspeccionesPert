using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Entidades;

namespace WebApi_inspeccionesPert.Controllers.Mantenimientos
{
    [EnableCors("*", "*", "*")]
    public class tbl_Proyecto_EmpresaColaboradoraController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tbl_Proyecto_EmpresaColaboradora
        public IQueryable<tbl_Proyecto_EmpresaColaboradora> Gettbl_Proyecto_EmpresaColaboradora()
        {
            return db.tbl_Proyecto_EmpresaColaboradora;
        }

        // GET: api/tbl_Proyecto_EmpresaColaboradora/5
        [ResponseType(typeof(tbl_Proyecto_EmpresaColaboradora))]
        public async Task<IHttpActionResult> Gettbl_Proyecto_EmpresaColaboradora(int id)
        {
            tbl_Proyecto_EmpresaColaboradora tbl_Proyecto_EmpresaColaboradora = await db.tbl_Proyecto_EmpresaColaboradora.FindAsync(id);
            if (tbl_Proyecto_EmpresaColaboradora == null)
            {
                return NotFound();
            }

            return Ok(tbl_Proyecto_EmpresaColaboradora);
        }

        public object Gettbl_Proyecto_EmpresaColaboradora(int opcion, string filtro)
        {
            db.Configuration.ProxyCreationEnabled = false;

            object resul = null;
            try
            {
                if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|');
                    int id_Proyecto = Convert.ToInt32(parametros[0].ToString());

                    resul = (from a in db.tbl_Proyecto_EmpresaColaboradora
                             join b in db.tbl_EmpresaColaboradora on a.id_EmpresaColaboradora equals b.id_EmpresaColaboradora
                             where a.id_Proyecto == id_Proyecto
                             select new
                             {
                                a.id_Proyecto_EmpresaColaboradora,
                                a.id_Proyecto,
                                a.id_EmpresaColaboradora,
                                b.ruc_EmpresaColaboradora,
                                b.RazonSocial_EmpresaColaboradora,
                                a.estado
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

        // PUT: api/tbl_Proyecto_EmpresaColaboradora/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttbl_Proyecto_EmpresaColaboradora(int id, tbl_Proyecto_EmpresaColaboradora tbl_Proyecto_EmpresaColaboradora)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Proyecto_EmpresaColaboradora.id_Proyecto_EmpresaColaboradora)
            {
                return BadRequest();
            }

            db.Entry(tbl_Proyecto_EmpresaColaboradora).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_Proyecto_EmpresaColaboradoraExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return StatusCode(HttpStatusCode.NoContent);
        }

        // POST: api/tbl_Proyecto_EmpresaColaboradora
        [ResponseType(typeof(tbl_Proyecto_EmpresaColaboradora))]
        public async Task<IHttpActionResult> Posttbl_Proyecto_EmpresaColaboradora(tbl_Proyecto_EmpresaColaboradora tbl_Proyecto_EmpresaColaboradora)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Proyecto_EmpresaColaboradora.Add(tbl_Proyecto_EmpresaColaboradora);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Proyecto_EmpresaColaboradora.id_Proyecto_EmpresaColaboradora }, tbl_Proyecto_EmpresaColaboradora);
        }

        // DELETE: api/tbl_Proyecto_EmpresaColaboradora/5
        [ResponseType(typeof(tbl_Proyecto_EmpresaColaboradora))]
        public async Task<IHttpActionResult> Deletetbl_Proyecto_EmpresaColaboradora(int id)
        {
            tbl_Proyecto_EmpresaColaboradora tbl_Proyecto_EmpresaColaboradora = await db.tbl_Proyecto_EmpresaColaboradora.FindAsync(id);

            tbl_Proyecto_EmpresaColaboradora = db.tbl_Proyecto_EmpresaColaboradora.Where(g => g.id_Proyecto_EmpresaColaboradora == id).FirstOrDefault<tbl_Proyecto_EmpresaColaboradora>();
            tbl_Proyecto_EmpresaColaboradora.estado = 0;
            db.Entry(tbl_Proyecto_EmpresaColaboradora).State = EntityState.Modified;
            await db.SaveChangesAsync();

            return Ok("OK");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_Proyecto_EmpresaColaboradoraExists(int id)
        {
            return db.tbl_Proyecto_EmpresaColaboradora.Count(e => e.id_Proyecto_EmpresaColaboradora == id) > 0;
        }
    }
}