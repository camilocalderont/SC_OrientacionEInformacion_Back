namespace WebApi.Requests.AtencionesWeb
{
    public class BandejaCasosWebRequest
    {
        public long EstadoId { get; set; }
        public DateTime DtFechaInicio { get; set; }
        public DateTime DtFechaFin { get; set; }
        public long UsuarioId { get; set; }
        public string VcCorreo { get; set; }

    }
}
