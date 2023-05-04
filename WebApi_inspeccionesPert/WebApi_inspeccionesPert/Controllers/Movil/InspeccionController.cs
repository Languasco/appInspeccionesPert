using Entidades;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Http;

using Entidades.Movil;
using Negocios.Movil;
using System.Net.Mail;

using Newtonsoft.Json;
using System.Net.Mime;
using System.IO;
using System.Web.Http.Cors;
using System.Threading.Tasks;
using System.Configuration;

namespace WebApi_inspeccionesPert.Controllers.Movil
{
    [EnableCors("*", "*", "*")]
    public class InspeccionController : ApiController
    {

        private static readonly string pathUpload = ConfigurationManager.AppSettings["uploadFile"];

        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();
 
        int cont = 0;

        [HttpPost]
        [Route("api/Inspeccion/SavePersonal")]
        public IHttpActionResult SavePersonal(Filtro f)
        {
            Personal personal = MigracionDA.savePersonal(f);
            if (personal != null)
            {
                return Ok(personal);
            }
            else
            {
                return Content(HttpStatusCode.BadRequest, new { Error = "Este usuario si existe" });
            }
        }

        [HttpGet]
        [Route("api/Inspeccion/GetUsuario")]
        public IHttpActionResult GetUsuario(string users, string password, string version, string imei)
        {
            try
            {
                Usuario user = LoginDA.GetUsuario(users, password, version, imei);
                if (user != null)
                {
                    return Ok(user);
                }
                else return BadRequest("Usuario no Existe");
            }
            catch (Exception e)
            {
                throw e;
            }         
        }

        public string getIncidencia(DataTable Lista_Data, int id_personal, int incidencia)
        {
            string cant = "";

            for (int i = 0; i < Lista_Data.Rows.Count; i++)
            {
                if (id_personal == Convert.ToInt32(Lista_Data.Rows[i]["id_personal"].ToString()) && incidencia == Convert.ToInt32(Lista_Data.Rows[i]["id_anomalia"].ToString()))
                {
                    cant = "<td>" + Lista_Data.Rows[i]["codigo_Anomalia"].ToString() + "</td>";
                    cont++;
                    break;
                }
            }

            return cant;
        }

        [HttpPost]
        [Route("api/Inspeccion/SaveMega")]
        public IHttpActionResult SaveMega(Mega mega)
        {
            Mensaje mensaje = MigracionDA.saveMega(mega);
            if (mensaje != null)
            {
                return Ok(mensaje);
            }
            else return NotFound();
        }

