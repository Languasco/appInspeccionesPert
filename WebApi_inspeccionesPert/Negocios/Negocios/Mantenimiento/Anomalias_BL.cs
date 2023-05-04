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

namespace Negocios.Mantenimiento
{
    public class Anomalias_BL
    {

        public string Exportar_Anomalias(int id_grupo )
        {

            string cadenaCnx = ConfigurationManager.ConnectionStrings["conexionDsige"].ConnectionString;
            string resultado = "";

            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("NEW_S_ANOMALIAS_REPORTE", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo;

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
            string _ruta;
            string resultado = "";
            int _fila = 2;
            string FileRuta = "";
            string FileExcel = "";

            try
            {
                _servidor = String.Format("{0:ddMMyyyy_hhmmss}.xlsx", DateTime.Now);
                FileRuta = System.Web.Hosting.HostingEnvironment.MapPath("~/ArchivosExcel/ReporteAnomalias_" + _servidor);
                string rutaServer = ConfigurationManager.AppSettings["servidor_archivos"];

                FileExcel = rutaServer + "ReporteAnomalias_" + _servidor;
                FileInfo _file = new FileInfo(FileRuta);
                if (_file.Exists)
                {
                    _file.Delete();
                    _file = new FileInfo(FileRuta);
                }
                using (Excel.ExcelPackage oEx = new Excel.ExcelPackage(_file))
                {
                    Excel.ExcelWorksheet oWs = oEx.Workbook.Worksheets.Add("Anomalias");
                    oWs.Cells.Style.Font.SetFromFont(new Font("Tahoma", 9)); 


                    for (int i = 1; i <= 5; i++)
                    {
                        oWs.Cells[1, i].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);    //marco
                    }
                    
                    oWs.Cells[1, 1].Value = "ID";
                    oWs.Cells[1, 2].Value = "ANOMALIA";
                    oWs.Cells[1, 3].Value = "ANOMALIA CRITICA";
                    oWs.Cells[1, 4].Value = "ANOMALIA GENERAL";
                    oWs.Cells[1, 5].Value = "ANOMALIA VALOR";
  
                    foreach (DataRow oBj in dt_detalles.Rows)
                    {
                        if (oBj["anomalia_titulo"].ToString() == "1")
                        {
                            oWs.Cells[_fila, 2].Style.Font.Color.SetColor(System.Drawing.ColorTranslator.FromHtml("#000000")); // color de la letra
                            oWs.Cells[_fila, 2].Style.Fill.PatternType = OfficeOpenXml.Style.ExcelFillStyle.Solid;   // fondo de celda
                            oWs.Cells[_fila, 2].Style.Fill.BackgroundColor.SetColor(System.Drawing.Color.Aquamarine); // fondo de celda

                            oWs.Cells[_fila, 1].Style.Font.Bold = true; //Letra negrita
                            oWs.Cells[_fila, 2].Style.Font.Bold = true; //Letra negrita
                        }

                       for (int i = 1; i <= 5; i++)
                        {
                            oWs.Cells[_fila, i].Style.Font.Size = 8; //letra tamaño  
                            oWs.Cells[_fila, i].Style.Border.BorderAround(OfficeOpenXml.Style.ExcelBorderStyle.Thin);    //marco
                        }

                       oWs.Cells[_fila, 1].Value = oBj["id_Anomalia"].ToString();
                       oWs.Cells[_fila, 2].Value = oBj["descripcion_Anomalia"].ToString();
                       oWs.Cells[_fila, 3].Value = oBj["anomalia_Critica"].ToString();
                       oWs.Cells[_fila, 3].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                       oWs.Cells[_fila, 3].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center; // alinear texto

                       oWs.Cells[_fila, 4].Value = oBj["anomalia_Critica_General"].ToString();
                       oWs.Cells[_fila, 4].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                       oWs.Cells[_fila, 4].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center; // alinear texto

                       oWs.Cells[_fila, 5].Value = oBj["anomalia_Valor"].ToString();
                       oWs.Cells[_fila, 5].Style.VerticalAlignment = OfficeOpenXml.Style.ExcelVerticalAlignment.Center;
                       oWs.Cells[_fila, 5].Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Right; // alinear texto
                        _fila++;
                    }

                    oWs.Row(1).Style.Font.Bold = true;
                    oWs.Row(1).Style.HorizontalAlignment = Style.ExcelHorizontalAlignment.Center;
                    oWs.Row(1).Style.VerticalAlignment = Style.ExcelVerticalAlignment.Center;


                    for (int i = 1; i <= 5; i++)
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
