
using Dominio.Models.AtencionesIndividuales;
using Persistencia.Repository;


namespace Aplicacion.Services.AtencionesIndividuales
{
    public class PersonaService : GenericService<Persona>
    {
        public PersonaRepository _personaRepository { get; }
        public IGenericRepository<Persona> _genericRepository { get; }

        public PersonaService(IGenericRepository<Persona> genericRepository ,PersonaRepository personaRepository) : base(genericRepository)
        {
            _genericRepository = genericRepository;
            _personaRepository = personaRepository;
        }

        public async Task<Persona> obtenerPorTipoDocumentoyDocumento(long tipoDocumentoId,string vcDocumento)
        {
            return await _personaRepository.obtenerPorTipoDocumentoyDocumento(tipoDocumentoId,vcDocumento);
        }
        
    }
}
