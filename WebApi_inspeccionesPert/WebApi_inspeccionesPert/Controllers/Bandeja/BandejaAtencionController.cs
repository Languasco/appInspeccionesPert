using Entidades;
using Entidades.Bandeja;
using Negocios.Bandeja;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi_inspeccionesPert.Controllers.Bandeja
{
 
    [EnableCors("*", "*", "*")]
    public class BandejaAtencionController : ApiController
    {
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        public object GetBandejaAtencion(int opcion, string filtro)
        {
            db.Configuration.ProxyCreationEnabled = false;

            object resul = null;
            try
            {
                if (opcion == 1)
                {
                    string[] parametros = filtro.Split('|');

                    int id_proyecto = Convert.ToInt32(parametros[0].ToString());
                    int id_estado = Convert.ToInt32(parametros[1].ToString());
                    int id_nivelInspeccion = Convert.ToInt32(parametros[2].ToString());
                    int id_inspector = Convert.ToInt32(parametros[3].ToString());
                    string fecha_ini = parametros[4].ToString();
                    string fecha_fin = parametros[5].ToString();

                    BandejaAtencionInspeccion_BL obj_negocio = new BandejaAtencionInspeccion_BL();
                    resul = obj_negocio.Listando_AtencionInspecccion_Cabecera(id_proyecto,id_estado,id_nivelInspeccion, id_inspector,  fecha_ini, fecha_fin);

                }
                else if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');

                    int id_inspeccion = Convert.ToInt32(parametros[0].ToString());

                    BandejaAtencionInspeccion_BL obj_negocio = new BandejaAtencionInspeccion_BL();
                    resul = obj_negocio.Listando_Fotos_Inspeccion(id_inspeccion);

                }
                else if (opcion == 3)
                {
                    string[] parametros = filtro.Split('|');

                    int id_inspeccion = Convert.ToInt32(parametros[0].ToString());
                    string Obs_Levantada = parametros[1].ToString();
                    int id_usuario = Convert.ToInt32(parametros[2].ToString());

                    BandejaAtencionInspeccion_BL obj_negocio = new BandejaAtencionInspeccion_BL();
                    resul = obj_negocio.Guardando_Observacion_Inspeccion(id_inspeccion, Obs_Levantada, id_usuario);

                }
                else if (opcion == 4)
                {
                    string[] parametros = filtro.Split('|');
                    int id_inspeccion = Convert.ToInt32(parametros[0].ToString());

                    BandejaAtencionInspeccion_BL obj_negocio = new BandejaAtencionInspeccion_BL();
                    resul = obj_negocio.Listando_Anomalias(id_inspeccion);
                }
                else if (opcion == 5)
                {
                    string[] parametros = filtro.Split('|');
                    int id_inspeccion_detalle = Convert.ToInt32(parametros[0].ToString());

                    BandejaAtencionInspeccion_BL obj_negocio = new BandejaAtencionInspeccion_BL();
                    resul = obj_negocio.Listando_Fotos_Anomalias(id_inspeccion_detalle);
                }
                else if (opcion == 6)
                {
                    BandejaAtencionInspeccion_BL obj_negocio = new BandejaAtencionInspeccion_BL();
                    resul = obj_negocio.Listando_Personal_Anomalias();
                }
                else if (opcion == 7)
                {
                    string[] parametros = filtro.Split('|');
                    int id_inspeccion_detalle = Convert.ToInt32(parametros[0].ToString());
                    string levantamiento = parametros[1].ToString();
                    string foto_levantamiento = parametros[2].ToString();
                    string descripcion_levantamiento = parametros[3].ToString();

                    BandejaAtencionInspeccion_BL obj_negocio = new BandejaAtencionInspeccion_BL();
                    resul = obj_negocio.Guardando_LevantamientoAnomalia(id_inspeccion_detalle, levantamiento, foto_levantamiento, descripcion_levantamiento);
                }
                else if (opcion == 8)
                {
                    string[] parametros = filtro.Split('|');
                    int id_usuario_cc = Convert.ToInt32(parametros[0].ToString());
                    int id_usuario_jefe = Convert.ToInt32(parametros[1].ToString());
                    int id_usuario_resp = Convert.ToInt32(parametros[2].ToString());

                    BandejaAtencionInspeccion_BL obj_negocio = new BandejaAtencionInspeccion_BL();
                    resul = obj_negocio.Listando_Personal_Email(id_usuario_cc, id_usuario_jefe, id_usuario_resp);
                }
                else if (opcion == 9)
                {
                    string[] parametros = filtro.Split('|');
                    int inspeccion = Convert.ToInt32(parametros[0].ToString());
                    string pdf = parametros[1].ToString();
                    string destinatario = parametros[2].ToString();
                    string copia = parametros[3].ToString();
                    string asunto = parametros[4].ToString();
                    string cuerpo = parametros[5].ToString();
                    int Formato = Convert.ToInt32(parametros[6].ToString());
                    
                    BandejaAtencionInspeccion_BL obj_negocio = new BandejaAtencionInspeccion_BL();
                    resul = obj_negocio.SendEmail_Web(inspeccion, pdf, destinatario, copia, asunto, cuerpo, Formato);
                }
                else if (opcion == 10)
                {
                    resul = (from a in db.tbl_FormatoInspeccion
                             select new
                             {
                                 a.id_Formato,
                                 a.nombre_formato 
                             }).ToList(); 
                }
                else if (opcion == 11)
                {
                    string[] parametros = filtro.Split('|');
                    int id_grupo = Convert.ToInt32(parametros[0].ToString());

                    resul = (from a in db.tbl_Cliente
                             //where a.id_grupo == id_grupo
                             select new
                             {
                                 a.id_Cliente,
                                 a.nombre_Cliente 
                             }).ToList();
                }
                else if (opcion == 12)
                {
                    BandejaAtencionInspeccion_BL obj_negocio = new BandejaAtencionInspeccion_BL();
                    resul = obj_negocio.Listando_CargosPersonal();
                }
                else if (opcion == 13)
                {
           
                    BandejaAtencionInspeccion_BL obj_negocio = new BandejaAtencionInspeccion_BL();
                    resul = obj_negocio.Guardando_NroSancionAnomalias(filtro);
                }
                else if (opcion == 14)
                {
                    string[] parametros = filtro.Split('|');

                    int id_personal = Convert.ToInt32(parametros[0].ToString());
 

                    BandejaAtencionInspeccion_BL obj_negocio = new BandejaAtencionInspeccion_BL();
                    resul = obj_negocio.Listando_Listado_Inspecccion_Cabecera(id_personal);

                }
                else if (opcion == 15)
                {
                    string[] parametros = filtro.Split('|');

                    int id_personal = Convert.ToInt32(parametros[0].ToString());

                    int id_pais = Convert.ToInt32(parametros[1].ToString());
                    int id_grupo = Convert.ToInt32(parametros[2].ToString());

                    string idDelegacion = parametros[3].ToString();
                    string idInspector = parametros[4].ToString();
                    string idRespCorreccion = parametros[5].ToString();
                    int opcionSelection = Convert.ToInt32(parametros[6].ToString());

                    string fecha_Ini = parametros[7].ToString();
                    string fecha_fin = parametros[8].ToString();

                    BandejaAtencionInspeccion_BL obj_negocio = new BandejaAtencionInspeccion_BL();
                    resul = obj_negocio.Listando_Listado_Inspecccion_Cabecera_new(id_personal, id_pais, id_grupo, idDelegacion, idInspector, idRespCorreccion, opcionSelection, fecha_Ini, fecha_fin);

                }
                else if (opcion == 16)
                {
                    string[] parametros = filtro.Split('|');
                    int inspeccion = Convert.ToInt32(parametros[0].ToString());
                    string nombre_pdf = parametros[1].ToString();
                    int Formato = Convert.ToInt32(parametros[2].ToString());

                    BandejaAtencionInspeccion_BL obj_negocio = new BandejaAtencionInspeccion_BL();
                    resul = obj_negocio.GetPdf_individual(inspeccion, nombre_pdf, Formato);
                }
                else if (opcion == 17)
                {
                    BandejaAtencionInspeccion_BL obj_negocio = new BandejaAtencionInspeccion_BL();
                    string[] parametros = filtro.Split('|');
                    int idCargo = Convert.ToInt32(parametros[0].ToString());

                    resul = obj_negocio.mostrando_tecnicosCargo(idCargo);
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
                  

        [HttpPost] // This is from System.Web.Http, and not from System.Web.Mvc
        public HttpResponseMessage Upload()
        {
            if (!Request.Content.IsMimeMultipartContent())
            {
                this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
            }

            string filename = String.Format("{0:ddMMyyyy_hhmmss}", DateTime.Now);
            string path = HttpContext.Current.Server.MapPath("~/Imagen/");
              string extension="";

            System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;
            
            for (int i = 0; i < files.Count; i++)
            {
                System.Web.HttpPostedFile file = files[i]; 

                if (file.ContentLength > 0)
                {
                    extension = Path.GetExtension(file.FileName);
                    file.SaveAs(path + filename  + extension);
                }
            }
            return this.Request.CreateResponse(HttpStatusCode.OK, new { ruta = path + filename + extension, 
                                                                        nombreArchivo = filename + extension });
        }



        [HttpPost]
        [Route("api/BandejaAtencion/get_relacionInspecciones")]
        public object get_relacionInspecciones(Inspecciones_E obj)
        {
            object resultado = null;
            try
            {
                BandejaAtencionInspeccion_BL obj_negocio = new BandejaAtencionInspeccion_BL();
                resultado = obj_negocio.Listando_Listado_Inspecccion_Cabecera_new(obj.id_personal, obj.id_pais, obj.id_grupo, obj.idDelegacion, obj.idInspector, obj.idRespCorreccion, obj.opcion, obj.fecha_Ini, obj.fecha_fin);
            }
            catch (Exception ex)
            {
                resultado = ex.Message;
            }
            return resultado;

        }


    }
}
