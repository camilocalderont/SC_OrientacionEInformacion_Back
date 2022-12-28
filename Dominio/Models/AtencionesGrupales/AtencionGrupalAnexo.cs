namespace Dominio.Models.AtencionesGrupales
{ 
    public class AtencionGrupalAnexo
    {
        public long     Id { get; set; }
        public long      AtencionGrupalId { get; set; }
        public string   VcNombre { get; set; }
        public string?   VcDescripcion { get; set; }
        public int      IBytes { get; set; }
        public string   VcRuta { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public long     UsuarioId { get; set; }

        public AtencionGrupal AtencionGrupales { get; set; }
    }
}
