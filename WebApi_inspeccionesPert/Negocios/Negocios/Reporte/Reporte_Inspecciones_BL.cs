using Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Configuration;
using System.Data;
using System.Data.SqlClient;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = OfficeOpenXml;
using Style = OfficeOpenXml.Style;


namespace Negocios.Reporte
{
    public class Reporte_Inspecciones_BL
    {
        string cadenaCnx = System.Configuration.ConfigurationManager.ConnectionStrings["conexionDsige"].ConnectionString;

        public List<Reporte_Insepcciones_E> Listando_Inspeccciones(int id_pais, int id_grupo ,int  id_delegacion , string fecha_ini, string fecha_fin)
        {
            try
            {
                List<Reporte_Insepcciones_E> obj_List = new List<Reporte_Insepcciones_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("NEW_REPORTE_LISTADO_INSPECCIONES", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@id_pais", SqlDbType.Int).Value = id_pais;
                        cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo;
                        cmd.Parameters.Add("@id_delegacion", SqlDbType.Int).Value = id_delegacion;

                        cmd.Parameters.Add("@fecha_ini", SqlDbType.VarChar).Value = fecha_ini;
                        cmd.Parameters.Add("@fecha_fin", SqlDbType.VarChar).Value = fecha_fin;

                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Reporte_Insepcciones_E Entidad = new Reporte_Insepcciones_E();

                                Entidad.nro_visita = row["nro_visita"].ToString();
                                Entidad.fecha_insp = row["fecha_insp"].ToString();
                                Entidad.zona = row["zona"].ToString();
                                Entidad.delegacion = row["delegacion"].ToString();

                                Entidad.actividad = row["actividad"].ToString();
                                Entidad.cliente = row["cliente"].ToString();
                                Entidad.nro_inspeccion = row["nro_inspeccion"].ToString();
                                Entidad.anio = row["anio"].ToString();


                                Entidad.mes = row["mes"].ToString();
                                Entidad.semana = row["semana"].ToString();
                                Entidad.autor = row["autor"].ToString();

                                Entidad.puesto = row["puesto"].ToString();
                                Entidad.jefeObra = row["jefeObra"].ToString();
                                Entidad.empresa_resp_propia = row["empresa_resp_propia"].ToString();
                                Entidad.empresa_resp_subContrata = row["empresa_resp_subContrata"].ToString();

                                Entidad.tipoInspeccion = row["tipoInspeccion"].ToString();
                                Entidad.incio_final_trabajos = row["incio_final_trabajos"].ToString();
                                Entidad.inspeccion_conjunta = row["inspeccion_conjunta"].ToString();
                                Entidad.resp_inspeccion = row["resp_inspeccion"].ToString();

                                Entidad.nro_Anomalias = row["nro_Anomalias"].ToString();
                                Entidad.aspectoAnomalo = row["aspectoAnomalo"].ToString();
                                Entidad.Decripcion_anomalia = row["Decripcion_anomalia"].ToString();
                                Entidad.anomalia_critica = row["anomalia_critica"].ToString();
                                Entidad.noEliminar = row["noEliminar"].ToString();

                                Entidad.accionPropuesta = row["accionPropuesta"].ToString();
                                Entidad.respCorr = row["respCorr"].ToString();
                                Entidad.fecha_Prop_Corr = row["fecha_Prop_Corr"].ToString();
                                Entidad.fecha_Cierre_Corr = row["fecha_Cierre_Corr"].ToString(); 

                                Entidad.noEliminar2 = row["noEliminar2"].ToString(); 
                                Entidad.paralizacionTrabajos = row["paralizacionTrabajos"].ToString(); 
                                Entidad.sancion = row["sancion"].ToString(); 
                                Entidad.tipoSancion = row["tipoSancion"].ToString(); 
                                Entidad.nro_trabajadoresSancionados = row["nro_trabajadoresSancionados"].ToString(); 
                                Entidad.observaciones = row["observaciones"].ToString(); 
                                Entidad.propia = row["propia"].ToString(); 
                                Entidad.subContrata = row["subContrata"].ToString();
                                Entidad.nombre_inspeccionado = row["nombre_inspeccionado"].ToString();
                                Entidad.estado = row["estado"].ToString(); 

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
        
        public string ExportarInspecciones(int id_pais, int id_grupo, int id_delegacion, string fecha_ini, string fecha_fin)
        {

            string cadenaCnx = System.Configuration.ConfigurationManager.ConnectionStrings["conexionDsige"].ConnectionString;
            string resultado = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("NEW_REPORTE_LISTADO_INSPECCIONES", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@id_pais", SqlDbType.Int).Value = id_pais;
                        cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo;
                        cmd.Parameters.Add("@id_delegacion", SqlDbType.Int).Value = id_delegacion;

                        cmd.Parameters.Add("@fecha_ini", SqlDbType.VarChar).Value = fecha_ini;
                        cmd.Parameters.Add("@fecha_fin", SqlDbType.VarChar).Value = fecha_fin;

                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                            if (dt_detalle.Rows.Count > 0)
                            {
                                resultado = GenerarArchivoExcel(dt_detalle);
                            }
                            else
                            {
                                resultado = "0|No hay informacion para Mostrar.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = "0|" + ex.Message;
            }
            return resultado;

        }

        public string GenerarArchivoExcel(DataTable dt_detalles)
        {
            string Res = "";
            string _servidor;
            int _fila = 2;
            string FileRuta = "";
            string FileExcel = "";

            try
            {
                _servidor = String.Format("{0:ddMMyyyy_hhmmss}.xlsx", DateTime.Now);
                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/ArchivosExcel/ReporteInspecciones_" + _servidor);
                string rutaServer = ConfigurationManager.AppSettings["servidor_archivoExcel"];

                FileExcel = rutaServer + _servidor;
                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }
                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("BaseDatos");
                    var headerCells = oWs.Cells[1, 1, 1, 36];
                    var headerFont = headerCells.Style.Font;
                    headerFont.Bold = true;
                    headerFont.SetFromFont(new Font("Tahoma", 8));

                    for (int i = 1; i <= 36; i++)
                    {
                       oWs.Cells[1, i].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);    //marco

                       if (i == 1 || i == 2 || i == 8 || i == 9 || i == 10 || i == 11 || i == 12 || i == 13 || i == 16 || i == 17 || i == 18 || i == 19 || i == 22 || i == 27 || i == 28)
                       {
                           oWs.Cells[1, i].Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF")); // color de la letra
                           oWs.Cells[1, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;   // fondo de celda
                           oWs.Cells[1, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.SteelBlue); // fondo de celda
                       }
                       else if (i == 3 || i == 4 || i == 5 || i == 6 || i == 14 || i == 15 || i == 21 || i == 25 || i == 26 || i == 32 || i == 33 || i == 34 || i == 35 || i == 38)
                       {
                           oWs.Cells[1, i].Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF")); // color de la letra
                           oWs.Cells[1, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;   // fondo de celda
                           oWs.Cells[1, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Black); // fondo de celda
                       }
                       else if (i == 3 || i == 7 || i == 5 || i == 20 || i == 23 || i == 30 || i == 31)
                       {
                           oWs.Cells[1, i].Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#000000")); // color de la letra
                           oWs.Cells[1, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;   // fondo de celda
                           oWs.Cells[1, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Aquamarine); // fondo de celda
                       }
                    }

  
                    oWs.Cells[1, 1].Value = "Visita Nº";
                    oWs.Cells[1, 2].Value = "Fecha_insp";
                    
                    oWs.Cells[1, 3].Value = "Zona";
                    oWs.Cells[1, 4].Value = "Delegación";

                    oWs.Cells[1, 5].Value = "Actividad";
                    oWs.Cells[1, 6].Value = "Cliente";
                    oWs.Cells[1, 7].Value = "Nº_Inspecc";
                    oWs.Cells[1, 8].Value = "AÑO";
    
                    oWs.Cells[1, 9].Value = "MES";
                    oWs.Cells[1, 10].Value = "SEMANA";
                    oWs.Cells[1, 11].Value = "Autor";
                    oWs.Cells[1, 12].Value = "Puesto";
                    
                    oWs.Cells[1, 13].Value = "Jefe de Obra";

                    oWs.Cells[1, 14].Value = "Empresa_resp PROPIA";
                    oWs.Cells[1, 15].Value = "Empresa_resp SUBCONTRATA";

                    oWs.Cells[1, 16].Value = "Tipo_Insp";
                    oWs.Cells[1, 17].Value = "Inicio/Final trabajos";
                    oWs.Cells[1, 18].Value = "Inspección conjunta";
                    oWs.Cells[1, 19].Value = "Res_Insp";

                    oWs.Cells[1, 20].Value = "Nº_Anomalas";
                    oWs.Cells[1, 21].Value = "Aspecto_anómalo";
                    oWs.Cells[1, 22].Value = "Decripción_anomalía";
                    oWs.Cells[1, 23].Value = "Anom_crítica";

                    oWs.Cells[1, 24].Value = "NO ELIMINAR";
                    oWs.Cells[1, 25].Value = "Acción propuesta";

                    oWs.Cells[1, 26].Value = "Resp_Corr";
                    oWs.Cells[1, 27].Value = "Fecha_Prop_Corr";
                    oWs.Cells[1, 28].Value = "Fecha_Cierre_Corr";                                     							

                    oWs.Cells[1, 29].Value = "NO ELIMINAR";
                    oWs.Cells[1, 30].Value = "Paralización trabajos";

                    oWs.Cells[1, 31].Value = "Sanción";
                    oWs.Cells[1, 32].Value = "Tipo de Sanción";
                    oWs.Cells[1, 33].Value = "Nº trabajadores sancionados";

                    oWs.Cells[1, 34].Value = "Observaciones";
                    oWs.Cells[1, 35].Value = "PROPIA";
                    oWs.Cells[1, 36].Value = "SUBCONTRATA";
                    oWs.Cells[1, 37].Value = "NOMBRE DE INSPECCIONADO";
                    oWs.Cells[1, 38].Value = "Estado de Inspeccion";
                     
                    foreach (DataRow oBj in dt_detalles.Rows)
                    {

                        headerCells = oWs.Cells[_fila, 1, _fila, 38];
                        headerFont = headerCells.Style.Font;
                        headerFont.Bold = true;
                        headerFont.SetFromFont(new Font("Tahoma", 8));

                        for (int i = 1; i <= 38; i++)
                        {
                            oWs.Cells[_fila, i].Style.Font.Size = 8; //letra tamaño  
                            oWs.Cells[_fila, i].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);    //marco
                        }

                        oWs.Cells[_fila, 1].Value = oBj["nro_visita"].ToString();
                        oWs.Cells[_fila, 2].Value = oBj["fecha_insp"].ToString();
                        oWs.Cells[_fila, 3].Value = oBj["zona"].ToString();
                        oWs.Cells[_fila, 4].Value = oBj["delegacion"].ToString();
                        oWs.Cells[_fila, 5].Value =  oBj["actividad"].ToString();
                        oWs.Cells[_fila, 6].Value =  oBj["cliente"].ToString();
                        oWs.Cells[_fila, 7].Value =  oBj["nro_inspeccion"].ToString();
                        oWs.Cells[_fila, 8].Value =  oBj["anio"].ToString();
                        oWs.Cells[_fila, 9].Value =  oBj["mes"].ToString();
                        oWs.Cells[_fila, 10].Value =  oBj["semana"].ToString();

                        oWs.Cells[_fila, 11].Value = oBj["autor"].ToString();
                        oWs.Cells[_fila, 12].Value =  oBj["puesto"].ToString();
                        oWs.Cells[_fila, 13].Value =  oBj["jefeObra"].ToString();
                        oWs.Cells[_fila, 14].Value = oBj["empresa_resp_propia"].ToString();
                        oWs.Cells[_fila, 15].Value =  oBj["empresa_resp_subContrata"].ToString();

                        oWs.Cells[_fila, 16].Value = oBj["tipoInspeccion"].ToString();
                        oWs.Cells[_fila, 17].Value =  oBj["incio_final_trabajos"].ToString();
                        oWs.Cells[_fila, 18].Value =  oBj["inspeccion_conjunta"].ToString();
                        oWs.Cells[_fila, 19].Value =  oBj["resp_inspeccion"].ToString();
                        oWs.Cells[_fila, 20].Value =  oBj["nro_Anomalias"].ToString();

                        oWs.Cells[_fila, 21].Value =  oBj["aspectoAnomalo"].ToString();
                        oWs.Cells[_fila, 22].Value =  oBj["Decripcion_anomalia"].ToString();
                        oWs.Cells[_fila, 23].Value =  oBj["anomalia_critica"].ToString();
                        oWs.Cells[_fila, 24].Value =  oBj["noEliminar"].ToString();
                        oWs.Cells[_fila, 25].Value =  oBj["accionPropuesta"].ToString();

                        oWs.Cells[_fila, 26].Value = oBj["respCorr"].ToString();
                        oWs.Cells[_fila, 27].Value =  oBj["fecha_Prop_Corr"].ToString();
                        oWs.Cells[_fila, 28].Value = oBj["fecha_Cierre_Corr"].ToString();

                        oWs.Cells[_fila, 29].Value = oBj["noEliminar2"].ToString();
                        oWs.Cells[_fila, 30].Value = oBj["paralizacionTrabajos"].ToString();                                    							

                        oWs.Cells[_fila, 31].Value =  oBj["sancion"].ToString();
                        oWs.Cells[_fila, 32].Value =  oBj["tipoSancion"].ToString();
                        oWs.Cells[_fila, 33].Value = oBj["nro_trabajadoresSancionados"].ToString();

                        oWs.Cells[_fila, 34].Value =  oBj["observaciones"].ToString();
                        oWs.Cells[_fila, 35].Value = oBj["propia"].ToString();
                        oWs.Cells[_fila, 36].Value =  oBj["subContrata"].ToString();
                        oWs.Cells[_fila, 37].Value = oBj["nombre_inspeccionado"].ToString();
                        oWs.Cells[_fila, 38].Value = oBj["estado"].ToString();
                        _fila++;
                    }

                    oWs.Row(1).Style.Font.Bold = true;
                    oWs.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(1).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;
                    oWs.Column(1).Style.Font.Bold = true;

                    for (int i = 1; i <= 38; i++)
                    {
                        oWs.Column(i).AutoFit();
                    }
                    oEx.Save();
                }

                Res = "1|" + FileExcel;
            }
            catch (Exception ex)
            {
                Res = "0|" + ex.Message;
            }
            return Res;
        }


        public string ExportarInspecciones_new(int id_pais, int id_grupo, int id_delegacion, string fecha_ini, string fecha_fin, int id_usuario)
        {

            string cadenaCnx = System.Configuration.ConfigurationManager.ConnectionStrings["conexionDsige"].ConnectionString;
            string resultado = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("PROC_REPORTE_LISTADO_INSPECCIONES_NEW", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@id_pais", SqlDbType.Int).Value = id_pais;
                        cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo;
                        cmd.Parameters.Add("@id_delegacion", SqlDbType.Int).Value = id_delegacion;
                        cmd.Parameters.Add("@fecha_ini", SqlDbType.VarChar).Value = fecha_ini;
                        cmd.Parameters.Add("@fecha_fin", SqlDbType.VarChar).Value = fecha_fin;

                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                            if (dt_detalle.Rows.Count > 0)
                            {
                                resultado = GenerarArchivoExcel_new(dt_detalle, id_usuario);
                            }
                            else
                            {
                                resultado = "0|No hay informacion para Mostrar.";
                            }
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                resultado = "0|" + ex.Message;
            }
            return resultado;
        }

        public string GenerarArchivoExcel_new(DataTable dt_detalles, int id_usuario)
        {
            string Res = "";
            int _fila = 2;
            string FileRuta = "";
            string FileExcel = "";
            try
            {
                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/ArchivosExcel/ReporteInspecciones_" + id_usuario + ".xlsx");
                string rutaServer = ConfigurationManager.AppSettings["servidor_archivos"];

                FileExcel = rutaServer + "ReporteInspecciones_" + id_usuario + ".xlsx";
                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }
                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("Reporte_Inspecciones");
                    oWs.Cells.Style.Font.SetFromFont(new Font("Tahoma", 9));

                    for (int i = 1; i <= 28; i++)
                    {
                            oWs.Cells[1, i].Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#FFFFFF ")); // color de la letra
                            oWs.Cells[1, i].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;   // fondo de celda
                            oWs.Cells[1, i].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.MidnightBlue); // fondo de celd
                    }


                    oWs.Cells[1, 1].Value = "Visita Nº";	
                    oWs.Cells[1, 2].Value = "Fecha. Inspección";
                    oWs.Cells[1, 3].Value = "Delegación";
                    oWs.Cells[1, 4].Value = "Actividad";
                    oWs.Cells[1, 5].Value = "Cliente";
                    oWs.Cells[1, 6].Value = "Autor";
                    oWs.Cells[1, 7].Value = "Puesto";
                    oWs.Cells[1, 8].Value = "Jefe de Obra";

                    oWs.Cells[1, 9].Value = "Empresa PROPIA";
                    oWs.Cells[1, 10].Value = "Empresa SUBCONTRATA";
                    oWs.Cells[1, 11].Value = "Inicio/Final trabajos";
                    oWs.Cells[1, 12].Value = "Insp. Fuera horario";
                    oWs.Cells[1, 13].Value = "Insp.  Pers. Propio";
                    oWs.Cells[1, 14].Value = "Insp. Subcontratas";
                    oWs.Cells[1, 15].Value = "Tipo Inspección";
                    oWs.Cells[1, 16].Value = "Tipo de Sanción";
                    oWs.Cells[1, 17].Value = "Nº trabajadores sancionados";

                    oWs.Cells[1, 18].Value = "Observaciones sanción";
                    oWs.Cells[1, 19].Value = "Observaciones inspección";
                    oWs.Cells[1, 20].Value = "Núm. Anomalías";
                    oWs.Cells[1, 21].Value = "Aspecto anómalo (Riesgo)";
                    oWs.Cells[1, 22].Value = "Descripción anomalía";
                    oWs.Cells[1, 23].Value = "Anom. crítica";
                    oWs.Cells[1, 24].Value = "Acción propuesta";
                    oWs.Cells[1, 25].Value = "Responsable corrección";
                    oWs.Cells[1, 26].Value = "Fecha Propuesta Corrección";
                    oWs.Cells[1, 27].Value = "Fecha Cierre Corrección";
                    oWs.Cells[1, 28].Value = "Paralización trabajos";

                    foreach (DataRow oBj in dt_detalles.Rows)
                    {
                        for (int i = 1; i <= 28; i++)
                        {
                            oWs.Cells[_fila, i].Style.Font.Size = 8; //letra tamaño  
                        }

                        oWs.Cells[_fila, 1].Value = oBj["Visita_nro"].ToString();
                        oWs.Cells[_fila, 2].Value = oBj["Fecha_Inspeccion"].ToString();
                        oWs.Cells[_fila, 3].Value = oBj["Delegacion"].ToString();
                        oWs.Cells[_fila, 4].Value = oBj["Actividad"].ToString();
                        oWs.Cells[_fila, 5].Value = oBj["Cliente"].ToString();
                        oWs.Cells[_fila, 6].Value = oBj["Autor"].ToString();
                        oWs.Cells[_fila, 7].Value = oBj["Puesto"].ToString();
                        oWs.Cells[_fila, 8].Value = oBj["Jefe_Obra"].ToString();
                        oWs.Cells[_fila, 9].Value = oBj["Empresa_PROPIA"].ToString();
                        oWs.Cells[_fila, 10].Value = oBj["Empresa_SUBCONTRATA"].ToString();
                        oWs.Cells[_fila, 11].Value = oBj["Inicio_Final_trabajos"].ToString();
                        oWs.Cells[_fila, 12].Value = oBj["Insp_Fuera_horario"].ToString();
                        oWs.Cells[_fila, 13].Value = oBj["Insp_Pers_Propio"].ToString();
                        oWs.Cells[_fila, 14].Value = oBj["Insp_Subcontratas"].ToString();
                        oWs.Cells[_fila, 15].Value = oBj["Tipo_Inspeccion"].ToString();
                        oWs.Cells[_fila, 16].Value = oBj["Tipo_Sancion"].ToString();
                        oWs.Cells[_fila, 17].Value = oBj["Nro_trabajadores_sancionados"].ToString();
                        oWs.Cells[_fila, 18].Value = oBj["Observaciones_sancion"].ToString();
                        oWs.Cells[_fila, 19].Value = oBj["Observaciones_inspeccion"].ToString();
                        oWs.Cells[_fila, 20].Value = oBj["Nro_Anomalias"].ToString();
                        oWs.Cells[_fila, 21].Value = oBj["Aspecto_anomalo"].ToString();
                        oWs.Cells[_fila, 22].Value = oBj["Descripcion_anomalia"].ToString();
                        oWs.Cells[_fila, 23].Value = oBj["Anom_critica"].ToString();
                        oWs.Cells[_fila, 24].Value = oBj["Accion_propuesta"].ToString();
                        oWs.Cells[_fila, 25].Value = oBj["Responsable_correccion"].ToString();
                        oWs.Cells[_fila, 26].Value = oBj["Fecha_Propuesta_Correccion"].ToString();
                        oWs.Cells[_fila, 27].Value = oBj["Fecha_Cierre_Correccion"].ToString();
                        oWs.Cells[_fila, 28].Value = oBj["Paralizacion_trabajos"].ToString();

                        _fila++;
                    }

                    oWs.Row(1).Style.Font.Bold = true;
                    oWs.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(1).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;
                    oWs.Column(1).Style.Font.Bold = true;

                    for (int i = 1; i <= 28; i++)
                    {
                        oWs.Column(i).AutoFit();
                    }
                    oEx.Save();
                }

                Res = "1|" + FileExcel;
            }
            catch (Exception ex)
            {
                Res = "0|" + ex.Message;
            }
            return Res;
        }


    }
}
