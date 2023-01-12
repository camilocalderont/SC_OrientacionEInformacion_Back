namespace Dominio.Mapper.AtencionesIndividuales
{
    public class AtencionIndividualAnexoDTO
    {
        public long     Id { get; set; }
        public long     AtencionIndividualId { get; set; }
        public string   VcNombre { get; set; }
        public string?   VcDescripcion { get; set; }
        public long     IBytes { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public long     UsuarioId { get; set; }
        public string VcRuta { get; set; }
    }
}
