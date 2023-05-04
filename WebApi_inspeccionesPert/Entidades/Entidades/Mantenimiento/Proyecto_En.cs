using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Mantenimiento
{
    public class Proyecto_En
    {
        public int id_Delegacion { get; set; }
        public string nombre_proyecto { get; set; }
        public int estado { get; set; }
        public int usuario_creacion { get; set; }
        public int id_Personal_JefeObra { get; set; }
        public int id_Cliente { get; set; }
        public List<Empresa_Colaboradora> Empresa_Cola { get; set; }
    }
    public class Empresa_Colaboradora
    {
        public int id_Proyecto { get; set; }
        public int id_EmpresaColaboradora { get; set; }
        public int estado { get; set; }
        public int usuario_creacion { get; set; }
    }
}
