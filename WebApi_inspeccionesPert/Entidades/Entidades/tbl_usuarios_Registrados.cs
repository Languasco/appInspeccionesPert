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
    
    public partial class tbl_usuarios_Registrados
    {
        public int id_usuario_Registrado { get; set; }
        public string nro_Documento { get; set; }
        public int id_Personal { get; set; }
        public int id_Pais { get; set; }
        public Nullable<int> id_Delegacion { get; set; }
        public System.DateTime fechaRegistro { get; set; }
        public int estado { get; set; }
        public Nullable<int> usuario_edicion { get; set; }
        public Nullable<System.DateTime> fechaAprobacion { get; set; }
        public Nullable<System.DateTime> fechaRechazo { get; set; }
    }
}
