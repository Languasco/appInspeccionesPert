using Entidades;
using Entidades.Acceso;
using Entidades.Mantenimiento;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Mail;
using System.Web.Http;
using System.Web.Http.Cors;


namespace WebApi_inspeccionesPert.Controllers.Mantenimientos
{
    [EnableCors("*", "*", "*")]
    public class LoginController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();


        public object GetLogin(int opcion, string filtro)
        {
            //filtro puede tomar cualquier valor
            db.Configuration.ProxyCreationEnabled = false;
            Resultado res = new Resultado();

            object resul = null;
            try
            {
                if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|'); 
                    string user = parametros[0].ToString();
                    string password = parametros[1].ToString();


                    resul = (from a in db.tbl_usuarios 
                             where a.usuario_login == user &&  a.contrasenia_login==password
                             select new
                             {
                                 id_Personal = a.id_usuario,
                                 nroDoc_Personal = a.nro_documento,
                                 apellidos_Personal = a.datos_personales,
                                 nombres_Personal = "",
                                 a.id_perfil,
                                 envio_Online = "NO"
                             }).ToList();   
                }
                else if (opcion == 2)
                {
                       string[] parametros = filtro.Split('|');
                       string correo = parametros[0].ToString();
                              
                       db.Configuration.ProxyCreationEnabled = false;
                       var verificacion = db.tbl_usuarios.Where(x => x.correo_electronico == correo).FirstOrDefault<tbl_usuarios>();

                       if (verificacion != null)
                       {
                           if (ModelState.IsValid)
                           {
                               var body = "<center><h2>Recuperación de Contraseña</h2></center>" +
                                   "<p> Usuario : " + verificacion.usuario_login + "</p> " +
                                   "<p> Contraseña : " + verificacion.contrasenia_login + "</p>" +
                                   "<p></p><p></p><p>Atte.</p><p>Administrador Web</p><p>Dsige</p>";                              
                               
                               var message = new MailMessage();
                               message.To.Add(new MailAddress(verificacion.correo_electronico));
                               message.From = new MailAddress("cobralecturas@gmail.com");
                               message.Subject = "Recuperación de Correo - Sistema de Dsige";
                               message.Body = body;
                               message.IsBodyHtml = true;

                               using (var smtp = new SmtpClient())
                               {
                                   var credential = new NetworkCredential
                                   {
                                       UserName = "cobrainspecciones@gmail.com",
                                       Password = "A.123456"
                                   };
                                   smtp.Credentials = credential;
                                   smtp.Host = "smtp.gmail.com";
                                   smtp.Port = 587;
                                   smtp.EnableSsl = true;
                                   smtp.Send(message);

                               }
                               resul = "OK";
                           }
                           else
                           {
                               resul = "FALLO";
                           }
                       }
                       else
                       {
                           resul = "0";
                       }       
                }
                else if (opcion == 3)
                {
                    string[] parametros = filtro.Split('|');
                    string login = parametros[0].ToString();
                    string contra = parametros[1].ToString();

                    var flagLogin = db.tbl_usuarios.Count(e => e.usuario_login == login && e.contrasenia_login == contra);

                    if (flagLogin == 0)
                    {
                        res.ok = false;
                        res.data = "El usuario y/o contraseña no son correctos, verifique ";
                        resul = res;
                    }
                    else
                    {
                        var Parents = new string[] { "1" };
                        tbl_usuarios objUsuario = db.tbl_usuarios.Where(p => p.usuario_login == login && p.contrasenia_login == contra).SingleOrDefault();

                        Menu listamenu = new Menu();
                        List<MenuPermisos> listaAccesos = new List<MenuPermisos>();

                        var listaMenu = (from w in db.tbl_AccesoOpciones
                                         join od in db.tbl_Definicion_Opciones on w.id_opcion equals od.id_opcion
                                         join u in db.tbl_usuarios on w.id_personal equals u.id_usuario
                                         where u.id_usuario == objUsuario.id_usuario && Parents.Contains(od.parentID.ToString()) && od.estado == 1
                                         orderby od.orden_Opcion ascending
                                         select new
                                         {
                                             id_opcion = w.id_opcion,
                                             id_usuarios = w.id_personal,
                                             nombre_principal = od.nombre_opcion,
                                             parent_id_principal = od.parentID,
                                             urlmagene_principal = od.urlImagen_Opcion
                                         }).Distinct();

                        foreach (var item in listaMenu)
                        {
                            MenuPermisos listaJsonObj = new MenuPermisos();

                            listaJsonObj.id_opcion = Convert.ToInt32(item.id_opcion);
                            listaJsonObj.id_usuarios = Convert.ToInt32(item.id_usuarios);
                            listaJsonObj.nombre_principal = item.nombre_principal;
                            listaJsonObj.parent_id_principal = Convert.ToInt32(item.parent_id_principal);
                            listaJsonObj.urlmagene_principal = item.urlmagene_principal;
                            listaJsonObj.listMenu = (from w in db.tbl_AccesoOpciones
                                                     join od in db.tbl_Definicion_Opciones on w.id_opcion equals od.id_opcion
                                                     join u in db.tbl_usuarios on w.id_personal equals u.id_usuario
                                                     where u.id_usuario == objUsuario.id_usuario && od.parentID == item.id_opcion && od.estado == 1
                                                     orderby od.orden_Opcion ascending
                                                     select new
                                                     {
                                                         nombre_page = od.nombre_opcion,
                                                         url_page = od.url_opcion,
                                                         orden = od.orden_Opcion
                                                     })
                                            .ToList()
                                            .Distinct();

                            listaAccesos.Add(listaJsonObj);
                        }

                        listamenu.menuPermisos = listaAccesos;
                        //listamenu.menuEventos = get_AccesoEventos(objUsuario.id_usuario);
                        listamenu.menuEventos = null;
                          listamenu.id_usuario = objUsuario.id_usuario;
                        listamenu.nombre_usuario = objUsuario.datos_personales;
                        listamenu.id_perfil = Convert.ToInt32( objUsuario.id_perfil);

                        res.ok = true;
                        res.data = listamenu;

                        resul = res;

                    }
                }
                else
                {
                    resul = "Opcion seleccionada invalida";
                }

            }
            catch (Exception ex)
            {
                resul = ex.Message;
            }
            return resul;
        }
         

    }
}
