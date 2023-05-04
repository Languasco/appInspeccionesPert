//------------------------------------------------------------------------------
// <auto-generated>
//     Este código se generó a partir de una plantilla.
//
//     Los cambios manuales en este archivo pueden causar un comportamiento inesperado de la aplicación.
//     Los cambios manuales en este archivo se sobrescribirán si se regenera el código.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Entidades
{
    using System;
    using System.Collections.Generic;
    
    public partial class tbl_Inspeccion_Cab
    {
        public tbl_Inspeccion_Cab()
        {
            this.tbl_Inspeccion_Cab_Detalle = new HashSet<tbl_Inspeccion_Cab_Detalle>();
        }
    
        public int id_Inspeccion { get; set; }
        public Nullable<int> id_Pais { get; set; }
        public Nullable<int> id_Empresa { get; set; }
        public Nullable<int> id_Delegacion { get; set; }
        public Nullable<int> id_Proyecto { get; set; }
        public Nullable<System.DateTime> fecha_Inspeccion { get; set; }
        public string numero_Inspeccion { get; set; }
        public Nullable<int> id_Actividad { get; set; }
        public string cliente_Inspeccion { get; set; }
        public Nullable<int> id_Personal_Inspector { get; set; }
        public Nullable<int> id_Personal_JefeObra { get; set; }
        public Nullable<int> id_TipoInspeccion { get; set; }
        public string Resultado_Inspeccion { get; set; }
        public Nullable<int> id_EmpresaColaboradora { get; set; }
        public string iniciofin_Trabajo { get; set; }
        public string inspeccionConjunta { get; set; }
        public Nullable<int> id_Anomalia { get; set; }
        public string descripcion_Inspeccion { get; set; }
        public string accionPropuesta_Correctiva { get; set; }
        public Nullable<int> id_Personal_Responsable { get; set; }
        public Nullable<System.DateTime> fechaPropuesta_Correctiva { get; set; }
        public string observacion_Correctiva { get; set; }
        public string paralizacion_Correctiva { get; set; }
        public string sancion_Correctiva { get; set; }
        public Nullable<int> id_TipoSancion { get; set; }
        public Nullable<int> nroTrabajadores_Correctiva { get; set; }
        public string lugar_Inspeccion { get; set; }
        public string actividadOT_Inspeccion { get; set; }
        public string trabajoArealizar_Inspeccion { get; set; }
        public Nullable<int> id_Area { get; set; }
        public Nullable<int> id_Personal_Coordinador { get; set; }
        public string placa_Inspeccion { get; set; }
        public Nullable<int> id_NivelInspeccion { get; set; }
        public Nullable<int> estado { get; set; }
        public Nullable<int> usuario_creacion { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<int> usuario_edicion { get; set; }
        public Nullable<System.DateTime> fecha_edicion { get; set; }
        public Nullable<int> id_Cargo { get; set; }
        public Nullable<int> id_Personal_Inspeccionado { get; set; }
        public string Obs_Levantada { get; set; }
        public Nullable<int> id_Inspeccion_Relacionada { get; set; }
        public Nullable<int> id_Cliente { get; set; }
        public string longitud { get; set; }
        public string latitud { get; set; }
        public Nullable<int> conjuntas { get; set; }
        public Nullable<System.DateTime> fecha_EnvioEmail { get; set; }
        public string EstadoEmail { get; set; }
        public Nullable<int> id_grupo { get; set; }
        public Nullable<int> id_OT { get; set; }
    
        public virtual ICollection<tbl_Inspeccion_Cab_Detalle> tbl_Inspeccion_Cab_Detalle { get; set; }
    }
}