namespace Dominio.Mapper.AtencionesGrupales
{
    public class AtencionGrupalDTO
    {
        public DateTime DtFechaRegistro { get; set; }
        public long     UsuarioId { get; set; }
        public int      INumeroUsuarios { get; set; }
        public long     TipoSolicitudId { get; set; }
        public long     MotivoId { get; set; }
        public long     SubMotivoId { get; set; }
        public string   TxAclaracionMotivo { get; set; }
        public long     TiempoDuracionId { get; set; }
        public long     TipoActividadId { get; set; }
        public long     LocalidadId { get; set; }
        public long     VcLugar { get; set; }

        //public IEnumerable<AtencionGrupalAnexoDTO> AtencionGrupal { get; set; }
        //    = Array.Empty<AtencionGrupalAnexoDTO>();

    }
}
