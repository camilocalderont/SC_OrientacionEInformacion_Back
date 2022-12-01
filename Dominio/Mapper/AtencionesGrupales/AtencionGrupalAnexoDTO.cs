namespace Dominio.Mapper.AtencionesGrupales
{ 
    public class AtencionGrupalAnexoDTO
    {
        public long      AtencionGrupalId { get; set; }
        public string   VcNombre { get; set; }
        public string   VcDescripcion { get; set; }
        public int      IBytes { get; set; }
        public string   VcRuta { get; set; }
        public string   VcUrl { get; set; }
        public DateTime DtFechaRegistro { get; set; }
        public long     UsuarioId { get; set; }

    }
}
