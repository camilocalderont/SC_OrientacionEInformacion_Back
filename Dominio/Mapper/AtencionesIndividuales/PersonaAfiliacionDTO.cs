namespace Dominio.Mapper.AtencionesIndividuales
{
   public class PersonaAfiliacionDTO
   {
        public long     Id { get; set; }
        public long     PersonaId { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public Guid     UsuarioId { get; set; }
        public Guid     RegimenId { get; set; }
        public Guid     AseguradoraId { get; set; }
        public Guid     EstadoAfiliacionId { get; set; }
        public Guid     NivelSisbenId { get; set; }
        public Guid?    InstitucionInstrumentoVinculadoId { get; set; }
    }
}
