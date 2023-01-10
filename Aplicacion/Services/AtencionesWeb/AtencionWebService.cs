
using Dominio.Models.AtencionesWeb;
using Persistencia.Repository;


namespace Aplicacion.Services.AtencionesWeb
{
    public class AtencionWebService : GenericService<AtencionWeb>
    {


        public AtencionWebRepository _AtencionWebRepository { get; }
        public IGenericRepository<AtencionWeb> _genericRepository { get; }
        public AtencionWebService(IGenericRepository<AtencionWeb> genericRepository, AtencionWebRepository AtencionWebRepository) : base(genericRepository)
        {
            _AtencionWebRepository = AtencionWebRepository;
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<AtencionWebDTO>> obtenerPorRangoFechasEstadoUsuarioYCorreo(
            long EstadoId,
            DateTime DtFechaInicio,
            DateTime DtFechaFin,
            long usuarioId,
            string VcCorreo
        )
        {
            return await _AtencionWebRepository.obtenerPorRangoFechasEstadoUsuarioYCorreo(EstadoId,DtFechaInicio, DtFechaFin, usuarioId, VcCorreo);
        }

        public async Task<IEnumerable<AtencionWebDTO>> obtenerPorPersonaWebYExcluyeCaso(long PersonaWebId, long AtencionWebId)
        {
            return await _AtencionWebRepository.obtenerPorPersonaWebYExcluyeCaso(PersonaWebId, AtencionWebId);
        }

        public async Task<AtencionWebDTO> obtenerPorId(long atencionWebId)
        {
            return await _AtencionWebRepository.obtenerPorId(atencionWebId);
        }

    }
}
