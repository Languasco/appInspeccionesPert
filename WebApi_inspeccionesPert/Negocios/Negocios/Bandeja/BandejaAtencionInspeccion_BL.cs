using Entidades;
using Entidades.Bandeja;
using Negocios.Movil;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Net.Mime;
using System.Text;
using System.Threading.Tasks;
using System.Web;
using System.Web.Http;
using SelectPdf;
using Entidades.Mantenimiento;

namespace Negocios.Bandeja
{
    public class BandejaAtencionInspeccion_BL
    {
        string cadenaCnx = System.Configuration.ConfigurationManager.ConnectionStrings["conexionDsige"].ConnectionString;
        string ServidorRuta_Foto = ConfigurationManager.AppSettings["ServidorRuta_Foto"];
        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();
        int cont = 0;

        public List<BandejaAtencionInspeccion_E> Listando_AtencionInspecccion_Cabecera(int id_proyecto, int id_estado, int id_nivelInspeccion, int id_inspector, string fecha_ini, string fecha_fin)
        {
            try
            {
                List<BandejaAtencionInspeccion_E> obj_List = new List<BandejaAtencionInspeccion_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_BANDEJA_ATENCION_INSPECCIONES", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_proyecto", SqlDbType.Int).Value = id_proyecto;
                        cmd.Parameters.Add("@id_estado", SqlDbType.Int).Value = id_estado;
                        cmd.Parameters.Add("@id_nivelInspeccion", SqlDbType.Int).Value = id_nivelInspeccion;
                        cmd.Parameters.Add("@id_inspector", SqlDbType.Int).Value = id_inspector;
                        cmd.Parameters.Add("@fecha_ini", SqlDbType.VarChar).Value = fecha_ini;
                        cmd.Parameters.Add("@fecha_fin", SqlDbType.VarChar).Value = fecha_fin;

                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                BandejaAtencionInspeccion_E Entidad = new BandejaAtencionInspeccion_E();

                                Entidad.id_Inspeccion = Convert.ToInt32(row["id_Inspeccion"].ToString());
                                Entidad.nro_inspeccion = row["nro_inspeccion"].ToString();
                                Entidad.fecha_inspeccion = row["fecha_inspeccion"].ToString();
                                Entidad.cargo = row["cargo"].ToString();
                                Entidad.nombre = row["nombre"].ToString();
                                Entidad.jefe_area = row["jefe_area"].ToString();
                                Entidad.inspector = row["inspector"].ToString();

                                Entidad.nivel = row["nivel"].ToString();
                                Entidad.anomalia = row["anomalia"].ToString();
                                Entidad.levanto_obs = row["levanto_obs"].ToString();
                                Entidad.fecha = row["fecha"].ToString();
                                Entidad.nro_insp_relacionada = row["nro_insp_relacionada"].ToString();

                                Entidad.id_EmpresaColaboradora = Convert.ToInt32(row["id_EmpresaColaboradora"].ToString());
                                Entidad.lugar_Inspeccion = row["lugar_Inspeccion"].ToString();
                                Entidad.actividadOT_Inspeccion = row["actividadOT_Inspeccion"].ToString();
                                Entidad.trabajoArealizar_Inspeccion = row["trabajoArealizar_Inspeccion"].ToString();

                                Entidad.id_Cargo = Convert.ToInt32(row["id_Cargo"].ToString());
                                Entidad.id_Personal_Inspeccionado = Convert.ToInt32(row["id_Personal_Inspeccionado"].ToString());
                                Entidad.id_Area = Convert.ToInt32(row["id_Area"].ToString());
                                Entidad.id_Personal_Coordinador = Convert.ToInt32(row["id_Personal_Coordinador"].ToString());

                                Entidad.id_Personal_JefeObra = Convert.ToInt32(row["id_Personal_JefeObra"].ToString());
                                Entidad.placa_Inspeccion = row["placa_Inspeccion"].ToString();
                                Entidad.id_NivelInspeccion = Convert.ToInt32(row["id_NivelInspeccion"].ToString());
                                Entidad.id_TipoInspeccion = Convert.ToInt32(row["id_TipoInspeccion"].ToString());

                                Entidad.Resultado_Inspeccion = row["Resultado_Inspeccion"].ToString();
                                Entidad.iniciofin_Trabajo = row["iniciofin_Trabajo"].ToString();
                                Entidad.id_Anomalia = Convert.ToInt32(row["id_Anomalia"].ToString());
                                Entidad.descripcion_Inspeccion = row["descripcion_Inspeccion"].ToString();

                                Entidad.accionPropuesta_Correctiva = row["accionPropuesta_Correctiva"].ToString();
                                Entidad.id_Personal_Responsable = Convert.ToInt32(row["id_Personal_Responsable"].ToString());
                                Entidad.fechaPropuesta_Correctiva = row["fechaPropuesta_Correctiva"].ToString();
                                Entidad.observacion_Correctiva = row["observacion_Correctiva"].ToString();
                                Entidad.paralizacion_Correctiva = row["paralizacion_Correctiva"].ToString();
                                Entidad.sancion_Correctiva = row["sancion_Correctiva"].ToString();
                                Entidad.id_TipoSancion = Convert.ToInt32(row["id_TipoSancion"].ToString());
                                Entidad.nroTrabajadores_Correctiva = Convert.ToInt32(row["nroTrabajadores_Correctiva"].ToString());

                                Entidad.colorfondo = row["colorfondo"].ToString();
                                Entidad.colorFuente = row["colorFuente"].ToString();
                                Entidad.Obs_Levantada = row["Obs_Levantada"].ToString();
                                Entidad.nro_inspeccionRelacionada = row["nro_inspeccionRelacionada"].ToString();
                                Entidad.estado = Convert.ToInt32(row["estado"].ToString());
                                Entidad.id_Cliente = Convert.ToInt32(row["id_Cliente"].ToString());

                                obj_List.Add(Entidad);
                            }
                        }
                    }
                }

                return obj_List;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public List<BandejaAtencionInspeccion_E> Listando_Fotos_Inspeccion(int id_inspeccion)
        {
            try
            {
                List<BandejaAtencionInspeccion_E> obj_List = new List<BandejaAtencionInspeccion_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_INSPECCION_FOTO", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_inspeccion", SqlDbType.Int).Value = id_inspeccion;
                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                BandejaAtencionInspeccion_E Entidad = new BandejaAtencionInspeccion_E();

                                Entidad.id_inspeccion = Convert.ToInt32(row["id_inspeccion"].ToString());
                                Entidad.id_inspeccion_foto = Convert.ToInt32(row["id_inspeccion_foto"].ToString());
                                Entidad.nombre_foto = row["nombre_foto"].ToString();
                                Entidad.descripcion_foto = row["descripcion_foto"].ToString();
                                Entidad.ruta_foto = ServidorRuta_Foto + row["nombre_foto"].ToString();
                                obj_List.Add(Entidad);
                            }
                        }
                    }
                }

                return obj_List;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public string Guardando_Observacion_Inspeccion(int id_inspeccion, string Obs_Levantada, int id_usuario)
        {
            string resultado = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_I_OBSERSVACION_INSPECCION", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_inspeccion", SqlDbType.Int).Value = id_inspeccion;
                        cmd.Parameters.Add("@Observacion", SqlDbType.VarChar).Value = Obs_Levantada;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;
                        cmd.ExecuteNonQuery();
                        resultado = "OK";
                    }
                }
            }
            catch (Exception e)
            {
                resultado = e.Message;
            }
            return resultado;
        }

        public List<BandejaAtencionInspeccion_E> Listando_Anomalias(int id_inspeccion)
        {
            try
            {
                List<BandejaAtencionInspeccion_E> obj_List = new List<BandejaAtencionInspeccion_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_INSPECCION_ANOMALIAS_NEW", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_inspeccion", SqlDbType.Int).Value = id_inspeccion;
                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                BandejaAtencionInspeccion_E Entidad = new BandejaAtencionInspeccion_E();

                                Entidad.id_inspeccion_detalle = Convert.ToInt32(row["id_inspeccion_detalle"].ToString());
                                Entidad.id_inspeccion = Convert.ToInt32(row["id_inspeccion"].ToString());
                                Entidad.id_personal = Convert.ToInt32(row["id_personal"].ToString());
                                Entidad.personal = row["personal"].ToString();
                                Entidad.nroDoc_Personal = row["nroDoc_Personal"].ToString();

                                Entidad.id_anomalia = Convert.ToInt32(row["id_anomalia"].ToString());
                                Entidad.codigo_Anomalia = row["codigo_Anomalia"].ToString();
                                Entidad.Anomalia = row["Anomalia"].ToString();
                                Entidad.descripcionAnomalia = row["descripcionAnomalia"].ToString();

                                Entidad.levantamiento = row["levantamiento"].ToString();
                                Entidad.foto_levantamiento = row["foto_levantamiento"].ToString();
                                Entidad.descripcion_levantamiento = row["descripcion_levantamiento"].ToString();

                                Entidad.fotoAnomalia = row["fotoAnomalia"].ToString();
                                Entidad.fotoLevantamiento = row["fotoLevantamiento"].ToString();
                                Entidad.conjuntas = row["conjuntas"].ToString() == "0" ? "NO" : "SI";
                                Entidad.actividadot_inspeccion = row["actividadot_inspeccion"].ToString();

                                if (row["fotoLevantamiento"].ToString() == "")
                                {
                                    Entidad.ruta_foto = "";
                                }
                                else
                                {
                                    Entidad.ruta_foto = ServidorRuta_Foto + row["foto_levantamiento"].ToString();
                                }

                                Entidad.id_formato = row["id_formato"].ToString();
                                Entidad.id_ValorInspeccion = row["id_ValorInspeccion"].ToString();

                                Entidad.Resultado_Inspeccion = row["Resultado_Inspeccion"].ToString();
                                Entidad.accionPropuesta_Correctiva = row["accionPropuesta_Correctiva"].ToString();
                                Entidad.fechaPropuesta_Correctiva = row["fechaPropuesta_Correctiva"].ToString();

                                Entidad.disponividad_uso = row["disponividad_uso"].ToString();

                                obj_List.Add(Entidad);
                            }
                        }
                    }
                }

                return obj_List;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public List<BandejaAtencionInspeccion_E> Listando_Fotos_Anomalias(int id_inspeccion_detalle)
        {
            try
            {
                List<BandejaAtencionInspeccion_E> obj_List = new List<BandejaAtencionInspeccion_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_INSPECCION_FOTO_ANOMALIA", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_inspeccion_detalle", SqlDbType.Int).Value = id_inspeccion_detalle;
                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                BandejaAtencionInspeccion_E Entidad = new BandejaAtencionInspeccion_E();
                                Entidad.ruta_foto = ServidorRuta_Foto + row["nombre_foto"].ToString();
                                obj_List.Add(Entidad);
                            }
                        }
                    }
                }

                return obj_List;
            }
            catch (Exception e)
            {

                throw e;
            }
        }


        public List<BandejaAtencionInspeccion_E> Listando_Personal_Anomalias()
        {
            try
            {
                List<BandejaAtencionInspeccion_E> obj_List = new List<BandejaAtencionInspeccion_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("NEW_BANDEJA_ATENCION_PERSONAL_ANOMALIAS", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        //cmd.Parameters.Add("@id_Pais", SqlDbType.Int).Value = id_Pais;
                        //cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo;
                        //cmd.Parameters.Add("@id_Delegacion", SqlDbType.Int).Value = id_Delegacion;

                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                BandejaAtencionInspeccion_E Entidad = new BandejaAtencionInspeccion_E();
                                Entidad.id_personal = Convert.ToInt32(row["id_Personal"].ToString());
                                Entidad.nroDoc_Personal = row["nroDoc_Personal"].ToString();
                                Entidad.personal = row["personal"].ToString();
                                obj_List.Add(Entidad);
                            }
                        }
                    }
                }

                return obj_List;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public string Guardando_LevantamientoAnomalia(int id_inspeccion_detalle, string levantamiento, string foto_levantamiento, string descripcion_levantamiento)
        {
            string resultado = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_INSPECCIONES_ANOMALIA_LEVANTAMIENTO", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_inspeccion_detalle", SqlDbType.Int).Value = id_inspeccion_detalle;
                        cmd.Parameters.Add("@levantamiento", SqlDbType.VarChar).Value = levantamiento;
                        cmd.Parameters.Add("@foto_levantamiento", SqlDbType.VarChar).Value = foto_levantamiento;
                        cmd.Parameters.Add("@descripcion_levantamiento", SqlDbType.VarChar).Value = descripcion_levantamiento;

                        cmd.ExecuteNonQuery();
                        resultado = "OK";
                    }
                }
            }
            catch (Exception e)
            {
                resultado = e.Message;
            }
            return resultado;
        }

        public List<BandejaAtencionInspeccion_E> Listando_Personal_Email(int id_usuario_cc, int id_usuario_jefe, int id_usuario_resp)
        {
            try
            {
                List<BandejaAtencionInspeccion_E> obj_List = new List<BandejaAtencionInspeccion_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_BANDEJA_ATENCION_EMAIL", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@usuario_cc", SqlDbType.Int).Value = id_usuario_cc;
                        cmd.Parameters.Add("@usuario_jefe", SqlDbType.VarChar).Value = id_usuario_jefe;
                        cmd.Parameters.Add("@usuario_resp", SqlDbType.VarChar).Value = id_usuario_resp;

                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                BandejaAtencionInspeccion_E Entidad = new BandejaAtencionInspeccion_E();
                                Entidad.dni = Convert.ToInt32(row["dni"].ToString());
                                Entidad.personal = row["personal"].ToString();
                                Entidad.correo = row["correo"].ToString();
                                obj_List.Add(Entidad);
                            }
                        }
                    }
                }

                return obj_List;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public string SendEmail_Web(int inspeccion, string pdf, string to, string cc, string asunto, string cuerpo, int formato)
        {
            string path;
            string pathExist;
            string mensaje = "";
            try
            {

                pathExist = string.Format("{0}/{1}", System.Web.Hosting.HostingEnvironment.MapPath("~/Pdf"), pdf);
                //pathExist = string.Format("C:/HostingSpaces/admincobraperu/www.cobraperu.com/wwwroot/webApiInspecciones/Pdf/{0}", pdf);

                if (System.IO.File.Exists(pathExist))
                {
                    System.IO.File.Delete(pathExist);
                }
                GetPdf(inspeccion, formato, pdf);
                var body = cuerpo;
                var message = new MailMessage();

                foreach (var curr_address in to.Split(new[] { ";" }, StringSplitOptions.RemoveEmptyEntries))
                {
                    message.To.Add(new MailAddress(curr_address));
                }

                if (!string.IsNullOrEmpty(cc))
                {
                    message.To.Add(new MailAddress(cc));
                }

                message.From = new MailAddress("cobrainspecciones@gmail.com");
                message.Subject = asunto;
                message.Body = body;
                message.IsBodyHtml = true;
                message.Priority = MailPriority.Normal;

                path = string.Format("{0}/{1}", System.Web.Hosting.HostingEnvironment.MapPath("~/Pdf"), pdf);
                //path = string.Format("C:/HostingSpaces/admincobraperu/www.cobraperu.com/wwwroot/webApiInspecciones/Pdf/{0}", pdf);

                Attachment attachment = new Attachment(path, MediaTypeNames.Application.Octet);
                attachment.Name = "Inspeccion.pdf";
                ContentDisposition disposition = attachment.ContentDisposition;
                disposition.CreationDate = System.IO.File.GetCreationTime(path);
                disposition.ModificationDate = System.IO.File.GetLastWriteTime(path);
                disposition.ReadDate = System.IO.File.GetLastAccessTime(path);

                message.Attachments.Add(attachment);
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

                mensaje = "OK";

            }
            catch (Exception ex)
            {
                mensaje = ex.Message;
            }
            return mensaje;
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
                                         empresa_razonsocial_empresa = d.RazonSocial_EmpresaColaboradora, //empresa
                                         empresa_subcontrata = j.RazonSocial_EmpresaColaboradora,// empresa sub contrata
                                         jefeObra_apellidos_Personal = e.apellidos_Personal + " " + e.nombres_Personal, //jefeObra
                                         jefeCoordinador_apellidos_Personal = f.apellidos_Personal + " " + f.nombres_Personal, //jefeCoordinador
                                         jefeCuadrilla = i.apellidos_Personal + " " + i.nombres_Personal,
                                         g.descripcion_Area, // area
                                         h.nombre_delegacion //delegacion                                      
                                     }).First();

            try
            {

                SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();

                string path = string.Format("{0}/{1}", System.Web.Hosting.HostingEnvironment.MapPath("~/Pdf"), pdf);
                string path2 = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", "logo.png");
                string path3;
                string nivel;
                if (obj_inspeccionCab.id_NivelInspeccion == 1)
                {
                    path3 = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", "normal.png");
                    nivel = "Normal";
                }
                else
                {
                    if (obj_inspeccionCab.id_NivelInspeccion == 2)
                    {
                        path3 = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", "bajo.png");
                        nivel = "Bajo";
                    }
                    else
                    {
                        if (obj_inspeccionCab.id_NivelInspeccion == 3)
                        {
                            path3 = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", "intermedio.png");
                            nivel = "Intermedio";
                        }
                        else
                        {
                            path3 = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", "alto.png");
                            nivel = "Alto";
                        }
                    }
                }
                var stringHtml = "<div><style type='text/css'>.responsive-image {  height: auto;  width: 100%;}.tableHeader{width: 100%;}.tableHeader h1{color:#9E9E9E;font-family: GillSans, Calibri, Trebuchet, sans-serif;font-size:28px;font-weight: 600;}.tableHeader p{font:menu;font-size:11px;color:#9E9E9E;}.inspeccionCab{text-align: center;font:menu;padding-top: 10px;}.inspeccionCab .content1{border: 1px black solid;}.inspeccionCab .content1 h1{background-color: #9E9E9E;border-bottom: 1px black solid;margin: 0px;font-size:19px;}.inspeccionCab .content2 h1, .content3 h1{background-color: #9E9E9E;border-top : 1px solid black;border-left : 1px solid black;border-right : 1px solid black;margin: 0px;font-size:18px;font-family: Arial, Helvetica Neue, Helvetica, sans-serif;font-weight: 500}.inspeccionCab p{color : black;font-weight: bold;font-family: Arial,Helvetica Neue,Helvetica,sans-serif}.inspeccionCab .content1 table{padding-top: 10px;padding-bottom: 10px;}.inspeccionCab .content1 table td{padding-left : 40px;}.inspeccionCab .content1 .contentImg{padding-left: 170px;}.inspeccionCab .content1 table tr{text-align: left;}.tblCuadrilla, .tblVerificación{width: 100%;}.tblCuadrilla th{font-family: Arial,Helvetica Neue,Helvetica,sans-serif;text-align: center;}.tblCuadrilla td{font-family: Arial,Helvetica Neue,Helvetica,sans-serif;}.tblVerificación th{background-color: #e6e6e6;font-family: Arial, Helvetica Neue, Helvetica, sans-serif;}.tblVerificación td{text-align: left;font-family: Arial, Helvetica Neue, Helvetica, sans-serif;}.contentEvidencias h1{font-family: Arial, Helvetica Neue, Helvetica, sans-serif;text-align: center;font-size:17px;text-decoration:underline;}</style><table class='tableHeader'><thead><th><img src='" + path2 + "' /></th><th><h1>INFORME DE INSPECCIÓN</h1></th><th><p>Página 1 de 1</p></th></thead></table><div class='inspeccionCab'><div class='content1'><h1>DATOS DE INSPECCIÓN</h1><table><tbody><tr><td><p>Fecha de Inspección : " + obj_inspeccionCab.fecha_Inspeccion + " </p></td><td rowspan='4' class='contentImg'><p><img src='" + path3 + "'></p><p>Nivel de Inspeccion : " + nivel + "</p></td></tr><tr><td><p>Proyecto : " + obj_inspeccionCab.resul_razonsocial_empresa.nombre_proyecto + " </p></td></tr>" +
                    "<tr><td><p>Inspección : " + inspeccion + "</p></td></tr>" +
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
                        string img = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", itemFoto.nombre_foto);
                        //string img = string.Format("http://www.dsige.com/dsige_Inspeccion_WebApi/Imagen/{0}", itemFoto.nombre_foto);
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
                            string img = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", item.foto_levantamiento);
                            //string img = string.Format("http://www.dsige.com/dsige_Inspeccion_WebApi/Imagen/{0}", item.foto_levantamiento);
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
                                string img = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", item.foto_levantamiento);
                                //string img = string.Format("http://www.dsige.com/dsige_Inspeccion_WebApi/Imagen/{0}", item.foto_levantamiento);
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

        public string GetPdf_individual_anterior(int inspeccion,  string pdf , int formato)
       {
           string ruta_descarga = "";

           string pathExist = string.Format("{0}/{1}", System.Web.Hosting.HostingEnvironment.MapPath("~/Pdf"), pdf);
                if (System.IO.File.Exists(pathExist))
                {
                    System.IO.File.Delete(pathExist);
                }

           var obj_inspeccionCab = (from a in db.tbl_Inspeccion_Cab
                                    join user in db.tbl_usuarios on a.id_Personal_Inspector equals user.id_usuario                                    
                                    join d in db.tbl_EmpresaColaboradora on a.id_EmpresaColaboradora equals d.id_EmpresaColaboradora
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
                                        resul_apellidos_Personal = user.datos_personales,///resul
                                        resul_razonsocial_empresa =(db.tbl_Empresas.Where(c => c.id_Empresas == a.id_Empresa)).FirstOrDefault(),//resul
                                        empresa_razonsocial_empresa = d.RazonSocial_EmpresaColaboradora, //empresa
                                        jefeObra_apellidos_Personal = e.apellidos_Personal + " " + e.nombres_Personal, //jefeObra
                                        jefeCoordinador_apellidos_Personal = f.apellidos_Personal + " " + f.nombres_Personal, //jefeCoordinador
                                        jefeCuadrilla = i.apellidos_Personal + " " + i.nombres_Personal,
                                        g.descripcion_Area, // area
                                        h.nombre_delegacion //delegacion


                                    }).First();
           try
           {

               SelectPdf.HtmlToPdf converter = new SelectPdf.HtmlToPdf();
               string path = string.Format("{0}/{1}", System.Web.Hosting.HostingEnvironment.MapPath("~/Pdf"), pdf);
               string path2 = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", "logo.png");
               string path3;
               string nivel;
               if (obj_inspeccionCab.id_NivelInspeccion == 1)
               {
                   path3 = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", "normal.png");
                   nivel = "Normal";
               }
               else
               {
                   if (obj_inspeccionCab.id_NivelInspeccion == 2)
                   {
                       path3 = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", "bajo.png");
                       nivel = "Bajo";
                   }
                   else
                   {
                       if (obj_inspeccionCab.id_NivelInspeccion == 3)
                       {
                           path3 = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", "intermedio.png");
                           nivel = "Intermedio";
                       }
                       else
                       {
                           path3 = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", "alto.png");
                           nivel = "Alto";
                       }
                   }
               }
               var stringHtml = "<div><style type='text/css'>.tableHeader{width: 100%;}.tableHeader h1{color:#9E9E9E;font-family: GillSans, Calibri, Trebuchet, sans-serif;font-size:28px;font-weight: 600;}.tableHeader p{font:menu;font-size:11px;color:#9E9E9E;}.inspeccionCab{text-align: center;font:menu;padding-top: 10px;}.inspeccionCab .content1{border: 1px black solid;}.inspeccionCab .content1 h1{background-color: #9E9E9E;border-bottom: 1px black solid;margin: 0px;font-size:19px;}.inspeccionCab .content2 h1, .content3 h1{background-color: #9E9E9E;border-top : 1px solid black;border-left : 1px solid black;border-right : 1px solid black;margin: 0px;font-size:18px;font-family: Arial, Helvetica Neue, Helvetica, sans-serif;font-weight: 500}.inspeccionCab p{color : black;font-weight: bold;font-family: Arial,Helvetica Neue,Helvetica,sans-serif}.inspeccionCab .content1 table{padding-top: 10px;padding-bottom: 10px;}.inspeccionCab .content1 table td{padding-left : 40px;}.inspeccionCab .content1 .contentImg{padding-left: 170px;}.inspeccionCab .content1 table tr{text-align: left;}.tblCuadrilla, .tblVerificación{width: 100%;}.tblCuadrilla th{font-family: Arial,Helvetica Neue,Helvetica,sans-serif;text-align: center;}.tblCuadrilla td{font-family: Arial,Helvetica Neue,Helvetica,sans-serif;}.tblVerificación th{background-color: #e6e6e6;font-family: Arial, Helvetica Neue, Helvetica, sans-serif;}.tblVerificación td{text-align: left;font-family: Arial, Helvetica Neue, Helvetica, sans-serif;}.contentEvidencias h1{font-family: Arial, Helvetica Neue, Helvetica, sans-serif;text-align: center;font-size:17px;text-decoration:underline;}</style><table class='tableHeader'><thead><th><img src='" + path2 + "' /></th><th><h1>INFORME DE INSPECCIÓN</h1></th><th><p>Página 1 de 1</p></th></thead></table><div class='inspeccionCab'><div class='content1'><h1>DATOS DE INSPECCIÓN</h1><table><tbody><tr><td><p>Fecha de Inspección : " + obj_inspeccionCab.fecha_Inspeccion + " </p></td><td rowspan='4' class='contentImg'><p><img src='" + path3 + "'></p><p>Nivel de Inspeccion : " + nivel + "</p></td></tr><tr><td><p>Empresa : " + obj_inspeccionCab.resul_razonsocial_empresa + " </p></td></tr><tr><td><p>Inspección : " + inspeccion + " </p></td></tr><tr><td><p>Empresa Contratista : " + obj_inspeccionCab.empresa_razonsocial_empresa + "</p></td></tr><tr><td>" +
                   "<p>Jefe de Cuadrilla : " + obj_inspeccionCab.jefeCuadrilla + "</p></td></tr><tr><td><p>Matrícula del Vehículo : " + obj_inspeccionCab.placa_Inspeccion + "</p></td></tr><tr><td><p>Dirección : " + obj_inspeccionCab.lugar_Inspeccion + "</p></td></tr><tr><td><p>Área : " + obj_inspeccionCab.descripcion_Area + "</p></td></tr><tr><td><p>Delegación : " + obj_inspeccionCab.nombre_delegacion + "</p></td></tr><tr><td><p>Jefe de obra : " + obj_inspeccionCab.jefeObra_apellidos_Personal + "</p></td><td><p>Trabajo a Realizar : " + obj_inspeccionCab.trabajoArealizar_Inspeccion + "</p></td></tr><tr><td><p>Coordinador : " + obj_inspeccionCab.jefeCoordinador_apellidos_Personal + "</p></td><td><p>Inspección conjunta : " + obj_inspeccionCab.id_NivelInspeccion + "</p></td></tr><tr><td>" +
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
               ///

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
                   if (item.id_anomalia != 0)
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
               }

               stringHtml += "</tbody></table></div></div><br style='page-break-after: always;'><div class='contentEvidencias'><h1>EVIDENCIAS DEL INCUMPLIMIENTO</h1>";

               foreach (var item in detalle)
               {
                   var detalleFoto = (from a in db.tbl_Inspeccion_Cab_Detalle_Foto where a.id_inspeccion_detalle == item.id_inspeccion_detalle select new { a.nombre_foto }).ToList();
                   foreach (var itemFoto in detalleFoto)
                   {
                       string img = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", itemFoto.nombre_foto);
                       stringHtml += "<div align='center'  style='page-break-after: always;padding:20px 0 20px 0;'>";
                       stringHtml += "<img src='" + img + "' width='800' height='600'>";
                       stringHtml += "</div>";
                   }

               }

               stringHtml += "</div>";
               stringHtml += "<div class='contentEvidencias'><h1>Levantamientos</h1>";

               foreach (var item in detalle)
               {
                   if (!item.foto_levantamiento.Equals("VACIO"))
                   {
                       string img = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", item.foto_levantamiento);
                       stringHtml += "<div align='center'  style='page-break-after: always;padding:20px 0 20px 0;'>";
                       stringHtml += "<img src='" + img + "'  width='800' height='600'>";
                       stringHtml += "<p>" + item.descripcion_levantamiento + "</p>";
                       stringHtml += "</div>";

                   }
               }



               stringHtml += "</div>";
               converter.Options.MarginTop = 15;
               converter.Options.MarginLeft = 5;
               converter.Options.MarginRight = 5;
               SelectPdf.PdfDocument doc = converter.ConvertHtmlString(stringHtml);
               doc.Save(path);
               doc.Close();

               ruta_descarga = "1|" + string.Format("http://www.cobraperu.com/webApiInspecciones/Pdf/{0}", pdf);


           }
           catch (Exception ex)
           {
               ruta_descarga = "0|" + ex.Message;
           }
           return ruta_descarga;
       }


        public string GetPdf_individual(int inspeccion, string pdf, int formato)
        {
            string ruta_descarga = "";

            string pathExist = string.Format("{0}/{1}", System.Web.Hosting.HostingEnvironment.MapPath("~/Pdf"), pdf);
            if (System.IO.File.Exists(pathExist))
            {
                System.IO.File.Delete(pathExist);
            }

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
                                         empresa_razonsocial_empresa = d.RazonSocial_EmpresaColaboradora, //empresa
                                         empresa_subcontrata = j.RazonSocial_EmpresaColaboradora,// empresa sub contrata
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
                string path2 = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", "logo.png");
                string path3;
                string nivel;
                if (obj_inspeccionCab.id_NivelInspeccion == 1)
                {
                    path3 = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", "normal.png");
                    nivel = "Normal";
                }
                else
                {
                    if (obj_inspeccionCab.id_NivelInspeccion == 2)
                    {
                        path3 = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", "bajo.png");
                        nivel = "Bajo";
                    }
                    else
                    {
                        if (obj_inspeccionCab.id_NivelInspeccion == 3)
                        {
                            path3 = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", "intermedio.png");
                            nivel = "Intermedio";
                        }
                        else
                        {
                            path3 = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", "alto.png");
                            nivel = "Alto";
                        }
                    }
                }
                var stringHtml = "<div><style type='text/css'>.responsive-image {  height: auto;  width: 100%;}.tableHeader{width: 100%;}.tableHeader h1{color:#9E9E9E;font-family: GillSans, Calibri, Trebuchet, sans-serif;font-size:28px;font-weight: 600;}.tableHeader p{font:menu;font-size:11px;color:#9E9E9E;}.inspeccionCab{text-align: center;font:menu;padding-top: 10px;}.inspeccionCab .content1{border: 1px black solid;}.inspeccionCab .content1 h1{background-color: #9E9E9E;border-bottom: 1px black solid;margin: 0px;font-size:19px;}.inspeccionCab .content2 h1, .content3 h1{background-color: #9E9E9E;border-top : 1px solid black;border-left : 1px solid black;border-right : 1px solid black;margin: 0px;font-size:18px;font-family: Arial, Helvetica Neue, Helvetica, sans-serif;font-weight: 500}.inspeccionCab p{color : black;font-weight: bold;font-family: Arial,Helvetica Neue,Helvetica,sans-serif}.inspeccionCab .content1 table{padding-top: 10px;padding-bottom: 10px;}.inspeccionCab .content1 table td{padding-left : 40px;}.inspeccionCab .content1 .contentImg{padding-left: 170px;}.inspeccionCab .content1 table tr{text-align: left;}.tblCuadrilla, .tblVerificación{width: 100%;}.tblCuadrilla th{font-family: Arial,Helvetica Neue,Helvetica,sans-serif;text-align: center;}.tblCuadrilla td{font-family: Arial,Helvetica Neue,Helvetica,sans-serif;}.tblVerificación th{background-color: #e6e6e6;font-family: Arial, Helvetica Neue, Helvetica, sans-serif;}.tblVerificación td{text-align: left;font-family: Arial, Helvetica Neue, Helvetica, sans-serif;}.contentEvidencias h1{font-family: Arial, Helvetica Neue, Helvetica, sans-serif;text-align: center;font-size:17px;text-decoration:underline;}</style><table class='tableHeader'><thead><th><img src='" + path2 + "' /></th><th><h1>INFORME DE INSPECCIÓN</h1></th><th><p>Página 1 de 1</p></th></thead></table><div class='inspeccionCab'><div class='content1'><h1>DATOS DE INSPECCIÓN</h1><table><tbody><tr><td><p>Fecha de Inspección : " + obj_inspeccionCab.fecha_Inspeccion + " </p></td><td rowspan='4' class='contentImg'><p><img src='" + path3 + "'></p><p>Nivel de Inspeccion : " + nivel + "</p></td></tr><tr><td><p>Proyecto : " + obj_inspeccionCab.resul_razonsocial_empresa.nombre_proyecto + " </p></td></tr>" +
                    "<tr><td><p>Inspección : " + inspeccion + "</p></td></tr>" +
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
                        string img = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", itemFoto.nombre_foto);
                        //string img = string.Format("http://www.dsige.com/dsige_Inspeccion_WebApi/Imagen/{0}", itemFoto.nombre_foto);
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
                            string img = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", item.foto_levantamiento);
                            //string img = string.Format("http://www.dsige.com/dsige_Inspeccion_WebApi/Imagen/{0}", item.foto_levantamiento);
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
                                string img = string.Format("http://www.cobraperu.com/webApiInspecciones/Imagen/{0}", item.foto_levantamiento);
                                //string img = string.Format("http://www.dsige.com/dsige_Inspeccion_WebApi/Imagen/{0}", item.foto_levantamiento);
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

                ruta_descarga = "1|" + string.Format("http://www.cobraperu.com/webApiInspecciones/Pdf/{0}", pdf);


            }
            catch (Exception ex)
            {
                ruta_descarga = "0|" + ex.Message;
            }
            return ruta_descarga;
        }

        public DataTable Get_DetallesAnomalias(int tipoFormato, int id_inspeccion)
        {
            DataTable resultado = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_Reporte_Inspecciones", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@TipoFormato", SqlDbType.Int).Value = tipoFormato;
                        cmd.Parameters.Add("@Inspeccion", SqlDbType.Int).Value = id_inspeccion;

                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                            resultado = dt_detalle;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;

        }

        public string getIncidencia(DataTable Lista_Data, int id_personal, int incidencia)
        {
            string cant = "";

            for (int i = 0; i < Lista_Data.Rows.Count; i++)
            {
                if (id_personal == Convert.ToInt32(Lista_Data.Rows[i]["id_personal"].ToString()) && incidencia == Convert.ToInt32(Lista_Data.Rows[i]["id_anomalia"].ToString()))
                {
                    cant = "<td>" + Lista_Data.Rows[i]["codigo_Anomalia"].ToString() + "</td>";
                    cont += 1;
                    break;
                }
            }

            return cant;
        }

        public List<BandejaAtencionInspeccion_E> Listando_CargosPersonal()
        {
            try
            {
                List<BandejaAtencionInspeccion_E> obj_List = new List<BandejaAtencionInspeccion_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_CARGO_PERSONAL", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
 
                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                BandejaAtencionInspeccion_E Entidad = new BandejaAtencionInspeccion_E();

                                Entidad.id_Cargo = Convert.ToInt32(row["id_Cargo"].ToString());
                                Entidad.nombre_cargo = row["nombre_cargo"].ToString(); 

                                obj_List.Add(Entidad);
                            }
                        }
                    }
                }

                return obj_List;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public string Guardando_NroSancionAnomalias(string id_inspeccion)
        {
            string resultado = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_U_BANDEJA_ATENCION_INSPECCIONES", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_inspeccion", SqlDbType.Int).Value =Convert.ToInt32(id_inspeccion);
                        cmd.ExecuteNonQuery();
                        resultado = "OK";
                    }
                }
            }
            catch (Exception e)
            {
                resultado = e.Message;
            }
            return resultado;
        }


        public List<BandejaAtencionInspeccion_E> Listando_Listado_Inspecccion_Cabecera(int id_personal)
        {
            try
            {
                List<BandejaAtencionInspeccion_E> obj_List = new List<BandejaAtencionInspeccion_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("USP_LIST_INSPECCION_CAB_WEB_NEW", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_personal", SqlDbType.Int).Value = id_personal;

                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                BandejaAtencionInspeccion_E Entidad = new BandejaAtencionInspeccion_E();

                                Entidad.id_Pais = Convert.ToInt32(row["id_Pais"].ToString());
                                Entidad.id_grupo = Convert.ToInt32(row["id_grupo"].ToString());
                                Entidad.id_Empresa = Convert.ToInt32(row["id_Empresa"].ToString());
                                Entidad.id_Delegacion = Convert.ToInt32(row["id_Delegacion"].ToString());

                                Entidad.id_Inspeccion = Convert.ToInt32(row["id_Inspeccion"].ToString());
                                Entidad.id_Actividad = Convert.ToInt32(row["id_Actividad"].ToString());
                                Entidad.nro_inspeccion = row["nro_inspeccion"].ToString();
                                Entidad.fecha_inspeccion = row["fecha_inspeccion"].ToString();
                                Entidad.cargo = row["cargo"].ToString();
                                Entidad.nombre = row["nombre"].ToString();
                                Entidad.jefe_area = row["jefe_area"].ToString();
                                Entidad.inspector = row["inspector"].ToString();

                                Entidad.nivel = row["nivel"].ToString();
                                Entidad.anomalia = row["anomalia"].ToString();
                                Entidad.levanto_obs = row["levanto_obs"].ToString();
                                Entidad.fecha = row["fecha"].ToString();
                                Entidad.nro_insp_relacionada = row["nro_insp_relacionada"].ToString();

                                Entidad.id_EmpresaColaboradora = Convert.ToInt32(row["id_EmpresaColaboradora"].ToString());
                                Entidad.lugar_Inspeccion = row["lugar_Inspeccion"].ToString();
                                Entidad.actividadOT_Inspeccion = row["actividadOT_Inspeccion"].ToString();
                                Entidad.trabajoArealizar_Inspeccion = row["trabajoArealizar_Inspeccion"].ToString();

                                Entidad.id_Cargo = Convert.ToInt32(row["id_Cargo"].ToString());
                                Entidad.id_Personal_Inspeccionado = Convert.ToInt32(row["id_Personal_Inspeccionado"].ToString());
                                Entidad.id_Area = Convert.ToInt32(row["id_Area"].ToString());
                                Entidad.id_Personal_Coordinador = Convert.ToInt32(row["id_Personal_Coordinador"].ToString());

                                Entidad.id_Personal_JefeObra = Convert.ToInt32(row["id_Personal_JefeObra"].ToString());
                                Entidad.placa_Inspeccion = row["placa_Inspeccion"].ToString();
                                Entidad.id_NivelInspeccion = Convert.ToInt32(row["id_NivelInspeccion"].ToString());
                                Entidad.id_TipoInspeccion = Convert.ToInt32(row["id_TipoInspeccion"].ToString());

                                Entidad.Resultado_Inspeccion = row["Resultado_Inspeccion"].ToString();
                                Entidad.iniciofin_Trabajo = row["iniciofin_Trabajo"].ToString();
                                Entidad.id_Anomalia = Convert.ToInt32(row["id_Anomalia"].ToString());
                                Entidad.descripcion_Inspeccion = row["descripcion_Inspeccion"].ToString();

                                Entidad.accionPropuesta_Correctiva = row["accionPropuesta_Correctiva"].ToString();
                                Entidad.id_Personal_Responsable = Convert.ToInt32(row["id_Personal_Responsable"].ToString());
                                Entidad.fechaPropuesta_Correctiva = row["fechaPropuesta_Correctiva"].ToString();
                                Entidad.observacion_Correctiva = row["observacion_Correctiva"].ToString();
                                Entidad.paralizacion_Correctiva = row["paralizacion_Correctiva"].ToString();
                                Entidad.sancion_Correctiva = row["sancion_Correctiva"].ToString();
                                Entidad.id_TipoSancion = Convert.ToInt32(row["id_TipoSancion"].ToString());
                                Entidad.nroTrabajadores_Correctiva = Convert.ToInt32(row["nroTrabajadores_Correctiva"].ToString());

                                Entidad.colorfondo = row["colorfondo"].ToString();
                                Entidad.colorFuente = row["colorFuente"].ToString();
                                Entidad.Obs_Levantada = row["Obs_Levantada"].ToString();
                                Entidad.nro_inspeccionRelacionada = row["nro_inspeccionRelacionada"].ToString();
                                Entidad.estado = Convert.ToInt32(row["estado"].ToString());
                                Entidad.id_Cliente = Convert.ToInt32(row["id_Cliente"].ToString());

                                Entidad.Flag_Nueva_Inspeccion = false;
                               if ( Convert.ToInt32(row["Flag_Nueva_Inspeccion"].ToString())  == 1){
                                   Entidad.Flag_Nueva_Inspeccion = true;
                               }
                                obj_List.Add(Entidad);
                            }
                        }
                    }
                }

                return obj_List;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public List<BandejaAtencionInspeccion_E> Listando_Listado_Inspecccion_Cabecera_new(int id_personal, int id_pais, int id_grupo, string idDelegacion, string idInspector, string idRespCorreccion, int opcion, string fecha_Ini, string fecha_fin)
        {
            try
            {
                List<BandejaAtencionInspeccion_E> obj_List = new List<BandejaAtencionInspeccion_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("NEW_LIST_INSPECCION_CAB_WEB_II", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_personal", SqlDbType.Int).Value = id_personal;
                        cmd.Parameters.Add("@id_pais", SqlDbType.Int).Value = id_pais;
                        cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo;
                        cmd.Parameters.Add("@idDelegacion", SqlDbType.VarChar).Value = idDelegacion;
                        cmd.Parameters.Add("@idInspector", SqlDbType.VarChar).Value = idInspector;
                        cmd.Parameters.Add("@idRespCorreccion", SqlDbType.VarChar).Value = idRespCorreccion;
                        cmd.Parameters.Add("@opcion", SqlDbType.Int).Value = opcion;

                        cmd.Parameters.Add("@fecha_Ini", SqlDbType.VarChar).Value = fecha_Ini;
                        cmd.Parameters.Add("@fecha_fin", SqlDbType.VarChar).Value = fecha_fin;

                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                BandejaAtencionInspeccion_E Entidad = new BandejaAtencionInspeccion_E();

                                Entidad.id_Pais = Convert.ToInt32(row["id_Pais"].ToString());
                                Entidad.pais = row["pais"].ToString();
                                Entidad.id_grupo = Convert.ToInt32(row["id_grupo"].ToString());
                                Entidad.grupo = row["grupo"].ToString();
                                Entidad.id_Delegacion = Convert.ToInt32(row["id_Delegacion"].ToString());
                                Entidad.delegacion = row["delegacion"].ToString();

                                Entidad.id_Inspeccion = Convert.ToInt32(row["id_Inspeccion"].ToString());
                                Entidad.id_Actividad = Convert.ToInt32(row["id_Actividad"].ToString());
                                Entidad.nro_inspeccion = row["nro_inspeccion"].ToString();
                                Entidad.fecha_inspeccion = row["fecha_inspeccion"].ToString();
                                Entidad.cargo = row["cargo"].ToString();
                                Entidad.nombre = row["nombre"].ToString();
                                Entidad.jefe_area = row["jefe_area"].ToString();
                                Entidad.inspector = row["inspector"].ToString();

                                Entidad.nivel = row["nivel"].ToString();
                                Entidad.anomalia = row["anomalia"].ToString();
                                Entidad.levanto_obs = row["levanto_obs"].ToString();
                                Entidad.fecha = row["fecha"].ToString();
                                Entidad.nro_insp_relacionada = row["nro_insp_relacionada"].ToString();

                                Entidad.id_EmpresaColaboradora = Convert.ToInt32(row["id_EmpresaColaboradora"].ToString());
                                Entidad.lugar_Inspeccion = row["lugar_Inspeccion"].ToString();
                                Entidad.actividadOT_Inspeccion = row["actividadOT_Inspeccion"].ToString();
                                Entidad.trabajoArealizar_Inspeccion = row["trabajoArealizar_Inspeccion"].ToString();

                                Entidad.id_Cargo = Convert.ToInt32(row["id_Cargo"].ToString());
                                Entidad.id_Personal_Inspeccionado = Convert.ToInt32(row["id_Personal_Inspeccionado"].ToString());
                                Entidad.id_Area = Convert.ToInt32(row["id_Area"].ToString());
                                Entidad.id_Personal_Coordinador = Convert.ToInt32(row["id_Personal_Coordinador"].ToString());

                                Entidad.id_Personal_JefeObra = Convert.ToInt32(row["id_Personal_JefeObra"].ToString());
                                Entidad.placa_Inspeccion = row["placa_Inspeccion"].ToString();
                                Entidad.id_NivelInspeccion = Convert.ToInt32(row["id_NivelInspeccion"].ToString());
                                Entidad.id_TipoInspeccion = Convert.ToInt32(row["id_TipoInspeccion"].ToString());

                                Entidad.Resultado_Inspeccion = row["Resultado_Inspeccion"].ToString();
                                Entidad.iniciofin_Trabajo = row["iniciofin_Trabajo"].ToString();
                                Entidad.id_Anomalia = Convert.ToInt32(row["id_Anomalia"].ToString());
                                Entidad.descripcion_Inspeccion = row["descripcion_Inspeccion"].ToString();

                                Entidad.accionPropuesta_Correctiva = row["accionPropuesta_Correctiva"].ToString();
                                Entidad.id_Personal_Responsable = Convert.ToInt32(row["id_Personal_Responsable"].ToString());
                                Entidad.fechaPropuesta_Correctiva = row["fechaPropuesta_Correctiva"].ToString();
                                Entidad.observacion_Correctiva = row["observacion_Correctiva"].ToString();
                                Entidad.paralizacion_Correctiva = row["paralizacion_Correctiva"].ToString();
                                Entidad.sancion_Correctiva = row["sancion_Correctiva"].ToString();
                                Entidad.id_TipoSancion = Convert.ToInt32(row["id_TipoSancion"].ToString());
                                Entidad.nroTrabajadores_Correctiva = Convert.ToInt32(row["nroTrabajadores_Correctiva"].ToString());

                                Entidad.colorfondo = row["colorfondo"].ToString();
                                Entidad.colorFuente = row["colorFuente"].ToString();
                                Entidad.Obs_Levantada = row["Obs_Levantada"].ToString();
                                Entidad.nro_inspeccionRelacionada = row["nro_inspeccionRelacionada"].ToString();
                                Entidad.estado = Convert.ToInt32(row["estado"].ToString());
                                Entidad.id_Cliente = Convert.ToInt32(row["id_Cliente"].ToString());
                                Entidad.empresa = row["RazonSocial_EmpresaColaboradora"].ToString();
                                Entidad.cliente = row["nombre_cliente"].ToString();
                                Entidad.Flag_Nueva_Inspeccion = false;
                                Entidad.actividad = row["codigo_actividad"].ToString();
                                Entidad.responsableInspeccionar = row["responsableInspeccionar"].ToString();
                                Entidad.descripcion_area = row["descripcion_area"].ToString();
                                Entidad.coordinador = row["coordinador"].ToString();
                                Entidad.jefeobras = row["jefeobras"].ToString();
                                Entidad.descripcion_tipoinspeccion = row["descripcion_tipoinspeccion"].ToString();

                                if (Convert.ToInt32(row["Flag_Nueva_Inspeccion"].ToString()) == 1)
                                {
                                    Entidad.Flag_Nueva_Inspeccion = true;
                                }
                                obj_List.Add(Entidad);
                            }
                        }
                    }
                }

                return obj_List;
            }
            catch (Exception e)
            {

                throw e;
            }
        }

        public object mostrando_tecnicosCargo(int idCargo)
        {
            Resultado res = new Resultado();
            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_BANDEJA_ATENCION_COMBO_TECNICOS_CARGO", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idCargo", SqlDbType.Int).Value = idCargo;


                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                            res.ok = true;
                            res.data = dt_detalle;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                res.ok = false;
                res.data = ex.Message;
            }
            return res;
        }

    }
}