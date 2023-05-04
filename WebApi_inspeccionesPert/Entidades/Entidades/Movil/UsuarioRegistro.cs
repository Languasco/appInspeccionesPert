using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Movil
{
   public class UsuarioRegistro
    {
        public int Id_pais { get; set; }
        public int Id_delegacion { get; set; }
        public int Estado { get; set; }
        public int Id_Personal { get; set; }
        public string Nro_documento { get; set; }
        public string Email_personal { get; set; }
    }
    public class DelegacionRegistro
    {
        public int Id_Delegacion { get; set; }
        public string Codigo_delegacion { get; set; }
        public string Nombre_delegacion { get; set; }
    }
    public class GetPersonal
    {
        public int Id_Personal { get; set; }
        public string NroDoc_Personal { get; set; }
        public string Apellidos_Personal { get; set; }
        public string Nombres_Personal { get; set; }
        public string Email_personal { get; set; }
        public int Id_delegacion { get; set; }
    }
}
