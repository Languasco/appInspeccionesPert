﻿using System;
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

namespace WebApi_inspeccionesPert.Controllers.Mantenimientos
{
    [EnableCors("*", "*", "*")]
    public class tblDefinicion_OpcionesController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblDefinicion_Opciones
        public IQueryable<tbl_Definicion_Opciones> Gettbl_Definicion_Opciones()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_Definicion_Opciones;
        }

        // GET: api/tblDefinicion_Opciones/5
        [ResponseType(typeof(tbl_Definicion_Opciones))]
        public IHttpActionResult Gettbl_Definicion_Opciones(int id)
        {
            tbl_Definicion_Opciones tbl_Definicion_Opciones = db.tbl_Definicion_Opciones.Find(id);
            db.Configuration.ProxyCreationEnabled = false;
            if (tbl_Definicion_Opciones == null)
            {
                return NotFound();
            }

            return Ok(tbl_Definicion_Opciones);
        }

        // PUT: api/tblDefinicion_Opciones/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Definicion_Opciones(int id, tbl_Definicion_Opciones tbl_Definicion_Opciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != tbl_Definicion_Opciones.id_opcion)
            {
                return BadRequest();
            }

            db.Entry(tbl_Definicion_Opciones).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_Definicion_OpcionesExists(id))
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

        // POST: api/tblDefinicion_Opciones
        [ResponseType(typeof(tbl_Definicion_Opciones))]
        public IHttpActionResult Posttbl_Definicion_Opciones(tbl_Definicion_Opciones tbl_Definicion_Opciones)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            db.tbl_Definicion_Opciones.Add(tbl_Definicion_Opciones);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Definicion_Opciones.id_opcion }, tbl_Definicion_Opciones);
        }

        // DELETE: api/tblDefinicion_Opciones/5
        [ResponseType(typeof(tbl_Definicion_Opciones))]
        public IHttpActionResult Deletetbl_Definicion_Opciones(int id)
        {
            tbl_Definicion_Opciones tbl_Definicion_Opciones = db.tbl_Definicion_Opciones.Find(id);
            if (tbl_Definicion_Opciones == null)
            {
                return NotFound();
            }

            db.tbl_Definicion_Opciones.Remove(tbl_Definicion_Opciones);
            db.SaveChanges();

            return Ok(tbl_Definicion_Opciones);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_Definicion_OpcionesExists(int id)
        {
            return db.tbl_Definicion_Opciones.Count(e => e.id_opcion == id) > 0;
        }
    }
}