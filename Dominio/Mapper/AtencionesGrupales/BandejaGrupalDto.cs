namespace Dominio.Mapper.AtencionesGrupales
{
    public class BandejaGrupalDto
    {
        public long Id { get; set; }
        public DateTime DtFechaOrientacion { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public long UsuarioId { get; set; }
        public string Usuario { get; set; }
        public int INumeroUsuarios { get; set; }
        public string TipoSolicitud { get; set; }
        public string Motivo { get; set; }
        public string SubMotivo { get; set; }
        public string TxAclaracionMotivo { get; set; }
        public string TiempoDuracion { get; set; }
        public string TipoActividad { get; set; }
        public string Localidad { get; set; }
        public string VcLugar { get; set; }
        public long TipoSolicitudId { get; set; }
        public long MotivoId { get; set; }
        public long SubMotivoId { get; set; }
        public long TiempoDuracionId { get; set; }
        public long TipoActividadId { get; set; }
        public long LocalidadId { get; set; }
        public int IAnexos { get; set; }
    }
}