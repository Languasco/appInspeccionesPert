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
    
    public partial class Tbl_Reporte_Cobra
    {
        public string nro_visita { get; set; }
        public string fecha_insp { get; set; }
        public string zona { get; set; }
        public string delegacion { get; set; }
        public string actividad { get; set; }
        public string cliente { get; set; }
        public int nro_inspeccion { get; set; }
        public Nullable<int> anio { get; set; }
        public Nullable<int> mes { get; set; }
        public Nullable<int> semana { get; set; }
        public string autor { get; set; }
        public string puesto { get; set; }
        public string jefeObra { get; set; }
        public string empresa_resp_propia { get; set; }
        public string empresa_resp_subContrata { get; set; }
        public string tipoInspeccion { get; set; }
        public string incio_final_trabajos { get; set; }
        public int inspeccion_conjunta { get; set; }
        public string resp_inspeccion { get; set; }
        public string nro_Anomalias { get; set; }
        public string aspectoAnomalo { get; set; }
        public string Decripcion_anomalia { get; set; }
        public string anomalia_critica { get; set; }
        public string noEliminar { get; set; }
        public string accionPropuesta { get; set; }
        public string respCorr { get; set; }
        public string fecha_Prop_Corr { get; set; }
        public string fecha_Cierre_Corr { get; set; }
        public string noEliminar2 { get; set; }
        public string paralizacionTrabajos { get; set; }
        public string sancion { get; set; }
        public string tipoSancion { get; set; }
        public Nullable<int> nro_trabajadoresSancionados { get; set; }
        public string observaciones { get; set; }
        public int propia { get; set; }
        public int subContrata { get; set; }
    }
}