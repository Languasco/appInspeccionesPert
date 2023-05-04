using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Movil
{
    public class MensajeRetorno
    {
        public string mensaje { get; set; }
        public int inspeccionCab { get; set; }
        public List<DetalleRetorno> detalle { get; set; }
         
    }
}