        [HttpPost]
        [Route("api/Inspeccion/GetMigracion")]
        public IHttpActionResult GetMigracion(Filtro f)
        {
            try
            {
                MensajeRetorno estado = MigracionDA.getEstadoUsuario(f.inspeccionId);
                if (estado != null)
                {
                    Migracion m = MigracionDA.getMigracion(f);
                    if (m != null)
                    {
                        return Ok(m);
                    }
                    else
                        return BadRequest("No se pudo sincronizar");
                }
                else
                    return BadRequest("No tiene permisos");
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        // NUEVOO EN DESARROLLO  

        [HttpPost]
        [Route("api/Inspeccion/ConfirmVersion/SaveInspeccionRx")]
        public async Task<IHttpActionResult> SaveInspeccionRxAsync()
        {
            try
            {
                string path = HttpContext.Current.Server.MapPath("~/Imagen/");
                var fotos = HttpContext.Current.Request.Files;
                var json = HttpContext.Current.Request.Form["model"];
                var pdf = HttpContext.Current.Request.Form["pdf"];
                var to = HttpContext.Current.Request.Form["to"];
                var asunto = HttpContext.Current.Request.Form["asunto"];
                var cuerpo = HttpContext.Current.Request.Form["cuerpo"];
                var formato = HttpContext.Current.Request.Form["formato"];
                var version = HttpContext.Current.Request.Form["version"];

                InspeccionCab p = JsonConvert.DeserializeObject<InspeccionCab>(json);
                MensajeRetorno estado = MigracionDA.getEstadoUsuario(p.PersonalInspectorId);

                if (estado != null)
                {
                    VersionAndroid v = MigracionDA.GetVersion(version);
                    if (v != null)
                    {
                        MensajeRetorno mensaje = MigracionDA.saveInspeccion(p);
                        if (mensaje != null)
                        {
                            for (int i = 0; i < fotos.Count; i++)
                            {
                                string fileName = Path.GetFileName(fotos[i].FileName);
                                fotos[i].SaveAs(path + fileName);
                            }
                            await Email(mensaje.inspeccionCab, pdf, to, asunto, cuerpo, Convert.ToInt32(formato));
                            return Ok(mensaje);
                        }
                        else
                            return BadRequest("Error");
                    }
                    else
                        return BadRequest("Actualizar Aplicativo");
                }
                else
                    return BadRequest("No tiene permisos");

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public async Task Email(int inspeccion, string pdf, string to, string asunto, string cuerpo, int formato)
        {
            try
            {
                string path;
                string pathExist;
                Mensaje mensaje = new Mensaje();
                pathExist = string.Format("{0}/{1}", System.Web.Hosting.HostingEnvironment.MapPath("~/Pdf"), pdf);
                if (File.Exists(pathExist))
                {
                    File.Delete(pathExist);
                }
                GetPdf(inspeccion, formato, pdf);
                var body = cuerpo;
                var message = new MailMessage();

                foreach (var curr_address in to.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    message.To.Add(new MailAddress(curr_address));
                }

                message.From = new MailAddress("cobrainspecciones@gmail.com");
                message.Subject = asunto;
                message.Body = body;
                message.IsBodyHtml = true;
                message.Priority = MailPriority.Normal;
                path = string.Format("{0}/{1}", System.Web.Hosting.HostingEnvironment.MapPath("~/Pdf"), pdf);
                Attachment attachment = new Attachment(path, MediaTypeNames.Application.Octet);
                attachment.Name = "Inspeccion.pdf";
                ContentDisposition disposition = attachment.ContentDisposition;
                disposition.CreationDate = File.GetCreationTime(path);
                disposition.ModificationDate = File.GetLastWriteTime(path);
                disposition.ReadDate = File.GetLastAccessTime(path);
                message.Attachments.Add(attachment);

                using (var smtp = new SmtpClient())
                {
                    var credential = new NetworkCredential("cobrainspecciones@gmail.com", "A.123456");
                    smtp.Credentials = credential;
                    smtp.Host = "smtp.gmail.com";
                    smtp.Port = 587;
                    smtp.EnableSsl = true;
                    await smtp.SendMailAsync(message);
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public void GetPdf(int inspeccion, int formato, string pdf)
        {
            var obj_inspeccionCab = (from a in db.tbl_Inspeccion_Cab
                                     join user in db.tbl_usuarios on a.id_Personal_Inspector equals user.id_usuario
                                     join c in db.tbl_OT on a.id_OT equals c.id_OT
                                     from j in db.tbl_EmpresaColaboradora.Where(j => j.id_EmpresaColaboradora == a.id_EmpresaColaboradora).DefaultIfEmpty()
                                     join d in db.tbl_EmpresaColaboradora on a.id_Empresa equals d.id_EmpresaColaboradora
                                     join e in db.tbl_Personal on a.id_Personal_JefeObra equals e.id_Personal
                                     join f in db.tbl_Personal on a.id_Personal_Coordinador equals f.id_Personal
                                     join g in db.tbl_Areas on a.id_Area equals g.id_Area
                                     join h in db.tbl_Delegacion on a.id_Delegacion equals h.id_Delegacion
                                     join i in db.tbl_Personal on a.id_Personal_Inspeccionado equals i.id_Personal                                     
                                     where a.id_Inspeccion == inspeccion
                                     select new
                                     {
                                         a.id_Inspeccion, ///obj_inspeccionCab
                                         a.id_Personal_Inspector,
                                         a.id_Empresa,
                                         a.id_Personal_JefeObra,
                                         a.id_Personal_Coordinador,
                                         a.id_Area,
                                         a.id_Delegacion,
                                         a.id_NivelInspeccion,
                                         a.trabajoArealizar_Inspeccion,
                                         a.placa_Inspeccion,
                                         a.lugar_Inspeccion,
                                         a.fecha_Inspeccion,
                                         a.conjuntas,
                                      resul_apellidos_Personal = user.datos_personales,///resul
                                      resul_razonsocial_empresa = (from t in db.tbl_Proyecto where t.id_Proyecto == c.id_Proyecto select t).FirstOrDefault(),
                                      empresa_razonsocial_empresa =d.RazonSocial_EmpresaColaboradora, //empresa
                                      empresa_subcontrata =j.RazonSocial_EmpresaColaboradora,// empresa sub contrata
                                      jefeObra_apellidos_Personal = e.apellidos_Personal + " " + e.nombres_Personal, //jefeObra
                                      jefeCoordinador_apellidos_Personal = f.apellidos_Personal + " " + f.nombres_Personal, //jefeCoordinador
                                      jefeCuadrilla = i.apellidos_Personal + " " + i.nombres_Personal,
                                      g.descripcion_Area, // area
                                      h.nombre_delegacion //delegacion                                      
                                     }).First();

            try
            {

                SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
                //string path = string.Format("{0}/{1}",Server.MapPath("~/Pdf"), pdf);   //// local
                // string path = string.Format("C:/HostingSpaces/admincobraperu/www.cobraperu.com/wwwroot/webApiInspecciones/Pdf/{0}", pdf);
                string path = string.Format("{0}/{1}", System.Web.Hosting.HostingEnvironment.MapPath("~/Pdf"), pdf);
                string path2 = pathUpload + "logo.png"; 
                string path3;
                string nivel;
                if (obj_inspeccionCab.id_NivelInspeccion == 1)
                {
                    path3 = pathUpload + "normal.png";
                    nivel = "Normal";
                }
                else
                {
                    if (obj_inspeccionCab.id_NivelInspeccion == 2)
                    {
                        path3 = pathUpload + "bajo.png";
                        nivel = "Bajo";
                    }
                    else
                    {
                        if (obj_inspeccionCab.id_NivelInspeccion == 3)
                        {
                            path3 = pathUpload + "intermedio.png";
                            nivel = "Intermedio";
                        }
                        else
                        {
                            path3 = pathUpload + "alto.png";
                            nivel = "Alto";
                        }
                    }
                }
                var stringHtml = "<div><style type='text/css'>.responsive-image {  height: 250px;  width: 250px;}.tableHeader{width: 100%;}.tableHeader h1{color:#9E9E9E;font-family: GillSans, Calibri, Trebuchet, sans-serif;font-size:28px;font-weight: 600;}.tableHeader p{font:menu;font-size:11px;color:#9E9E9E;}.inspeccionCab{text-align: center;font:menu;padding-top: 10px;}.inspeccionCab .content1{border: 1px black solid;}.inspeccionCab .content1 h1{background-color: #9E9E9E;border-bottom: 1px black solid;margin: 0px;font-size:19px;}.inspeccionCab .content2 h1, .content3 h1{background-color: #9E9E9E;border-top : 1px solid black;border-left : 1px solid black;border-right : 1px solid black;margin: 0px;font-size:18px;font-family: Arial, Helvetica Neue, Helvetica, sans-serif;font-weight: 500}.inspeccionCab p{color : black;font-weight: bold;font-family: Arial,Helvetica Neue,Helvetica,sans-serif}.inspeccionCab .content1 table{padding-top: 10px;padding-bottom: 10px;}.inspeccionCab .content1 table td{padding-left : 40px;}.inspeccionCab .content1 .contentImg{padding-left: 170px;}.inspeccionCab .content1 table tr{text-align: left;}.tblCuadrilla, .tblVerificación{width: 100%;}.tblCuadrilla th{font-family: Arial,Helvetica Neue,Helvetica,sans-serif;text-align: center;}.tblCuadrilla td{font-family: Arial,Helvetica Neue,Helvetica,sans-serif;}.tblVerificación th{background-color: #e6e6e6;font-family: Arial, Helvetica Neue, Helvetica, sans-serif;}.tblVerificación td{text-align: left;font-family: Arial, Helvetica Neue, Helvetica, sans-serif;}.contentEvidencias h1{font-family: Arial, Helvetica Neue, Helvetica, sans-serif;text-align: center;font-size:17px;text-decoration:underline;}</style><table class='tableHeader'><thead><th><img src='" + path2 + "' /></th><th><h1>INFORME DE INSPECCIÓN</h1></th><th><p>Página 1 de 1</p></th></thead></table><div class='inspeccionCab'><div class='content1'><h1>DATOS DE INSPECCIÓN</h1><table><tbody><tr><td><p>Fecha de Inspección : " + obj_inspeccionCab.fecha_Inspeccion + " </p></td><td rowspan='4' class='contentImg'><p><img src='" + path3 + "'></p><p>Nivel de Inspeccion : " + nivel + "</p></td></tr><tr><td><p>Proyecto : " + obj_inspeccionCab.resul_razonsocial_empresa.nombre_proyecto + " </p></td></tr>" +
                    "<tr><td><p>Inspección : " + inspeccion +  "</p></td></tr>" +
                    "<tr><td><p>Empresa Propia : " + obj_inspeccionCab.empresa_razonsocial_empresa + "</p></td></tr>" +
                    "<tr><td><p>Empresa SubContrata : " + obj_inspeccionCab.empresa_subcontrata + "</p></td></tr>" +
                    "<tr><td><p>Jefe de Cuadrilla : " + obj_inspeccionCab.jefeCuadrilla + "</p></td></tr><tr><td><p>Matrícula del Vehículo : " + obj_inspeccionCab.placa_Inspeccion + "</p></td></tr><tr><td><p>Dirección : " + obj_inspeccionCab.lugar_Inspeccion + "</p></td></tr><tr><td><p>Área : " + obj_inspeccionCab.descripcion_Area + "</p></td></tr><tr><td><p>Delegación : " + obj_inspeccionCab.nombre_delegacion + "</p></td></tr><tr><td><p>Jefe de obra : " + obj_inspeccionCab.jefeObra_apellidos_Personal + "</p></td><td><p>Trabajo a Realizar : " + obj_inspeccionCab.trabajoArealizar_Inspeccion + "</p></td></tr><tr><td><p>Coordinador : " + obj_inspeccionCab.jefeCoordinador_apellidos_Personal +
                    "</p></td><td><p>Inspección conjunta : " + (obj_inspeccionCab.conjuntas == 1 ? "SI" : "NO") + "</p></td></tr><tr><td>" +
                    "<p>Realizado por : " + obj_inspeccionCab.resul_apellidos_Personal + "</p></td></tr></tbody></table></div></div><br>";

                stringHtml += "<div class='inspeccionCab'><div class='content2'><h1>DATOS DE LA CUADRILLA</h1><table class='tblCuadrilla' border=1 cellspacing=0><tbody><thead><tr><th>Nro</th><th>DNI</th><th>Apellidos y Nombres</th><th>Cargo</th><th>INC1</th><th>INC2</th><th>INC3</th><th>INC4</th><th>INC5</th><th>INC6</th><th>INC7</th><th>INC8</th></tr></thead><tbody>";
                var i = 1;

                MigracionDA migracion = new MigracionDA();
                DataTable cuadrilla = new DataTable();
                DataTable cuadrillaIncidencia = new DataTable();
                DataTable cuadrillaDetallado = new DataTable();
                DataTable T_detallesAnomalias = new DataTable();

                cuadrilla = migracion.Get_Cuadrilla_Nombre(inspeccion);
                cuadrillaIncidencia = migracion.Get_Cuadrilla_Incidencia(inspeccion);
                cuadrillaDetallado = migracion.Get_Cuadrilla_Detallado(inspeccion);
                               
                foreach (DataRow Obj in cuadrilla.Rows)
                {
                    stringHtml += "<tr><td>" + i + "</td>" +
                       "<td>" + Obj[1].ToString() + "</td>" +
                       "<td>" + Obj[2] + "</td>" +
                       "<td>" + Obj[3] + "</td>";

                    cont = 0;
                    foreach (DataRow Obj2 in cuadrillaIncidencia.Rows)
                    {
                        stringHtml += getIncidencia(cuadrillaDetallado, (int)Obj["id_personal"], (int)Obj2["id_anomalia"]);
                    }

                    ///completando los td de las tablas
                    for (int j = 1; j <= (8 - cont); j++)
                    {
                        stringHtml += "<td></td>";
                    }

                    stringHtml += "</tr>";
                    i++;
                }

                stringHtml += "</tbody></table></div></div><br>";
                                
                ///----Generando la parte de Anomalias

                T_detallesAnomalias = migracion.Get_DetallesAnomalias(Convert.ToInt32(formato), inspeccion, Convert.ToInt32(obj_inspeccionCab.id_Empresa));

                int inc = 8;

                int ac = 0;
                foreach (DataRow items in T_detallesAnomalias.Rows)
                {
                    //---VALIDACION SALTO PAGINA
                    if (inc == 30)
                    {
                        ac = 0;
                        stringHtml += "</tbody>";
                        stringHtml += "</table><br>";
                        stringHtml += "<br style='page-break-after: always;'>";
                        stringHtml += "<table class='tableHeader'>";
                        stringHtml += "<thead>";
                        stringHtml += "  <img src='" + path2 + "' />";
                        stringHtml += "  <th><h1>INFORME DE INSPECCIÓN</h1></th>";
                        stringHtml += "  <th><p>Página 1 de 2</p></th>";
                        stringHtml += "</thead>";
                        stringHtml += "</table>";
                        stringHtml += "<br><br>";
                    }
                    //--- FIN VALIDACION SALTO PAGINA
                    if (ac == 0)
                    {
                        stringHtml += "<table class='tblVerificación'  border=1 cellspacing=0>";
                        stringHtml += "<thead>";
                        stringHtml += " <tr>";
                        stringHtml += "    <th colspan='4' style='text-align:center'>VERIFICACIÓN DEL CUMPLIMIENTO DE ASPECTOS DE SEGURIDAD INDUSTRIAL</th>";
                        stringHtml += " </tr>";
                        stringHtml += " <tr>";
                        stringHtml += "    <th style='text-align:center'>DESCRIPCIÓN</th>";
                        stringHtml += "    <th colspan='3' style='text-align:center'>CUMPLE</th>";
                        stringHtml += " </tr>";
                        stringHtml += "</thead>";
                        stringHtml += "<tbody>";
                    }

                    if (items["anomalia_titulo"].ToString() == "1")
                    {
                        stringHtml += "<tr style='background-color: #e6e6e6;font-family: Arial, Helvetica Neue, Helvetica, sans-serif;font-weight:bold'>";
                        stringHtml += "   <td style='text-align:center ;'>" + items["descripcion_Anomalia"].ToString() + "</td>";
                        stringHtml += "   <td style='text-align:center'>SI</td>";
                        stringHtml += "   <td style='text-align:center'>NO</td>";
                        stringHtml += "   <td style='text-align:center'>N/A</td>";
                        stringHtml += "</tr>";
                    }
                    else
                    {
                        stringHtml += "<tr>";
                        stringHtml += "   <td>" + items["descripcion_Anomalia"].ToString() + "</td>";
                        stringHtml += "   <td style='text-align:center;' >" + items["valor_SI"].ToString() + "</td>";
                        stringHtml += "   <td style='text-align:center;' >" + items["valor_NO"].ToString() + "</td>";
                        stringHtml += "   <td style='text-align:center;' >" + items["valor_NA"].ToString() + "</td>";
                        stringHtml += "</tr>";
                    }
                    ac += 1;
                    inc += 1;
                }

                if (inc > 30)
                {
                    stringHtml += "</tbody>";
                    stringHtml += "</table><br>";
                }

                ///Fin de Generando la parte de Anomalias

                stringHtml += "<div class='inspeccionCab'>" +
                 "<div class='content3'>" +
                 "<h1>OBSERVACIONES</h1>" +
                 "<table class='tblVerificación' border=1 cellspacing=0>" +
                 "<tbody>" +
                 "<thead>" +
                 "<tr><th>Inc.</th><th>Dni</th><th>Descripción</th><th>Observación</th></tr>" +
                 "</thead>" +
                 "<tbody>";

                var detalle = (from a in db.tbl_Inspeccion_Cab_Detalle where a.id_inspeccion == obj_inspeccionCab.id_Inspeccion select new { a.id_inspeccion_detalle, a.id_anomalia, a.id_personal, a.descripcion, a.foto_levantamiento, a.descripcion_levantamiento }).ToList();
                
                foreach (var item in detalle)
                {
                    var anomalia = (from a in db.tbl_Anomalia where a.id_Anomalia == item.id_anomalia select new { a.codigo_Anomalia, a.descripcion_Anomalia }).First();
                    var persona = (from a in db.tbl_Personal where a.id_Personal == item.id_personal select new { a.nroDoc_Personal }).First();
                    stringHtml += "<tr>" +
                    "<td>" + anomalia.codigo_Anomalia + "</td>" +
                    "<td>" + persona.nroDoc_Personal + "</td>" +
                    "<td>" + anomalia.descripcion_Anomalia.Substring(4) + "</td>" +
                    "<td>" + item.descripcion + "</td>" +
                    "</tr>";
                }

                stringHtml += "</tbody></table></div></div><br style='page-break-after: always;'><div class='contentEvidencias'><h1>EVIDENCIAS DEL INCUMPLIMIENTO</h1>";

                foreach (var item in detalle)
                {
                    var detalleFoto = (from a in db.tbl_Inspeccion_Cab_Detalle_Foto where a.id_inspeccion_detalle == item.id_inspeccion_detalle select new { a.nombre_foto }).ToList();
                    foreach (var itemFoto in detalleFoto)
                    {
                        // string img = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", itemFoto.nombre_foto);
                        string img = pathUpload + itemFoto.nombre_foto;
                        stringHtml += "<div align='center'  style='page-break-after: always;padding:20px 0 20px 0;'>";
                        stringHtml += "<img src='" + img + "'  class='responsive-image'>";
                        //stringHtml += "<img src='" + img + "' width='800' height='600'>";
                        stringHtml += "</div>";
                    }
                }

                stringHtml += "</div>";
                stringHtml += "<div class='contentEvidencias'><h1>Levantamientos</h1>";

                int contadorLevantamiento = 0;

                foreach (var item in detalle)
                {
                    if (!item.foto_levantamiento.Equals("VACIO"))
                    {
                        string foto = item.foto_levantamiento;

                        if (contadorLevantamiento == 0)
                        {
                            // string img = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", item.foto_levantamiento);
                            //string img = string.Format("http://www.dsige.com/dsige_Inspeccion_WebApi/Imagen/{0}", item.foto_levantamiento);
                            string img = pathUpload + item.foto_levantamiento;

                            stringHtml += "<div align='center'  style='page-break-after: always;padding:20px 0 20px 0;'>";
                            stringHtml += "<img src='" + img + "'  class='responsive-image'>";
                            //stringHtml += "<img src='" + img + "'  class='responsive-image' width='800' height='600'>";
                            stringHtml += "<p>" + item.descripcion_levantamiento + "</p>";
                            stringHtml += "</div>";
                            contadorLevantamiento = 1;
                        }
                        else
                        {
                            if (foto != item.foto_levantamiento)
                            {
                                //  string img = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", item.foto_levantamiento);
                                //string img = string.Format("http://www.dsige.com/dsige_Inspeccion_WebApi/Imagen/{0}", item.foto_levantamiento);
                                string img = pathUpload + item.foto_levantamiento;
                                stringHtml += "<div align='center'  style='page-break-after: always;padding:20px 0 20px 0;'>";
                                stringHtml += "<img src='" + img + "'  class='responsive-image'>";
                                //stringHtml += "<img src='" + img + "'  width='800' height='600'>";
                                stringHtml += "<p>" + item.descripcion_levantamiento + "</p>";
                                stringHtml += "</div>";
                            }
                        }
                    }
                }

                stringHtml += "</div>";
                converter.Options.MarginTop = 15;
                converter.Options.MarginLeft = 5;
                converter.Options.MarginRight = 5;
                SelectPdf.PdfDocument doc = converter.ConvertHtmlString(stringHtml);
                doc.Save(path);
                doc.Close();
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost]
        [Route("api/Inspeccion/BusquedaAvanzada")]
        public IHttpActionResult BusquedaAvanzada(Filtro f)
        {
            try
            {
                List<Personal> personal = MigracionDA.buscarPersonal(f);
                if (personal != null)
                {
                    return Ok(personal);
                }
                else
                    return BadRequest("No hay personal");
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}

