using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Movil
{
    public class Empresa
    {
        public int EmpresaId { get; set; }
        public int id_grupo { get; set; }
        public string Ruc { get; set; }
        public string RazonSocial { get; set; }
    }
}
