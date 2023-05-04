using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Movil
{
    public class Migracion
    {
        public int id { get; set; }
        public List<EmpresaColaboradora> empresaColaboradoras { get; set; }
        public List<Cargo> cargos { get; set; }
        public List<Personal> personals { get; set; }
        public List<Area> areas { get; set; }
        public List<Sancion> sancions { get; set; }
        public List<Formato> formatos { get; set; }
        public List<TipoInspeccion> tipoInspeccions { get; set; }
        public List<NivelInspeccion> nivelInspeccions { get; set; }
        public List<Anomalia> anomalias { get; set; }
        public List<Cliente> clientes { get; set; }
        public List<Actividad> actividades { get; set; }
        public Ot ot { get; set; }
        public List<InspeccionCab> inspeccions { get; set; }
        public string mensaje { get; set; }
    }
}
