using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Movil
{
    public class InspeccionCabPhoto
    {
        public int Id { get; set; }
        public int DetalleInspeccionId { get; set; }
        public int DetalleInspeccionRetorno { get; set; }
        public string Foto { get; set; }
        public string Fecha { get; set; }
        public int size { get; set; }
        public int estado { get; set; }
        public string tipo { get; set; }
    }
}
