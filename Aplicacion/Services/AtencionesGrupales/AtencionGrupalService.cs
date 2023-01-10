using Dominio.Mapper.AtencionesGrupales;
using Dominio.Models.AtencionesGrupales;
using Persistencia.Repository;


namespace Aplicacion.Services.AtencionesGrupales
{
    public class AtencionGrupalService : GenericService<AtencionGrupal>
    {


        public AtencionGrupalRepository _atencionGrupalRepository { get; }
        public IGenericRepository<AtencionGrupal> _genericRepository { get; }
        public AtencionGrupalService(IGenericRepository<AtencionGrupal> genericRepository, AtencionGrupalRepository atencionGrupalRepository) : base(genericRepository)
        {
            _atencionGrupalRepository = atencionGrupalRepository;
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<AtencionGrupal>> obtenerPorRangoFechasYUsuario(DateTime DtFechaInicio, DateTime DtFechaFin, long usuarioId)
        {
            return await _atencionGrupalRepository.obtenerPorRangoFechasYUsuario(DtFechaInicio, DtFechaFin, usuarioId);
        }

        public async Task<AtencionGrupalDTO> obtenerPorId(long atencionGrupalId)
        {
            return await _atencionGrupalRepository.obtenerPorId(atencionGrupalId);
        }
    }
}
