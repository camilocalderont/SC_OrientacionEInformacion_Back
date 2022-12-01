namespace Dominio.Mapper.AtencionesIndividuales
{
    public class AtencionIndividualDTO
    {
        public long     Id { get; set; }
        public long     PersonaId { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public Guid     UsuarioId { get; set; }
        public Guid     CanalAtencionId { get; set; }
        public string   VcTurnoSat { get; set; }
        public Guid     TipoSolicitudId { get; set; }
        public Guid     MotivoId { get; set; }
        public Guid     SubMotivoId { get; set; }
        public string   TxAclaracionMotivo { get; set; }
        public string   TxGestionRealizada { get; set; }
        public string   VcRadicadoBte { get; set; }
        public Guid     EstadoId { get; set; }
    }
}
