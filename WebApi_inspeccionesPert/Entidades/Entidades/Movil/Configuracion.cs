using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Movil
{
    public class Configuracion
    {
        public int id_usuario_configuracion { get; set; }
        public int id_usuario { get; set; }
        public Pais pais { get; set; }
        public List<Grupo> grupos { get; set; }
        public List<Delegacion> delegacions { get; set; }
        //public List<Proyecto> proyectos { get; set; }
        public List<Ot> ots { get; set; }
        public int estado { get; set; }
    }
}
