using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Movil
{
    
    public class NivelInspeccion
    {
        public int NivelInspeccionId { get; set; }
        public string Descripcion { get; set; }
        public int Cantidad { get; set; }
        public int estado { get; set; }
    }
}
