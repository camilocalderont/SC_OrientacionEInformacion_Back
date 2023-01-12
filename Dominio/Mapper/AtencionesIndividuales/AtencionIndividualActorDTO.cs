namespace Dominio.Mapper.AtencionesIndividuales
{
    public class AtencionIndividualActorDTO
    {
        public long     Id { get; set; }
        public long     AtencionIndividualId { get; set; }
        public long     TipoActorId { get; set; }
        public long     TipoId { get; set; }
        public long     SedeId { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public long     UsuarioId { get; set; }
    }
}
