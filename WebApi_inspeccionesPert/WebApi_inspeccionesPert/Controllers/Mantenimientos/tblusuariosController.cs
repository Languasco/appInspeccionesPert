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
using Negocios.Mantenimiento;

namespace WebApi_inspeccionesPert.Controllers.Mantenimientos
{
    [EnableCors("*", "*", "*")]
    public class tblusuariosController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        // GET: api/tblusuarios
        public IQueryable<tbl_usuarios> Gettbl_usuarios()
        {
            db.Configuration.ProxyCreationEnabled = false;
            return db.tbl_usuarios;
        }

        public object Gettbl_usuarios(int opcion, string filtro)
        {
            db.Configuration.ProxyCreationEnabled = false;

            object resul = null;
            try
            {
                if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|');
                    int id_usuario = Convert.ToInt32(parametros[0].ToString());
                    int usuario_Loggin = Convert.ToInt32(parametros[1].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Listando_Usuario_Pais(id_usuario, usuario_Loggin); 
                }
                else if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    int id_usuario = Convert.ToInt32(parametros[0].ToString());
                    string codigoPais = parametros[1].ToString();
                    int usuario_Loggin = Convert.ToInt32(parametros[2].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Listando_Usuario_Grupo(id_usuario, codigoPais, usuario_Loggin);
                }
                else if (opcion == 3)
                {
                    string[] parametros = filtro.Split('|');
                    int id_usuario = Convert.ToInt32(parametros[0].ToString());
                    string codigoGrupos = parametros[1].ToString();
                    int usuario_Loggin = Convert.ToInt32(parametros[2].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Listando_Usuario_Empresas(id_usuario, codigoGrupos, usuario_Loggin);
                }
                else if (opcion == 4)
                {
                    string[] parametros = filtro.Split('|');
                    int id_usuario = Convert.ToInt32(parametros[0].ToString());
                    string codigoGrupo = parametros[1].ToString();
                    int usuario_Loggin = Convert.ToInt32(parametros[2].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Listando_Usuario_Delegaciones(id_usuario, codigoGrupo, usuario_Loggin);
                }
                else if (opcion == 5)
                {
                    string[] parametros = filtro.Split('|');
                    int id_usuarioGeneral = Convert.ToInt32(parametros[0].ToString());
                    string codigoPais = parametros[1].ToString();
                    string codigoGrupo = parametros[2].ToString();
                    string codigoEmpresa = parametros[3].ToString();
                    string codigoDelegacion = parametros[4].ToString();
                    int id_usuarioCreacion = Convert.ToInt32(parametros[5].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Guardando_Configuracion_Usuarios(id_usuarioGeneral, codigoPais, codigoGrupo, codigoEmpresa, codigoDelegacion, id_usuarioCreacion);
                }
                else if (opcion == 6)
                {
                    string[] parametros = filtro.Split('|');
                    int usuario_Loggin = Convert.ToInt32(parametros[0].ToString());
                    int id_pais = Convert.ToInt32(parametros[1].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Listando_Usuario_All(usuario_Loggin, id_pais);
                }

                else if (opcion == 7)
                {
                    string[] parametros = filtro.Split('|');
                    string pas_actual = parametros[0].ToString();
                    string pas_nueva = parametros[1].ToString();
                    string pas_nueva_confirma = parametros[2].ToString();
                    int id_Personal = Convert.ToInt32(parametros[3].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Set_generando_Cambio_Contrasenia(pas_actual, pas_nueva, pas_nueva_confirma, id_Personal);
                }
                // nueva funcinalidad del mantenimiento de usuarios
                else if (opcion == 8) 
                {
                    string[] parametros = filtro.Split('|');
                    int id_usuario = Convert.ToInt32(parametros[0].ToString());
                    string codigoPais = parametros[1].ToString();
                    int usuario_Loggin = Convert.ToInt32(parametros[2].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Listando_pais_configuracion(id_usuario, codigoPais, usuario_Loggin);
                }
                else if (opcion == 9)  
                {
                    string[] parametros = filtro.Split('|');
                    int id_usuario = Convert.ToInt32(parametros[0].ToString());
                    string codigoGrupo = parametros[1].ToString();
                    int usuario_Loggin = Convert.ToInt32(parametros[2].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Listando_grupo_configuracion(id_usuario, codigoGrupo, usuario_Loggin);
                }
                else if (opcion == 10)
                {
                    string[] parametros = filtro.Split('|');
                    int id_usuario = Convert.ToInt32(parametros[0].ToString());
                    string codigoEmpresa = parametros[1].ToString();
                    int usuario_Loggin = Convert.ToInt32(parametros[2].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Listando_empresa_configuracion(id_usuario, codigoEmpresa, usuario_Loggin);
                }
                else if (opcion == 11)
                {
                    string[] parametros = filtro.Split('|');
                    int id_usuarioGeneral = Convert.ToInt32(parametros[0].ToString());
                    string codigoPais = parametros[1].ToString();
                    string codigoGrupo = parametros[2].ToString();
                    string codigoDelegacion = parametros[3].ToString();
                    string codigoProyecto = parametros[4].ToString();

                    int id_usuarioCreacion = Convert.ToInt32(parametros[5].ToString());

                    int id_pais_config = Convert.ToInt32(parametros[6].ToString());
                    int id_grupo_config = Convert.ToInt32(parametros[7].ToString());
                    int id_delegacion_config = Convert.ToInt32(parametros[8].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Guardando_Configuracion_Usuarios_V2(id_usuarioGeneral, codigoPais, codigoGrupo, codigoDelegacion, codigoProyecto, id_usuarioCreacion, id_pais_config, id_grupo_config, id_delegacion_config);
                }
                else if (opcion == 12)
                {
                    string[] parametros = filtro.Split('|');
                    string usuario_login = parametros[0].ToString();

                    resul = db.tbl_usuarios.Count(e => e.nro_documento == usuario_login); 
                }
                else if (opcion == 13)
                {
                    string[] parametros = filtro.Split('|');
                    int id_usuario = Convert.ToInt32(parametros[0].ToString());
                    string codigodelegacion = parametros[1].ToString();
                    int usuario_Loggin = Convert.ToInt32(parametros[2].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Listando_Usuario_Proyecto(id_usuario, codigodelegacion, usuario_Loggin);
                }
                else if (opcion == 14)
                {
                    string[] parametros = filtro.Split('|');
                    int id_usuario = Convert.ToInt32(parametros[0].ToString());
                    string codigoDelegacion = parametros[1].ToString();
                    int usuario_Loggin = Convert.ToInt32(parametros[2].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Listando_delegacion_configuracion(id_usuario, codigoDelegacion, usuario_Loggin);
                }
                else if (opcion == 15)
                {
                    string[] parametros = filtro.Split('|');
                    int id_usuario = Convert.ToInt32(parametros[0].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.get_usuarios_masivos_alertas(id_usuario);
                }
                else if (opcion == 16)
                {
                    string[] parametros = filtro.Split('|');
                    int id_usuario = Convert.ToInt32(parametros[0].ToString());
                    string codigodelegacion = parametros[1].ToString();
                    int usuario_Loggin = Convert.ToInt32(parametros[2].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Listando_Usuario_OT(id_usuario, codigodelegacion, usuario_Loggin);
                }
                else if (opcion == 17)
                {
                    string[] parametros = filtro.Split('|');
                    int id_usuarioGeneral = Convert.ToInt32(parametros[0].ToString());
                    string codigoPais = parametros[1].ToString();
                    string codigoGrupo = parametros[2].ToString();
                    string codigoDelegacion = parametros[3].ToString();
                    string codigoOT = parametros[4].ToString();

                    int id_usuarioCreacion = Convert.ToInt32(parametros[5].ToString());

                    int id_pais_config = Convert.ToInt32(parametros[6].ToString());
                    int id_grupo_config = Convert.ToInt32(parametros[7].ToString());
                    int id_delegacion_config = Convert.ToInt32(parametros[8].ToString());
                    string flag_administrador = parametros[9].ToString();

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Guardando_Configuracion_Usuarios_V3(id_usuarioGeneral, codigoPais, codigoGrupo, codigoDelegacion, codigoOT, id_usuarioCreacion, id_pais_config, id_grupo_config, id_delegacion_config, flag_administrador);
                }
                else if (opcion == 18)
                {
                    string[] parametros = filtro.Split('|');
                    int id_pais = Convert.ToInt32(parametros[0].ToString());
                    int id_grupo = Convert.ToInt32(parametros[1].ToString());
                    int id_delegacion = Convert.ToInt32(parametros[2].ToString());

                    Usuario_BL obj_negocio = new Usuario_BL();
                    resul = obj_negocio.Listando_Usuario_Configuracion(id_pais, id_grupo, id_delegacion);
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


        // PUT: api/tblusuarios/5
        [ResponseType(typeof(void))]
        public IHttpActionResult Puttbl_usuarios(int id, tbl_usuarios obj_entidad)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            if (id != obj_entidad.id_usuario)
            {
                return BadRequest();
            }


            int usuario_creacion = Convert.ToInt32(obj_entidad.usuario_creacion);
            int id_usuario = Convert.ToInt32(obj_entidad.id_usuario);
            int id_perfil = Convert.ToInt32(obj_entidad.id_perfil);
            
            tbl_usuarios Ent_UsuarioR;
            Ent_UsuarioR = db.tbl_usuarios.Where(g => g.id_usuario == obj_entidad.id_usuario).FirstOrDefault<tbl_usuarios>();

            Ent_UsuarioR.nro_documento = obj_entidad.nro_documento;
            Ent_UsuarioR.datos_personales = obj_entidad.datos_personales;
            Ent_UsuarioR.correo_electronico = obj_entidad.correo_electronico;
            Ent_UsuarioR.usuario_login = obj_entidad.usuario_login;
            Ent_UsuarioR.contrasenia_login = obj_entidad.contrasenia_login;
            Ent_UsuarioR.id_perfil = obj_entidad.id_perfil;

            Ent_UsuarioR.estado = obj_entidad.estado;
            Ent_UsuarioR.usuario_edicion = obj_entidad.usuario_creacion;
            Ent_UsuarioR.fecha_edicion = DateTime.Now;            
            db.Entry(Ent_UsuarioR).State = EntityState.Modified;

            try
            {
                db.SaveChanges();

                Usuario_BL obj_negocio = new Usuario_BL();
                obj_negocio.Set_Insertando_Usuario_WebAccesos(usuario_creacion, id_usuario, id_perfil);

            }
            catch (DbUpdateConcurrencyException)
            {
                if (!tbl_usuariosExists(id))
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

        // POST: api/tblusuarios
        [ResponseType(typeof(tbl_usuarios))]
        public IHttpActionResult Posttbl_usuarios(tbl_usuarios tbl_usuarios)
        {
            int usuario_creacion = Convert.ToInt32(tbl_usuarios.usuario_creacion);
            int id_usuario =0;
            int id_perfil =  Convert.ToInt32(tbl_usuarios.id_perfil);

            if (!ModelState.IsValid)
            {
                return BadRequest(ModelState);
            }

            tbl_usuarios.fecha_creacion = DateTime.Now;
            db.tbl_usuarios.Add(tbl_usuarios);
            db.SaveChanges();

            id_usuario = tbl_usuarios.id_usuario;
            Usuario_BL obj_negocio = new Usuario_BL();
            obj_negocio.Set_Insertando_Usuario_WebAccesos(usuario_creacion, id_usuario, id_perfil);

            return CreatedAtRoute("DefaultApi", new { id = tbl_usuarios.id_usuario }, tbl_usuarios);
            
        }



        [ResponseType(typeof(tbl_usuarios))]
        public async Task<IHttpActionResult> Deletetbl_usuarios(int id)
        {
            tbl_usuarios obj_entidad = await db.tbl_usuarios.FindAsync(id);

            obj_entidad = db.tbl_usuarios.Where(g => g.id_usuario == id).FirstOrDefault<tbl_usuarios>();
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

        private bool tbl_usuariosExists(int id)
        {
            return db.tbl_usuarios.Count(e => e.id_usuario == id) > 0;
        }
    }
}