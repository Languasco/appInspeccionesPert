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
    
    public partial class tbl_Personal
    {
        public int id_Personal { get; set; }
        public Nullable<int> id_Empresa { get; set; }
        public Nullable<int> id_Delegacion { get; set; }
        public Nullable<int> id_Proyecto { get; set; }
        public string tipoDoc_Personal { get; set; }
        public string nroDoc_Personal { get; set; }
        public string apellidos_Personal { get; set; }
        public string nombres_Personal { get; set; }
        public Nullable<System.DateTime> fechaIngreso_Personal { get; set; }
        public Nullable<int> id_Cargo { get; set; }
        public string tipoPersonal { get; set; }
        public Nullable<System.DateTime> fechaCese_Personal { get; set; }
        public string email_personal { get; set; }
        public string login_Sistema { get; set; }
        public string Contrasenia_sistema { get; set; }
        public Nullable<int> id_Perfil { get; set; }
        public string envio_Online { get; set; }
        public Nullable<int> estado { get; set; }
        public Nullable<int> usuario_creacion { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<int> usuario_edicion { get; set; }
        public Nullable<System.DateTime> fecha_edicion { get; set; }
        public Nullable<int> id_grupo { get; set; }
        public Nullable<int> id_Pais { get; set; }
        public Nullable<int> id_EmpresaColaboradora { get; set; }
        public Nullable<int> Status { get; set; }
        public Nullable<int> Alter { get; set; }
        public Nullable<int> IdPersonal { get; set; }
        public Nullable<int> delete { get; set; }
        public string codigo_personal { get; set; }
    }
}