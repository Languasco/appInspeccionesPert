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
    public class tblCargo_PersonalController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblCargo_Personal
        public IQueryable<tbl_Cargo_Personal> Gettbl_Cargo_Personal()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_Cargo_Personal;
        }

        public object Gettbl_Cargo_Personal(int opcion, string filtro)
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
                    //string id_Empresas = (Convert.ToInt32(parametros[2].ToString()) == 0) ? "" : parametros[2].ToString();
                    //string id_Delegacion = (Convert.ToInt32(parametros[3].ToString()) == 0) ? "" : parametros[3].ToString();
                    int id_grupo = Convert.ToInt32(parametros[1].ToString());

                    resul = (from a in db.tbl_Cargo_Personal
                             join d in db.tbl_grupo on a.id_grupo equals d.id_grupo
                             where a.id_grupo == id_grupo
                             select new
                             {
                                d.id_pais,
                                a.id_grupo,
                                a.id_delegacion,
                                a.id_Cargo,
                                a.nombre_cargo,
                                nombre_cargo_Espania =a.nombre_cargo_España,
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

                    string nombre_cargo = parametros[0].ToString();
                    int id_grupo = Convert.ToInt32(parametros[1].ToString());

                    resul = db.tbl_Cargo_Personal.Count(e => e.nombre_cargo == nombre_cargo && e.id_grupo  == id_grupo); 
                }
                else if (opcion == 3)
                {

                    string[] parametros = filtro.Split('|');
                    int id_grupo = Convert.ToInt32(parametros[0].ToString());

                    resul = (from a in db.tbl_Cargo_Personal
                             where a.id_grupo == id_grupo && a.estado ==1
                             select new
                             {
                                 a.id_Cargo,
                                 a.nombre_cargo
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

        // PUT: api/tblCargo_Personal/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Cargo_Personal(int id, tbl_Cargo_Personal obj_entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj_entidad.id_Cargo)
            {
                return BadRequest();
            }

            tbl_Cargo_Personal Ent_CargoR;
            // DATA ACTUAL
            Ent_CargoR = db.tbl_Cargo_Personal.Where(g => g.id_Cargo == obj_entidad.id_Cargo).FirstOrDefault<tbl_Cargo_Personal>();

            Ent_CargoR.id_delegacion  = obj_entidad.id_delegacion ;
            Ent_CargoR.nombre_cargo_España = obj_entidad.nombre_cargo_España;
            Ent_CargoR.nombre_cargo  = obj_entidad.nombre_cargo;
            Ent_CargoR.estado = obj_entidad.estado;
            Ent_CargoR.usuario_edicion = obj_entidad.usuario_creacion;
            Ent_CargoR.fecha_edicion = DateTime.Now;
            db.Entry(Ent_CargoR).State = EntityState.Modified;
 

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_Cargo_PersonalExists(id))
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

        // POST: api/tblCargo_Personal
        [ResponseType(typeof(tbl_Cargo_Personal))]
        public IHttpActionResult Posttbl_Cargo_Personal(tbl_Cargo_Personal tbl_Cargo_Personal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_Cargo_Personal.fecha_creacion = DateTime.Now;
            db.tbl_Cargo_Personal.Add(tbl_Cargo_Personal);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Cargo_Personal.id_Cargo }, tbl_Cargo_Personal);
        }

        // DELETE: api/tblCargo_Personal/5

        [ResponseType(typeof(tbl_Cargo_Personal))]
        public async Task<IHttpActionResult> Deletetbl_Cargo_Personal(int id)
        {
            tbl_Cargo_Personal obj_entidad = await db.tbl_Cargo_Personal.FindAsync(id);

            obj_entidad = db.tbl_Cargo_Personal.Where(g => g.id_Cargo == id).FirstOrDefault<tbl_Cargo_Personal>();
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

        private bool tbl_Cargo_PersonalExists(int id)
        {
            return db.tbl_Cargo_Personal.Count(e => e.id_Cargo == id) > 0;
        }
    }
}