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
    
    public partial class tbl_grupo
    {
        public tbl_grupo()
        {
            this.tbl_usuarios_configuracion = new HashSet<tbl_usuarios_configuracion>();
            this.tbl_OT = new HashSet<tbl_OT>();
        }
    
        public int id_grupo { get; set; }
        public Nullable<int> id_pais { get; set; }
        public string descripcion { get; set; }
        public Nullable<int> estado { get; set; }
        public Nullable<int> usuario_creacion { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<int> usuario_edicion { get; set; }
        public Nullable<System.DateTime> fecha_edicion { get; set; }
    
        public virtual tbl_pais tbl_pais { get; set; }
        public virtual ICollection<tbl_usuarios_configuracion> tbl_usuarios_configuracion { get; set; }
        public virtual ICollection<tbl_OT> tbl_OT { get; set; }
    }
}