using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Movil
{
    public class Grupo
    {
        public int id_grupo { get; set; }
        public int id_pais { get; set; }
        public string descripcion { get; set; }
        public int estado { get; set; }
        public List<Delegacion> delegacions { get; set; }     
    }

}
