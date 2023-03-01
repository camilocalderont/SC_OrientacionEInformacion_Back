using Dominio.Models.AtencionesIndividuales;

namespace Dominio.Request
{
    public class PersonaRequest
    {
        public List<string>? Errores { get; set; }
        public int Registros { get; set; }
    }
}
