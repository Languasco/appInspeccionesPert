using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;
using Entidades;

namespace WebApi_inspeccionesPert.Controllers.Mantenimientos
{
    [EnableCors("*", "*", "*")]
    public class tblAnomalia_SMSController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblAnomalia_SMS
        public IQueryable<tbl_Anomalia_SMS> Gettbl_Anomalia_SMS()
        {
            return db.tbl_Anomalia_SMS;
        }

        // GET: api/tblAnomalia_SMS/5
        [ResponseType(typeof(tbl_Anomalia_SMS))]
        public IHttpActionResult Gettbl_Anomalia_SMS(int id)
        {
            tbl_Anomalia_SMS tbl_Anomalia_SMS = db.tbl_Anomalia_SMS.Find(id);
            if (tbl_Anomalia_SMS == null)
            {
                return NotFound();
            }

            return Ok(tbl_Anomalia_SMS);
        }

        // PUT: api/tblAnomalia_SMS/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Anomalia_SMS(int id, tbl_Anomalia_SMS tbl_Anomalia_SMS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Anomalia_SMS.id_anomalia_sms)
            {
                return BadRequest();
            }

            db.Entry(tbl_Anomalia_SMS).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_Anomalia_SMSExists(id))
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

        // POST: api/tblAnomalia_SMS
        [ResponseType(typeof(tbl_Anomalia_SMS))]
        public IHttpActionResult Posttbl_Anomalia_SMS(tbl_Anomalia_SMS tbl_Anomalia_SMS)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Anomalia_SMS.Add(tbl_Anomalia_SMS);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Anomalia_SMS.id_anomalia_sms }, tbl_Anomalia_SMS);
        }

        // DELETE: api/tblAnomalia_SMS/5
        [ResponseType(typeof(tbl_Anomalia_SMS))]
        public IHttpActionResult Deletetbl_Anomalia_SMS(int id)
        {
            tbl_Anomalia_SMS tbl_Anomalia_SMS = db.tbl_Anomalia_SMS.Find(id);
            if (tbl_Anomalia_SMS == null)
            {
                return NotFound();
            }

            db.tbl_Anomalia_SMS.Remove(tbl_Anomalia_SMS);
            db.SaveChanges();

            return Ok(tbl_Anomalia_SMS);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_Anomalia_SMSExists(int id)
        {
            return db.tbl_Anomalia_SMS.Count(e => e.id_anomalia_sms == id) > 0;
        }
    }
}