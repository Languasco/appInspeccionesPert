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
using Negocios.Mantenimiento;

namespace WebApi_inspeccionesPert.Controllers.Mantenimientos
{
    [EnableCors("*", "*", "*")]
    public class tblUsuariosRegistradosController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblUsuariosRegistrados
        public IQueryable<tbl_usuarios_Registrados> Gettbl_usuarios_Registrados()
        {
            return db.tbl_usuarios_Registrados;
        }

        // GET: api/tblUsuariosRegistrados/5
        [ResponseType(typeof(tbl_usuarios_Registrados))]
        public async Task<IHttpActionResult> Gettbl_usuarios_Registrados(int id)
        {
            tbl_usuarios_Registrados tbl_usuarios_Registrados = await db.tbl_usuarios_Registrados.FindAsync(id);
            if (tbl_usuarios_Registrados == null)
            {
                return NotFound();
            }

            return Ok(tbl_usuarios_Registrados);
        }

        public object GetTbl_Usuarios_Registrado(int opcion, string filtro)
        {
            db.Configuration.ProxyCreationEnabled = false;
            object result = null;
            try
            {
                if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|');
                    int id_Pais = Convert.ToInt32(parametros[0].ToString());
                    int id_Delegacion = Convert.ToInt32(parametros[1].ToString());
                    int estado = Convert.ToInt32(parametros[2].ToString());


                    Usuario_BL content = new Usuario_BL();

                    result = content.GetList_Usuario_Registrado(id_Pais, id_Delegacion, estado);

                }
                else if(opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    int id_registro = Convert.ToInt32(parametros[0].ToString());
                    int id_personal = Convert.ToInt32(parametros[1].ToString());
                    int id_usuario = Convert.ToInt32(parametros[2].ToString());
                    int estado = Convert.ToInt32(parametros[3].ToString());

                    Usuario_BL content = new Usuario_BL();

                    result = content.Aceptar_Rechazar_Usuarios(id_registro,id_personal, id_usuario, estado);
                }
                else
                {
                    result = "Opcion selecciona invalida";
                }
            }
            catch (Exception ex)
            {
                result = ex.Message;
            }
            return result;
        }

        // PUT: api/tblUsuariosRegistrados/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttbl_usuarios_Registrados(int id, tbl_usuarios_Registrados tbl_usuarios_Registrados)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_usuarios_Registrados.id_usuario_Registrado)
            {
                return BadRequest();
            }

            db.Entry(tbl_usuarios_Registrados).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_usuarios_RegistradosExists(id))
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

        // POST: api/tblUsuariosRegistrados
        [ResponseType(typeof(tbl_usuarios_Registrados))]
        public async Task<IHttpActionResult> Posttbl_usuarios_Registrados(tbl_usuarios_Registrados tbl_usuarios_Registrados)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_usuarios_Registrados.Add(tbl_usuarios_Registrados);

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateException)
            {
                if (tbl_usuarios_RegistradosExists(tbl_usuarios_Registrados.id_usuario_Registrado))
                {
                    return Conflict();
                }
                else
                {
                    throw;
                }
            }

            return CreatedAtRoute("DefaultApi", new { id = tbl_usuarios_Registrados.id_usuario_Registrado }, tbl_usuarios_Registrados);
        }

        // DELETE: api/tblUsuariosRegistrados/5
        [ResponseType(typeof(tbl_usuarios_Registrados))]
        public async Task<IHttpActionResult> Deletetbl_usuarios_Registrados(int id)
        {
            tbl_usuarios_Registrados tbl_usuarios_Registrados = await db.tbl_usuarios_Registrados.FindAsync(id);
            if (tbl_usuarios_Registrados == null)
            {
                return NotFound();
            }

            db.tbl_usuarios_Registrados.Remove(tbl_usuarios_Registrados);
            await db.SaveChangesAsync();

            return Ok(tbl_usuarios_Registrados);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_usuarios_RegistradosExists(int id)
        {
            return db.tbl_usuarios_Registrados.Count(e => e.id_usuario_Registrado == id) > 0;
        }
    }
}