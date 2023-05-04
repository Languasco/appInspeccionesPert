
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
using System.IO;
using Negocios.Procesos;

namespace WebApi_inspeccionesPert.Controllers.Movil
{
    [EnableCors("*", "*", "*")]
    public class tblPersonalController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblPersonal
        public IQueryable<tbl_Personal> Gettbl_Personal()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_Personal;
            //return db.tbl_Personal.Where(p => p.estado == 0);
        }

        public object Gettbl_Personal(int opcion, string filtro)
        {
            db.Configuration.ProxyCreationEnabled = false;

            object resul = null;
            try
            {
                if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|');

                    int id_personal = Convert.ToInt32(parametros[0].ToString());
                    int id_empresa = Convert.ToInt32(parametros[1].ToString());

                    Personal_BL obj_negocio = new Personal_BL();
                    resul = obj_negocio.Listando_Personal_DelegacionEmpresa(id_personal, id_empresa);
                }
                else if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');

                    int id_personal = Convert.ToInt32(parametros[0].ToString());
                    string id_delegacion = parametros[1].ToString();
                    int id_empresa = Convert.ToInt32(parametros[2].ToString());

                    Personal_BL obj_negocio = new Personal_BL();
                    resul = obj_negocio.Guardando_PersonalDelegacion(id_personal, id_delegacion, id_empresa);
                }
                else if (opcion == 3)
                {
                    string[] parametros = filtro.Split('|');
                    int id_pais = Convert.ToInt32(parametros[0].ToString());
                    int id_grupo = Convert.ToInt32(parametros[1].ToString());
                    int id_Delegacion = Convert.ToInt32(parametros[2].ToString());
                    int estado  = Convert.ToInt32(parametros[3].ToString());

                    if (estado == -1)
                    {
                        resul = (from a in db.tbl_Personal
                                 join b in db.tbl_Delegacion on a.id_Delegacion equals b.id_Delegacion
                                 join d in db.tbl_grupo on b.id_grupo equals d.id_grupo
                                 join e in db.tbl_Cargo_Personal on a.id_Cargo equals e.id_Cargo
                                 where a.id_Pais == id_pais &&  a.id_grupo == id_grupo && a.id_Delegacion == id_Delegacion
                                 select new
                                 {
                                     d.id_pais,
                                     b.id_grupo,
                                     a.id_Personal,
                                     a.id_Empresa,
                                     a.id_Delegacion,
                                     b.nombre_delegacion,
                                     a.id_Proyecto,
                                     a.tipoDoc_Personal,
                                     a.nroDoc_Personal,
                                     a.apellidos_Personal,
                                     a.nombres_Personal,
                                     a.fechaIngreso_Personal,
                                     a.id_Cargo,
                                     e.nombre_cargo,
                                     a.tipoPersonal,
                                     a.fechaCese_Personal,
                                     a.email_personal,
                                     a.login_Sistema,
                                     a.Contrasenia_sistema,
                                     a.id_Perfil,
                                     a.envio_Online,
                                     a.estado,
                                     a.usuario_creacion,
                                     a.fecha_creacion,
                                     a.usuario_edicion,
                                     a.fecha_edicion,
                                     a.id_EmpresaColaboradora
                                 }).ToList();
                    }
                    else {

                        resul = (from a in db.tbl_Personal
                                 join b in db.tbl_Delegacion on a.id_Delegacion equals b.id_Delegacion
                                 join d in db.tbl_grupo on b.id_grupo equals d.id_grupo
                                 join e in db.tbl_Cargo_Personal on a.id_Cargo equals e.id_Cargo
                                 where a.id_Pais == id_pais && a.id_grupo == id_grupo && a.id_Delegacion == id_Delegacion && a.estado == estado
                                 select new
                                 {
                                     d.id_pais,
                                     b.id_grupo,
                                     a.id_Personal,
                                     a.id_Empresa,
                                     a.id_Delegacion,
                                     b.nombre_delegacion,
                                     a.id_Proyecto,
                                     a.tipoDoc_Personal,
                                     a.nroDoc_Personal,
                                     a.apellidos_Personal,
                                     a.nombres_Personal,
                                     a.fechaIngreso_Personal,
                                     a.id_Cargo,
                                     e.nombre_cargo,
                                     a.tipoPersonal,
                                     a.fechaCese_Personal,
                                     a.email_personal,
                                     a.login_Sistema,
                                     a.Contrasenia_sistema,
                                     a.id_Perfil,
                                     a.envio_Online,
                                     a.estado,
                                     a.usuario_creacion,
                                     a.fecha_creacion,
                                     a.usuario_edicion,
                                     a.fecha_edicion,
                                     a.id_EmpresaColaboradora
                                 }).ToList();
                    } 
                }

                else if (opcion == 4)
                {
                    string[] parametros = filtro.Split('|');

                    int id_pais = Convert.ToInt32(parametros[0].ToString());
                    int id_grupo = Convert.ToInt32(parametros[1].ToString());
                    int id_Delegacion = Convert.ToInt32(parametros[2].ToString());
                    
                    resul = (from a in db.tbl_Personal
                             join b in db.tbl_Cargo_Personal on a.id_Cargo equals b.id_Cargo 
                             where a.id_Pais == id_pais && a.id_grupo == id_grupo && a.id_Delegacion == id_Delegacion && a.estado ==1
                             select new
                             {
                               a.id_Personal,
                               a.apellidos_Personal,
                               a.nombres_Personal,
                               a.id_Cargo,
                               b.nombre_cargo,
                             }).ToList();
                }
                else if (opcion == 5)
                {
                    string[] parametros = filtro.Split('|');

                    int id_pais = Convert.ToInt32(parametros[0].ToString());
                    int id_grupo = Convert.ToInt32(parametros[1].ToString());
                    string obj_id_Delegacion = parametros[2].ToString();
                    int id_usuario = Convert.ToInt32(parametros[3].ToString());

                    Personal_BL obj_negocio = new Personal_BL();
                    resul = obj_negocio.Listando_Personal_Pais_Grupo_Empresa_Delegacion_new(id_pais, id_grupo, obj_id_Delegacion, id_usuario);
                }
                else if (opcion == 6)
                {
                    string[] parametros = filtro.Split('|');

                    int idpersonal = Convert.ToInt32(parametros[0].ToString());
                    int idopcion = Convert.ToInt32(parametros[1].ToString());
        
                    Personal_BL obj_negocio = new Personal_BL();
                    resul = obj_negocio.Delete_Acceso(idpersonal, idopcion);
                }
                else if (opcion == 7)
                {
                    string[] parametros = filtro.Split('|');

                    int idpersonal = Convert.ToInt32(parametros[0].ToString());
                    int idopcion = Convert.ToInt32(parametros[1].ToString());

                    Personal_BL obj_negocio = new Personal_BL();
                    resul = obj_negocio.Save_Acceso(idpersonal, idopcion);
                }
                else if (opcion == 8)
                {
                    string[] parametros = filtro.Split('|');

                    int id_pais = Convert.ToInt32(parametros[0].ToString());
                    int id_grupo = Convert.ToInt32(parametros[1].ToString());
                    string search = parametros[2].ToString();

                    Personal_BL obj_negocio = new Personal_BL();
                    resul = obj_negocio.Listando_Personal_Representante(id_pais, id_grupo, search);
                }
                //trae a los jefes con el filtro de delegacion
                else if (opcion == 9)
                {
                    string[] parametros = filtro.Split('|');

                    int id_delegacion = Convert.ToInt32(parametros[0].ToString());
                    Personal_BL obj_user = new Personal_BL();
                    resul = obj_user.GetListPersonal_Jefe_Proyecto(id_delegacion);
                }
                else if (opcion == 10) ///------validacion Reniec en mantenimiento de personal
                {
                    string[] parametros = filtro.Split('|');
                    string nroDoc = parametros[0].ToString();

                    string resultado = "";
                    var url = "http://aplicaciones007.jne.gob.pe/srop_publico/Consulta/Afiliado/GetNombresCiudadano?DNI=" + nroDoc;
                    var web_request = System.Net.WebRequest.Create(url);

                    using (var response = web_request.GetResponse())
                    {
                        using (var reader = new StreamReader(response.GetResponseStream()))
                        {
                            resultado = reader.ReadToEnd().ToString();
                        }
                    }
                    resul = resultado;
                }
                else if(opcion == 11)
                {
                    string[] parametros = filtro.Split('|');

                    string tipo_Doc = parametros[0].ToString();
                    string Nro_Doc = parametros[1].ToString();

                    resul = (from a in db.tbl_Personal
                             join b in db.tbl_pais  on a.id_Pais equals b.id_pais
                             where a.tipoDoc_Personal == tipo_Doc && a.nroDoc_Personal == Nro_Doc && a.estado == 1
                             select new
                             {
                                 a.id_Personal,
                                 a.nroDoc_Personal,
                                 a.apellidos_Personal,
                                 a.nombres_Personal,
                                 a.email_personal,
                                 a.id_Cargo,
                                 b.iso

                             }).ToList();
                }
                else if (opcion == 12)
                {
                    string[] parametros = filtro.Split('|');

                    string nombre = parametros[0].ToString();
                    string dominio = parametros[1].ToString();

                    Personal_BL obj_user = new Personal_BL();
                    resul = obj_user.Consultando_direccion_electronica(nombre, dominio); 
                }
                else if (opcion == 13)
                {
                    string[] parametros = filtro.Split('|');
                    string nroDocumento = parametros[0].ToString();

                    resul = db.tbl_Personal.Count(e => e.nroDoc_Personal == nroDocumento);
                }
                else if (opcion == 14)
                {
                    string[] parametros = filtro.Split('|');
                    int id_usuario =Convert.ToInt32(parametros[0].ToString());

                    ImportarArchivo_BL obj_user = new ImportarArchivo_BL();
                    resul = obj_user.ListaAgrupadoTemporal_Alerta(id_usuario);
                }
                else if (opcion == 15)
                {
                    string[] parametros = filtro.Split('|');
                    int id_pais = Convert.ToInt32(parametros[0].ToString());
                    int id_grupo = Convert.ToInt32(parametros[1].ToString());
                    int id_Delegacion = Convert.ToInt32(parametros[2].ToString());
                    int estado = Convert.ToInt32(parametros[3].ToString());

                    Personal_BL obj_user = new Personal_BL();
                    resul = obj_user.get_listado_personal(id_pais, id_grupo, id_Delegacion, estado);

                }
                else if (opcion == 16)
                {
                    string[] parametros = filtro.Split('|');
                    int id_personal = Convert.ToInt32(parametros[0].ToString());
                    int id_estado = Convert.ToInt32(parametros[1].ToString()); 

                    Personal_BL obj_user = new Personal_BL();
                    resul = obj_user.Aceptar_Rechazar(id_personal, id_estado);

                }
                else if (opcion == 17)
                {
                    string[] parametros = filtro.Split('|');

                    int id_delegacion = Convert.ToInt32(parametros[0].ToString());
                    Personal_BL obj_user = new Personal_BL();
                    resul = obj_user.get_Cordinador_jefeObra(id_delegacion);
                }
                else if (opcion == 18)
                {
                    string[] parametros = filtro.Split('|');
                    int id_pais = Convert.ToInt32(parametros[0].ToString());
                    int id_grupo = Convert.ToInt32(parametros[1].ToString());
                    int id_Delegacion = Convert.ToInt32(parametros[2].ToString());
                    int estado = Convert.ToInt32(parametros[3].ToString());
                    string consulta_filtro =  parametros[4].ToString();
                    int pageindex =  Convert.ToInt32(parametros[5].ToString());
                    int pagesize = Convert.ToInt32(parametros[6].ToString());

                    Personal_BL obj_user = new Personal_BL();
                    resul = obj_user.get_listado_personal_v2(id_pais, id_grupo, id_Delegacion, estado, consulta_filtro, pageindex, pagesize);

                }
                else if (opcion == 19)
                {
                    string[] parametros = filtro.Split('|');
                    string Nro_Doc = parametros[0].ToString();

                    resul = (from a in db.tbl_Personal
                             join b in db.tbl_pais on a.id_Pais equals b.id_pais
                             where  a.nroDoc_Personal == Nro_Doc && a.estado == 1
                             select new
                             {
                                 a.id_Personal,
                                 a.nroDoc_Personal,
                                 a.apellidos_Personal,
                                 a.nombres_Personal,
                                 a.email_personal,
                                 a.id_Cargo,
                                 b.iso
                             }).ToList();
                }
                else if (opcion == 20)
                {
                    string[] parametros = filtro.Split('|');

                    string nombre = parametros[0].ToString();
                    string dominio = parametros[1].ToString();

                    Personal_BL obj_user = new Personal_BL();
                    resul = obj_user.Consultando_direccion_electronica_usuario(nombre, dominio);
                }
                else if (opcion == 21)
                {
                    string[] parametros = filtro.Split('|');
                    int id_pais = Convert.ToInt32(parametros[0].ToString());
                    string codigo = parametros[1].ToString();

                    resul = db.tbl_Personal.Count(e => e.codigo_personal == codigo && e.id_Pais == id_pais);
                }
                else if (opcion == 22)
                {
                    string[] parametros = filtro.Split('|');
                    int id_pais = Convert.ToInt32(parametros[0].ToString());
                    int id_grupo = Convert.ToInt32(parametros[1].ToString());
                    int id_delegacion = Convert.ToInt32(parametros[2].ToString());

                    resul = (from a in db.tbl_Personal
                             where a.id_Pais  == id_pais && a.id_grupo == id_grupo && a.id_Delegacion  == id_delegacion
                             select new
                             {
                               a.id_Personal,
                               personal =  a.apellidos_Personal
                             }).ToList();
                }
                else if (opcion == 23)
                {
                    string[] parametros = filtro.Split('|');
                    int id_pais = Convert.ToInt32(parametros[0].ToString());
                    int id_grupo = Convert.ToInt32(parametros[1].ToString());
                    int id_Delegacion = Convert.ToInt32(parametros[2].ToString());
                    int estado = Convert.ToInt32(parametros[3].ToString());

                    Personal_BL obj_user = new Personal_BL();
                    resul = obj_user.get_listado_personal_V3(id_pais, id_grupo, id_Delegacion, estado);

                }
                else if (opcion == 24)
                {
                    Personal_BL obj_user = new Personal_BL();
                    resul = obj_user.personalesBandejaAtencion();
                }
                else if (opcion == 25)
                {
                    string[] parametros = filtro.Split('|');

                    int id_pais = Convert.ToInt32(parametros[0].ToString());
                    int id_grupo = Convert.ToInt32(parametros[1].ToString());
                    string obj_id_Delegacion = parametros[2].ToString();
                    int id_usuario = Convert.ToInt32(parametros[3].ToString());

                    Personal_BL obj_negocio = new Personal_BL();
                    resul = obj_negocio.get_Inspector_responsableCorreccion(id_pais, id_grupo, obj_id_Delegacion, id_usuario);
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


        // PUT: api/tblPersonal/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_Personal(int id, tbl_Personal obj_entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj_entidad.id_Personal)
            {
                return BadRequest();
            }


            tbl_Personal Ent_Personal;
            // DATA ACTUAL
            Ent_Personal = db.tbl_Personal.Where(g => g.id_Personal == obj_entidad.id_Personal).FirstOrDefault<tbl_Personal>();

            Ent_Personal.id_EmpresaColaboradora = obj_entidad.id_EmpresaColaboradora;
            //Ent_Personal.id_Empresa = obj_entidad.id_Empresa;
            //Ent_Personal.id_Delegacion = obj_entidad.id_Delegacion;
            Ent_Personal.id_Proyecto = obj_entidad.id_Proyecto;
            Ent_Personal.tipoDoc_Personal = obj_entidad.tipoDoc_Personal;

            Ent_Personal.nroDoc_Personal = obj_entidad.nroDoc_Personal;
            Ent_Personal.apellidos_Personal = obj_entidad.apellidos_Personal;
            Ent_Personal.nombres_Personal = obj_entidad.nombres_Personal;

            Ent_Personal.id_Cargo = obj_entidad.id_Cargo;
            Ent_Personal.tipoPersonal = obj_entidad.tipoPersonal;

            Ent_Personal.email_personal = obj_entidad.email_personal;
            Ent_Personal.login_Sistema = obj_entidad.login_Sistema;
            Ent_Personal.Contrasenia_sistema = obj_entidad.Contrasenia_sistema;

            Ent_Personal.id_Perfil = obj_entidad.id_Perfil;
            Ent_Personal.envio_Online = obj_entidad.envio_Online;
            Ent_Personal.id_Cargo = obj_entidad.id_Cargo;

            Ent_Personal.estado = obj_entidad.estado;
            Ent_Personal.usuario_edicion = obj_entidad.usuario_creacion;
            Ent_Personal.fecha_edicion = DateTime.Now;


            db.Entry(Ent_Personal).State = EntityState.Modified;

            try
            {
                db.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_PersonalExists(id))
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

        // POST: api/tblPersonal
        [ResponseType(typeof(tbl_Personal))]
        public IHttpActionResult Posttbl_Personal(tbl_Personal tbl_Personal)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_Personal.fechaIngreso_Personal = DateTime.Now;
            tbl_Personal.fecha_creacion = DateTime.Now;
            db.tbl_Personal.Add(tbl_Personal);
            db.SaveChanges();

            return CreatedAtRoute("DefaultApi", new { id = tbl_Personal.id_Personal }, tbl_Personal);
        }

        // DELETE: api/tblPersonal/5
        [ResponseType(typeof(tbl_Personal))]
        public IHttpActionResult Deletetbl_Personal(int id)
        {
            tbl_Personal tbl_Personal = db.tbl_Personal.Find(id);
            if (tbl_Personal == null)
            {
                return NotFound();
            }

            db.tbl_Personal.Remove(tbl_Personal);
            db.SaveChanges();

            return Ok(tbl_Personal);
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }

        private bool tbl_PersonalExists(int id)
        {
            return db.tbl_Personal.Count(e => e.id_Personal == id) > 0;
        }
    }
}