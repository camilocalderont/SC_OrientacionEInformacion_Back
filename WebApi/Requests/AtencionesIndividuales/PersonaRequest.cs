namespace WebApi.Requests.AtencionesIndividuales
{
    public class PersonaRequest
    {
        public  long     Id { get; set; }
        public  long     TipoDocumentoId { get; set; }
        public  string   VcDocumento { get; set; }
        public  string   VcPrimerNombre { get; set; }
        public  string   VcSegundoNombre { get; set; }
        public  string   VcPrimerApellido { get; set; }
        public  string   VcSegundoApellido { get; set; }
        public  long     GeneroId { get; set; }
        public  string   VcOtroGenero { get; set; }
        public  string   VcNombreIdentitario { get; set; }
        public  long     OrientacionSexualId { get; set; }
        public  string   VcOtraOrientacionSexual { get; set; }
        public  long     SexoId { get; set; }
        public  DateTime? DtFechaNacimiento { get; set; }
        public  long     EnfoquePoblacionalId { get; set; }
        public  long?     HechoVictimizanteId { get; set; }
        public  long?     DepartamentoOrigenVictimaId { get; set; }
        public  long?     MunicipioOrigenVictimaId { get; set; }
        public  long     EtniaId { get; set; }
        public  long?     SubEtniaId { get; set; }
        public  long     PoblacionPrioritariaId { get; set; }
        public  long?     SubPoblacionPrioritariaId { get; set; }
        public  string   VcCorreo { get; set; }
        public  DateTime DtFechaRegistro { get; set; }
        public  long     UsuarioId { get; set; }
        public  DateTime? DtFechaActualizacion { get; set; }
        public  long?     UsuarioActualizacionId { get; set; }


        public long     RegimenId { get; set; }
        public long     AseguradoraId { get; set; }
        public long     EstadoAfiliacionId { get; set; }
        public long     NivelSisbenId { get; set; }
        public long?    InstitucionInstrumentoVinculadoId { get; set; }        


        public long     PaisId { get; set; }
        public long     DepartamentoId { get; set; }
        public long     MunicipioId { get; set; }
        public long     LocalidadId { get; set; }
        public long?    UpzId { get; set; }
        public long?    BarrioId { get; set; }
        public long     ZonaId { get; set; }
        public string   VcDireccion { get; set; }
        public string   TxDatosContactoAclaraciones { get; set; }
        public string   VcTelefono1 { get; set; }
        public string   VcTelefono2 { get; set; }        
    }
}