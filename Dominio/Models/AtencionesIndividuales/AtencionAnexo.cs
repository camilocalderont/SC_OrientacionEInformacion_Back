namespace Dominio.Models.AtencionesIndividuales
{
    public class AtencionAnexo
    {
        public long     Id { get; set; }
        public long     AtencionIndividualId { get; set; }
        public string   VcNombre { get; set; }
        public string   VcDescripcion { get; set; }
        public long     IBytes { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public long     UsuarioId { get; set; }

        public AtencionIndividual AtencionIndividual { get; set; }
    }
}
