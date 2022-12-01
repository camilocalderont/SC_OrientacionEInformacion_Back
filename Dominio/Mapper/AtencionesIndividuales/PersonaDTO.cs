namespace Dominio.Mapper.AtencionesIndividuales
{
    public class PersonaDTO
    {
        public  long     Id{ get; set; }
        public  Guid     TipoDocumentoId { get; set; }
        public  string   VcDocumento { get; set; }
        public  string   VcPrimerNombre { get; set; }
        public  string   VcSegundoNombre { get; set; }
        public  string   VcPrimerApellido { get; set; }
        public  string   VcSegundoApellido { get; set; }
        public  Guid     GeneroId { get; set; }
        public  string   VcOtroGenero { get; set; }
        public  string   VcNombreIdentitario { get; set; }
        public  Guid     OrientacionSexualId { get; set; }
        public  string   VcOtraOrientacionSexual { get; set; }
        public  Guid     SexoId { get; set; }
        public  DateTime DtFechaNacimiento { get; set; }
        public  Guid     EnfoquePoblacionalId { get; set; }
        public  Guid?     HechoVictimizanteId { get; set; }
        public  Guid?     DepartamentoOrigenVictimaId { get; set; }
        public  Guid?     MunicipioOrigenVictimaId { get; set; }
        public  Guid     EtniaId { get; set; }
        public  Guid?     SubEtniaId { get; set; }
        public  Guid     PoblacionPrioritariaId { get; set; }
        public  Guid?     SubPoblacionPrioritariaId { get; set; }
        public  string   VcCorreo { get; set; }
        public  DateTime DtFechaRegistro { get; set; }
        public  Guid     UsuarioId { get; set; }
        public  DateTime? DtFechaActualizacion { get; set; }
        public  Guid?     UsuarioActualizacionId { get; set; }
    }
}
