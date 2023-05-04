using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Movil
{
    public class Usuario
    {
        public int id_usuario { get; set; }
        public string nro_documento { get; set; }
        public string datos_personales { get; set; }
        public string correo_electronico { get; set; }
        public int id_perfil { get; set; }
        public int estado { get; set; }
        public string mensaje { get; set; }
        public int empresaColaboradora { get; set; }
        public int directoEmpresaColaboradora { get; set; }
        public List<Configuracion> configuracions { get; set; }
    }
}
