using AutoMapper;
using Dominio.Mapper.AtencionesGrupales;
using Dominio.Models.AtencionesGrupales;
using Persistencia.Repository;


namespace Aplicacion.Services.AtencionesGrupales
{
    public class AtencionGrupalService : GenericService<AtencionGrupal>
    {


        public AtencionGrupalRepository _atencionGrupalRepository { get; }
        public IGenericRepository<AtencionGrupal> _genericRepository { get; }
        private readonly IMapper _mapper;
        public AtencionGrupalService(
            IGenericRepository<AtencionGrupal> genericRepository, 
            AtencionGrupalRepository atencionGrupalRepository,
            IMapper mapper
        ) : base(genericRepository)
        {
            _atencionGrupalRepository = atencionGrupalRepository;
            _genericRepository = genericRepository;
            _mapper = mapper;
        }

        public async Task<IEnumerable<BandejaGrupalDto>> obtenerPorRangoFechasYUsuario(DateTime DtFechaInicio, DateTime DtFechaFin, long usuarioId)
        {
            var atencionesGrupales = await _atencionGrupalRepository.obtenerPorRangoFechasYUsuario(DtFechaInicio, DtFechaFin, usuarioId); ;
            var bandejaGrupal = _mapper.Map<IEnumerable<BandejaGrupalDto>>(atencionesGrupales);
            return bandejaGrupal;
        }

        public async Task<IEnumerable<AtencionGrupal>> obtenerPorRangoFechas(DateTime DtFechaInicio, DateTime DtFechaFin)
        {
            return await _atencionGrupalRepository.obtenerPorRangoFechas(DtFechaInicio, DtFechaFin);
        }

        public async Task<AtencionGrupalDTO> obtenerPorId(long atencionGrupalId)
        {
            return await _atencionGrupalRepository.obtenerPorId(atencionGrupalId);
        }
    }
}
