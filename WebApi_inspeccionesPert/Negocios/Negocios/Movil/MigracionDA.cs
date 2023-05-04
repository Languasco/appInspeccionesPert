using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Configuration;
using System.Data.SqlClient;
using Entidades.Movil;
using System.Data;
using System.Globalization;

namespace Negocios.Movil
{

    public class MigracionDA
    {
        private static string db = ConfigurationManager.ConnectionStrings["conexionDsige"].ConnectionString;
        public DataTable Get_Cuadrilla_Nombre(int id_inspeccion)
        {
            DataTable resultado = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_cuadrilla_nombre", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_inspeccion", SqlDbType.Int).Value = id_inspeccion;

                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                            resultado = dt_detalle;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;

        }
        public DataTable Get_Cuadrilla_Incidencia(int id_inspeccion)
        {
            DataTable resultado = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_cuadrilla_incidencia", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_inspeccion", SqlDbType.Int).Value = id_inspeccion;

                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                            resultado = dt_detalle;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;

        }
        public DataTable Get_Cuadrilla_Detallado(int id_inspeccion)
        {
            DataTable resultado = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("usp_cuadrilla_detallado", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_inspeccion", SqlDbType.Int).Value = id_inspeccion;

                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                            resultado = dt_detalle;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;

        }
        public DataTable Get_DetallesAnomalias(int tipoFormato, int id_inspeccion, int empresaId)
        {
            DataTable resultado = new DataTable();
            try
            {
                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("DSIGE_Reporte_Inspecciones", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;

                        cmd.Parameters.Add("@TipoFormato", SqlDbType.Int).Value = tipoFormato;
                        cmd.Parameters.Add("@Inspeccion", SqlDbType.Int).Value = id_inspeccion;
                        cmd.Parameters.Add("@Id_Empresa", SqlDbType.Int).Value = empresaId;

                        DataTable dt_detalle = new DataTable();
                        using (SqlDataAdapter da = new SqlDataAdapter(cmd))
                        {
                            da.Fill(dt_detalle);
                            resultado = dt_detalle;
                        }
                    }
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
            return resultado;
        }
        public static Personal savePersonal(Filtro f)
        {
            try
            {
                Personal personal = null;
                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();
                    //using (SqlCommand cmd = new SqlCommand("USP_SAVE_PERSONAL", cn))
                    using (SqlCommand cmd = new SqlCommand("USP_SAVE_PERSONAL_IRVIN", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@id_Pais", SqlDbType.Int).Value = f.paisId;
                        cmd.Parameters.Add("@id_Grupo", SqlDbType.Int).Value = f.grupoId;
                        cmd.Parameters.Add("@id_Delegacion", SqlDbType.Int).Value = f.delegacionId;
                        cmd.Parameters.Add("@tipoDoc_Personal", SqlDbType.VarChar).Value = f.tipoDoc;
                        cmd.Parameters.Add("@nroDoc_Personal", SqlDbType.VarChar).Value = f.nroDoc;
                        cmd.Parameters.Add("@apellidos_Personal", SqlDbType.VarChar).Value = f.apellidos;
                        cmd.Parameters.Add("@nombres_Personal", SqlDbType.VarChar).Value = f.nombre;
                        cmd.Parameters.Add("@id_Cargo", SqlDbType.Int).Value = f.cargoId;
                        cmd.Parameters.Add("@usuario_creacion", SqlDbType.Int).Value = f.inspeccionId;
                        cmd.Parameters.Add("@email", SqlDbType.VarChar).Value = f.email;
                        cmd.Parameters.Add("@fecha", SqlDbType.VarChar).Value = f.fecha;

                        SqlDataReader rd = cmd.ExecuteReader();
                        if (rd.HasRows)
                        {
                            while (rd.Read())
                            {
                                personal = new Personal()
                                {
                                    PersonalId = rd.GetInt32(0),
                                    NroDoc = rd.GetString(1),
                                    Apellido = rd.GetString(2),
                                    Nombre = rd.GetString(3),
                                    CargoId = rd.GetInt32(4),
                                    NombreCargo = rd.GetString(5),
                                    Email = rd.GetString(6)
                                };
                            }
                        }

                    }
                    cn.Close();
                }
                return personal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static VersionAndroid GetVersion(string v)
        {
            try
            {
                VersionAndroid version = null;

                using (SqlConnection cn = new SqlConnection(db))
                {

                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("USP_GET_VERSION", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "USP_GET_VERSION";
                        cmd.Parameters.Add("@version", SqlDbType.VarChar).Value = v;
                        SqlDataReader rd = cmd.ExecuteReader();
                        if (rd.HasRows)
                        {
                            while (rd.Read())
                            {
                                version = new VersionAndroid()
                                {
                                    code = rd.GetInt32(0),
                                    name = rd.GetString(1)
                                };
                            }
                        }
                    }
                    cn.Close();
                }
                return version;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static MensajeRetorno saveInspeccion(InspeccionCab Row)
        {
            try
            {
                int inspeccionCabId;
                int inspeccionDetalleId;
                MensajeRetorno m = null;

                List<DetalleRetorno> d = null;

                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();
                    SqlCommand cmdInspeccion = con.CreateCommand();
                    cmdInspeccion.CommandTimeout = 0;
                    cmdInspeccion.CommandType = CommandType.StoredProcedure;
                    cmdInspeccion.CommandText = "USP_SAVE_INSPECCION_CAB";
                    cmdInspeccion.Parameters.AddWithValue("@ResultadoId", Row.inspeccionCab);
                    cmdInspeccion.Parameters.AddWithValue("@id_Empresa", Row.EmpresaInspectorId);
                    cmdInspeccion.Parameters.AddWithValue("@id_Delegacion", Row.DelegacionInspectorId);
                    cmdInspeccion.Parameters.AddWithValue("@id_Proyecto ", Row.ProyectoInspectorId);
                    cmdInspeccion.Parameters.AddWithValue("@id_Personal_Inspector", Row.PersonalInspectorId);
                    cmdInspeccion.Parameters.AddWithValue("@id_EmpresaColaboradora ", Row.EmpresaId);
                    cmdInspeccion.Parameters.AddWithValue("@lugar_Inspeccion", Row.Lugar);
                    cmdInspeccion.Parameters.AddWithValue("@actividadOT_Inspeccion", Row.ActividadOT);
                    cmdInspeccion.Parameters.AddWithValue("@trabajoArealizar_Inspeccion", Row.TrabajoRealizar);
                    cmdInspeccion.Parameters.AddWithValue("@id_Cargo", Row.CargoId);
                    cmdInspeccion.Parameters.AddWithValue("@id_Personal_Inspeccionado", Row.PersonalId);
                    cmdInspeccion.Parameters.AddWithValue("@id_Area ", Row.AreaId);
                    cmdInspeccion.Parameters.AddWithValue("@id_Personal_Coordinador", Row.CoordinadorId);
                    cmdInspeccion.Parameters.AddWithValue("@id_Personal_JefeObra", Row.JefeObraId);
                    cmdInspeccion.Parameters.AddWithValue("@placa_Inspeccion", Row.Placa);
                    cmdInspeccion.Parameters.AddWithValue("@id_NivelInspeccion", Row.NivelInspeccion);
                    cmdInspeccion.Parameters.AddWithValue("@id_TipoInspeccion", Row.TipoInspeccionId);
                    cmdInspeccion.Parameters.AddWithValue("@Resultado_Inspeccion", Row.ResultadoInspeccion);
                    cmdInspeccion.Parameters.AddWithValue("@iniciofin_Trabajo", Row.InicioFinal);
                    cmdInspeccion.Parameters.AddWithValue("@id_Anomalia", Row.TipoAnomaliaId);
                    cmdInspeccion.Parameters.AddWithValue("@accionPropuesta_Correctiva", Row.AccionPropuesto);
                    cmdInspeccion.Parameters.AddWithValue("@id_Personal_Responsable", Row.PersonalResponsable);
                    cmdInspeccion.Parameters.AddWithValue("@fechaPropuesta_Correctiva", Row.FechaCorrecion);
                    cmdInspeccion.Parameters.AddWithValue("@observacion_Correctiva", Row.ObservacionAccion);
                    cmdInspeccion.Parameters.AddWithValue("@paralizacion_Correctiva", Row.ParalizacionTrabajo);
                    cmdInspeccion.Parameters.AddWithValue("@sancion_Correctiva", Row.AlgunaSancion);
                    cmdInspeccion.Parameters.AddWithValue("@id_TipoSancion", Row.TipoSancionId);
                    cmdInspeccion.Parameters.AddWithValue("@nroTrabajadores_Correctiva", Row.NumeroSancionados);
                    cmdInspeccion.Parameters.AddWithValue("@fecha_Inspeccion", Row.FechaInspeccion);
                    cmdInspeccion.Parameters.AddWithValue("@id_cliente", Row.ClienteId);
                    cmdInspeccion.Parameters.AddWithValue("@latitud", Row.latitud);
                    cmdInspeccion.Parameters.AddWithValue("@longitud", Row.longitud);
                    cmdInspeccion.Parameters.AddWithValue("@id_actividad", Row.ActividadId);
                    cmdInspeccion.Parameters.AddWithValue("@conjuntas", Row.Conjuntas);
                    cmdInspeccion.Parameters.AddWithValue("@id_pais", Row.PaisId);
                    cmdInspeccion.Parameters.AddWithValue("@id_grupo", Row.GrupoId);
                    cmdInspeccion.Parameters.AddWithValue("@ot_Id", Row.otId);
                    SqlDataReader drInspeccion = cmdInspeccion.ExecuteReader();

                    m = new MensajeRetorno();
                    if (drInspeccion.HasRows)
                    {
                        while (drInspeccion.Read())
                        {
                            inspeccionCabId = drInspeccion.GetInt32(0);

                            m.mensaje = "Enviado";
                            m.inspeccionCab = inspeccionCabId;
                            d = new List<DetalleRetorno>();
                            foreach (var r in Row.inspeccionDetalle)
                            {
                                SqlCommand cmdDetalle = con.CreateCommand();
                                cmdDetalle.CommandTimeout = 0;
                                cmdDetalle.CommandType = CommandType.StoredProcedure;
                                cmdDetalle.CommandText = "USP_SAVE_INSPECCION_CAB_DETALLE_NEW";
                                cmdDetalle.Parameters.Add("@id_inspeccion_detalle", SqlDbType.Int).Value = r.inspeccionCabDetalle;
                                cmdDetalle.Parameters.Add("@id_inspeccion", SqlDbType.Int).Value = inspeccionCabId;
                                cmdDetalle.Parameters.Add("@id_personal", SqlDbType.Int).Value = r.PersonalId;
                                cmdDetalle.Parameters.Add("@id_anomalia", SqlDbType.Int).Value = r.AnomaliaId;
                                cmdDetalle.Parameters.Add("@descripcion", SqlDbType.VarChar).Value = r.Detalle;
                                cmdDetalle.Parameters.Add("@valor", SqlDbType.VarChar).Value = r.Valor;
                                cmdDetalle.Parameters.Add("@levantamiento", SqlDbType.VarChar).Value = r.Levantamiento;
                                cmdDetalle.Parameters.Add("@foto_levantamiento", SqlDbType.VarChar).Value = r.LevantamientoFoto;
                                cmdDetalle.Parameters.Add("@descripcion_levantamiento", SqlDbType.VarChar).Value = r.LevantamientoDescripcion;
                                cmdDetalle.Parameters.Add("@accionPropuesta_Correctiva", SqlDbType.VarChar).Value = r.AccionPropuesto;
                                cmdDetalle.Parameters.Add("@fechaPropuesta_Correctiva", SqlDbType.VarChar).Value = r.FechaCorrecion;
                                cmdDetalle.Parameters.Add("@usuario_creacion", SqlDbType.Int).Value = r.Usuario;
                                cmdDetalle.Parameters.Add("@fecha_creacion", SqlDbType.VarChar).Value = r.Fecha;
                                cmdDetalle.Parameters.Add("@latitud", SqlDbType.VarChar).Value = r.latitud;
                                cmdDetalle.Parameters.Add("@longitud", SqlDbType.VarChar).Value = r.longitud;
                                cmdDetalle.Parameters.Add("@disponibilidad", SqlDbType.VarChar).Value = r.disponividad_uso;
                                cmdDetalle.Parameters.Add("@size", SqlDbType.Int).Value = r.size;
                                cmdDetalle.Parameters.Add("@id_tipoInspeccion", SqlDbType.Int).Value = r.TipoInspeccionId;
                                cmdDetalle.Parameters.Add("@personal_sancion", SqlDbType.Int).Value = r.personalSancionado;
                                cmdDetalle.Parameters.Add("@anomalia_critica", SqlDbType.Int).Value = r.anomalia_critica;
                                cmdDetalle.Parameters.Add("@id_anomalia_detalle", SqlDbType.Int).Value = r.anomaliaDetalleId;

                                SqlDataReader drDetalle = cmdDetalle.ExecuteReader();

                                if (drDetalle.HasRows)
                                {
                                    while (drDetalle.Read())
                                    {
                                        inspeccionDetalleId = drDetalle.GetInt32(0);

                                        foreach (var photo in r.inspeccionCabPhoto)
                                        {
                                            SqlCommand cmdPhoto = con.CreateCommand();
                                            cmdPhoto.CommandTimeout = 0;
                                            cmdPhoto.CommandType = CommandType.StoredProcedure;
                                            cmdPhoto.CommandText = "USP_SAVE_INSPECCION_CAB_DETALLE_FOTO";
                                            cmdPhoto.Parameters.Add("@id_inspeccion_detalle", SqlDbType.Int).Value = inspeccionDetalleId;
                                            cmdPhoto.Parameters.Add("@nombre_foto", SqlDbType.VarChar).Value = photo.Foto;
                                            cmdPhoto.Parameters.Add("@fecha_creacion", SqlDbType.VarChar).Value = photo.Fecha;
                                            cmdPhoto.Parameters.Add("@size", SqlDbType.Int).Value = photo.size;
                                            cmdPhoto.Parameters.Add("@tipo", SqlDbType.VarChar).Value = photo.tipo;
                                            cmdPhoto.ExecuteNonQuery();
                                        }

                                        d.Add(new DetalleRetorno()
                                        {
                                            id = r.Id,
                                            inspeccionCabDetalle = inspeccionDetalleId
                                        });
                                    }
                                }
                            }
                            m.detalle = d;
                        }
                    }
                    else
                    {
                        m.mensaje = "Error";
                        return m;
                    }

                    con.Close();
                    return m;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public static Migracion getMigracion(Filtro f)
        {
            try
            {
                Migracion migracion = new Migracion();
                migracion.id = 1;
                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();

                    // Version

                    SqlCommand cmdVersion = con.CreateCommand();
                    cmdVersion.CommandTimeout = 0;
                    cmdVersion.CommandType = CommandType.StoredProcedure;
                    cmdVersion.CommandText = "USP_GET_VERSION";
                    cmdVersion.Parameters.Add("@version", SqlDbType.VarChar).Value = f.version;

                    SqlDataReader drVersion = cmdVersion.ExecuteReader();
                    if (!drVersion.HasRows)
                    {
                        migracion.mensaje = "Actualizar Versión del Aplicativo.";
                    }
                    else
                    {
                        // EMPRESA COLABORADORA
                        SqlCommand cmdEmpresa = con.CreateCommand();
                        cmdEmpresa.CommandTimeout = 0;
                        cmdEmpresa.CommandType = CommandType.StoredProcedure;
                        cmdEmpresa.CommandText = "USP_LIST_EMPRESACOLABORADORA";
                        cmdEmpresa.Parameters.Add("@id_grupo", SqlDbType.Int).Value = f.grupoId;
                        SqlDataReader drEmpresa = cmdEmpresa.ExecuteReader();
                        if (drEmpresa.HasRows)
                        {
                            List<EmpresaColaboradora> empresa = new List<EmpresaColaboradora>();
                            while (drEmpresa.Read())
                            {
                                empresa.Add(new EmpresaColaboradora()
                                {
                                    EmpresaId = drEmpresa.GetInt32(0),
                                    Ruc = drEmpresa.GetString(1),
                                    RazonSocial = drEmpresa.GetString(2),
                                    Directo = drEmpresa.GetInt32(3)
                                });
                            }
                            migracion.empresaColaboradoras = empresa;
                        }

                        SqlCommand cmdCargo = con.CreateCommand();
                        cmdCargo.CommandTimeout = 0;
                        cmdCargo.CommandType = CommandType.StoredProcedure;
                        cmdCargo.CommandText = "NEW_GETCARGO";
                        cmdCargo.Parameters.Add("@grupoId", SqlDbType.Int).Value = f.grupoId;
                        SqlDataReader drCargo = cmdCargo.ExecuteReader();
                        if (drCargo.HasRows)
                        {
                            List<Cargo> cargo = new List<Cargo>();
                            while (drCargo.Read())
                            {
                                cargo.Add(new Cargo()
                                {
                                    CargoId = drCargo.GetInt32(0),
                                    Nombre = drCargo.GetString(1)
                                });
                            }
                            migracion.cargos = cargo;
                        }

                        // PERSONAL
                        SqlCommand cmdPersonal = con.CreateCommand();
                        cmdPersonal.CommandTimeout = 0;
                        cmdPersonal.CommandType = CommandType.StoredProcedure;
                        cmdPersonal.CommandText = "USP_LIST_PERSONAL";
                        cmdPersonal.Parameters.Add("@id_Pais", SqlDbType.Int).Value = f.paisId;
                        cmdPersonal.Parameters.Add("@id_Delegacion", SqlDbType.Int).Value = f.delegacionId;
                        cmdPersonal.Parameters.Add("@id_grupo", SqlDbType.Int).Value = f.grupoId;
                        SqlDataReader drPersonal = cmdPersonal.ExecuteReader();
                        if (drPersonal.HasRows)
                        {
                            List<Personal> personal = new List<Personal>();
                            while (drPersonal.Read())
                            {
                                personal.Add(new Personal()
                                {
                                    PersonalId = drPersonal.GetInt32(0),
                                    NroDoc = drPersonal.GetString(1),
                                    Apellido = drPersonal.GetString(2),
                                    Nombre = drPersonal.GetString(3),
                                    CargoId = drPersonal.GetInt32(4),
                                    NombreCargo = drPersonal.GetString(5),
                                    Email = drPersonal.GetString(6),
                                    EmpresaColaboradoraId = drPersonal.GetInt32(7)
                                });
                            }
                            migracion.personals = personal;
                        }

                        // AREAS
                        SqlCommand cmdAreas = con.CreateCommand();
                        cmdAreas.CommandTimeout = 0;
                        cmdAreas.CommandType = CommandType.StoredProcedure;
                        cmdAreas.CommandText = "USP_LIST_AREAS";
                        cmdAreas.Parameters.Add("@id_delegacion", SqlDbType.Int).Value = f.delegacionId;
                        SqlDataReader drAreas = cmdAreas.ExecuteReader();
                        if (drAreas.HasRows)
                        {
                            List<Area> area = new List<Area>();
                            while (drAreas.Read())
                            {
                                area.Add(new Area()
                                {
                                    AreaId = drAreas.GetInt32(0),
                                    Descripcion = drAreas.GetString(1)
                                });
                            }
                            migracion.areas = area;
                        }

                        // SANCION 
                        SqlCommand cmdSancion = con.CreateCommand();
                        cmdSancion.CommandTimeout = 0;
                        cmdSancion.CommandType = CommandType.StoredProcedure;
                        cmdSancion.CommandText = "USP_LIST_TIPOSANCION";
                        SqlDataReader drSancion = cmdSancion.ExecuteReader();
                        if (drSancion.HasRows)
                        {
                            List<Sancion> sancion = new List<Sancion>();
                            while (drSancion.Read())
                            {
                                sancion.Add(new Sancion()
                                {
                                    SancionId = drSancion.GetInt32(0),
                                    Descripcion = drSancion.GetString(1)
                                });
                            }
                            migracion.sancions = sancion;
                        }

                        // FORMATO
                        SqlCommand cmdFormato = con.CreateCommand();
                        cmdFormato.CommandTimeout = 0;
                        cmdFormato.CommandType = CommandType.StoredProcedure;
                        cmdFormato.CommandText = "USP_LIST_FORMATO";
                        SqlDataReader drFormato = cmdFormato.ExecuteReader();
                        if (drFormato.HasRows)
                        {
                            List<Formato> formato = new List<Formato>();
                            while (drFormato.Read())
                            {
                                formato.Add(new Formato()
                                {
                                    FormatoId = drFormato.GetInt32(0),
                                    Nombre = drFormato.GetString(1)
                                });
                            }
                            migracion.formatos = formato;
                        }

                        // TIPO INSPECCION
                        SqlCommand cmdTipoInspeccion = con.CreateCommand();
                        cmdTipoInspeccion.CommandTimeout = 0;
                        cmdTipoInspeccion.CommandType = CommandType.StoredProcedure;
                        cmdTipoInspeccion.CommandText = "USP_LIST_TIPOINSPECCION";
                        SqlDataReader drTipoInspeccion = cmdTipoInspeccion.ExecuteReader();
                        if (drTipoInspeccion.HasRows)
                        {
                            List<TipoInspeccion> tipoInspeccion = new List<TipoInspeccion>();
                            while (drTipoInspeccion.Read())
                            {
                                tipoInspeccion.Add(new TipoInspeccion()
                                {
                                    TipoInspeccionId = drTipoInspeccion.GetInt32(0),
                                    Descripcion = drTipoInspeccion.GetString(1)
                                });
                            }
                            migracion.tipoInspeccions = tipoInspeccion;
                        }

                        // NIVEL INSPECCION
                        SqlCommand cmdNivelInspeccion = con.CreateCommand();
                        cmdNivelInspeccion.CommandTimeout = 0;
                        cmdNivelInspeccion.CommandType = CommandType.StoredProcedure;
                        cmdNivelInspeccion.CommandText = "USP_LIST_NIVELINSPECCION";
                        SqlDataReader drNivelInspeccion = cmdNivelInspeccion.ExecuteReader();
                        if (drNivelInspeccion.HasRows)
                        {
                            List<NivelInspeccion> nivelInspeccion = new List<NivelInspeccion>();
                            while (drNivelInspeccion.Read())
                            {
                                nivelInspeccion.Add(new NivelInspeccion()
                                {
                                    NivelInspeccionId = drNivelInspeccion.GetInt32(0),
                                    Descripcion = drNivelInspeccion.GetString(1),
                                    Cantidad = drNivelInspeccion.GetInt32(2),
                                    estado = drNivelInspeccion.GetInt32(3)
                                });
                            }
                            migracion.nivelInspeccions = nivelInspeccion;
                        }

                        // ANOMALIAS
                        SqlCommand cmdAnomalia = con.CreateCommand();
                        cmdAnomalia.CommandTimeout = 0;
                        cmdAnomalia.CommandType = CommandType.StoredProcedure;
                        cmdAnomalia.CommandText = "USP_LIST_ANOMALIA";
                        cmdAnomalia.Parameters.Add("@id_grupo", SqlDbType.Int).Value = f.grupoId;
                        SqlDataReader drAnomalia = cmdAnomalia.ExecuteReader();
                        if (drAnomalia.HasRows)
                        {
                            List<Anomalia> anomalia = new List<Anomalia>();
                            while (drAnomalia.Read())
                            {
                                Anomalia a = new Anomalia();
                                a.AnomaliaId = drAnomalia.GetInt32(0);
                                a.Descripcion = drAnomalia.GetString(1);
                                a.Formato = drAnomalia.GetInt32(2);
                                a.Titulo = drAnomalia.GetInt32(3);
                                a.Valor = drAnomalia.GetString(4);
                                a.Orden = drAnomalia.GetInt32(5);
                                a.verValidacion = drAnomalia.GetString(6);
                                a.personalNuevo = drAnomalia.GetInt32(7);
                                a.critica = drAnomalia.GetString(8);

                                SqlCommand cmdAnomaliaDetalle = con.CreateCommand();
                                cmdAnomaliaDetalle.CommandTimeout = 0;
                                cmdAnomaliaDetalle.CommandType = CommandType.StoredProcedure;
                                cmdAnomaliaDetalle.CommandText = "USP_GET_DETALLE_ANOMALIA";
                                cmdAnomaliaDetalle.Parameters.Add("@anomaliaId", SqlDbType.Int).Value = a.AnomaliaId;
                                SqlDataReader drDetalle = cmdAnomaliaDetalle.ExecuteReader();
                                if (drDetalle.HasRows)
                                {
                                    List<AnomaliaDetalle> detalles = new List<AnomaliaDetalle>();
                                    while (drDetalle.Read())
                                    {
                                        detalles.Add(new AnomaliaDetalle()
                                        {
                                            anomaliaDetalleId = drDetalle.GetInt32(0),
                                            tipoInspeccionId = drDetalle.GetInt32(1),
                                            anomaliaId = drDetalle.GetInt32(2),
                                            codigoDetalle = drDetalle.GetString(3),
                                            nombreDetalle = drDetalle.GetString(4),
                                            titutoDetalle = drDetalle.GetInt32(5),
                                            ordenDetalle = drDetalle.GetInt32(6),
                                            grupoDetalle = drDetalle.GetString(7),
                                            criticaDetalle = drDetalle.GetInt32(8),
                                            estado = drDetalle.GetInt32(9)
                                        });
                                    }
                                    a.anomaliaDetalles = detalles;
                                }
                                anomalia.Add(a);
                            }
                            migracion.anomalias = anomalia;
                        }

                        //Cliente
                        SqlCommand cmdCliente = con.CreateCommand();
                        cmdCliente.CommandTimeout = 0;
                        cmdCliente.CommandType = CommandType.StoredProcedure;
                        cmdCliente.CommandText = "USP_LIST_CLIENTE";
                        cmdCliente.Parameters.Add("@id_ot", SqlDbType.Int).Value = f.otId;
                        SqlDataReader drcliente = cmdCliente.ExecuteReader();
                        if (drcliente.HasRows)
                        {
                            List<Cliente> cliente = new List<Cliente>();
                            while (drcliente.Read())
                            {
                                cliente.Add(new Cliente()
                                {
                                    ClienteId = drcliente.GetInt32(0),
                                    Ruc = drcliente.GetString(1),
                                    Nombre = drcliente.GetString(2)
                                });
                            }
                            migracion.clientes = cliente;
                        }

                        // Actividades
                        SqlCommand cmdActividad = con.CreateCommand();
                        cmdActividad.CommandTimeout = 0;
                        cmdActividad.CommandType = CommandType.StoredProcedure;
                        cmdActividad.CommandText = "USP_LIST_ACTIVIDAD";
                        cmdActividad.Parameters.Add("@id_delegacion", SqlDbType.Int).Value = f.delegacionId;
                        SqlDataReader drActividad = cmdActividad.ExecuteReader();
                        if (drActividad.HasRows)
                        {
                            List<Actividad> actividad = new List<Actividad>();
                            while (drActividad.Read())
                            {
                                actividad.Add(new Actividad()
                                {
                                    ActividadId = drActividad.GetInt32(0),
                                    Nombre = drActividad.GetString(1)
                                });
                            }
                            migracion.actividades = actividad;
                        }

                        // OT

                        SqlCommand cmdOt = con.CreateCommand();
                        cmdOt.CommandTimeout = 0;
                        cmdOt.CommandType = CommandType.StoredProcedure;
                        cmdOt.CommandText = "USP_GET_OT";
                        cmdOt.Parameters.Add("@otId", SqlDbType.Int).Value = f.otId;
                        SqlDataReader drOt = cmdOt.ExecuteReader();

                        if (drOt.HasRows)
                        {
                            Ot ot = null;
                            while (drOt.Read())
                            {
                                ot = new Ot()
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
                                };
                            }
                            migracion.ot = ot;
                        }


                        // Inspeccion

                        SqlCommand cmdInspeccion = con.CreateCommand();
                        cmdInspeccion.CommandTimeout = 0;
                        cmdInspeccion.CommandType = CommandType.StoredProcedure;
                        cmdInspeccion.CommandText = "USP_LIST_INSPECCIONCAB";
                        cmdInspeccion.Parameters.Add("@id_pais", SqlDbType.Int).Value = f.paisId;
                        cmdInspeccion.Parameters.Add("@id_ot", SqlDbType.Int).Value = f.otId;
                        cmdInspeccion.Parameters.Add("@id_delegacion", SqlDbType.Int).Value = f.delegacionId;
                        cmdInspeccion.Parameters.Add("@id_grupo", SqlDbType.Int).Value = f.grupoId;
                        cmdInspeccion.Parameters.Add("@InspeccionId", SqlDbType.Int).Value = f.inspeccionId;
                        SqlDataReader drInspeccion = cmdInspeccion.ExecuteReader();
                        if (drInspeccion.HasRows)
                        {
                            List<InspeccionCab> inspeccion = new List<InspeccionCab>();
                            int i = 1;
                            int j = 1;
                            int z = 1;
                            while (drInspeccion.Read())
                            {
                                InspeccionCab item = new InspeccionCab();

                                item.Id = i++;
                                item.inspeccionCab = drInspeccion.GetInt32(0);
                                item.PaisId = drInspeccion.GetInt32(1);
                                item.EmpresaInspectorId = drInspeccion.GetInt32(2);
                                item.DelegacionInspectorId = drInspeccion.GetInt32(3);
                                item.ProyectoInspectorId = drInspeccion.GetInt32(4);
                                item.FechaInspeccion = drInspeccion.GetDateTime(5).ToString("dd/MM/yyyy HH:mm:ss");
                                item.PersonalInspectorId = drInspeccion.GetInt32(6);
                                item.JefeObraId = drInspeccion.GetInt32(7);
                                item.TipoInspeccionId = drInspeccion.GetInt32(8);
                                item.ResultadoInspeccion = drInspeccion.GetString(9);
                                item.EmpresaId = drInspeccion.GetInt32(10);
                                item.InicioFinal = drInspeccion.GetString(11);
                                item.AccionPropuesto = drInspeccion.GetString(12);
                                item.PersonalResponsable = drInspeccion.GetInt32(13);
                                item.FechaCorrecion = drInspeccion.GetDateTime(14).ToString("dd/MM/yyyy HH:mm:ss");
                                item.ObservacionAccion = drInspeccion.GetString(15);
                                item.ParalizacionTrabajo = drInspeccion.GetString(16);
                                item.AlgunaSancion = drInspeccion.GetString(17);
                                item.TipoSancionId = drInspeccion.GetInt32(18);
                                item.NumeroSancionados = drInspeccion.GetInt32(19);
                                item.Lugar = drInspeccion.GetString(20);
                                item.ActividadOT = drInspeccion.GetString(21);
                                item.TrabajoRealizar = drInspeccion.GetString(22);
                                item.AreaId = drInspeccion.GetInt32(23);
                                item.CoordinadorId = drInspeccion.GetInt32(24);
                                item.Placa = drInspeccion.GetString(25);
                                item.NivelInspeccion = drInspeccion.GetInt32(26);
                                item.CargoId = drInspeccion.GetInt32(27);
                                item.PersonalId = drInspeccion.GetInt32(28);
                                item.ClienteId = drInspeccion.GetInt32(29);
                                item.latitud = drInspeccion.GetString(30);
                                item.longitud = drInspeccion.GetString(31);
                                item.ActividadId = drInspeccion.GetInt32(32);
                                item.Conjuntas = drInspeccion.GetInt32(33);
                                item.GrupoId = drInspeccion.GetInt32(34);
                                item.Estado = 0;
                                item.TipoAnomaliaId = drInspeccion.GetInt32(35);

                                SqlCommand cmdCoordinador = con.CreateCommand();
                                cmdCoordinador.CommandTimeout = 0;
                                cmdCoordinador.CommandType = CommandType.StoredProcedure;
                                cmdCoordinador.CommandText = "NEW_GETPERSONALBYID";
                                cmdCoordinador.Parameters.Add("@personalId", SqlDbType.Int).Value = item.CoordinadorId;
                                SqlDataReader drCoordinador = cmdCoordinador.ExecuteReader();
                                if (drCoordinador.HasRows)
                                {
                                    while (drCoordinador.Read())
                                    {
                                        Personal cood = new Personal()
                                        {
                                            PersonalId = drCoordinador.GetInt32(0),
                                            NroDoc = drCoordinador.GetString(1),
                                            Apellido = drCoordinador.GetString(2),
                                            Nombre = drCoordinador.GetString(3),
                                            CargoId = drCoordinador.GetInt32(4),
                                            NombreCargo = drCoordinador.GetString(5),
                                            Email = drCoordinador.GetString(6)
                                        };
                                        item.coordinador = cood;
                                    }
                                }

                                SqlCommand cmdJefe = con.CreateCommand();
                                cmdJefe.CommandTimeout = 0;
                                cmdJefe.CommandType = CommandType.StoredProcedure;
                                cmdJefe.CommandText = "NEW_GETPERSONALBYID";
                                cmdJefe.Parameters.Add("@personalId", SqlDbType.Int).Value = item.JefeObraId;
                                SqlDataReader drJefe = cmdJefe.ExecuteReader();
                                if (drJefe.HasRows)
                                {
                                    while (drJefe.Read())
                                    {
                                        Personal jefe = new Personal()
                                        {
                                            PersonalId = drJefe.GetInt32(0),
                                            NroDoc = drJefe.GetString(1),
                                            Apellido = drJefe.GetString(2),
                                            Nombre = drJefe.GetString(3),
                                            CargoId = drJefe.GetInt32(4),
                                            NombreCargo = drJefe.GetString(5),
                                            Email = drJefe.GetString(6)
                                        };
                                        item.jefeObra = jefe;
                                    }
                                }

                                SqlCommand cmdPersonalInsp = con.CreateCommand();
                                cmdPersonalInsp.CommandTimeout = 0;
                                cmdPersonalInsp.CommandType = CommandType.StoredProcedure;
                                cmdPersonalInsp.CommandText = "NEW_GETPERSONALBYID";
                                cmdPersonalInsp.Parameters.Add("@personalId", SqlDbType.Int).Value = item.PersonalId;
                                SqlDataReader drPersonalInsp = cmdPersonalInsp.ExecuteReader();
                                if (drPersonalInsp.HasRows)
                                {
                                    while (drPersonalInsp.Read())
                                    {
                                        Personal personalInsp = new Personal()
                                        {
                                            PersonalId = drPersonalInsp.GetInt32(0),
                                            NroDoc = drPersonalInsp.GetString(1),
                                            Apellido = drPersonalInsp.GetString(2),
                                            Nombre = drPersonalInsp.GetString(3),
                                            CargoId = drPersonalInsp.GetInt32(4),
                                            NombreCargo = drPersonalInsp.GetString(5),
                                            Email = drPersonalInsp.GetString(6)
                                        };
                                        item.personalInspeccionado = personalInsp;
                                    }
                                }

                                SqlCommand cmdNivel = con.CreateCommand();
                                cmdNivel.CommandTimeout = 0;
                                cmdNivel.CommandType = CommandType.StoredProcedure;
                                cmdNivel.CommandText = "NEW_GetNivelInspeccionById";
                                cmdNivel.Parameters.Add("@nivelId", SqlDbType.Int).Value = item.NivelInspeccion;
                                SqlDataReader drNivel = cmdNivel.ExecuteReader();
                                if (drNivel.HasRows)
                                {
                                    while (drNivel.Read())
                                    {
                                        NivelInspeccion n = new NivelInspeccion()
                                        {
                                            NivelInspeccionId = drNivel.GetInt32(0),
                                            Descripcion = drNivel.GetString(1),
                                            Cantidad = drNivel.GetInt32(2),
                                            estado = drNivel.GetInt32(3)
                                        };
                                        item.nivel = n;
                                    }
                                }

                                SqlCommand cmdInspeccionDetalle = con.CreateCommand();
                                cmdInspeccionDetalle.CommandTimeout = 0;
                                cmdInspeccionDetalle.CommandType = CommandType.StoredProcedure;
                                cmdInspeccionDetalle.CommandText = "USP_LIST_INSPECCIONCABDETALLE";
                                cmdInspeccionDetalle.Parameters.Add("@InspeccionId", SqlDbType.Int).Value = item.inspeccionCab;
                                SqlDataReader drDetalle = cmdInspeccionDetalle.ExecuteReader();
                                List<InspeccionCabDetalle> inspeccionDetalle = null;
                                if (drDetalle.HasRows)
                                {
                                    inspeccionDetalle = new List<InspeccionCabDetalle>();
                                    while (drDetalle.Read())
                                    {
                                        InspeccionCabDetalle d = new InspeccionCabDetalle();
                                        d.Id = j++;
                                        d.InspeccionId = item.Id;
                                        d.inspeccionCabDetalle = drDetalle.GetInt32(0);
                                        d.PersonalId = drDetalle.GetInt32(1);
                                        d.AnomaliaId = drDetalle.GetInt32(2);
                                        d.Detalle = drDetalle.GetString(3);
                                        d.Valor = drDetalle.GetString(4);
                                        d.Levantamiento = drDetalle.GetString(5);
                                        d.LevantamientoFoto = drDetalle.GetString(6);
                                        d.LevantamientoDescripcion = drDetalle.GetString(7);
                                        d.Estado = drDetalle.GetInt32(8);
                                        d.Usuario = drDetalle.GetInt32(9);
                                        d.Fecha = drDetalle.GetDateTime(10).ToString("dd/MM/yyyy HH:mm:ss");
                                        d.AccionPropuesto = drDetalle.GetString(11);
                                        d.FechaCorrecion = drDetalle.GetDateTime(12).ToString("dd/MM/yyyy HH:mm:ss");
                                        d.latitud = drDetalle.GetString(13);
                                        d.longitud = drDetalle.GetString(14);
                                        d.disponividad_uso = drDetalle.GetString(15);
                                        d.size = drDetalle.GetInt32(16);
                                        d.TipoInspeccionId = drDetalle.GetInt32(17);
                                        d.personalSancionado = drDetalle.GetInt32(18);
                                        d.anomalia_critica = drDetalle.GetInt32(19);
                                        d.anomaliaDetalleId = drDetalle.GetInt32(20);
                                        d.resultadoInspeccionId = drDetalle.GetInt32(21);

                                        SqlCommand cmdFoto = con.CreateCommand();
                                        cmdFoto.CommandTimeout = 0;
                                        cmdFoto.CommandType = CommandType.StoredProcedure;
                                        cmdFoto.CommandText = "USP_LIST_INSPECCIONCABDETALLEFOTO";
                                        cmdFoto.Parameters.Add("@id_inspeccion_detalle", SqlDbType.Int).Value = d.inspeccionCabDetalle;
                                        SqlDataReader drFoto = cmdFoto.ExecuteReader();
                                        List<InspeccionCabPhoto> fotos = null;
                                        if (drFoto.HasRows)
                                        {
                                            fotos = new List<InspeccionCabPhoto>();
                                            while (drFoto.Read())
                                            {
                                                fotos.Add(new InspeccionCabPhoto()
                                                {
                                                    Id = z++,
                                                    DetalleInspeccionId = j - 1,
                                                    DetalleInspeccionRetorno = drFoto.GetInt32(0),
                                                    Foto = drFoto.GetString(1),
                                                    Fecha = drFoto.GetDateTime(3).ToString("dd/MM/yyyy HH:mm:ss"),
                                                    size = drFoto.GetInt32(4),
                                                    tipo = drFoto.GetString(5),
                                                    estado = 2
                                                });
                                            }
                                        }
                                        d.inspeccionCabPhoto = fotos;
                                        inspeccionDetalle.Add(d);
                                    }
                                    item.inspeccionDetalle = inspeccionDetalle;
                                }
                                inspeccion.Add(item);
                            }
                            migracion.inspeccions = inspeccion;
                        }
                        migracion.mensaje = "Sincronización Completada.";
                    }
                    con.Close();
                }
                return migracion;
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        public static Mensaje saveMega(Mega r)
        {
            try
            {
                Mensaje m = null;
                using (SqlConnection con = new SqlConnection(db))
                {
                    con.Open();
                    SqlCommand cmd = con.CreateCommand();
                    cmd.CommandTimeout = 0;
                    cmd.CommandType = System.Data.CommandType.StoredProcedure;
                    cmd.CommandText = "USP_SAVE_MEGAS";
                    cmd.Parameters.Add("@megaId", SqlDbType.Int).Value = r.megaId;
                    cmd.Parameters.Add("@kilobytes", SqlDbType.Int).Value = r.bytes;
                    cmd.Parameters.Add("@usuario", SqlDbType.Int).Value = r.usuario;
                    cmd.Parameters.Add("@fecha", SqlDbType.VarChar).Value = r.fecha;

                    SqlDataReader drInspeccion = cmd.ExecuteReader();

                    m = new Mensaje();
                    if (drInspeccion.HasRows)
                    {
                        m.mensaje = "Enviado";
                    }
                    else
                    {
                        m.mensaje = "Error";
                    }

                    con.Close();
                    return m;
                }

            }
            catch (Exception ex)
            {
                throw ex;
            }

        }

        // NUEVO 

        public static List<Personal> buscarPersonal(Filtro f)
        {
            try
            {
                List<Personal> personal = null;
                using (SqlConnection cn = new SqlConnection(db))
                {
                    cn.Open();
                    using (SqlCommand cmd = new SqlCommand("NEW_BUSQUEDA", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.Parameters.Add("@paisId", SqlDbType.Int).Value = f.paisId;
                        cmd.Parameters.Add("@tipo", SqlDbType.Int).Value = f.tipo;
                        cmd.Parameters.Add("@busqueda", SqlDbType.VarChar).Value = f.nombre;

                        SqlDataReader rd = cmd.ExecuteReader();
                        if (rd.HasRows)
                        {
                            personal = new List<Personal>();
                            while (rd.Read())
                            {
                                personal.Add(new Personal()
                                {
                                    PersonalId = rd.GetInt32(0),
                                    NroDoc = rd.GetString(1),
                                    CargoId = rd.GetInt32(2),
                                    NombreCargo = rd.GetString(3),
                                    Apellido = rd.GetString(4),
                                    Nombre = rd.GetString(5),
                                    Email = rd.GetString(6)
                                });
                            }
                        }
                        rd.Close();
                    }
                }
                return personal;
            }
            catch (Exception e)
            {
                throw e;
            }
        }

        public static MensajeRetorno getEstadoUsuario(int usuarioId)
        {
            try
            {
                MensajeRetorno m = null;

                using (SqlConnection cn = new SqlConnection(db))
                {

                    cn.Open();

                    using (SqlCommand cmd = new SqlCommand("USP_GET_USUARIO_BY_ESTADO", cn))
                    {
                        cmd.CommandTimeout = 0;
                        cmd.CommandType = CommandType.StoredProcedure;
                        cmd.CommandText = "USP_GET_USUARIO_BY_ESTADO";
                        cmd.Parameters.Add("@usuarioId", SqlDbType.Int).Value = usuarioId;
                        SqlDataReader rd = cmd.ExecuteReader();
                        if (rd.HasRows)
                        {
                            while (rd.Read())
                            {
                                if (rd.GetInt32(0) == 1)
                                {
                                    m = new MensajeRetorno()
                                    {
                                        inspeccionCab = rd.GetInt32(0)
                                    };
                                }
                            }
                        }
                    }
                    cn.Close();
                }
                return m;
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}