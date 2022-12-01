namespace Dominio.Mapper.AtencionesIndividuales
{
    public class PersonaContactoDTO
    {
        public long     Id { get; set; }
        public long     PersonaId { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public Guid     UsuarioId { get; set; }
        public Guid     PaisId { get; set; }
        public Guid     DepartamentoId { get; set; }
        public Guid     LocalidadId { get; set; }
        public Guid     UpzId { get; set; }
        public Guid     BarrioId { get; set; }
        public Guid     ZonaId { get; set; }
        public string   VcDireccion { get; set; }
        public string   TxDatosContactoAclaraciones { get; set; }
        public string   VcTelefono1 { get; set; }
        public string   VcTelefono2 { get; set; }
    }
}
