namespace Dominio.Mapper.AtencionesWeb
{
    public class AtencionWebReporteDTO
    {
        public long Id { get; set; }
        public long Mes { get; set; }
        public DateTime FechaAlmacenamiento { get; set; }
        public DateTime FechaOrientacion { get; set; }
        public string PrimerApellido { get; set; }
        public string SegundoApellido { get; set; }
        public string PrimerNombre { get; set; }
        public string SegundoNombre { get; set; }
        public string CorreoElectronico { get; set; }
        public string Telefono1 { get; set; }
        public string Celular { get; set; }
        public long MotivoId { get; set; }
        public long SubMotivoId { get; set; }
        public long CanalAtencionId { get; set; }
        public long TipoGestionId { get; set; }
        public long TipoProcesoFallidoId { get; set; }
        public string AsuntoCorreoElectronico { get; set; }
        public string AclaracionMotivoOrientacion { get; set; }
        public long UsuarioId { get; set; }
    }
}
