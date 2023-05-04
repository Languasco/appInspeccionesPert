using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Reporte
{
    public class Reporte_Dashboard_E
    {
          public int   Codigo { set; get; }
          public string  Dato { set; get; }
          public int   total { set; get; }
          public int   normales  { set; get; }
          public int   anomalias { set; get; }
          public int   levantadas { set; get; }
          public int   pendientes { set; get; }
          public string  fecha { set; get; }
          public string  nivel { set; get; }
          public string ver_detalle { set; get; }
          public int  perfil { set; get; }
    }
}
