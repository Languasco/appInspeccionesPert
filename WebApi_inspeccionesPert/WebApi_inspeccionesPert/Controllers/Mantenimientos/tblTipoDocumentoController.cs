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
    public class TblTipoDocumentoController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblTipoDocumento
        public IQueryable<tbl_Tipo_Documento> Gettbl_Tipo_Documento()
        {
            return db.tbl_Tipo_Documento;
        }

        // GET: api/tblTipoDocumento/5
        [ResponseType(typeof(tbl_Tipo_Documento))]
        public async Task<IHttpActionResult> Gettbl_Tipo_Documento(int id)
        {
            tbl_Tipo_Documento tbl_Tipo_Documento = await db.tbl_Tipo_Documento.FindAsync(id);
            if (tbl_Tipo_Documento == null)
            {
                return NotFound();
            }

            return Ok(tbl_Tipo_Documento);
        }

        // PUT: api/tblTipoDocumento/5
        [ResponseType(typeof(void))]
        public async Task<IHttpActionResult> Puttbl_Tipo_Documento(int id, tbl_Tipo_Documento tbl_Tipo_Documento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Tipo_Documento.id_Tipo_Documento)
            {
                return BadRequest();
            }

            db.Entry(tbl_Tipo_Documento).State = EntityState.Modified;

            try
            {
                await db.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_Tipo_DocumentoExists(id))
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

        // POST: api/tblTipoDocumento
        [ResponseType(typeof(tbl_Tipo_Documento))]
        public async Task<IHttpActionResult> Posttbl_Tipo_Documento(tbl_Tipo_Documento tbl_Tipo_Documento)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Tipo_Documento.Add(tbl_Tipo_Documento);
            await db.SaveChangesAsync();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Tipo_Documento.id_Tipo_Documento }, tbl_Tipo_Documento);
        }

        // DELETE: api/tblTipoDocumento/5
        [ResponseType(typeof(tbl_Tipo_Documento))]
        public async Task<IHttpActionResult> Deletetbl_Tipo_Documento(int id)
        {
            tbl_Tipo_Documento tbl_Tipo_Documento = await db.tbl_Tipo_Documento.FindAsync(id);
            if (tbl_Tipo_Documento == null)
            {
                return NotFound();
            }

            db.tbl_Tipo_Documento.Remove(tbl_Tipo_Documento);
            await db.SaveChangesAsync();

            return Ok(tbl_Tipo_Documento);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_Tipo_DocumentoExists(int id)
        {
            return db.tbl_Tipo_Documento.Count(e => e.id_Tipo_Documento == id) > 0;
        }
    }
}