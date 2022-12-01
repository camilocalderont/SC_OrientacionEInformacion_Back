namespace Dominio.Mapper.AtencionesIndividuales
{
    public class AtencionActorDTO
    {
        public long     Id { get; set; }
        public long     AtencionIndividualId { get; set; }
        public Guid     TipoActorId { get; set; }
        public Guid     TipoId { get; set; }
        public Guid     SedeId { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public Guid UsuarioId { get; set; }
    }
}
