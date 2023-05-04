using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Movil.Reporte
{
    public class ReporteEN
    {
        public int Codigo { get; set; }
        public string Dato { get; set; }
        public int total { get; set; }
        public int normales { get; set; }
        public int anomalias { get; set; }
        public int levantadas { get; set; }
        public int pendientes { get; set; }
        public string fecha { get; set; }
        public string nivel { get; set; }
        public string ipal  { get; set; }
    }
}
