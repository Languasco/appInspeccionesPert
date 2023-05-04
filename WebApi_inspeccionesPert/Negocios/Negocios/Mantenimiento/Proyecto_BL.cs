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
    public class Proyecto_BL
    {
        string lastId = "";
        private static SqlDataReader dr;

        private static SqlConnection con = null;
        private static SqlCommand cmd = null;
        private static string cadenaCnx = System.Configuration.ConfigurationManager.ConnectionStrings["conexionDsige"].ConnectionString;
        public static string saveMaterialOCompra_Web(Proyecto_En a)
        {
            try
            {
                string lastId = "";
                using (con = new SqlConnection(cadenaCnx))
                {
                    con.Open();
                    using (cmd = new SqlCommand("NEW_INSERT_PROYECTO", con))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_delegacion", SqlDbType.Int).Value = a.id_Delegacion;
                        cmd.Parameters.Add("@nombre_proyecto", SqlDbType.VarChar, 100).Value = a.nombre_proyecto;
                        cmd.Parameters.Add("@estado", SqlDbType.Int).Value = a.estado;
                        cmd.Parameters.Add("@usuario_creacion", SqlDbType.Int).Value = a.usuario_creacion;
                        cmd.Parameters.Add("@id_personal_jefeobra", SqlDbType.Int).Value = a.id_Personal_JefeObra;
                        cmd.Parameters.Add("@id_cliente", SqlDbType.Int).Value = a.id_Cliente;
  
                        dr = cmd.ExecuteReader();
                        while (dr.Read())
                        {
                            lastId = dr[0].ToString();
                            foreach (var item in a.Empresa_Cola)
                            {
                                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                                {
                                    SqlCommand cmds = cn.CreateCommand();
                                    cmds.Connection.Open();
                                    cmds.CommandType = CommandType.StoredProcedure;
                                    cmds.CommandText = "NEW_INSERT_EMPRESA_COLABORADORA";
                                    cmds.Parameters.Add("@id_proyecto", SqlDbType.Int).Value = Convert.ToInt32(lastId);
                                    cmds.Parameters.Add("@id_empresacolaboradora", SqlDbType.Int).Value = item.id_EmpresaColaboradora;
                                    cmds.Parameters.Add("@estado", SqlDbType.Int).Value = item.estado;
                                    cmds.Parameters.Add("@usuario_creacion", SqlDbType.Int).Value = item.usuario_creacion;
                                    cmds.ExecuteNonQuery();
                                    cmds.Connection.Close();
                                }
                            }

                        }
                    }

                }
                return "OK";
            }
            catch (Exception ex)
            {
                throw;

            }
        }

        public static string saved_Empresas_Proyecto(List<Empresa_Colaboradora> a)
        {
            try
            {
                foreach (var item in a)
                {
                    using (SqlConnection cn = new SqlConnection(cadenaCnx))
                    {
                        SqlCommand cmds = cn.CreateCommand();
                        cmds.Connection.Open();
                        cmds.CommandType = CommandType.StoredProcedure;
                        cmds.CommandText = "NEW_INSERT_EMPRESA_COLABORADORA";
                        cmds.Parameters.Add("@id_proyecto", SqlDbType.Int).Value = item.id_Proyecto;
                        cmds.Parameters.Add("@id_empresacolaboradora", SqlDbType.Int).Value = item.id_EmpresaColaboradora;
                        cmds.Parameters.Add("@estado", SqlDbType.Int).Value = item.estado;
                        cmds.Parameters.Add("@usuario_creacion", SqlDbType.Int).Value = item.usuario_creacion;
                        cmds.ExecuteNonQuery();
                        cmds.Connection.Close();
                    }
                }
                return "OK";
            }
            catch (Exception ex)
            {
                throw;

            }
        }
    }
}
