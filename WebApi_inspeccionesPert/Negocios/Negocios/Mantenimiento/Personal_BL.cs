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
    public class Personal_BL
    {
        private static string cadenaCnx = System.Configuration.ConfigurationManager.ConnectionStrings["conexionDsige"].ConnectionString;
        public List<Personal_E> Listando_Personal_DelegacionEmpresa(int id_personal, int id_empresa)
        {
            try
            {
                List<Personal_E> obj_List = new List<Personal_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_PERSONAL_ASIGNAR_DELEGACION", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_personal", SqlDbType.Int).Value = id_personal;
                        cmd.Parameters.Add("@id_empresa", SqlDbType.VarChar).Value = id_empresa;

                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Personal_E Entidad = new Personal_E();
                                Entidad.checkeado = Convert.ToInt32(row["checkeado"].ToString()) == 0 ? false : true;
                                Entidad.id_personal_Delegacion = Convert.ToInt32(row["id_personal_Delegacion"].ToString());
                                Entidad.id_Personal = Convert.ToInt32(row["id_Personal"].ToString());
                                Entidad.id_Delegacion = Convert.ToInt32(row["id_Delegacion"].ToString());
                                Entidad.nombre_delegacion = row["nombre_delegacion"].ToString();
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

        public string Guardando_PersonalDelegacion(int id_personal, string obj_delegacion, int id_empresa)
        {
            string resultado = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_I_U_GUARDAR_PERSONAL_DELEGACION", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_personal", SqlDbType.Int).Value = id_personal;
                        cmd.Parameters.Add("@delegacion", SqlDbType.VarChar).Value = obj_delegacion;
                        cmd.Parameters.Add("@id_empresa", SqlDbType.Int).Value = id_empresa;
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

        public List<Personal_E> Listando_Personal_Pais_Grupo_Empresa_Delegacion_new(int id_pais, int id_grupo, string obj_id_Delegacion, int idusuario)
        {
            try
            {
                List<Personal_E> obj_List = new List<Personal_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("NEW_PERSONAL_PAIS_GRUPO_DELEGACION", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_pais", SqlDbType.Int).Value = id_pais;
                        cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo;
                        cmd.Parameters.Add("@obj_id_Delegacion", SqlDbType.VarChar).Value = obj_id_Delegacion;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = idusuario;

                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Personal_E Entidad = new Personal_E();

                                Entidad.id_grupo = row["id_grupo"].ToString();
                                Entidad.id_Personal = Convert.ToInt32(row["id_Personal"].ToString());
                                Entidad.id_Cargo = Convert.ToInt32(row["id_Cargo"].ToString());
                                Entidad.apellidos_Personal = row["apellidos_Personal"].ToString();
                                Entidad.nombres_Personal = row["nombres_Personal"].ToString();
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

        public List<Personal_Repre> Listando_Personal_Representante(int id_pais, int id_grupo, string search)
        {
            try
            {
                List<Personal_Repre> obj_List = new List<Personal_Repre>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("NEW_LIST_REPRESENTANTE", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_pais", SqlDbType.Int).Value = id_pais;
                        cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo;
                        cmd.Parameters.Add("@filter", SqlDbType.VarChar).Value = search;

                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Personal_Repre Entidad = new Personal_Repre();

                                Entidad.id_Personal = Convert.ToInt32(row["id_Personal"].ToString());
                                Entidad.nroDoc_Personal = row["nroDoc_Personal"].ToString();
                                Entidad.apellidos_Personal = row["apellidos_Personal"].ToString();
                                Entidad.nombres_Personal = row["nombres_Personal"].ToString();
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

        public string Delete_Acceso(int idpersonal, int idopcion)
        {
            string resultado = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_D_ACCESO_OPCIONES", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_personal", SqlDbType.Int).Value = idpersonal;
                        cmd.Parameters.Add("@id_opcion", SqlDbType.Int).Value = idopcion;
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

        public string Save_Acceso(int idpersonal, int idopcion)
        {
            string resultado = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))

                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_I_ACCESO_OPCIONES", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_personal", SqlDbType.Int).Value = idpersonal;
                        cmd.Parameters.Add("@id_opcion", SqlDbType.Int).Value = idopcion;
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

        public string Save_Configuracion_Perfil(int idusuario, int idpais, int idgrupo, int iddelegacion, int idot)
        {
            string resultado = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))

                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("PROC_I_CONFIGURACION_USUARIO_OT", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = idusuario;
                        cmd.Parameters.Add("@id_pais", SqlDbType.Int).Value = idpais;
                        cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = idgrupo;
                        cmd.Parameters.Add("@id_Delegacion", SqlDbType.Int).Value = iddelegacion;
                        cmd.Parameters.Add("@id_OT", SqlDbType.Int).Value = idot;
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

        public List<Personal_Repre> GetListPersonal_Jefe_Proyecto(int id_Delegacion)
        {
            try
            {
                List<Personal_Repre> item = null;
                using (SqlConnection con = new SqlConnection(cadenaCnx))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "NEW_LIST_PERSONAL_CARGO";
                    cmd.Parameters.AddWithValue("@ID_Delegacion", id_Delegacion);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        item = new List<Personal_Repre>();
                        while (dr.Read())
                        {
                            Personal_Repre a = new Personal_Repre();
                            a.id_Personal = dr.GetInt32(0);
                            a.nombres_Personal = dr.GetString(1);
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

        public List<Personal_E> Consultando_direccion_electronica(string nombre, string dominio)
        {
            try
            {
                List<Personal_E> obj_List = new List<Personal_E>();
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_PERSONAL_CONSULTA_DIRECCION", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;
                        cmd.Parameters.Add("@dominio", SqlDbType.VarChar).Value = dominio;
 
                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Personal_E Entidad = new Personal_E();

                                Entidad.datos = row["datos"].ToString();
                                Entidad.cantidades =Convert.ToInt32(row["cantidades"].ToString());
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


        public List<Personal_E> Consultando_direccion_electronica_usuario(string nombre, string dominio)
        {
            try
            {
                List<Personal_E> obj_List = new List<Personal_E>();
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_PERSONAL_CONSULTA_DIRECCION_USER", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@nombre", SqlDbType.VarChar).Value = nombre;
                        cmd.Parameters.Add("@dominio", SqlDbType.VarChar).Value = dominio;

                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Personal_E Entidad = new Personal_E();

                                Entidad.datos = row["datos"].ToString();
                                Entidad.cantidades = Convert.ToInt32(row["cantidades"].ToString());
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


        public List<Personal_E> get_listado_personal(int id_pais,  int id_grupo, int id_Delegacion,  int estado)
        {
            try
            {
                List<Personal_E> obj_List = new List<Personal_E>();
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_MANT_PERSONAL", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_pais", SqlDbType.Int).Value = id_pais;
                        cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo;
                        cmd.Parameters.Add("@id_Delegacion", SqlDbType.Int).Value = id_Delegacion;
                        cmd.Parameters.Add("@estado", SqlDbType.Int).Value = estado;

                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Personal_E Entidad = new Personal_E();                       
                                    Entidad.id_pais = Convert.ToInt32(row["id_pais"].ToString());
                                    Entidad.id_grupo = row["id_grupo"].ToString();
                                    Entidad.id_Personal = Convert.ToInt32(row["id_Personal"].ToString());
                                    Entidad.id_Delegacion = Convert.ToInt32(row["id_Delegacion"].ToString());
                                    Entidad.nombre_delegacion = row["nombre_delegacion"].ToString();
                                    Entidad.id_Cargo = Convert.ToInt32(row["id_Cargo"].ToString());

                                    Entidad.id_Empresa = Convert.ToInt32(row["id_Empresa"].ToString());
                                    Entidad.id_Proyecto = Convert.ToInt32(row["id_Proyecto"].ToString());
                                    Entidad.tipoDoc_Personal = row["tipoDoc_Personal"].ToString();
                                    Entidad.nroDoc_Personal = row["nroDoc_Personal"].ToString();
                                    Entidad.apellidos_Personal = row["apellidos_Personal"].ToString();
                                    Entidad.nombres_Personal = row["nombres_Personal"].ToString();
                                
                                    Entidad.fechaIngreso_Personal = row["fechaIngreso_Personal"].ToString();
                                    Entidad.nombre_cargo = row["nombre_cargo"].ToString();
                                    Entidad.tipoPersonal = row["tipoPersonal"].ToString();
                                    Entidad.fechaCese_Personal = row["fechaCese_Personal"].ToString();
                                    Entidad.email_personal = row["email_personal"].ToString();
                                    Entidad.login_Sistema = row["login_Sistema"].ToString();
                                    Entidad.Contrasenia_sistema = row["Contrasenia_sistema"].ToString();
                                    Entidad.id_Perfil = Convert.ToInt32(row["id_Perfil"].ToString());
                                    Entidad.envio_Online = row["envio_Online"].ToString();
                                    Entidad.estado = Convert.ToInt32(row["estado"].ToString());
                                    Entidad.usuario_creacion = row["usuario_creacion"].ToString();
                                    Entidad.fecha_creacion = row["fecha_creacion"].ToString();
                                    Entidad.usuario_edicion = row["usuario_edicion"].ToString();
                                    Entidad.fecha_edicion = row["fecha_edicion"].ToString();
                                    Entidad.id_EmpresaColaboradora = Convert.ToInt32(row["id_EmpresaColaboradora"].ToString());
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

        public string Aceptar_Rechazar(int id_personal, int id_estado)
        {
            string resultado = "";
            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_U_MANT_PERSONAL_APROBAR_RECHAZAR", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_personal", SqlDbType.Int).Value = id_personal;
                        cmd.Parameters.Add("@id_estado", SqlDbType.Int).Value = id_estado;
                        cmd.ExecuteNonQuery();
                    }
                    resultado = "OK";
                }
            }
            catch (Exception e)
            {
                resultado = e.Message;
            }
            return resultado;
        }

        public List<Personal_E> get_Cordinador_jefeObra(int id_delegacion)
        {
            try
            {
                List<Personal_E> obj_List = new List<Personal_E>();
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_OT_PERSONAL_JEFEOBRA_CORDINADOR", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_Delegacion", SqlDbType.Int).Value = id_delegacion;
   

                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Personal_E Entidad = new Personal_E();
                     
                                Entidad.identificador = row["identificador"].ToString();
                                Entidad.id_Personal = Convert.ToInt32(row["id_Personal"].ToString());
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

        
        public Personal_list get_listado_personal_v2(int id_pais, int id_grupo, int id_Delegacion, int estado, string consulta_filtro , int pageindex, int pagesize )
        {
            try
            {
                Personal_list listPersonal = new Personal_list();

                List<Personal_E> obj_List = new List<Personal_E>();
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_MANT_PERSONAL_V2", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_pais", SqlDbType.Int).Value = id_pais;
                        cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo;
                        cmd.Parameters.Add("@id_Delegacion", SqlDbType.Int).Value = id_Delegacion;
                        cmd.Parameters.Add("@estado", SqlDbType.Int).Value = estado;
                        cmd.Parameters.Add("@filtro", SqlDbType.VarChar).Value = consulta_filtro;
                        cmd.Parameters.Add("@Pageindex", SqlDbType.Int).Value = pageindex;
                        cmd.Parameters.Add("@Pagesize", SqlDbType.Int).Value = pagesize;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {                       
                                Personal_E Entidad = new Personal_E();

                                Entidad.id_pais = Convert.ToInt32(dr["id_pais"].ToString());
                                Entidad.id_grupo = dr["id_grupo"].ToString();
                                Entidad.id_Personal = Convert.ToInt32(dr["id_Personal"].ToString());
                                Entidad.id_Delegacion = Convert.ToInt32(dr["id_Delegacion"].ToString());
                                Entidad.nombre_delegacion = dr["nombre_delegacion"].ToString();
                                Entidad.id_Cargo = Convert.ToInt32(dr["id_Cargo"].ToString());

                                Entidad.id_Empresa = Convert.ToInt32(dr["id_Empresa"].ToString());
                                Entidad.id_Proyecto = Convert.ToInt32(dr["id_Proyecto"].ToString());
                                Entidad.tipoDoc_Personal = dr["tipoDoc_Personal"].ToString();
                                Entidad.nroDoc_Personal = dr["nroDoc_Personal"].ToString();
                                Entidad.apellidos_Personal = dr["apellidos_Personal"].ToString();
                                Entidad.nombres_Personal = dr["nombres_Personal"].ToString();

                                Entidad.fechaIngreso_Personal = dr["fechaIngreso_Personal"].ToString();
                                Entidad.nombre_cargo = dr["nombre_cargo"].ToString();
                                Entidad.tipoPersonal = dr["tipoPersonal"].ToString();
                                Entidad.fechaCese_Personal = dr["fechaCese_Personal"].ToString();
                                Entidad.email_personal = dr["email_personal"].ToString();
                                Entidad.login_Sistema = dr["login_Sistema"].ToString();
                                Entidad.Contrasenia_sistema = dr["Contrasenia_sistema"].ToString();
                                Entidad.id_Perfil = Convert.ToInt32(dr["id_Perfil"].ToString());
                                Entidad.envio_Online = dr["envio_Online"].ToString();
                                Entidad.estado = Convert.ToInt32(dr["estado"].ToString());
                                Entidad.usuario_creacion = dr["usuario_creacion"].ToString();
                                Entidad.fecha_creacion = dr["fecha_creacion"].ToString();
                                Entidad.usuario_edicion = dr["usuario_edicion"].ToString();
                                Entidad.fecha_edicion = dr["fecha_edicion"].ToString();
                                Entidad.id_EmpresaColaboradora = Convert.ToInt32(dr["id_EmpresaColaboradora"].ToString());
                                Entidad.codigo_personal = dr["codigo_personal"].ToString();  
                                obj_List.Add(Entidad);
                            }

                            dr.NextResult();

                            while (dr.Read())
                            {
                                listPersonal.totalcount = Convert.ToInt32(dr["totalcount"]);
                            }

                            dr.Close();

                            listPersonal.list_data = obj_List;
                        }
                    }
                }

                return listPersonal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }


        public Personal_list get_listado_personal_V3(int id_pais, int id_grupo, int id_Delegacion, int estado)
        {
            try
            {
                Personal_list listPersonal = new Personal_list();

                List<Personal_E> obj_List = new List<Personal_E>();
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_MANT_PERSONAL_V3", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_pais", SqlDbType.Int).Value = id_pais;
                        cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo;
                        cmd.Parameters.Add("@id_Delegacion", SqlDbType.Int).Value = id_Delegacion;
                        cmd.Parameters.Add("@estado", SqlDbType.Int).Value = estado;

                        using (SqlDataReader dr = cmd.ExecuteReader())
                        {
                            while (dr.Read())
                            {
                                Personal_E Entidad = new Personal_E();

                                Entidad.id_pais = Convert.ToInt32(dr["id_pais"].ToString());
                                Entidad.id_grupo = dr["id_grupo"].ToString();
                                Entidad.id_Personal = Convert.ToInt32(dr["id_Personal"].ToString());
                                Entidad.id_Delegacion = Convert.ToInt32(dr["id_Delegacion"].ToString());
                                Entidad.nombre_delegacion = dr["nombre_delegacion"].ToString();
                                Entidad.id_Cargo = Convert.ToInt32(dr["id_Cargo"].ToString());

                                Entidad.id_Empresa = Convert.ToInt32(dr["id_Empresa"].ToString());
                                Entidad.id_Proyecto = Convert.ToInt32(dr["id_Proyecto"].ToString());
                                Entidad.tipoDoc_Personal = dr["tipoDoc_Personal"].ToString();
                                Entidad.nroDoc_Personal = dr["nroDoc_Personal"].ToString();
                                Entidad.apellidos_Personal = dr["apellidos_Personal"].ToString();
                                Entidad.nombres_Personal = dr["nombres_Personal"].ToString();

                                Entidad.fechaIngreso_Personal = dr["fechaIngreso_Personal"].ToString();
                                Entidad.nombre_cargo = dr["nombre_cargo"].ToString();
                                Entidad.tipoPersonal = dr["tipoPersonal"].ToString();
                                Entidad.fechaCese_Personal = dr["fechaCese_Personal"].ToString();
                                Entidad.email_personal = dr["email_personal"].ToString();
                                Entidad.login_Sistema = dr["login_Sistema"].ToString();
                                Entidad.Contrasenia_sistema = dr["Contrasenia_sistema"].ToString();
                                Entidad.id_Perfil = Convert.ToInt32(dr["id_Perfil"].ToString());
                                Entidad.envio_Online = dr["envio_Online"].ToString();
                                Entidad.estado = Convert.ToInt32(dr["estado"].ToString());
                                Entidad.usuario_creacion = dr["usuario_creacion"].ToString();
                                Entidad.fecha_creacion = dr["fecha_creacion"].ToString();
                                Entidad.usuario_edicion = dr["usuario_edicion"].ToString();
                                Entidad.fecha_edicion = dr["fecha_edicion"].ToString();
                                Entidad.id_EmpresaColaboradora = Convert.ToInt32(dr["id_EmpresaColaboradora"].ToString());
                                Entidad.codigo_personal = dr["codigo_personal"].ToString();
                                obj_List.Add(Entidad);
                            }

                            dr.Close();
                            listPersonal.totalcount = 0;          
                            listPersonal.list_data = obj_List;
                        }
                    }
                }

                return listPersonal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public object personalesBandejaAtencion()
        {
            Resultado res = new Resultado();
            try
            {
                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("SP_S_BANDEJA_ATENCION_PERSONALES", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

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

        public List<Personal_E> get_Inspector_responsableCorreccion(int id_pais, int id_grupo, string obj_id_Delegacion, int idusuario)
        {
            try
            {
                List<Personal_E> obj_List = new List<Personal_E>();

                using (SqlConnection cn = new SqlConnection(cadenaCnx))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("NEW_PERSONAL_PAIS_GRUPO_DELEGACION", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_pais", SqlDbType.Int).Value = id_pais;
                        cmd.Parameters.Add("@id_grupo", SqlDbType.Int).Value = id_grupo;
                        cmd.Parameters.Add("@obj_id_Delegacion", SqlDbType.VarChar).Value = obj_id_Delegacion;
                        cmd.Parameters.Add("@id_usuario", SqlDbType.Int).Value = idusuario;

                        DataTable dt_detalle = new DataTable();

                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);

                            foreach (DataRow row in dt_detalle.Rows)
                            {
                                Personal_E Entidad = new Personal_E();

                                Entidad.id_grupo = row["id_grupo"].ToString();
                                Entidad.id_Personal = Convert.ToInt32(row["id_Personal"].ToString());
                                Entidad.id_Cargo = Convert.ToInt32(row["id_Cargo"].ToString());
                                Entidad.apellidos_Personal = row["apellidos_Personal"].ToString();
                                Entidad.nombres_Personal = row["nombres_Personal"].ToString();
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
