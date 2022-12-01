namespace Dominio.Models.AtencionesIndividuales
{
    public class AtencionActor
    {
        public long     Id { get; set; }
        public long     AtencionIndividualId { get; set; }
        public Guid     TipoActorId { get; set; }
        public Guid     TipoId { get; set; }
        public Guid     SedeId { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public Guid     UsuarioId { get; set; }

        public AtencionIndividual AtencionIndividual { get; set; }
    }
}
