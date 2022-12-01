using Dominio.Mapper.AtencionesGrupales;
using Dominio.Models.AtencionesGrupales;
using System.Net;

namespace WebApi.Responses
{
    public class GrupalResponse<T> : GenericResponse
    {
        private HttpStatusCode codigo;
        private string titulo;
        private string mensaje;
        private AtencionGrupal atenciongrupalDTO;
        private AtencionGrupalAnexo atenciongrupalanexolDTO;
        private AtencionGrupalDTO atenciongrupal;

        public GrupalResponse(HttpStatusCode codigo, string titulo, string mensaje, AtencionGrupal atenciongrupalDTO) : base(codigo, titulo, mensaje)
        {
            this.codigo = codigo;
            this.titulo = titulo;
            this.mensaje = mensaje;
            this.atenciongrupalDTO = atenciongrupalDTO;
        }

        public GrupalResponse(HttpStatusCode codigo, string titulo, string mensaje, AtencionGrupalAnexo atenciongrupalanexolDTO) : base(codigo, titulo, mensaje)
        {
            this.codigo = codigo;
            this.titulo = titulo;
            this.mensaje = mensaje;
            this.atenciongrupalanexolDTO = atenciongrupalanexolDTO;
        }

        public GrupalResponse(HttpStatusCode codigo, string titulo, string mensaje, AtencionGrupalDTO atenciongrupal) : base(codigo, titulo, mensaje)
        {
            this.atenciongrupal = atenciongrupal;
        }
    }
}