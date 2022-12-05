using AutoMapper;

using Dominio.Mapper.AtencionesGrupales;
using Dominio.Models.AtencionesGrupales;
using WebApi.Requests.AtencionesGrupales;

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

        }
    }
}
