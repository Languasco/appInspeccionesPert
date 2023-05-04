
namespace Entidades.Movil
{
    public class AnomaliaDetalle
    {
        public int anomaliaDetalleId { get; set; }
        public int tipoInspeccionId { get; set; }
        public int anomaliaId { get; set; }
        public string codigoDetalle { get; set; }
        public string nombreDetalle { get; set; }
        public int titutoDetalle { get; set; }
        public int ordenDetalle { get; set; }
        public string grupoDetalle { get; set; }
        public int criticaDetalle { get; set; }
        public int estado { get; set; }
    }
}
