using Entidades.Reporte;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Reporte
{
    public class Reporte_Dashboard_BL
    {

        string cadenaCnx = System.Configuration.ConfigurationManager.ConnectionStrings["conexionDsige"].ConnectionString;

        public List<Reporte_Dashboard_E> Listando_Reporte_DashBoard(int id_pais,int  id_grupo, int id_delegacion, string fecha_ini, string fecha_fin, int id_personal, int idDetalle, int tiporeporte)
        {
            try
            {
                List<Reporte_Dashboard_E> obj_List = new List<Reporte_Dashboard_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("NEW_REPORTE_INSPECCIONES_ANOMALIAS_WEB", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@id_pais", SqlDbType.Int).Value = id_pais;
                        cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo;
                        cmd.Parameters.Add("@id_delegacion", SqlDbType.Int).Value = id_delegacion;

                        cmd.Parameters.Add("@fecha_ini", SqlDbType.VarChar).Value = fecha_ini;
                        cmd.Parameters.Add("@fecha_fin", SqlDbType.VarChar).Value = fecha_fin;
                        cmd.Parameters.Add("@id_personal", SqlDbType.Int).Value = id_personal;
                        cmd.Parameters.Add("@TipoReport", SqlDbType.Int).Value = tiporeporte;
                        cmd.Parameters.Add("@id_detalle", SqlDbType.Int).Value = idDetalle;

                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Reporte_Dashboard_E Entidad = new Reporte_Dashboard_E();
                                
                                Entidad.total = Convert.ToInt32(row["total"].ToString());
                                Entidad.normales = Convert.ToInt32(row["normales"].ToString());
                                Entidad.anomalias = Convert.ToInt32(row["anomalias"].ToString());
                                Entidad.levantadas = Convert.ToInt32(row["levantadas"].ToString());
                                Entidad.pendientes = Convert.ToInt32(row["pendientes"].ToString());
                                Entidad.fecha =  row["fecha"].ToString();
                                Entidad.nivel = row["nivel"].ToString();
                                Entidad.ver_detalle = row["ver_detalle"].ToString();
                                Entidad.perfil = Convert.ToInt32(row["perfil"].ToString());                         
                  
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
