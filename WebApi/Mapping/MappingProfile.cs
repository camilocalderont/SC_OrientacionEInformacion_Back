                      using AutoMapper;

using Dominio.Mapper.AtencionesGrupales;
using Dominio.Models.AtencionesGrupales;
using Dominio.Models.AtencionesWeb;
using WebApi.Requests.AtencionesGrupales;
using WebApi.Requests.AtencionesWeb;

namespace Aplicacion.Mapping
{
    public class MappingProfile: Profile
    {
        public MappingProfile()    
        {
           
            CreateMap<AtencionGrupal,AtencionGrupalDTO>().ReverseMap();

            CreateMap<AtencionGrupalAnexo, AtencionGrupalAnexoDTO>().ReverseMap();

            CreateMap<AtencionGrupalAnexo, AnexosCasosDTO>().ReverseMap();

            CreateMap<AtencionGrupal, AtencionGrupalRequest>().ReverseMap();

            CreateMap<AtencionWeb, AtencionWebRequest>().ReverseMap();

            CreateMap<PersonaWeb, AtencionWebRequest>().ReverseMap();

            CreateMap<AtencionWebAnexo, AtencionWenAnexoRequest>().ReverseMap();

            CreateMap<AtencionWeb, AtencionWebDTO>().ReverseMap();

        }
    }
}
