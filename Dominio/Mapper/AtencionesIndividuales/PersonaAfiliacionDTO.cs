namespace Dominio.Mapper.AtencionesIndividuales
{
   public class PersonaAfiliacionDTO
   {
        public long     Id { get; set; }
        public long     PersonaId { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public long     UsuarioId { get; set; }
        public long     RegimenId { get; set; }
        public long     AseguradoraId { get; set; }
        public long     EstadoAfiliacionId { get; set; }
        public long     NivelSisbenId { get; set; }
        public long?    InstitucionInstrumentoVinculadoId { get; set; }
    }
}
