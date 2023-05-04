using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Entidades.Movil.Reporte;
using System.Data.SqlClient;
using System.Data;

namespace Negocios.Movil.Reporte
{
    public class ReporteBL
    {
        private static string db = ConfigurationManager.ConnectionStrings["conexionDsige"].ConnectionString;

        public List<ReporteEN> getInspectionAnomaly(int proyectoId, int delegacionId, int empresaId, string fecha, int perfil, int tipoReport, int inspectorId)
        {
            try
            {
                List<ReporteEN> obj_List = new List<ReporteEN>();

                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();
                    //using (SqlCommand cmd = new SqlCommand("SP_S_REPORTE_INSPECCIONES_ANOMALIAS", cn))
                    using (SqlCommand cmd = new SqlCommand("SP_S_REPORTE_INSPECCIONES_ANOMALIAS_IRVIN", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_ot", SqlDbType.Int).Value = proyectoId; // ot
                        cmd.Parameters.Add("@id_delegacion", SqlDbType.Int).Value = delegacionId;
                        cmd.Parameters.Add("@id_empresa", SqlDbType.Int).Value = empresaId;
                        cmd.Parameters.Add("@fecha", SqlDbType.VarChar).Value = fecha;
                        cmd.Parameters.Add("@Perfil", SqlDbType.Int).Value = perfil;
                        cmd.Parameters.Add("@TipoReport", SqlDbType.Int).Value = tipoReport;
                        cmd.Parameters.Add("@id_Personal_Inspector", SqlDbType.Int).Value = inspectorId;
                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow dr in dt_detalle.Rows)
                            {
                                ReporteEN item3 = new ReporteEN();
                                item3.anomalias = Convert.ToInt32(dr["Codigo"]);
                                item3.Dato = dr["Dato"].ToString();
                                item3.total = Convert.ToInt32(dr["total"]);
                                item3.normales = Convert.ToInt32(dr["normales"]);
                                item3.anomalias = Convert.ToInt32(dr["anomalias"]);
                                item3.levantadas = Convert.ToInt32(dr["levantadas"]);
                                item3.pendientes = Convert.ToInt32(dr["pendientes"]);
                                item3.fecha = dr["fecha"].ToString();
                                item3.nivel = dr["nivel"].ToString();
                                item3.ipal = dr["Ipal"].ToString();
                                obj_List.Add(item3);
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
