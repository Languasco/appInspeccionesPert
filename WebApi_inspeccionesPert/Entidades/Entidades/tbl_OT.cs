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
    
    public partial class tbl_OT
    {
        public int id_OT { get; set; }
        public string codigo_ot { get; set; }
        public string nombre_ot { get; set; }
        public string Tipo_OT { get; set; }
        public int id_Proyecto { get; set; }
        public int id_Cliente { get; set; }
        public int id_delegacion { get; set; }
        public int id_grupo { get; set; }
        public int id_Pais { get; set; }
        public int id_Personal_JefeObra { get; set; }
        public int id_Personal_Coordinador { get; set; }
        public int id_Actividad { get; set; }
        public int estado { get; set; }
        public int usuario_creacion { get; set; }
        public System.DateTime fecha_creacion { get; set; }
        public int usuario_edicion { get; set; }
        public System.DateTime fecha_edicion { get; set; }
        public Nullable<int> existe { get; set; }
    
        public virtual tbl_Actividad tbl_Actividad { get; set; }
        public virtual tbl_Delegacion tbl_Delegacion { get; set; }
        public virtual tbl_grupo tbl_grupo { get; set; }
        public virtual tbl_pais tbl_pais { get; set; }
    }
}
