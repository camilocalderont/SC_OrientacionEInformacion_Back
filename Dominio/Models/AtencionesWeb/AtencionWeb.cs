namespace Dominio.Models.AtencionesWeb
{
    public class AtencionWeb
    {
        public long Id { get; set; }
        public long     PersonaWebId { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public Guid     UsuarioId { get; set; }
        public Guid     CanalAtencionId { get; set; }
        public Guid     TipoSolicitudId { get; set; }
        public Guid     MotivoId { get; set; }
        public Guid     SubMotivoId { get; set; }
        public string   TxAclaracionMotivo { get; set; }
        public string   TxAsuntoCorreo { get; set; }
        public Boolean  BProcesoFallido { get; set; }
        public Guid     TipoProcesoFallidoId { get; set; }
        public Guid     TipoGestionId  { get; set; }
        public Guid     EstadoId { get; set; }

        public virtual List<AtencionWebReasignacion> AtencionReasignaciones { get; set; }
        public virtual List<AtencionWebSeguimiento> AtencionSeguimientos { get; set; }
        public virtual List<AtencionWebAnexo> AtencionAnexos { get; set; }
        public PersonaWeb PersonasWeb { get; set; }
    }
}
