namespace WebApi.Requests.AtencionesGrupales
{
    public class AtencionGrupalRequest
    {
        public long Id { get; set; }
        public DateTime DtFechaOrientacion { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public long UsuarioId { get; set; }
        public int INumeroUsuarios { get; set; }
        public long TipoSolicitudId { get; set; }
        public long MotivoId { get; set; }
        public long SubMotivoId { get; set; }
        public string TxAclaracionMotivo { get; set; }
        public long TiempoDuracionId { get; set; }
        public long TipoActividadId { get; set; }
        public long LocalidadId { get; set; }
        public string VcLugar { get; set; }
        public IFormFile Anexo { get; set; }


    }
}
