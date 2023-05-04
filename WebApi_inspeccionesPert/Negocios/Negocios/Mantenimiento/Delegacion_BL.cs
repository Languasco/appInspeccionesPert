using Entidades.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Negocios.Mantenimiento
{
   public class Delegacion_BL
    {
        private static string cadenaCnx = System.Configuration.ConfigurationManager.ConnectionStrings["conexionDsige"].ConnectionString;
        public static string Save_Delegacion(Save_Delegacion a)
        {
            string resultado = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("NEW_INSERT_DELEGACION", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.AddWithValue("@codigo_delegacion", a.codigo_delegacion);
                        cmd.Parameters.AddWithValue("@nombre_delegacion", a.nombre_delegacion);
                        cmd.Parameters.AddWithValue("@estado", a.estado);
                        cmd.Parameters.AddWithValue("@usuario_creacion", a.usuario_creacion);
                        cmd.Parameters.AddWithValue("@id_grupo", a.id_grupo);
                        cmd.Parameters.AddWithValue("@id_personal_representante", a.id_Personal);

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

    }
}
