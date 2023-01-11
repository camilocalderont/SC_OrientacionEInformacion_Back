namespace Dominio.Mapper.AtencionesIndividuales
{
    public class AtencionIndividualAnexoDTO
    {
        public long     Id { get; set; }
        public long     AtencionIndividualId { get; set; }
        public string   VcNombre { get; set; }
        public string   VcDescripcion { get; set; }
        public Guid     IBytes { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public Guid     UsuarioId { get; set; }
    }
}
