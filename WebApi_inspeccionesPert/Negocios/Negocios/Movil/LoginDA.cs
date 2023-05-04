using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Entidades.Movil;
using System.Data;

namespace Negocios.Movil
{
    public class LoginDA
    {
        private static string db = ConfigurationManager.ConnectionStrings["conexionDsige"].ConnectionString;

        //Login

        public static Persona GetOne(string users)
        {
            try
            {
                using (SqlConnection cn = new SqlConnection(db))
                {
                    Persona entity = null;
                    SqlCommand cmd = cn.CreateCommand();
                    cmd.CommandType = System.Data.CommandType.Text;
                    cmd.CommandText = "SELECT id_Personal,id_Empresa,id_Delegacion,id_Proyecto,apellidos_Personal,nombres_Personal,id_Perfil,email_personal,envio_Online,Contrasenia_sistema,login_Sistema FROM tbl_Personal WHERE login_Sistema=@Users";
                    cmd.Parameters.AddWithValue("@Users", users);
                    cmd.Connection.Open();
                    SqlDataReader rd = cmd.ExecuteReader();

                    while (rd.Read())
                    {
                        entity = new Persona()
                        {
                            PersonaId = rd.GetInt32(0),
                            EmpresaId = rd.GetInt32(1),
                            DelegacionId = rd.GetInt32(2),
                            ProyectoId = rd.GetInt32(3),
                            Apellidos = rd.GetString(4),
                            Nombres = rd.GetString(5),
                            Perfil = rd.GetInt32(6),
                            Email = rd.GetString(7),
                            EnvioOnline = rd.GetString(8),
                            Pass = rd.GetString(9),
                            Users = rd.GetString(10)
                        };

                    }
                    return entity;

                }
            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        public static Usuario GetUsuario(string usuario, string password, string version, string imei)
        {
            try
            {
                Usuario user = null;
                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    //cmd.CommandText = "USP_LOGIN_USUARIO_ACCESO";
                    cmd.CommandText = "USP_LOGIN_USUARIO_ACCESO_IRVIN";
                    cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario;
                    cmd.Parameters.Add("@version", SqlDbType.VarChar).Value = version;
                    cmd.Parameters.Add("@imei", SqlDbType.VarChar).Value = imei;
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        user = new Usuario();
                        dr.Read();
                        if (password == dr.GetString(4))
                        {
                            if (dr.GetInt32(6) == 1)
                            {
                                user.id_usuario = dr.GetInt32(0);
                                user.nro_documento = dr.GetString(1);
                                user.datos_personales = dr.GetString(2);
                                user.correo_electronico = dr.GetString(3);
                                user.id_perfil = dr.GetInt32(5);
                                user.estado = dr.GetInt32(6);
                                user.empresaColaboradora = dr.GetInt32(7);
                                user.directoEmpresaColaboradora = dr.GetInt32(8);
                                user.mensaje = "Go";

                                SqlCommand cmdConfig = con.CreateCommand();
                                cmdConfig.CommandType = System.Data.CommandType.StoredProcedure;
                                //cmdConfig.CommandText = "USP_USUARIO_CONFIGURATION";
                                cmdConfig.CommandText = "USP_USUARIO_CONFIGURATION_IRVIN";
                                cmdConfig.Parameters.Add("@id", SqlDbType.Int).Value = user.id_usuario;
                                SqlDataReader drs = cmdConfig.ExecuteReader();
                                if (drs.HasRows)
                                {
                                    user.configuracions = new List<Configuracion>();
                                    while (drs.Read())
                                    {

                                        Configuracion conf = new Configuracion();

                                        conf.id_usuario_configuracion = drs.GetInt32(0);
                                        conf.id_usuario = drs.GetInt32(1);
                                        conf.estado = drs.GetInt32(7);

                                        SqlCommand cmdPais = con.CreateCommand();
                                        cmdPais.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmdPais.CommandText = "USP_GET_PAIS";
                                        cmdPais.Parameters.Add("@id_pais", SqlDbType.Int).Value = drs.GetInt32(2);
                                        SqlDataReader pdr = cmdPais.ExecuteReader();
                                        Pais pais = null;
                                        if (pdr.HasRows)
                                        {
                                            pais = new Pais();
                                            pdr.Read();
                                            pais.id_pais = pdr.GetInt32(0);
                                            pais.descripcion = pdr.GetString(1);
                                            conf.pais = pais;
                                        }
                                        else conf.pais = pais;

                                        SqlCommand cmdGrupo = con.CreateCommand();
                                        cmdGrupo.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmdGrupo.CommandText = "USP_LIST_GRUPO";
                                        cmdGrupo.Parameters.Add("@id_grupo", SqlDbType.Int).Value = drs.GetInt32(3);
                                        SqlDataReader gdr = cmdGrupo.ExecuteReader();
                                        List<Grupo> grupos = null;
                                        if (gdr.HasRows)
                                        {
                                            grupos = new List<Grupo>();
                                            while (gdr.Read())
                                            {
                                                grupos.Add(new Grupo()
                                                {
                                                    id_grupo = gdr.GetInt32(0),
                                                    id_pais = gdr.GetInt32(1),
                                                    descripcion = gdr.GetString(2),
                                                    estado = gdr.GetInt32(3)
                                                });
                                            }
                                            conf.grupos = grupos;

                                        }
                                        else conf.grupos = grupos;

                                        SqlCommand cmdDelegacion = con.CreateCommand();
                                        cmdDelegacion.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmdDelegacion.CommandText = "USP_LIST_DELEGACION";
                                        cmdDelegacion.Parameters.Add("@id_delegacion", SqlDbType.Int).Value = drs.GetInt32(5);
                                        SqlDataReader ddr = cmdDelegacion.ExecuteReader();
                                        List<Delegacion> delegacion = null;
                                        if (ddr.HasRows)
                                        {
                                            delegacion = new List<Delegacion>();
                                            while (ddr.Read())
                                            {
                                                delegacion.Add(new Delegacion()
                                                {
                                                    id_Delegacion = ddr.GetInt32(0),
                                                    id_Grupo = ddr.GetInt32(1),
                                                    codigo_delegacion = ddr.GetString(2),
                                                    nombre_delegacion = ddr.GetString(3),
                                                    estado = ddr.GetInt32(4)
                                                });
                                            }
                                            conf.delegacions = delegacion;
                                        }
                                        else conf.delegacions = delegacion;

                                        // Changed by Ot -- Proyecto -- Empresa

                                        SqlCommand cmdOT = con.CreateCommand();
                                        cmdOT.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmdOT.CommandText = "USP_GET_OT";
                                        cmdOT.Parameters.Add("@otId", SqlDbType.Int).Value = drs.GetInt32(6);
                                        SqlDataReader drOt = cmdOT.ExecuteReader();

                                        if (drOt.HasRows)
                                        {
                                            List<Ot> ot = new List<Ot>();
                                            while (drOt.Read())
                                            {
                                                ot.Add(new Ot()
                                                {
                                                    id_OT = drOt.GetInt32(0),
                                                    codigo_ot = drOt.GetString(1),
                                                    nombre_ot = drOt.GetString(2),
                                                    Tipo_OT = drOt.GetString(3),
                                                    id_Proyecto = drOt.GetInt32(4),
                                                    id_Cliente = drOt.GetInt32(5),
                                                    id_delegacion = drOt.GetInt32(6),
                                                    id_grupo = drOt.GetInt32(7),
                                                    id_Pais = drOt.GetInt32(8),
                                                    id_Personal_JefeObra = drOt.GetInt32(9),
                                                    id_Personal_Coordinador = drOt.GetInt32(10),
                                                    id_Actividad = drOt.GetInt32(11),
                                                    estado = drOt.GetInt32(12)
                                                });
                                            }
                                            conf.ots = ot;
                                        }

                                        user.configuracions.Add(conf);
                                    }
                                    drs.Close();
                                }
                            }
                            else user.mensaje = "Permisos";                           
                        }
                        else user.mensaje = "Pass";
                    }
                    con.Close();
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<DelegacionRegistro> GetDelegacion(int id_Pais)
        {
            try
            {
                List<DelegacionRegistro> result = null;
                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();

                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "NEW_GET_DELEGACION";
                    cmd.Parameters.AddWithValue("@id_Pais", id_Pais);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        result = new List<DelegacionRegistro>();
                        while (dr.Read())
                        {
                            DelegacionRegistro item = new DelegacionRegistro()
                            {
                                Id_Delegacion = Convert.ToInt32(dr["id_Delegacion"].ToString()),
                                Codigo_delegacion = dr["codigo_delegacion"].ToString(),
                                Nombre_delegacion = dr["nombre_delegacion"].ToString()
                            };
                            result.Add(item);
                        }
                    }
                    con.Close();
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Mensaje Registrar_usuario(UsuarioRegistro a)
        {
            Mensaje result = null;
            try
            {
                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "NEW_INSERT_USUARIO_REGISTRO";
                    cmd.Parameters.AddWithValue("@id_pais", a.Id_pais);
                    cmd.Parameters.AddWithValue("@id_delegacion", a.Id_delegacion);
                    cmd.Parameters.AddWithValue("@estado", a.Estado);
                    cmd.Parameters.AddWithValue("@id_Personal", a.Id_Personal);
                    cmd.Parameters.AddWithValue("@nro_documento", a.Nro_documento);
                    cmd.Parameters.AddWithValue("@email_personal", a.Email_personal);
                    cmd.Parameters.AddWithValue("@mensaje", "");
                    cmd.Parameters["@mensaje"].Direction = ParameterDirection.InputOutput;
                    cmd.ExecuteNonQuery();
                    result = new Mensaje();
                    result.mensaje = ShowResult((string)cmd.Parameters["@mensaje"].Value);
                    cmd.Connection.Close();
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public Mensaje Validar_Dominio(string dominio)
        {
            Mensaje result = null;
            try
            {
                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "NEW_DOMINIO_EXISTE";
                    cmd.Parameters.AddWithValue("@dominio", dominio);
                    cmd.Parameters.AddWithValue("@mensaje", "");
                    cmd.Parameters["@mensaje"].Direction = ParameterDirection.InputOutput;
                    cmd.ExecuteNonQuery();
                    result = new Mensaje();
                    result.mensaje = (string)cmd.Parameters["@mensaje"].Value;
                    cmd.Connection.Close();
                }
                return result;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public GetPersonal Get_Personal_Doc(int id_Pais, string nro_Doc)
        {
            try
            {
                GetPersonal result = null;
                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = CommandType.StoredProcedure;
                    cmd.CommandText = "NEW_NRO_DOCUMENTO";
                    cmd.Parameters.AddWithValue("@id_Pais", id_Pais);
                    cmd.Parameters.AddWithValue("@nro_Doc", nro_Doc);
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        result = new GetPersonal();
                        while (dr.Read())
                        {
                            result.Id_Personal = Convert.ToInt32(dr["id_Personal"].ToString());
                            result.NroDoc_Personal = dr["nroDoc_Personal"].ToString();
                            result.Nombres_Personal = dr["apellidos_Personal"].ToString();
                            result.Apellidos_Personal = dr["nombres_Personal"].ToString();
                            result.Email_personal = dr["email_personal"].ToString();
                            result.Id_delegacion = Convert.ToInt32(dr["id_Delegacion"].ToString());
                        }
                    }
                }
                return result;

            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public string ShowResult(String mensaje)
        {
            string resul = "";
            if (mensaje == "1")
            {
                resul = "1";
            }
            else if (mensaje == "2")
            {
                resul = "El nro. Documento ingresado ya existe...!";
            }
            else if (mensaje == "3")
            {
                resul = "El nro. Documento ingresado no esta registrado en el sistema...!";
            }
            return resul;
        }

        // Mapeando

        public static Usuario GetUsuarioMapeado(string usuario, string password, string version, string imei)
        {
            try
            {
                Usuario user = null;
                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "USP_LOGIN_USUARIO_ACCESO";
                    cmd.Parameters.Add("@usuario", SqlDbType.VarChar).Value = usuario;
                    cmd.Parameters.Add("@version", SqlDbType.VarChar).Value = version;
                    cmd.Parameters.Add("@imei", SqlDbType.VarChar).Value = imei;
                    SqlDataReader dr = cmd.ExecuteReader();
                    if (dr.HasRows)
                    {
                        user = new Usuario();
                        dr.Read();
                        if (password == dr.GetString(4))
                        {
                            user.id_usuario = dr.GetInt32(0);
                            user.nro_documento = dr.GetString(1);
                            user.datos_personales = dr.GetString(2);
                            user.correo_electronico = dr.GetString(3);
                            user.id_perfil = dr.GetInt32(5);
                            user.estado = dr.GetInt32(6);
                            user.mensaje = "Go";

                            SqlCommand cmdConfig = con.CreateCommand();
                            cmdConfig.CommandType = System.Data.CommandType.StoredProcedure;
                            cmdConfig.CommandText = "USP_USUARIO_CONFIGURATION";
                            cmdConfig.Parameters.Add("@id", SqlDbType.Int).Value = user.id_usuario;
                            SqlDataReader drs = cmdConfig.ExecuteReader();
                            if (drs.HasRows)
                            {
                                user.configuracions = new List<Configuracion>();
                                while (drs.Read())
                                {

                                    Configuracion conf = new Configuracion();

                                    conf.id_usuario_configuracion = drs.GetInt32(0);
                                    conf.id_usuario = drs.GetInt32(1);
                                    conf.estado = drs.GetInt32(6);

                                    SqlCommand cmdPais = con.CreateCommand();
                                    cmdPais.CommandType = System.Data.CommandType.StoredProcedure;
                                    cmdPais.CommandText = "USP_GET_PAIS";
                                    cmdPais.Parameters.Add("@id_pais", SqlDbType.Int).Value = drs.GetInt32(2);
                                    SqlDataReader pdr = cmdPais.ExecuteReader();
                                    Pais pais = null;
                                    if (pdr.HasRows)
                                    {
                                        pais = new Pais();
                                        pdr.Read();
                                        pais.id_pais = pdr.GetInt32(0);
                                        pais.descripcion = pdr.GetString(1);

                                        SqlCommand cmdGrupo = con.CreateCommand();
                                        cmdGrupo.CommandType = System.Data.CommandType.StoredProcedure;
                                        cmdGrupo.CommandText = "USP_LIST_GRUPO";
                                        cmdGrupo.Parameters.Add("@id_grupo", SqlDbType.Int).Value = drs.GetInt32(3);
                                        SqlDataReader gdr = cmdGrupo.ExecuteReader();
                                        List<Grupo> grupos = new List<Grupo>();
                                        if (gdr.HasRows)
                                        {
                                            grupos = new List<Grupo>();
                                            while (gdr.Read())
                                            {
                                                Grupo g = new Grupo();
                                                g.id_grupo = gdr.GetInt32(0);
                                                g.id_pais = gdr.GetInt32(1);
                                                g.descripcion = gdr.GetString(2);
                                                g.estado = gdr.GetInt32(3);

                                                SqlCommand cmdDelegacion = con.CreateCommand();
                                                cmdDelegacion.CommandType = System.Data.CommandType.StoredProcedure;
                                                cmdDelegacion.CommandText = "USP_LIST_DELEGACION";
                                                cmdDelegacion.Parameters.Add("@id_delegacion", SqlDbType.Int).Value = drs.GetInt32(5);
                                                SqlDataReader ddr = cmdDelegacion.ExecuteReader();
                                                List<Delegacion> delegacion = new List<Delegacion>();
                                                if (ddr.HasRows)
                                                {
                                                    Delegacion d = new Delegacion();
                                                    while (ddr.Read())
                                                    {

                                                        d.id_Delegacion = ddr.GetInt32(0);
                                                        d.id_Grupo = ddr.GetInt32(1);
                                                        d.codigo_delegacion = ddr.GetString(2);
                                                        d.nombre_delegacion = ddr.GetString(3);
                                                        d.estado = ddr.GetInt32(4);

                                                        SqlCommand cmdProyecto = con.CreateCommand();
                                                        cmdProyecto.CommandType = System.Data.CommandType.StoredProcedure;
                                                        cmdProyecto.CommandText = "USP_LIST_PROYECTO";
                                                        cmdProyecto.Parameters.Add("@id_proyecto", SqlDbType.Int).Value = drs.GetInt32(4);
                                                        SqlDataReader edr = cmdProyecto.ExecuteReader();
                                                        List<Proyecto> proyecto = null;
                                                        if (edr.HasRows)
                                                        {
                                                            proyecto = new List<Proyecto>();
                                                            while (edr.Read())
                                                            {
                                                                proyecto.Add(new Proyecto()
                                                                {
                                                                    proyectoId = edr.GetInt32(0),
                                                                    nombre = edr.GetString(1),
                                                                    id_Delegacion = edr.GetInt32(2)
                                                                });
                                                            }
                                                            d.proyectos = proyecto;
                                                        }
                                                        delegacion.Add(d);
                                                    }
                                                    g.delegacions = delegacion;
                                                }
                                                grupos.Add(g);
                                            }
                                            pais.grupos = grupos;
                                        }
                                        conf.pais = pais;
                                    }
                                    else conf.pais = pais;

                                    user.configuracions.Add(conf);
                                }
                                drs.Close();
                            }
                        }
                        else user.mensaje = "Pass";
                    }
                    con.Close();
                }
                return user;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
