namespace Dominio.Mapper.AtencionesIndividuales
{
    public class AtencionSeguimientoDTO
    {
        public long     Id { get; set; }
        public long     AtencionIndividualId { get; set; }
        public string   VcDescripcion { get; set; }
        public Boolean  BCierraCaso { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public Guid     UsuarioId { get; set; }
    }
}
