using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Mantenimiento
{
    public class Usuario_E
    {
        public bool checkeado { set; get; }
        public int id_pais { set; get; }
        public int id_grupo { set; get; }
        public int id_Empresas { set; get; }
        public int id_Delegacion { set; get; }
        public int id_Proyecto { set; get; }
        public int id_OT { set; get; }
        public string mensaje { set; get; }

        public string nombre_delegacion { set; get; }
        public string Nro_Documento { set; get; }
        public string usuarios { set; get; }

        public string descripcion { set; get; }

        public string Pais { set; get; }
        public string Grupo { set; get; }
        public string Empresa { set; get; }
        public string Delegacion { set; get; }

        public int id_perfil { set; get; }
        public string des_perfil { set; get; }
 

        public int id_usuario  { set; get; }
       public string nro_documento  { set; get; }
       public string datos_personales { set; get; }
       public string correo_electronico { set; get; }
       public string usuario_login { set; get; }
       public string contrasenia_login { set; get; }

       public string estado { set; get; }
       public string usuario_creacion { set; get; }
       public string fecha_creacion { set; get; }
       public string usuario_edicion { set; get; }
       public string fecha_edicion { set; get; }



    }

    public class Usuario_Registrado
    {
        public int Id_usuario_Registrado { get; set; }
        public int id_Personal { get; set; }
        public int Estado { get; set; }
        public string Nro_Documento { get; set; }
        public string FechaRegistro { get; set; }
        public string Nombres_Personal { get; set; }
        public string Apellidos_Personal { get; set; }
        public string Datos_personales { get; set; }
        public string FechaAprobacion { get; set; }
        public string FechaRechazo { get; set; }
        public string Pais { get; set; }
    }
}
