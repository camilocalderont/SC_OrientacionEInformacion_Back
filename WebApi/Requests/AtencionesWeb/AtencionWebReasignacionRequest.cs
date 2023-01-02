namespace WebApi.Requests.AtencionesWeb
{
    public class AtencionWebReasignacionRequest
    {
        public long Id { get; set; }
        public long AtencionWebId { get; set; }
        public string VcDescripcion { get; set; }
        public DateTime DtFechaAsignacion { get; set; }
        public long UsuarioAsignaId { get; set; }
        public DateTime DtFechaReAsignacion { get; set; }
        public long UsuarioActualId { get; set; }

    }
}
