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
    
    public partial class T_USUARIO_BORRADO
    {
        public int id_usuario { get; set; }
        public string nro_documento { get; set; }
        public string datos_personales { get; set; }
        public string correo_electronico { get; set; }
        public string usuario_login { get; set; }
        public string contrasenia_login { get; set; }
        public Nullable<int> id_perfil { get; set; }
        public Nullable<int> estado { get; set; }
        public Nullable<int> usuario_creacion { get; set; }
        public Nullable<System.DateTime> fecha_creacion { get; set; }
        public Nullable<int> usuario_edicion { get; set; }
        public Nullable<System.DateTime> fecha_edicion { get; set; }
        public string imei { get; set; }
        public string version { get; set; }
        public Nullable<System.DateTime> fechaInicioSesion { get; set; }
    }
}