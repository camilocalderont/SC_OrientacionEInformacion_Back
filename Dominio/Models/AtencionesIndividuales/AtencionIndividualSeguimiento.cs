namespace Dominio.Models.AtencionesIndividuales
{
    public class AtencionIndividualSeguimiento
    {
        public long     Id { get; set; }
        public long     AtencionIndividualId { get; set; }
        public string   VcDescripcion { get; set; }
        public Boolean  BCierraCaso { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public long     UsuarioId { get; set; }

        public AtencionIndividual AtencionIndividual { get; set; }

    }
}
