using System.Data;

namespace Dominio.Request
{
    public class ParametroRequest
    {
        public DataTable AtencionGrupal { get; set; }

        public DataTable AtencionGrupalAnexo { get; set; }
        //public List<Parametro>? Parametros { get; set; }
        //public List<ParametroDetalle>? ParametroDetalles { get; set; } 
        public List<string>? Errores { get; set; }
        public int Registros { get; set; }

    }
}
