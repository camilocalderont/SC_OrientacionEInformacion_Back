namespace Dominio.Mapper.AtencionesIndividuales
{
    public class AtencionIndividualReporteDto
    {
        public string Consecutivo { get; set; }
        public string Cas_Id { get; set; }
        public int Mes { get; set; }
        public string FechaAlmacenamiento { get; set; }
        public long TipoDocumentoId { get; set; }
        public string NumeroIdent { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string EdadAlRegistroDelCaso { get; set; }
        public string RangoDeEdadAlRegistroDelCaso { get; set; }
        public string FechaNacimiento { get; set; }
        public long SexoId { get; set; }
        public long GeneroId { get; set; }
        public long OrientacionSexualId { get; set; }
        public long EnfoquePoblacionalId { get; set; }
        public long PoblacionPrioritariaId { get; set; }
        public long SubPoblacionPrioritariaId { get; set; }
        public long SubEtniaId { get; set; }
        public long EtniaId { get; set; }
        public long RegimenId { get; set; }
        public long AseguradoraId { get; set; }
        public long NivelSisbenId { get; set; }
        public long DepartamentoId { get; set; }
        public long MunicipioId { get; set; }
        public string Direccion { get; set; }
        public long LocalidadId { get; set; }
        public long BarrioId { get; set; }
        public long UpzId { get; set; }
        public string  CorreoElectronico { get; set; }
        public string Telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string AclaracionesTelfDir { get; set; }
        public long TipoOrientacion { get; set; }
        public long MotivoId { get; set; }
        public long SubMotivoId { get; set; }
        public string AclaracionMotivoOrientacion { get; set; }
        public string GestionResolucionPAcceso { get; set; }
        public string DatosDeContactoAclaración { get; set; }
        public long CanalAtencionId { get; set; }
        public long EstadoCasoId { get; set; }
        public long UsuarioId { get; set; }
    }
}
