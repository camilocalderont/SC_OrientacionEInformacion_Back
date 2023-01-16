
namespace WebApi.Requests.AtencionesIndividuales
{
    public class AtencionIndividualRequest
    {
        public long     Id { get; set; }
        public long     PersonaId { get; set; }
        public long     UsuarioId { get; set; }
        public long     CanalAtencionId { get; set; }
        public string   VcTurnoSat { get; set; }
        public long     TipoSolicitudId { get; set; }
        public long     MotivoId { get; set; }
        public long     SubMotivoId { get; set; }
        public string   TxAclaracionMotivo { get; set; }
        public string   TxGestionRealizada { get; set; }
        public string   VcRadicadoBte { get; set; }
        public long     EstadoId { get; set; }
        public IFormFile? Anexo { get; set; }
    }
}
