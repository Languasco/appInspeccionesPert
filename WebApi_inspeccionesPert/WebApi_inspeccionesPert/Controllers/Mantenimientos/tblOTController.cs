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
    public class tblOTController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblOT
        public IQueryable<tbl_OT> Gettbl_OT()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_OT;
        }


        public object Gettbl_OT(int opcion, string filtro)
        {
            db.Configuration.ProxyCreationEnabled = false;

            object resul = null;
            try
            {
                if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|');
                    int pais = Convert.ToInt32(parametros[0].ToString());
                    int grupo = Convert.ToInt32(parametros[1].ToString());
                    int delegacion = Convert.ToInt32(parametros[2].ToString());

                    resul = (from a in db.tbl_OT
                             join b in db.tbl_Delegacion on a.id_delegacion equals b.id_Delegacion
                             join c in db.tbl_Actividad on a.id_Actividad equals c.id_Actividad
                             where a.id_Pais == pais && a.id_grupo == grupo && a.id_delegacion == delegacion && b.estado == 1 && c.estado == 1
                             select new
                             {
                                 a.id_OT,
                                 a.codigo_ot,
                                 a.nombre_ot,
                                 a.Tipo_OT,
                                 a.id_Proyecto,
                                 a.id_Cliente,
                                 a.id_delegacion,
                                 b.codigo_delegacion,
                                 a.id_grupo,
                                 a.id_Pais,
                                 a.id_Personal_JefeObra,
                                 a.id_Personal_Coordinador,
                                 a.id_Actividad,
                                 c.descripcion_Actividad,
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
                    string codigoOt = parametros[0].ToString();

                    resul = db.tbl_OT.Count(e => e.codigo_ot == codigoOt);
                }
                else if (opcion == 3)
                {
                    string[] parametros = filtro.Split('|');
                    int pais = Convert.ToInt32(parametros[0].ToString());
                    int grupo = Convert.ToInt32(parametros[1].ToString());
                    int delegacion = Convert.ToInt32(parametros[2].ToString());

                    resul =( from a in db.tbl_OT
                             where a.id_Pais== pais && a.id_grupo == grupo && a.id_delegacion == delegacion && a.estado==1
                             select new {
                                 a.id_OT,
                                 a.nombre_ot
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


        // PUT: api/tblOT/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_OT(int id, tbl_OT obj_entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj_entidad.id_OT)
            {
                return BadRequest();
            }

            tbl_OT Ent_OtR;
            Ent_OtR = db.tbl_OT.Where(g => g.id_OT  == obj_entidad.id_OT).FirstOrDefault<tbl_OT>();
                       
            Ent_OtR.id_OT= obj_entidad.id_OT;
            Ent_OtR.codigo_ot= obj_entidad.codigo_ot;
            Ent_OtR.nombre_ot= obj_entidad.nombre_ot;
            Ent_OtR.Tipo_OT= obj_entidad.Tipo_OT;
            Ent_OtR.id_Proyecto= obj_entidad.id_Proyecto;
            Ent_OtR.id_Cliente= obj_entidad.id_Cliente;
            Ent_OtR.id_delegacion= obj_entidad.id_delegacion;

            Ent_OtR.id_grupo= obj_entidad.id_grupo;
            Ent_OtR.id_Pais= obj_entidad.id_Pais;
            Ent_OtR.id_Personal_JefeObra= obj_entidad.id_Personal_JefeObra;
            Ent_OtR.id_Personal_Coordinador= obj_entidad.id_Personal_Coordinador;

            Ent_OtR.id_Actividad= obj_entidad.id_Actividad;
            Ent_OtR.estado= obj_entidad.estado;
            Ent_OtR.fecha_edicion = DateTime.Now;


            db.Entry(Ent_OtR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_OTExists(id))
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

        // POST: api/tblOT
        [ResponseType(typeof(tbl_OT))]
        public IHttpActionResult Posttbl_OT(tbl_OT tbl_OT)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_OT.fecha_creacion = DateTime.Now;
            tbl_OT.fecha_edicion = DateTime.Now;
            db.tbl_OT.Add(tbl_OT);
            db.SaveChanges();

            try
            {
                Personal_BL obj_negocio = new Personal_BL();
                obj_negocio.Save_Configuracion_Perfil(tbl_OT.usuario_creacion , tbl_OT.id_Pais, tbl_OT.id_grupo, tbl_OT.id_delegacion, tbl_OT.id_OT);
            }
            catch (Exception)
            {

            }
            return CreatedAtRoute("DefaultApi", new { id = tbl_OT.id_OT }, tbl_OT);
        }

        //// DELETE: api/tblOT/5
        //[ResponseType(typeof(tbl_OT))]
        //public IHttpActionResult Deletetbl_OT(int id)
        //{
        //    tbl_OT tbl_OT = db.tbl_OT.Find(id);
        //    if (tbl_OT == null)
        //    {
        //        return NotFound();
        //    }

        //    db.tbl_OT.Remove(tbl_OT);
        //    db.SaveChanges();

        //    return Ok(tbl_OT);
        //}

        [ResponseType(typeof(tbl_OT))]
        public async Task<IHttpActionResult> Deletetbl_usuarios(int id)
        {
            tbl_OT obj_entidad = await db.tbl_OT.FindAsync(id);

            obj_entidad = db.tbl_OT.Where(g => g.id_OT == id).FirstOrDefault<tbl_OT>();
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

        private bool tbl_OTExists(int id)
        {
            return db.tbl_OT.Count(e => e.id_OT == id) > 0;
        }
    }
}