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
using Negocios.Mantenimiento;

namespace WebApi_inspeccionesPert.Controllers.Mantenimientos
{
     [EnableCors("*", "*", "*")]
    public class tblPerfilController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblPerfil
        public IQueryable<tbl_Perfil> Gettbl_Perfil()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_Perfil;
        }

        //// GET: api/tblPerfil/5
        //[ResponseType(typeof(tbl_Perfil))]
        //public IHttpActionResult Gettbl_Perfil(int id)
        //{
        //    db.Configuration.ProxyCreationEnabled = false;
        //    tbl_Perfil tbl_Perfil = db.tbl_Perfil.Find(id);
        //    if (tbl_Perfil == null)
        //    {
        //        return NotFound();
        //    }

        //    return Ok(tbl_Perfil);
        //}


        public object Gettbl_Perfil(int opcion, string filtro)
        {
            db.Configuration.ProxyCreationEnabled = false;
            object resul = null;
            try
            {
               if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|');
                    int idusuario = Convert.ToInt32(parametros[0].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Listando_Perfil_Usuario(idusuario);
 
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


        // PUT: api/tblPerfil/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Perfil(int id, tbl_Perfil tbl_Perfil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Perfil.id_perfil)
            {
                return BadRequest();
            }

            db.Entry(tbl_Perfil).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_PerfilExists(id))
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

        // POST: api/tblPerfil
        [ResponseType(typeof(tbl_Perfil))]
        public IHttpActionResult Posttbl_Perfil(tbl_Perfil tbl_Perfil)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Perfil.Add(tbl_Perfil);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Perfil.id_perfil }, tbl_Perfil);
        }

        // DELETE: api/tblPerfil/5
        [ResponseType(typeof(tbl_Perfil))]
        public IHttpActionResult Deletetbl_Perfil(int id)
        {
            tbl_Perfil tbl_Perfil = db.tbl_Perfil.Find(id);
            if (tbl_Perfil == null)
            {
                return NotFound();
            }

            db.tbl_Perfil.Remove(tbl_Perfil);
            db.SaveChanges();

            return Ok(tbl_Perfil);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_PerfilExists(int id)
        {
            return db.tbl_Perfil.Count(e => e.id_perfil == id) > 0;
        }
    }
}