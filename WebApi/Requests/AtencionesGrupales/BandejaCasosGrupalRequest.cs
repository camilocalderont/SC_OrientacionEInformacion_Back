namespace WebApi.Requests.AtencionesGrupales
{
    public class BandejaCasosGrupalRequest
    {
        public DateTime DtFechaInicio { get; set; }

        public DateTime DtFechaFin { get; set; }
        public long UsuarioId { get; set; }
    }
}
