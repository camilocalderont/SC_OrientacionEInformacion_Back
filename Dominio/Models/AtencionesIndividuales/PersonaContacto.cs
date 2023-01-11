namespace Dominio.Models.AtencionesIndividuales
{
    public class PersonaContacto
    {
        public long     Id { get; set; }
        public long     PersonaId { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public long     UsuarioId { get; set; }
        public long     PaisId { get; set; }
        public long     DepartamentoId { get; set; }
        public long     MunicipioId { get; set; }
        public long     LocalidadId { get; set; }
        public long?    UpzId { get; set; }
        public long?    BarrioId { get; set; }
        public long     ZonaId { get; set; }
        public string   VcDireccion { get; set; }
        public string   TxDatosContactoAclaraciones { get; set; }
        public string   VcTelefono1 { get; set; }
        public string   VcTelefono2 { get; set; }

        public Persona Personas { get; set; }
    }
}
