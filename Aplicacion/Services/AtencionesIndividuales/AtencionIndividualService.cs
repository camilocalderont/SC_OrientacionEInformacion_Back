
using Dominio.Models.AtencionesIndividuales;
using Dominio.Mapper.AtencionesIndividuales;
using Persistencia.Repository;


namespace Aplicacion.Services.AtencionesIndividuales
{
    public class AtencionIndividualService : GenericService<AtencionIndividual>
    {
        public AtencionIndividualRepository _AtencionIndividualRepository { get; }
        public IGenericRepository<AtencionIndividual> _genericRepository { get; }
        public AtencionIndividualService(IGenericRepository<AtencionIndividual> genericRepository, AtencionIndividualRepository AtencionIndividualRepository) : base(genericRepository)
        {
            _AtencionIndividualRepository = AtencionIndividualRepository;
            _genericRepository = genericRepository;
        }

        public async Task<IEnumerable<AtencionIndividualReporteResponse>> obtenerPorRangoFechas(DateTime DtFechaInicio, DateTime DtFechaFin)
        {
            return await _AtencionIndividualRepository.obtenerPorRangoFechas(DtFechaInicio, DtFechaFin);
        }

        public async Task<IEnumerable<BandejaIndividualDTO>> obtenerPorRangoFechasEstadoUsuarioYDocumento(
            long EstadoId,
            DateTime DtFechaInicio,
            DateTime DtFechaFin,
            long usuarioId,
            string VcDocumento
        )
        {
            return await _AtencionIndividualRepository.obtenerPorRangoFechasEstadoUsuarioYDocumento(EstadoId,DtFechaInicio, DtFechaFin, usuarioId, VcDocumento);
        }


        public async Task<IEnumerable<AtencionIndividualReporteDto>> obtenerPorRangoFechasParaReporteOYP(DateTime fechaInicio, DateTime fechaFinal)
        {
            return await _AtencionIndividualRepository.obtenerPorRangoFechasParaReporteOYP(fechaInicio, fechaFinal);
        }


        public async Task<IEnumerable<BandejaIndividualDTO>> obtenerPorPersonaYExcluyeCaso(long PersonaId, long AtencionIndividualId)
        {
            return await _AtencionIndividualRepository.obtenerPorPersonaYExcluyeCaso(PersonaId, AtencionIndividualId);
        }

        public async Task<BandejaIndividualDTO> obtenerPorId(long atencionIndividualId)
        {
            return await _AtencionIndividualRepository.obtenerPorId(atencionIndividualId);
        }

        public async Task<IEnumerable<BandejaIndividualDTO>> obtenerPorTipoDocumentoDocumentoYEstado(long tipoDocumentoId, string VcDocumento, long EstadoId)
        {
            return await _AtencionIndividualRepository.obtenerPorTipoDocumentoDocumentoYEstado(tipoDocumentoId,VcDocumento,EstadoId);
        }        

    }
}
