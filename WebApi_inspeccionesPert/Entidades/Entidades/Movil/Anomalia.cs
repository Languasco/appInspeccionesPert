using System.Collections.Generic;

namespace Entidades.Movil
{
    public class Anomalia
    {
        public int AnomaliaId { get; set; }
        public string Descripcion { get; set; }
        public int Formato { get; set; }
        public int Titulo { get; set; }
        public string Valor { get; set; }
        public int Orden { get; set; }
        public string verValidacion { get; set; }
        public int personalNuevo { get; set; }
        public string critica { get; set; }
        public List<AnomaliaDetalle> anomaliaDetalles { get; set; }
    }
}
