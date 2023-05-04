using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Movil
{
    public class Pais
    {
        public int id_pais { get; set; }
        public string descripcion { get; set; }
        public List<Grupo> grupos { get; set; }    
    }
}
