using Entidades;
using Entidades.Mantenimiento;
using System;
using System.Collections.Generic;
using System.Data;
using System.Data.SqlClient;
using System.Linq;
using System.Net;
using System.Net.Mail;
using System.Text;
using System.Threading.Tasks;
using System.Web.ModelBinding;

namespace Negocios.Mantenimiento
{
    public class Usuario_BL
    {
        readonly string cadenaCnx = System.Configuration.ConfigurationManager.ConnectionStrings["conexionDsige"].ConnectionString;

        public List<Usuario_E> Listando_Usuario_Pais(int id_usuario, int usuario_Loggin)
        {
            try
            {
                List<Usuario_E> obj_List = new List<Usuario_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_USUARIO_PAIS_III", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;
                        cmd.Parameters.Add("@usuario_Loggin", SqlDbType.Int).Value = usuario_Loggin;

                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Usuario_E Entidad = new Usuario_E
                                {
                                    checkeado = Convert.ToInt32(row["checkeado"].ToString()) == 0 ? false : true,
                                    id_pais = Convert.ToInt32(row["id_pais"].ToString()),
                                    descripcion = row["descripcion"].ToString()
                                };

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

        public List<Usuario_E> Listando_Usuario_Grupo(int id_usuario, string codigoPais, int usuario_Loggin)
        {
            try
            {
                List<Usuario_E> obj_List = new List<Usuario_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_USUARIO_GRUPO_II", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;
                        cmd.Parameters.Add("@codPais", SqlDbType.VarChar).Value = codigoPais;
                        cmd.Parameters.Add("@usuario_Loggin", SqlDbType.Int).Value = usuario_Loggin;


                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Usuario_E Entidad = new Usuario_E
                                {
                                    checkeado = Convert.ToInt32(row["checkeado"].ToString()) == 0 ? false : true,
                                    id_grupo = Convert.ToInt32(row["id_grupo"].ToString()),
                                    descripcion = row["descripcion"].ToString()
                                };

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

        public List<Usuario_E> Listando_Usuario_Empresas(int id_usuario, string codigoGrupos, int usuario_Loggin)
        {
            try
            {
                List<Usuario_E> obj_List = new List<Usuario_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_USUARIO_EMPRESAS_II", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;
                        cmd.Parameters.Add("@codGrupos", SqlDbType.VarChar).Value = codigoGrupos;
                        cmd.Parameters.Add("@usuario_Loggin", SqlDbType.Int).Value = usuario_Loggin;

                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Usuario_E Entidad = new Usuario_E
                                {
                                    checkeado = Convert.ToInt32(row["checkeado"].ToString()) == 0 ? false : true,
                                    id_Empresas = Convert.ToInt32(row["id_Empresas"].ToString()),
                                    descripcion = row["descripcion"].ToString()
                                };
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

        public List<Usuario_E> Listando_Usuario_Delegaciones(int id_usuario, string codigoGrupo, int usuario_Loggin)
        {
            try
            {
                List<Usuario_E> obj_List = new List<Usuario_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("NEW_S_USUARIO_DELEGACION", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;
                        cmd.Parameters.Add("@codGrupo", SqlDbType.VarChar).Value = codigoGrupo;
                        cmd.Parameters.Add("@usuario_Loggin", SqlDbType.Int).Value = usuario_Loggin;

                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Usuario_E Entidad = new Usuario_E
                                {
                                    checkeado = Convert.ToInt32(row["checkeado"].ToString()) == 0 ? false : true,
                                    id_Delegacion = Convert.ToInt32(row["id_Delegacion"].ToString()),
                                    descripcion = row["descripcion"].ToString()
                                };
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

        public string Guardando_Configuracion_Usuarios(int id_usuarioGeneral, string codigoPais, string codigoGrupo, string codigoEmpresa, string codigoDelegacion, int id_usuarioCreacion)
        {
            string resultado = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_I_USUARIO_CONFIGURACION", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuarioGeneral", SqlDbType.Int).Value = id_usuarioGeneral;
                        cmd.Parameters.Add("@codigoPais", SqlDbType.VarChar).Value = codigoPais;
                        cmd.Parameters.Add("@codigoGrupo", SqlDbType.VarChar).Value = codigoGrupo;
                        cmd.Parameters.Add("@codigoEmpresa", SqlDbType.VarChar).Value = codigoEmpresa;
                        cmd.Parameters.Add("@codigoDelegacion", SqlDbType.VarChar).Value = codigoDelegacion;
                        cmd.Parameters.Add("@id_usuarioCreacion", SqlDbType.Int).Value = id_usuarioCreacion;
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

        public List<Usuario_E> Listando_Perfil_Usuario(int id_usuarioLogin)
        {
            try
            {
                List<Usuario_E> obj_List = new List<Usuario_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_USUARIO_PERFIL", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@idUsuario", SqlDbType.Int).Value = id_usuarioLogin;
                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Usuario_E Entidad = new Usuario_E
                                {
                                    id_perfil = Convert.ToInt32(row["id_perfil"].ToString()),
                                    des_perfil = row["des_perfil"].ToString()
                                };
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
        //-----
        public List<Usuario_E> Listando_Pais_Usuario(int id_usuario)
        {
            try
            {
                List<Usuario_E> obj_List = new List<Usuario_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_PAIS_USUARIO", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;
                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Usuario_E Entidad = new Usuario_E
                                {
                                    id_pais = Convert.ToInt32(row["id_pais"].ToString()),
                                    Pais = row["Pais"].ToString()
                                };
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

        public List<Usuario_E> Listando_Grupo_Pais_Usuario(int id_pais, int id_usuarioLogin)
        {
            try
            {
                List<Usuario_E> obj_List = new List<Usuario_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_GRUPO_PAIS_USUARIO", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_pais", SqlDbType.Int).Value = id_pais;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuarioLogin;
                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Usuario_E Entidad = new Usuario_E
                                {
                                    id_grupo = Convert.ToInt32(row["id_grupo"].ToString()),
                                    Grupo = row["Grupo"].ToString()
                                };
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

        public List<Usuario_E> Listando_Empresa_Grupo_Usuario(int id_grupo, int id_usuarioLogin)
        {
            try
            {
                List<Usuario_E> obj_List = new List<Usuario_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_EMPRESA_GRUPO_USUARIO", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuarioLogin;
                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Usuario_E Entidad = new Usuario_E
                                {
                                    id_Empresas = Convert.ToInt32(row["id_Empresas"].ToString()),
                                    Empresa = row["Empresa"].ToString()
                                };
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

        public List<Delegacion_En> Listando_Delegacion(int id_grupo)
        {
            try
            {
                List<Delegacion_En> obj_List = new List<Delegacion_En>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("NEW_LIST_DELEGACION", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo;
                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Delegacion_En Entidad = new Delegacion_En
                                {
                                    id_pais = Convert.ToInt32(row["id_pais"].ToString()),
                                    id_grupo = Convert.ToInt32(row["id_grupo"].ToString()),
                                    id_Delegacion = Convert.ToInt32(row["id_Delegacion"].ToString()),
                                    estado = Convert.ToInt32(row["estado"].ToString()),
                                    id_Personal = row["id_Personal"].ToString(),
                                    usuario_creacion = row["usuario_creacion"].ToString(),
                                    usuario_edicion = row["usuario_edicion"].ToString(),
                                    apellidos_Personal = row["apellidos_Personal"].ToString(),
                                    nombres_Personal = row["nombres_Personal"].ToString(),
                                    codigo_delegacion = row["codigo_delegacion"].ToString(),
                                    nombre_delegacion = row["nombre_delegacion"].ToString(),
                                    fecha_creacion = row["fecha_creacion"].ToString(),
                                    fecha_edicion = row["fecha_edicion"].ToString()
                                };
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

        public List<Usuario_E> Listando_Delegacion_Empresa_Usuario(int id_grupo, int id_usuarioLogin)
        {
            try
            {
                List<Usuario_E> obj_List = new List<Usuario_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("NEW_DELEGACION_GRUPO_USUARIO", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuarioLogin;
                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Usuario_E Entidad = new Usuario_E
                                {
                                    id_Delegacion = Convert.ToInt32(row["id_Delegacion"].ToString()),
                                    Delegacion = row["Delegacion"].ToString()
                                };
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

        public string Set_Insertando_Usuario_WebAccesos(int usuario_creacion, int id_usuario, int id_perfil)
        {
            string resultado = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_I_MANT_USUARIO_WEB_ACCESOS", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@usuario_creacion", SqlDbType.Int).Value = usuario_creacion;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;
                        cmd.Parameters.Add("@id_perfil", SqlDbType.Int).Value = id_perfil;
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

        public List<Usuario_E> Listando_Usuario_All(int usuario_Loggin, int id_pais)
        {
            try
            {
                List<Usuario_E> obj_List = new List<Usuario_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("NEW_SP_S_MANT_USUARIOS", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = usuario_Loggin;
                        cmd.Parameters.Add("@id_pais", SqlDbType.Int).Value = id_pais;

                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Usuario_E Entidad = new Usuario_E
                                {
                                    id_usuario = Convert.ToInt32(row["id_usuario"].ToString()),
                                    nro_documento = row["nro_documento"].ToString(),

                                    datos_personales = row["datos_personales"].ToString(),
                                    correo_electronico = row["correo_electronico"].ToString(),
                                    usuario_login = row["usuario_login"].ToString(),
                                    contrasenia_login = row["contrasenia_login"].ToString(),

                                    id_perfil = Convert.ToInt32(row["id_perfil"].ToString()),
                                    estado = row["estado"].ToString(),
                                    usuario_creacion = row["usuario_creacion"].ToString(),

                                    fecha_creacion = row["fecha_creacion"].ToString(),
                                    usuario_edicion = row["usuario_edicion"].ToString(),
                                    fecha_edicion = row["fecha_edicion"].ToString()
                                };


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

        public string Set_generando_Cambio_Contrasenia(string pas_actual, string pas_nueva, string pas_nueva_confirma, int id_Personal)
        {
            string resultado = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_USUARIO_CAMBIO_PASSWORD", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@pas_actual", SqlDbType.VarChar).Value = pas_actual;
                        cmd.Parameters.Add("@pas_nueva", SqlDbType.VarChar).Value = pas_nueva;
                        cmd.Parameters.Add("@pas_nueva_confirma", SqlDbType.VarChar).Value = pas_nueva_confirma;
                        cmd.Parameters.Add("@id_Personal", SqlDbType.Int).Value = id_Personal;
                        resultado = Convert.ToString(cmd.ExecuteNonQuery());
                    }
                }
            }
            catch (Exception e)
            {
                resultado = e.Message;
            }
            return resultado;
        }

        public List<Usuario_E> Listando_pais_configuracion(int id_usuario, string codigoPais, int usuario_Loggin)
        {
            try
            {
                List<Usuario_E> obj_List = new List<Usuario_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_USUARIO_PAIS_CONFIGURACION", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;
                        cmd.Parameters.Add("@codPais", SqlDbType.VarChar).Value = codigoPais;
                        cmd.Parameters.Add("@usuario_Loggin", SqlDbType.Int).Value = usuario_Loggin;


                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Usuario_E Entidad = new Usuario_E
                                {
                                    id_pais = Convert.ToInt32(row["id_pais"].ToString()),
                                    Pais = row["Pais"].ToString()
                                };

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

        public List<Usuario_E> Listando_grupo_configuracion(int id_usuario, string codigoGrupo, int usuario_Loggin)
        {
            try
            {
                List<Usuario_E> obj_List = new List<Usuario_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_USUARIO_GRUPO_CONFIGURACION", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;
                        cmd.Parameters.Add("@codigoGrupo", SqlDbType.VarChar).Value = codigoGrupo;
                        cmd.Parameters.Add("@usuario_Loggin", SqlDbType.Int).Value = usuario_Loggin;


                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Usuario_E Entidad = new Usuario_E
                                {
                                    id_grupo = Convert.ToInt32(row["id_grupo"].ToString()),
                                    Grupo = row["Grupo"].ToString()
                                };

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

        public List<Usuario_E> Listando_empresa_configuracion(int id_usuario, string codigoEmpresa, int usuario_Loggin)
        {
            try
            {
                List<Usuario_E> obj_List = new List<Usuario_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_USUARIO_EMPRESA_CONFIGURACION", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;
                        cmd.Parameters.Add("@codigoEmpresa", SqlDbType.VarChar).Value = codigoEmpresa;
                        cmd.Parameters.Add("@usuario_Loggin", SqlDbType.Int).Value = usuario_Loggin;


                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Usuario_E Entidad = new Usuario_E
                                {
                                    id_Empresas = Convert.ToInt32(row["id_Empresas"].ToString()),
                                    Empresa = row["Empresa"].ToString()
                                };

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

        public string Guardando_Configuracion_Usuarios_V2(int id_usuarioGeneral, string codigoPais, string codigoGrupo, string codigoDelegacion, string codigoProyecto, int id_usuarioCreacion, int id_pais_config, int id_grupo_config, int id_delegacion_config)
        {
            string resultado = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("NEW_I_USUARIO_CONFIGURACION", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuarioGeneral", SqlDbType.Int).Value = id_usuarioGeneral;
                        cmd.Parameters.Add("@codigoPais", SqlDbType.VarChar).Value = codigoPais;
                        cmd.Parameters.Add("@codigoGrupo", SqlDbType.VarChar).Value = codigoGrupo;
                        cmd.Parameters.Add("@codigoDelegacion", SqlDbType.VarChar).Value = codigoDelegacion;
                        cmd.Parameters.Add("@codigoProyecto", SqlDbType.VarChar).Value = codigoProyecto;

                        cmd.Parameters.Add("@id_usuarioCreacion", SqlDbType.Int).Value = id_usuarioCreacion;
                        cmd.Parameters.Add("@id_pais_config", SqlDbType.Int).Value = id_pais_config;
                        cmd.Parameters.Add("@id_grupo_config", SqlDbType.Int).Value = id_grupo_config;
                        cmd.Parameters.Add("@id_delegacion_config", SqlDbType.Int).Value = id_delegacion_config;

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

        public string Guardando_Configuracion_Usuarios_V3(int id_usuarioGeneral, string codigoPais, string codigoGrupo, string codigoDelegacion, string codigoOT, int id_usuarioCreacion, int id_pais_config, int id_grupo_config, int id_delegacion_config, string flag_administrador)
        {
            string resultado = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("NEW_I_USUARIO_CONFIGURACION_3", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuarioGeneral", SqlDbType.Int).Value = id_usuarioGeneral;
                        cmd.Parameters.Add("@codigoPais", SqlDbType.VarChar).Value = codigoPais;
                        cmd.Parameters.Add("@codigoGrupo", SqlDbType.VarChar).Value = codigoGrupo;
                        cmd.Parameters.Add("@codigoDelegacion", SqlDbType.VarChar).Value = codigoDelegacion;
                        cmd.Parameters.Add("@codigoOT", SqlDbType.VarChar).Value = codigoOT;

                        cmd.Parameters.Add("@id_usuarioCreacion", SqlDbType.Int).Value = id_usuarioCreacion;
                        cmd.Parameters.Add("@id_pais_config", SqlDbType.Int).Value = id_pais_config;
                        cmd.Parameters.Add("@id_grupo_config", SqlDbType.Int).Value = id_grupo_config;
                        cmd.Parameters.Add("@id_delegacion_config", SqlDbType.Int).Value = id_delegacion_config;
                        cmd.Parameters.Add("@flag_administrador", SqlDbType.VarChar).Value = flag_administrador;                       

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


        //Aceptar o Rechazar usuarios registrados
        public string Aceptar_Rechazar_Usuarios(int usuario_registrado, int id_personal, int id_usuario, int estado)
        {
            string resultado = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("NEW_VALIDAR_USUARIOS_REGISTRADOS", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_registrado", SqlDbType.Int).Value = usuario_registrado;
                        cmd.Parameters.Add("@id_personal", SqlDbType.Int).Value = id_personal;
                        cmd.Parameters.Add("@usuario_edicion", SqlDbType.Int).Value = id_usuario;
                        cmd.Parameters.Add("@estado", SqlDbType.Int).Value = estado;
                        cmd.ExecuteNonQuery();

                        resultado = Generar_EnvioCorreo(id_personal);
                    }
                }
            }
            catch (Exception e)
            {
                resultado = e.Message;
            }
            return resultado;
        }

        private DSIGE_InspeccionesEntities db = new DSIGE_InspeccionesEntities();

        public string Generar_EnvioCorreo(int id_personal)
        {
            string resultado = "";    
            try
            {
                db.Configuration.ProxyCreationEnabled = false;
                var obj_personal = db.tbl_usuarios.Where(x => x.id_Personal == id_personal).FirstOrDefault<tbl_usuarios>();

                if (obj_personal != null)
                {


                    if (string.IsNullOrEmpty(obj_personal.correo_electronico))
                    {
                        resultado = "No se pudo encontrar el correo electrónico del usuario, no se envio el mensaje..";
                    }
                    else {
                        var body = "<center><h2>Bienvenido Usted ya puede acceder al aplicativo</h2></center>" +
                                    "<p> Usuario : " + obj_personal.usuario_login + "</p> " +
                                    "<p> Contraseña : " + obj_personal.contrasenia_login + "</p>" +
                                    "<p></p><p></p><p>Atte.</p><p>Administrador Web</p><p>Dsige</p>";

                        var message = new MailMessage();
                        message.To.Add(new MailAddress(obj_personal.correo_electronico));
                        message.From = new MailAddress("cobralecturas@gmail.com");
                        message.Subject = "Administracion de Credenciales - Inspecciones";
                        message.Body = body;
                        message.IsBodyHtml = true;

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
                        resultado = "OK";
                    
                    }      
                }
                else
                {
                    resultado = "No hay informacion para mostrar";
                } 

            }
            catch (Exception e)
            {
                resultado = e.Message;
            }
            return resultado;
        }
        
        public List<Usuario_Registrado> GetList_Usuario_Registrado(int id_Pais, int id_Delegacion, int estado)
        {
            try
            {
                List<Usuario_Registrado> item = null;
                using (SqlConnection con = new SqlConnection(cadenaCnx))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "NEW_GET_USUARIO_REGISTRADO";
                    cmd.Parameters.AddWithValue("@id_Pais", id_Pais);
                    cmd.Parameters.AddWithValue("@id_Delegacion", id_Delegacion);
                    cmd.Parameters.AddWithValue("@estado", estado);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        item = new List<Usuario_Registrado>();
                        while (dr.Read())
                        {
                            Usuario_Registrado a = new Usuario_Registrado
                            {
                                Id_usuario_Registrado = Convert.ToInt32(dr["id_usuario_Registrado"].ToString()),
                                id_Personal = Convert.ToInt32(dr["id_Personal"].ToString()),
                                Estado = Convert.ToInt32(dr["estado"].ToString()),
                                Nro_Documento = dr["nro_Documento"].ToString(),
                                FechaRegistro = dr["fechaRegistro"].ToString(),
                                Nombres_Personal = dr["nombres_Personal"].ToString(),
                                Apellidos_Personal = dr["apellidos_Personal"].ToString(),
                                Datos_personales = dr["datos_personales"].ToString(),
                                FechaAprobacion = dr["fechaAprobacion"].ToString(),
                                FechaRechazo = dr["fechaRechazo"].ToString(),
                                Pais = dr["pais"].ToString()
                            };
                            item.Add(a);
                        }
                    }
                    con.Close();
                }
                return item;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public void EnviarCorreo()
        {
            /*-------------------------MENSAJE DE CORREO----------------------*/

            //Creamos un nuevo Objeto de mensaje
            System.Net.Mail.MailMessage mmsg = new System.Net.Mail.MailMessage();

            //Direccion de correo electronico a la que queremos enviar el mensaje
            mmsg.To.Add("destinatario@servidordominio.com");

            //Nota: La propiedad To es una colección que permite enviar el mensaje a más de un destinatario

            //Asunto
            mmsg.Subject = "Asunto del correo";
            mmsg.SubjectEncoding = Encoding.UTF8;
            //Direccion de correo electronico que queremos que reciba una copia del mensaje
            mmsg.Bcc.Add("destinatariocopia@servidordominio.com"); //Opcional
            //Cuerpo del Mensaje
            mmsg.Body = "Texto del contenio del mensaje de correo";
            mmsg.BodyEncoding = System.Text.Encoding.UTF8;
            mmsg.IsBodyHtml = false; //Si no queremos que se envíe como HTML

            //Correo electronico desde la que enviamos el mensaje
            mmsg.From = new System.Net.Mail.MailAddress("micuenta@servidordominio.com");

            //Creamos un objeto de cliente de correo
            System.Net.Mail.SmtpClient cliente = new System.Net.Mail.SmtpClient();

            //Hay que crear las credenciales del correo emisor
            cliente.Credentials =
                new System.Net.NetworkCredential("micuenta@servidordominio.com", "micontraseña");

            //Lo siguiente es obligatorio si enviamos el mensaje desde Gmail
            /*
            cliente.Port = 587;
            cliente.EnableSsl = true;
            */
            cliente.Host = "mail.servidordominio.com"; //Para Gmail "smtp.gmail.com";
            try
            {
                //Enviamos el mensaje      
                cliente.Send(mmsg);
            }
            catch (System.Net.Mail.SmtpException ex)
            {
                //Aquí gestionamos los errores al intentar enviar el correo
            }
        }
        
        public List<Usuario_E> Listando_Usuario_Proyecto(int id_usuario, string codigoDelegacion, int usuario_Loggin)
        {
            try
            {
                List<Usuario_E> obj_List = new List<Usuario_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_USUARIO_PROYECTO", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;
                        cmd.Parameters.Add("@codDelegacion", SqlDbType.VarChar).Value = codigoDelegacion;
                        cmd.Parameters.Add("@usuario_Loggin", SqlDbType.Int).Value = usuario_Loggin;


                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Usuario_E Entidad = new Usuario_E
                                {
                                    checkeado = Convert.ToInt32(row["checkeado"].ToString()) == 0 ? false : true,
                                    id_Proyecto = Convert.ToInt32(row["id_Proyecto"].ToString()),
                                    descripcion = row["descripcion"].ToString()
                                };

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
        
        public List<Usuario_E> Listando_delegacion_configuracion(int id_usuario, string codigoDelegacion, int usuario_Loggin)
        {
            try
            {
                List<Usuario_E> obj_List = new List<Usuario_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_USUARIO_DELEGACION_CONFIGURACION", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;
                        cmd.Parameters.Add("@codigoDelegacion", SqlDbType.VarChar).Value = codigoDelegacion;
                        cmd.Parameters.Add("@usuario_Loggin", SqlDbType.Int).Value = usuario_Loggin;


                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Usuario_E Entidad = new Usuario_E
                                {
                                    id_Delegacion = Convert.ToInt32(row["id_Delegacion"].ToString()),
                                    Delegacion = row["Delegacion"].ToString()
                                };

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
        
        public List<Usuario_E> get_usuarios_masivos_alertas(int id_usuario)
        {
            try
            {
                List<Usuario_E> obj_List = new List<Usuario_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_I_USUARIO_MASIVO_ALERTA", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;

                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                               Usuario_E Entidad = new Usuario_E
                                {
                                    id_pais = Convert.ToInt32(row["id_pais"].ToString()),
                                    Pais = row["Pais"].ToString(),
                                    id_grupo = Convert.ToInt32(row["id_grupo"].ToString()),
                                    Grupo = row["Grupo"].ToString(),
                                    Nro_Documento = row["Nro_Documento"].ToString(),
                                    mensaje = row["mensaje"].ToString(),
                                    id_Delegacion = Convert.ToInt32(row["id_Delegacion"].ToString()),
                                    nombre_delegacion = row["nombre_delegacion"].ToString(),
                                    id_perfil = Convert.ToInt32(row["id_perfil"].ToString()) ,
                                   des_perfil = row["des_perfil"].ToString(),
                               };
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
        
        public List<Usuario_E> Listando_Usuario_OT(int id_usuario, string codigoDelegacion, int usuario_Loggin)
        {
            try
            {
                List<Usuario_E> obj_List = new List<Usuario_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_USUARIO_OT", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = id_usuario;
                        cmd.Parameters.Add("@codDelegacion", SqlDbType.VarChar).Value = codigoDelegacion;
                        cmd.Parameters.Add("@usuario_Loggin", SqlDbType.Int).Value = usuario_Loggin;


                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Usuario_E Entidad = new Usuario_E
                                {
                                    checkeado = Convert.ToInt32(row["checkeado"].ToString()) == 0 ? false : true,
                                    id_OT = Convert.ToInt32(row["id_OT"].ToString()),
                                    descripcion = row["descripcion"].ToString()
                                };

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


        public List<Usuario_E> Listando_Usuario_Configuracion(int  id_pais, int id_grupo,int id_delegacion)
        {
            try
            {
                List<Usuario_E> obj_List = new List<Usuario_E>();
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_USUARIOS_CONFIGURACION", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_pais", SqlDbType.Int).Value = id_pais;
                        cmd.Parameters.Add("@id_grupo", SqlDbType.VarChar).Value = id_grupo;
                        cmd.Parameters.Add("@id_delegacion", SqlDbType.Int).Value = id_delegacion;
                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Usuario_E Entidad = new Usuario_E
                                {
                                    id_usuario = Convert.ToInt32(row["id_usuario"].ToString()),
                                    usuarios = row["usuarios"].ToString()
                                };

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
