using Dominio.Models.AtencionesIndividuales;

namespace Dominio.Mapper.AtencionesIndividuales
{
    public class AtencionIndividualReporteResponse
    {
        public long Id { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public long EstadoId { get; set; }
        public long MotivoId { get; set; }
        public long SubMotivoId { get; set; }
        public string TxAclaracionMotivo { get; set; } = string.Empty;
        public string TxGestionRealizada { get; set; } = string.Empty;
        public long UsuarioId { get; set; }
        public long UsuarioActualId { get; set; }
        public long PersonaId { get; set; }
        public string FechaCambioEstadoCaso { get; set; } = string.Empty;
        public string FechaUltimoSeguimientoCaso { get; set; } = string.Empty;

        public long TipoDocumentoId { get; set; }
        public long EnfoquePoblacionalId { get; set; }
        public long PoblacionPrioritariaId { get; set; }
        public long SubPoblacionPrioritariaId { get; set; }
        public string VcDocumento { get; set; } = string.Empty;
        public string VcPrimerApellido { get; set; } = string.Empty;
        public string VcSegundoApellido { get; set; } = string.Empty;
        public string VcPrimerNombre { get; set; } = string.Empty;
        public string VcSegundoNombre { get; set; } = string.Empty;
    }
}
