using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Movil
{
    public class Ot
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
    }
}
