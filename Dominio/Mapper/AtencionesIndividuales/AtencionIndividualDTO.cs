namespace Dominio.Mapper.AtencionesIndividuales
{
    public class AtencionIndividualDTO
    {
        public long     Id { get; set; }
        public long     PersonaId { get; set; }
        public DateTime DtFechaRegistro { get; set; }
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
        public int      IAnexos { get; set; }
        public long     UsuarioActualId { get; set; }
        public long    TipoDocumentoId { get; set; }
        public string  VcDocumento   { get; set; }
        public string VcNombrecompleto { get; set; }

    }
}
