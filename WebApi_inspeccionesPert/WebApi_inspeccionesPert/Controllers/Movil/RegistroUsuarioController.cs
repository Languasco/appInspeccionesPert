using Entidades.Movil;
using Negocios.Movil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Text;
using System.Web.Hosting;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace WebApi_inspeccionesPert.Controllers.Movil
{
    [RoutePrefix("api/Registro")]
    [EnableCors("*", "*", "*")]
    public class RegistroUsuarioController : ApiController
    {
        [HttpGet]
        [Route("getDelegacion")]
        public HttpResponseMessage getDelegacion(int id_Pais, string token)
        {
            // BLEKER
            HttpResponseMessage response;
            try
            {
                LoginDA sOrden = new LoginDA();
                if (token == "8HJH89KJGH98kJH889988887jhS")
                {
                    List<DelegacionRegistro> m = sOrden.GetDelegacion(id_Pais);
                    response = this.Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonConvert.SerializeObject(m), Encoding.UTF8, "application/json");
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    String filePath = HostingEnvironment.MapPath("~/Img/error.png");
                    response.Content = new StreamContent(new FileStream(filePath, FileMode.Open)); // this file stream will be closed by lower layers of web api for you once the response is completed.
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                }
            }
            catch (Exception ex)
            {
                response = this.Request.CreateResponse(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(JsonConvert.SerializeObject(ex.Message), Encoding.UTF8, "application/json"); ;
            }
            return response;
        }

        [HttpGet]
        [Route("getPersonal")]
        public HttpResponseMessage getPersonal(int id_Pais, string nro_Doc, string token)
        {
            HttpResponseMessage response;
            try
            {
                LoginDA sOrden = new LoginDA();
                if (token == "8HJH89GH98kJH889F887jhS")
                {
                    GetPersonal m = sOrden.Get_Personal_Doc(id_Pais, nro_Doc);
                    response = this.Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonConvert.SerializeObject(m), Encoding.UTF8, "application/json");
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    String filePath = HostingEnvironment.MapPath("~/Img/error.png");
                    response.Content = new StreamContent(new FileStream(filePath, FileMode.Open)); // this file stream will be closed by lower layers of web api for you once the response is completed.
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                }
            }
            catch (Exception ex)
            {
                response = this.Request.CreateResponse(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(JsonConvert.SerializeObject(ex.Message), Encoding.UTF8, "application/json"); ;
            }
            return response;
        }

        [HttpGet]
        [Route("getDominio")]
        public HttpResponseMessage getDominio(string dominio, string token)
        {
            HttpResponseMessage response;
            try
            {
                LoginDA sOrden = new LoginDA();
                if (token == "8HJH89GH98kJH899877jhS")
                {
                    Mensaje m = sOrden.Validar_Dominio(dominio);
                    response = this.Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonConvert.SerializeObject(m), Encoding.UTF8, "application/json");
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    String filePath = HostingEnvironment.MapPath("~/Img/error.png");
                    response.Content = new StreamContent(new FileStream(filePath, FileMode.Open)); // this file stream will be closed by lower layers of web api for you once the response is completed.
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                }
            }
            catch (Exception ex)
            {
                response = this.Request.CreateResponse(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(JsonConvert.SerializeObject(ex.Message), Encoding.UTF8, "application/json"); ;
            }
            return response;
        }

        [HttpPost]
        [Route("savedRegistroUsuario")]
        [ResponseType(typeof(UsuarioRegistro))]
        public HttpResponseMessage savedOrdenCompra(UsuarioRegistro a, string token)
        {
            HttpResponseMessage response;
            try
            {
                LoginDA sOrden = new LoginDA();
                if (token == "8HJH89KJGH98kJH889988JHG867KS")
                {
                    Mensaje m = sOrden.Registrar_usuario(a);
                    response = this.Request.CreateResponse(HttpStatusCode.OK);
                    response.Content = new StringContent(JsonConvert.SerializeObject(m), Encoding.UTF8, "application/json");
                }
                else
                {
                    response = Request.CreateResponse(HttpStatusCode.OK);
                    String filePath = HostingEnvironment.MapPath("~/Img/error.png");
                    response.Content = new StreamContent(new FileStream(filePath, FileMode.Open)); // this file stream will be closed by lower layers of web api for you once the response is completed.
                    response.Content.Headers.ContentType = new MediaTypeHeaderValue("image/png");
                }
            }
            catch (Exception ex)
            {
                response = this.Request.CreateResponse(HttpStatusCode.InternalServerError);
                response.Content = new StringContent(JsonConvert.SerializeObject(ex.Message), Encoding.UTF8, "application/json"); ;
            }
            return response;
        }
    }
}
