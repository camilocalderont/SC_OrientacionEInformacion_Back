namespace Dominio.Models.AtencionesIndividuales
{
    public class Persona
    {
        public  long     Id{ get; set; }
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
        public  DateTime DtFechaNacimiento { get; set; }
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


        public virtual List<AtencionIndividual> AtencionIndividual { get; set; }
        public virtual List<PersonaAfiliacion> PersonaAfiliaciones { get; set; }
        public virtual List<PersonaContacto> PersonaContactos { get; set; }


    }
}
