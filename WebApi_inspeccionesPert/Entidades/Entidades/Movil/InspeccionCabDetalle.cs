using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Entidades.Movil
{
    public class InspeccionCabDetalle
    {
        public int Id { get; set; }
        public int InspeccionId { get; set; }
        public int inspeccionCabDetalle { get; set; }     
        public int PersonalId { get; set; }
        public int AnomaliaId { get; set; }
        public int Estado { get; set; }
        public int Usuario { get; set; }
        public string Detalle { get; set; }
        public string Valor { get; set; }
        public string Fecha { get; set; }
        public string Levantamiento { get; set; }
        public string LevantamientoFoto { get; set; }
        public string LevantamientoDescripcion { get; set; }
        public string AccionPropuesto { get; set; }
        public string FechaCorrecion { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public string disponividad_uso { get; set; }
        public int size { get; set; }
        public int TipoInspeccionId { get; set; }
        public int personalSancionado { get; set; }
        public int anomalia_critica { get; set; }     
        public int anomaliaDetalleId { get; set; }    
        public int resultadoInspeccionId { get; set; }    
        
        public List<InspeccionCabPhoto> inspeccionCabPhoto { get; set; }
    }
}
