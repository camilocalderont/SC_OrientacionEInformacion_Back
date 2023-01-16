namespace Dominio.Models.AtencionesIndividuales
{
   public class PersonaAfiliacion
   {
        public long? Id { get; set; }
        public long     PersonaId { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public long     UsuarioId { get; set; }
        public long     RegimenId { get; set; }
        public long     AseguradoraId { get; set; }
        public long     EstadoAfiliacionId { get; set; }
        public long     NivelSisbenId { get; set; }
        public long?    InstitucionInstrumentoVinculadoId { get; set; }

        public Persona Personas { get; set; }
    }
}
