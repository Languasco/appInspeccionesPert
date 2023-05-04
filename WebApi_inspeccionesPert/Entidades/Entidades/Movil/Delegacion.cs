using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Movil
{
    public class Delegacion
    {
        public int id_Delegacion { get; set; }
        public int id_Grupo { get; set; }
        public string codigo_delegacion { get; set; }
        public string nombre_delegacion { get; set; }
        public int estado { get; set; }
        public List<Proyecto> proyectos { get; set; }
    }
}
