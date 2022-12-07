namespace Dominio.Models.AtencionesIndividuales
{
    public class AtencionIndividual
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

        public virtual List<AtencionReasignacion> AtencionReasignaciones { get; set; }
        public virtual List<AtencionSeguimiento> AtencionSeguimientos { get; set; }
        public virtual List<AtencionAnexo> AtencionAnexos { get; set; }
        public virtual List<AtencionActor> AtencionActores { get; set; }
        public Persona Personas { get; set; }
    }
}
