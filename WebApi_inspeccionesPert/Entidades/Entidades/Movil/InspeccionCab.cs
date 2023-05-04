using System.Collections.Generic;

namespace Entidades.Movil
{
    public class InspeccionCab
    {
        public int Id { get; set; }
        public int inspeccionCab { get; set; }
        public int EmpresaInspectorId { get; set; }
        public int DelegacionInspectorId { get; set; }
        public int ProyectoInspectorId { get; set; }
        public int PersonalInspectorId { get; set; }
        public int EmpresaId { get; set; } // TODO SE GUARDARA LA EMPRESA COLABORADO DIRECTA
        public int ClienteId { get; set; }
        public string Lugar { get; set; }
        public string ActividadOT { get; set; }
        public string TrabajoRealizar { get; set; }
        public int CargoId { get; set; }
        public int PersonalId { get; set; }
        public int AreaId { get; set; }
        public int CoordinadorId { get; set; }
        public int JefeObraId { get; set; }
        public string Placa { get; set; }
        public int NivelInspeccion { get; set; }
        public int TipoInspeccionId { get; set; }
        public string ResultadoInspeccion { get; set; }
        public string InicioFinal { get; set; }
        public int TipoAnomaliaId { get; set; }
        public string AccionPropuesto { get; set; }
        public int PersonalResponsable { get; set; }
        public string FechaCorrecion { get; set; }
        public string ObservacionAccion { get; set; }
        public string ParalizacionTrabajo { get; set; }
        public string AlgunaSancion { get; set; }
        public int TipoSancionId { get; set; }
        public int NumeroSancionados { get; set; }
        public string FechaInspeccion { get; set; }
        public string NombrePdf { get; set; }
        public string FechaEditar { get; set; }
        public int Estado { get; set; }
        public string latitud { get; set; }
        public string longitud { get; set; }
        public int ActividadId { get; set; }
        public int Conjuntas { get; set; }
        public int PaisId { get; set; }
        public int GrupoId { get; set; }
        public int otId { get; set; }

        public Personal coordinador { get; set; }
        public Personal jefeObra { get; set; }
        public Personal personalInspeccionado { get; set; }
        public NivelInspeccion nivel { get; set; }
        public List<InspeccionCabDetalle> inspeccionDetalle { get; set; }
    }
}
