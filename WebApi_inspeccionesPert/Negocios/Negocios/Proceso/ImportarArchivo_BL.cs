using Entidades.Procesos;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.OleDb;
using System.Data.SqlClient;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Procesos
{
    public class ImportarArchivo_BL
    {
        string cn = System.Configuration.ConfigurationManager.ConnectionStrings["conexionDsige"].ConnectionString;
      
        public string Get_ImportarArchivo(int user)  ///--- informacion del personal importado...
        {

            string Resultado = null;

            DataTable dt_detalle = new DataTable();
            try
            {
                string rutaExcel = "";
                rutaExcel = System.Web.Hosting.HostingEnvironment.MapPath("~/ArchivosExcel/ImportarPersonal.xlsx");

                String strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaExcel + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                OleDbConnection MyConnection;

                OleDbDataAdapter MyCommand = null;
                MyConnection = new OleDbConnection(strExcelConn);

                try
                {
                    MyCommand = new OleDbDataAdapter("select * from [Importar$]", MyConnection);
                    MyCommand.Fill(dt_detalle);
                }
                catch (Exception)
                {
                    throw;
                }
                //---agregando nueva columna
                dt_detalle.Columns.Add("existe_reniec");

                foreach (DataRow row in dt_detalle.Rows)
                {
                    if (string.IsNullOrEmpty(row[6].ToString()))
                    {
                        row["existe_reniec"] = "0";
                    } else {
                            if (row[0].ToString().ToUpper() == "PERU")
                            {
                                if (row[5].ToString().ToUpper() == "DNI" )
                                {
                                    var resultado = ConsultaReniec_sync(row[6].ToString().Replace(" ", ""));
                                    string[] objs = resultado.Result.ToString().Split('|');

                                    if (string.IsNullOrEmpty(objs[0]))
                                    {
                                        row["existe_reniec"] = "0";
                                    }
                                    else
                                    {
                                        row["existe_reniec"] = "1";
                                        row["Apellidos"] = objs[0] + " " + objs[1];
                                        row["Nombres"] = objs[2];
                                    }
                                }
                                else
                                {
                                    row["existe_reniec"] = "1";
                                }
                            } else {
                                row["existe_reniec"] = "1";
                            }                       
                    }
                }

                using (SqlConnection con = new SqlConnection(cn))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_D_TEMPORAL_PERSONAL_II", con))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = user;
                        cmd.ExecuteNonQuery();
                    }

                    //guardando al informacion de la importacion
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                    {
                        bulkCopy.BatchSize = 500;
                        bulkCopy.NotifyAfter = 1000;
                        bulkCopy.DestinationTableName = "TEMPORAL_PERSONAL_NEW";
                        bulkCopy.WriteToServer(dt_detalle);

                        //Actualizando campos 
                        string Sql = "UPDATE TEMPORAL_PERSONAL_NEW SET  id_usuario_importa ='" + user + "' , fecha_Importa=getdate() , existe_codigo = 0  WHERE id_usuario_importa IS NULL  ";

                        using (SqlCommand cmd = new SqlCommand(Sql, con))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                        }
                    }

                    Resultado = "1|OK";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Resultado;
        }

        static async Task<string> ConsultaReniec_sync(string nroDocumento)
        {
            string resultado = "";
            var url = "http://aplicaciones007.jne.gob.pe/srop_publico/Consulta/Afiliado/GetNombresCiudadano?DNI=" + nroDocumento;
            var web_request = System.Net.WebRequest.Create(url);

            using (var response = web_request.GetResponse())
            {
                using (StreamReader reader = new StreamReader(response.GetResponseStream()))
                {
                    resultado = await reader.ReadToEndAsync();
                }
            }

            return resultado;
        }

        string cadenaCnx = System.Configuration.ConfigurationManager.ConnectionStrings["conexionDsige"].ConnectionString;
        
        public List<ImportarArchivo_E> ListaAgrupadoTemporal(int usuario) //----agregando la informacion que no exista..
        {
            try
            {
                List<ImportarArchivo_E> obj_List = new List<ImportarArchivo_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_PERSONAL_TEMPORAL_AGRUPADO_NEW", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = usuario;
                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                ImportarArchivo_E Entidad = new ImportarArchivo_E();
                                                              
                                Entidad.id_pais = Convert.ToInt32(row["id_pais"].ToString());
                                Entidad.Nombre_Pais = row["Nombre_Pais"].ToString();

                                Entidad.id_grupo = Convert.ToInt32(row["id_grupo"].ToString());
                                Entidad.Nombre_Grupo = row["Nombre_Grupo"].ToString();

                                Entidad.id_Delegacion = Convert.ToInt32(row["id_Delegacion"].ToString());
                                Entidad.Delegacion =  row["Delegacion"].ToString();
 
                                Entidad.id_EmpresaColaboradora = Convert.ToInt32(row["id_EmpresaColaboradora"].ToString());
                                Entidad.RUC_Empresa_Colaboradora = row["RUC_Empresa_Colaboradora"].ToString();
                                Entidad.Nombre_Empresa_Colaboradora = row["Nombre_Empresa_Colaboradora"].ToString();

                                Entidad.Tipo_documento =  row["Tipo_documento"].ToString();
                                Entidad.mensaje_reniec = row["mensaje_reniec"].ToString();
                                Entidad.Nro_Documento =  row["Nro_Documento"].ToString();

                                Entidad.Apellidos =  row["Apellidos"].ToString();
                                Entidad.Nombres =  row["Nombres"].ToString();
                                Entidad.id_Cargo = Convert.ToInt32(row["id_Cargo"].ToString());
                                Entidad.Cargo =  row["Cargo"].ToString();

                                Entidad.Estado =  row["Estado"].ToString();
                                Entidad.Email =  row["Email"].ToString();
                                Entidad.Codigo = row["Codigo"].ToString();

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

        public string Guardando_Personal_Masivo(int usuario)
        {
            string resultado = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_I_PERSONAL_MASIVO_NEW", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@usuario", SqlDbType.Int).Value = usuario;
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

        
        public string Get_ImportarArchivo_Usuario(int user)
        {

            string Resultado = null;

            DataTable dt_detalle = new DataTable();
            try
            {
                string rutaExcel = "";
                rutaExcel = System.Web.Hosting.HostingEnvironment.MapPath("~/ArchivosExcel/" + user + "_ImportarUsuarioMasivo.xlsx");
                String strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaExcel + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";

                OleDbConnection MyConnection;

                OleDbDataAdapter MyCommand = null;
                MyConnection = new OleDbConnection(strExcelConn);

                try
                {
                    MyCommand = new OleDbDataAdapter("select * from [Importar$]", MyConnection);
                    MyCommand.Fill(dt_detalle);
                }
                catch (Exception)
                {
                    throw;
                }

                using (SqlConnection con = new SqlConnection(cn))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_D_TEMPORAL_USUARIO", con))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = user;
                        cmd.ExecuteNonQuery();
                    }

                    //guardando al informacion de la importacion
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                    {

                        bulkCopy.BatchSize = 500;
                        bulkCopy.NotifyAfter = 1000;
                        bulkCopy.DestinationTableName = "TEMPORAL_USUARIO";
                        bulkCopy.WriteToServer(dt_detalle);

                        //Actualizando campos 

                        string Sql = "UPDATE TEMPORAL_USUARIO SET  id_usuario_importa ='" + user + "' , fecha_Importa=getdate()  WHERE id_usuario_importa IS NULL  ";

                        using (SqlCommand cmd = new SqlCommand(Sql, con))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    
                    using (SqlCommand cmd = new SqlCommand("SP_I_USUARIO_MASIVO_V2", con))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = user;
                        cmd.ExecuteNonQuery();
                    }
                }
                Resultado = "OK";
            }
            catch (Exception ex)
            {
                throw new ArgumentNullException(ex.Message);
            }
            return Resultado;
        }

        public List<ImportarArchivo_E> ListaAgrupadoTemporal_Alerta(int usuario) //----agregando la informacion que no exista..
        {
            try
            {
                List<ImportarArchivo_E> obj_List = new List<ImportarArchivo_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_PERSONAL_TEMPORAL_AGRUPADO_ALERTA_NEW", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = usuario;
                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                ImportarArchivo_E Entidad = new ImportarArchivo_E();

                                Entidad.id_pais = Convert.ToInt32(row["id_pais"].ToString());
                                Entidad.Nombre_Pais = row["Nombre_Pais"].ToString();

                                Entidad.id_grupo = Convert.ToInt32(row["id_grupo"].ToString());
                                Entidad.Nombre_Grupo = row["Nombre_Grupo"].ToString();

                                Entidad.id_Delegacion = Convert.ToInt32(row["id_Delegacion"].ToString());
                                Entidad.Delegacion = row["Delegacion"].ToString();

                                Entidad.id_EmpresaColaboradora = Convert.ToInt32(row["id_EmpresaColaboradora"].ToString());
                                Entidad.RUC_Empresa_Colaboradora = row["RUC_Empresa_Colaboradora"].ToString();
                                Entidad.Nombre_Empresa_Colaboradora = row["Nombre_Empresa_Colaboradora"].ToString();

                                Entidad.Tipo_documento = row["Tipo_documento"].ToString();
                                Entidad.mensaje_reniec = row["mensaje_reniec"].ToString();
                                Entidad.Nro_Documento = row["Nro_Documento"].ToString();

                                Entidad.Apellidos = row["Apellidos"].ToString();
                                Entidad.Nombres = row["Nombres"].ToString();
                                Entidad.id_Cargo = Convert.ToInt32(row["id_Cargo"].ToString());
                                Entidad.Cargo = row["Cargo"].ToString();

                                Entidad.Estado = row["Estado"].ToString();
                                Entidad.Email = row["Email"].ToString();
                                Entidad.Codigo = row["Codigo"].ToString();

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




        public string Get_ImportarArchivo_OT(int user)  ///--- informacion del personal importado...
        {

            string Resultado = null;

            DataTable dt_detalle = new DataTable();
            try
            {
                string rutaExcel = "";
                rutaExcel = System.Web.Hosting.HostingEnvironment.MapPath("~/ArchivosExcel/ImportarOT"+ user + ".xlsx");

                String strExcelConn = "Provider=Microsoft.ACE.OLEDB.12.0;Data Source=" + rutaExcel + ";Extended Properties=\"Excel 12.0;HDR=Yes;IMEX=2\"";
                OleDbConnection MyConnection;

                OleDbDataAdapter MyCommand = null;
                MyConnection = new OleDbConnection(strExcelConn);

                try
                {
                    MyCommand = new OleDbDataAdapter("select * from [Importar$]", MyConnection);
                    MyCommand.Fill(dt_detalle);
                }
                catch (Exception)
                {
                    throw;
                }

                using (SqlConnection con = new SqlConnection(cn))
                {
                    con.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_D_TEMPORAL_OT", con))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = user;
                        cmd.ExecuteNonQuery();
                    }

                    //guardando al informacion de la importacion
                    using (SqlBulkCopy bulkCopy = new SqlBulkCopy(con))
                    {
                        bulkCopy.BatchSize = 500;
                        bulkCopy.NotifyAfter = 1000;
                        bulkCopy.DestinationTableName = "TEMPORAL_OT";
                        bulkCopy.WriteToServer(dt_detalle);

                        //Actualizando campos 
                        string Sql = "UPDATE TEMPORAL_OT SET  id_usuario_importa ='" + user + "' , fecha_Importa=getdate()  , existe_ot_bd = 0 WHERE id_usuario_importa IS NULL  ";

                        using (SqlCommand cmd = new SqlCommand(Sql, con))
                        {
                            cmd.CommandTimeout = 0;
                            cmd.CommandType = CommandType.Text;
                            cmd.ExecuteNonQuery();
                        }
                    }
                    Resultado = "1|OK";
                }
            }
            catch (Exception)
            {
                throw;
            }
            return Resultado;
        }

        public List<ImportarArchivo_E> ListaAgrupadoTemporal_ot(int usuario) //----agregando la informacion que no exista..
        {
            try
            {
                List<ImportarArchivo_E> obj_List = new List<ImportarArchivo_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_OT_TEMPORAL_AGRUPADO", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = usuario;
                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                ImportarArchivo_E Entidad = new ImportarArchivo_E();
                                
                                Entidad.Codigo_ot = row["Codigo_ot"].ToString();
                                Entidad.Nombre_ot = row["Nombre_ot"].ToString();
                                Entidad.id_Delegacion = Convert.ToInt32(row["id_Delegacion"].ToString());
                                Entidad.Codigo_delegacion = row["Codigo_delegacion"].ToString();

                                Entidad.id_Cliente =  Convert.ToInt32(row["id_Cliente"].ToString());
                                Entidad.Ruc_cliente = row["Ruc_cliente"].ToString();
                                Entidad.id_Proyecto = Convert.ToInt32(row["id_Proyecto"].ToString());
                                Entidad.Nombre_proyecto = row["Nombre_proyecto"].ToString();

                                Entidad.id_jefe = Convert.ToInt32(row["id_jefe"].ToString());
                                Entidad.Dni_jefeObra = row["Dni_jefeObra"].ToString();
                                Entidad.id_cordinador = Convert.ToInt32(row["id_cordinador"].ToString());
                                Entidad.Dni_coordinado = row["Dni_coordinado"].ToString();

                                Entidad.id_Actividad = Convert.ToInt32(row["id_Actividad"].ToString());
                                Entidad.Codigo_actividad = row["Codigo_actividad"].ToString();
                                Entidad.mensaje = row["mensaje"].ToString();

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


        public string Guardando_OT_Masivo(int usuario)
        {
            string resultado = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_I_OT_MASIVO", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@usuario", SqlDbType.Int).Value = usuario;
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

        public List<ImportarArchivo_E> ListaAgrupadoTemporal_ot_alertas(int usuario) //----agregando la informacion que no exista..
        {
            try
            {
                List<ImportarArchivo_E> obj_List = new List<ImportarArchivo_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_OT_TEMPORAL_AGRUPADO_ALERTA", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = usuario;
                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                ImportarArchivo_E Entidad = new ImportarArchivo_E();

                                Entidad.Codigo_ot = row["Codigo_ot"].ToString();
                                Entidad.Nombre_ot = row["Nombre_ot"].ToString();
                                Entidad.id_Delegacion = Convert.ToInt32(row["id_Delegacion"].ToString());
                                Entidad.Codigo_delegacion = row["Codigo_delegacion"].ToString();

                                Entidad.id_Cliente = Convert.ToInt32(row["id_Cliente"].ToString());
                                Entidad.Ruc_cliente = row["Ruc_cliente"].ToString();
                                Entidad.id_Proyecto = Convert.ToInt32(row["id_Proyecto"].ToString());
                                Entidad.Nombre_proyecto = row["Nombre_proyecto"].ToString();

                                Entidad.id_jefe = Convert.ToInt32(row["id_jefe"].ToString());
                                Entidad.Dni_jefeObra = row["Dni_jefeObra"].ToString();
                                Entidad.id_cordinador = Convert.ToInt32(row["id_cordinador"].ToString());
                                Entidad.Dni_coordinado = row["Dni_coordinado"].ToString();

                                Entidad.id_Actividad = Convert.ToInt32(row["id_Actividad"].ToString());
                                Entidad.Codigo_actividad = row["Codigo_actividad"].ToString();
                                Entidad.mensaje = row["mensaje"].ToString();

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




    }
}
