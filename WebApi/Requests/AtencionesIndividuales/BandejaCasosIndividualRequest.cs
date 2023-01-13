namespace WebApi.Requests.AtencionesIndividuales
{
    public class BandejaCasosIndividualRequest
    {
        public long EstadoId { get; set; }
        public DateTime DtFechaInicio { get; set; }
        public DateTime DtFechaFin { get; set; }
        public long UsuarioId { get; set; }
        public string VcDocumento { get; set; }

    }
}
