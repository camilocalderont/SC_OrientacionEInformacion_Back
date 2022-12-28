namespace WebApi.Storage
{
    public class ArchivoCarga
    {
        public Stream archivoStream { get; set; }
        public string rutaLocal { get; set; }
        public string carpetaInicial { get; set; }        
        public string rutaRemota { get; set; }
        public bool esPublico { get; set; }
    }
}
