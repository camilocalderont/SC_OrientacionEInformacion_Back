namespace Dominio.Models.AtencionesWeb
{
    public class AtencionWebDTO
    {
        // Días sin respuesta,  
        public long Id { get; set; }
        public long     PersonaWebId { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public long     UsuarioId { get; set; }
        public long     CanalAtencionId { get; set; }
        public long     TipoSolicitudId { get; set; }
        public long     MotivoId { get; set; }
        public long     SubMotivoId { get; set; }
        public string   TxAclaracionMotivo { get; set; }
        public string   TxAsuntoCorreo { get; set; }
        public Boolean?  BProcesoFallido { get; set; }
        public long     TipoProcesoFallidoId { get; set; }
        public long?    TipoGestionId  { get; set; }
        public long     EstadoId { get; set; }

        public string VcNombreCompleto { get; set; }
        public string VcCorreo { get; set; }
        public string VcTelefono1 { get; set; }
        public string VcTelefono2 { get; set; }
        public DateTime DtFechaOrientacion { get; set; }
        public int  IAnexos { get; set; }


    }
}
