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
    public class ImportarArchivo_UsuariosController : ApiController
    {        
        [HttpPost]
        public HttpResponseMessage Upload_Usuario(int usuario)
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
                        modifiedFilename = usuario + "_ImportarUsuarioMasivo.xlsx";
                        file.SaveAs(path + Path.GetFileName(modifiedFilename));
                    }
                }
                estado = 1;
                ImportarArchivo_BL obImportar = new ImportarArchivo_BL();
                res = "";
                returnData = obImportar.Get_ImportarArchivo_Usuario(usuario);
                estado = 3;
                return this.Request.CreateResponse(HttpStatusCode.OK, new { returnData });
            }
            catch (Exception ex)
            {
                //return this.Request.CreateResponse(HttpStatusCode.OK, new { error = new { ex = ex.Message, inner = ex.InnerException }, stado = estado });
                throw new ArgumentNullException(ex.Message);
            }
        }
        
    }
}
