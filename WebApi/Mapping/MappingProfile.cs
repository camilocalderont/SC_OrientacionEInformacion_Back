using AutoMapper;

using Dominio.Mapper.AtencionesGrupales;
using Dominio.Models.AtencionesGrupales;
using Dominio.Models.AtencionesWeb;
using Dominio.Models.AtencionesIndividuales;
using WebApi.Requests.AtencionesGrupales;
using WebApi.Requests.AtencionesWeb;
using WebApi.Requests.AtencionesIndividuales;

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

            CreateMap<AtencionWebAnexo, AtencionWebAnexoRequest>().ReverseMap();

            CreateMap<AtencionWeb, AtencionWebDTO>().ReverseMap();

            CreateMap<Persona, PersonaRequest>().ReverseMap();

            CreateMap<PersonaAfiliacion, PersonaRequest>().ReverseMap();

            CreateMap<PersonaContacto, PersonaRequest>().ReverseMap();

            CreateMap<AtencionIndividual, AtencionIndividualRequest>().ReverseMap();

            CreateMap<AtencionIndividualSeguimiento, AtencionIndividualSeguimientoRequest>().ReverseMap();

            CreateMap<AtencionWebSeguimiento, AtencionWebSeguimientoRequest>().ReverseMap();
            
            CreateMap<AtencionGrupal, BandejaGrupalDto>()
                .ForMember(dest=>dest.IAnexos,opt=>opt.MapFrom(src=>src.AtencionGrupalesAnexos.Count()));

        }
    }
}
