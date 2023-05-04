using Negocios.Procesos;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;

namespace WebApi_inspeccionesPert.Controllers.Proceso
{
    [EnableCors("*", "*", "*")]
    public class ImportarArchivoController : ApiController
    {

        [HttpPost]
        public HttpResponseMessage Upload(int usuario)
        {
            object returnData = null;
            string res = "";
            int estado = 0;

            try
            {
                if (!Request.Content.IsMimeMultipartContent())
                {

                    this.Request.CreateResponse(HttpStatusCode.UnsupportedMediaType);
                }

                string filename = "";
                string path = System.Web.Hosting.HostingEnvironment.MapPath("~/ArchivosExcel/");
                System.Web.HttpFileCollection files = System.Web.HttpContext.Current.Request.Files;

                for (int i = 0; i < files.Count; i++)
                {
                    System.Web.HttpPostedFile file = files[i];
                    filename = new FileInfo(file.FileName).Name;

                    if (file.ContentLength > 0)
                    {
                        string modifiedFilename = "";
                        modifiedFilename = "ImportarPersonal.xlsx";
                        file.SaveAs(path + Path.GetFileName(modifiedFilename));
                    }
                }
                estado = 1;
                ImportarArchivo_BL obImportar = new ImportarArchivo_BL();
                res = "";
                res = obImportar.Get_ImportarArchivo(usuario);

                estado = 2;
                string[] split = res.Split(new Char[] { '|' });

                if (split[0] == "0")
                {
                    returnData = res;
                }
                else
                {
                    returnData = obImportar.ListaAgrupadoTemporal(usuario);
                }

                estado = 3;
                return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData });
            }
            catch (Exception ex)
            {
                //return this.Request.CreateResponse(HttpStatusCode.OK, new { error = new { ex = ex.Message, inner = ex.InnerException }, stado = estado });;
                throw new ArgumentNullException(ex.Message);

            }
        }
        
        public object GetImportarArchivo_Excel(int opcion, string filtro)
        {
            object resul = null;
            try
            { 
                  if (opcion == 2)
                {
                    string[] parametros = filtro.Split('|');
                    int id_usuario = Convert.ToInt32(parametros[0].ToString());

                    ImportarArchivo_BL obj_negocio = new ImportarArchivo_BL();
                    resul = obj_negocio.Guardando_Personal_Masivo(id_usuario);

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
    }
}
