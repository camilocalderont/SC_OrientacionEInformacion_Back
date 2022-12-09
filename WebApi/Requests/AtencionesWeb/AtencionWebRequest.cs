using System.ComponentModel.DataAnnotations;

namespace WebApi.Requests.AtencionesWeb
{
    public class AtencionWebRequest
    {
        public long Id { get; set; }
        public string VcPrimerNombre { get; set; }
        public string VcSegundoNombre { get; set; }
        public string VcPrimerApellido { get; set; }
        public string VcSegundoApellido { get; set; }
        public string VcCorreo { get; set; }
        public string VcTelefono1 { get; set; }
        public string VcTelefono2 { get; set; }
        public long UsuarioId { get; set; }
        public long UsuarioActualizacionId { get; set; }

        public long CanalAtencionId { get; set; }
        public long TipoSolicitudId { get; set; }
        public long MotivoId { get; set; }
        public long SubMotivoId { get; set; }
        public string TxAclaracionMotivo { get; set; }
        public string TxAsuntoCorreo { get; set; }
        public Boolean BProcesoFallido { get; set; }
        public long TipoProcesoFallidoId { get; set; }
        public long TipoGestionId { get; set; }
        public long EstadoId { get; set; }

        public IFormFile Anexo { get; set; }
    }
}
