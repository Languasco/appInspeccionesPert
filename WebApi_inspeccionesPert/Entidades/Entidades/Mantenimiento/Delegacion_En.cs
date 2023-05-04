using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Mantenimiento
{
    public class Delegacion_En
    {
        public int id_pais { get; set; }
        public int id_grupo { get; set; }
        public int id_Delegacion { get; set; }
        public string id_Personal { get; set; }
        public string apellidos_Personal { get; set; }
        public string nombres_Personal { get; set; }
        public string codigo_delegacion { get; set; }
        public string nombre_delegacion { get; set; }
        public int estado { get; set; }
        public string usuario_creacion { get; set; }
        public string fecha_creacion { get; set; }
        public string usuario_edicion { get; set; }
        public string fecha_edicion { get; set; }
    }
    public class Save_Delegacion
    {
        public string codigo_delegacion { get; set; }
        public int estado { get; set; }
        public int id_Personal { get; set; }
        public int id_grupo { get; set; }
        public int id_pais { get; set; }
        public string nombre_Personal { get; set; }
        public string nombre_delegacion { get; set; }
        public int usuario_creacion { get; set; }
    }
}
