namespace Dominio.Models.AtencionesWeb
{
    public class PersonaWebDTO
    {
        public long     Id { get; set; }
        public string   VcPrimerNombre { get; set; }
        public string   VcSegundoNombre { get; set; }
        public string   VcPrimerApellido { get; set; }
        public string   VcSegundoApellido { get; set; }
        public string   VcCorreo { get; set; }
        public string   VcTelefono1 { get; set; }
        public string   VcTelefono2 { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public long     UsuarioId { get; set; }
        public DateTime DtFechaActualizacion { get; set; }
        public long     UsuarioActualizacionId { get; set; }

    }
}
