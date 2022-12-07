namespace Dominio.Models.AtencionesWeb
{
    public class AtencionWebSeguimiento
    {
        public long     Id { get; set; }
        public long     AtencionWebId { get; set; }
        public string   VcDescripcion { get; set; }
        public Boolean  BCierraCaso { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public long     UsuarioId { get; set; }

        public AtencionWeb AtencionesWeb { get; set; }

    }
}
