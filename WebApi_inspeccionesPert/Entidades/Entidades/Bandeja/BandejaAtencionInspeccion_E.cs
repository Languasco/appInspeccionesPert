using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Bandeja
{
    public class BandejaAtencionInspeccion_E
    {                          
        public int id_Pais { set; get; }
        public string pais { set; get; }
        public int id_grupo { set; get; }
        public string grupo { set; get; }
        public int id_Empresa { set; get; }
        public string empresa { set; get; }
        public int id_Delegacion { set; get; }
        public string delegacion { set; get; }
        public string cliente { get; set; }
        public int id_Inspeccion { set; get; }
        public string nro_inspeccion { set; get; }
        public string fecha_inspeccion { set; get; }
        public string cargo { set; get; }
        public string nombre { set; get; }
        public string jefe_area { set; get; }
        public string inspector { set; get; }
        public string nivel { set; get; }
        public string anomalia { set; get; }
        public string levanto_obs { set; get; }
        public string fecha { set; get; }
        public string nro_insp_relacionada { set; get; }
        public int  id_EmpresaColaboradora  { set; get; }
        public string lugar_Inspeccion { set; get; }
        public string actividadOT_Inspeccion  { set; get; }
        public string trabajoArealizar_Inspeccion { set; get; }
        public int  id_Cargo { set; get; }
        public int  id_Personal_Inspeccionado { set; get; }
        public int id_Area { set; get; }
        public int id_Personal_Coordinador { set; get; }
        public int id_Personal_JefeObra { set; get; }
        public string placa_Inspeccion { set; get; }
        public int id_NivelInspeccion { set; get; }
        public int id_TipoInspeccion { set; get; }
        public string Resultado_Inspeccion { set; get; }
        public string iniciofin_Trabajo { set; get; }
        public int id_Anomalia { set; get; }
        public string descripcion_Inspeccion { set; get; }
        public string accionPropuesta_Correctiva { set; get; }
        public int id_Personal_Responsable { set; get; }
        public string fechaPropuesta_Correctiva { set; get; }
        public string observacion_Correctiva { set; get; }
        public string paralizacion_Correctiva { set; get; }
        public string sancion_Correctiva { set; get; }
        public int id_TipoSancion { set; get; }
        public int  nroTrabajadores_Correctiva  { set; get; }

         public int id_inspeccion { set; get; }
         public int id_inspeccion_foto { set; get; }
         public string nombre_foto { set; get; }
         public string descripcion_foto { set; get; }
         public string ruta_foto { set; get; }
         public int  estado { set; get; }

         public string colorfondo { set; get; }
         public string colorFuente { set; get; }
         public string Obs_Levantada { set; get; }
         public string nro_inspeccionRelacionada { set; get; }


        public int  id_inspeccion_detalle { set; get; }
        public int id_personal { set; get; }
        public int id_anomalia { set; get; }
        public string personal { set; get; }
        public string nroDoc_Personal { set; get; }

        
        public string codigo_Anomalia { set; get; }
        public string Anomalia { set; get; }
        public string descripcionAnomalia { set; get; }
        public string fotoAnomalia { set; get; }
        public string fotoLevantamiento { set; get; }
        public string conjuntas { get; set; }
        public string actividadot_inspeccion { get; set; }
        public string levantamiento { set; get; }
        public string  foto_levantamiento { set; get; }
        public string descripcion_levantamiento { set; get; }

        public int dni { set; get; }
        public string correo { set; get; }
        public string id_formato { set; get; }
        public string id_ValorInspeccion { set; get; }
        public int id_Cliente { set; get; }
        public string nombre_cargo { set; get; }

        public bool Flag_Nueva_Inspeccion { set; get; }
        public int id_Actividad { set; get; }
        public string disponividad_uso { set; get; }
        public string actividad { set; get; }
        public string responsableInspeccionar { get; set; }
        public string descripcion_area { get; set; }
        public string coordinador { get; set; }
        public string jefeobras { get; set; }
        public string descripcion_tipoinspeccion { get;set;}
    }
 

    public class Inspecciones_E {
         public int id_personal { set; get; }
        public int id_pais { set; get; }
        public int id_grupo { set; get; }
        public string idDelegacion { set; get; }
        public string idInspector { set; get; }
        public string idRespCorreccion { set; get; }
        public int opcion { set; get; }
        public string fecha_Ini { set; get; }
        public string fecha_fin { set; get; }

    }
}
