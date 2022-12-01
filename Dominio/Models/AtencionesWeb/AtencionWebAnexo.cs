namespace Dominio.Models.AtencionesWeb
{
    public class AtencionWebAnexo
    {
        public long     Id { get; set; }
        public long      AtencionWebId { get; set; }
        public string   VcNombre { get; set; }
        public string   VcDescripcion { get; set; }
        public int      IBytes { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public Guid     UsuarioId { get; set; }

        public AtencionWeb AtencionesWeb { get; set; }
    }
}
