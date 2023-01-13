
using Dominio.Models.AtencionesIndividuales;
using Persistencia.Repository;


namespace Aplicacion.Services.AtencionesIndividuales
{
    public class PersonaAfiliacionService : GenericService<PersonaAfiliacion>
    {

        public IGenericRepository<PersonaAfiliacion> _genericRepository { get; }
        public PersonaAfiliacionRepository _personaAfiliacionRepository { get; }
        public PersonaAfiliacionService(
            IGenericRepository<PersonaAfiliacion> genericRepository,
            PersonaAfiliacionRepository personaAfiliacionRepository ) : base(genericRepository)
        {
            _genericRepository = genericRepository;
            _personaAfiliacionRepository = personaAfiliacionRepository;
        }

        public async Task<bool> CambioModelo(PersonaAfiliacion personaAfiliacion, long personaId)
        {
            bool cambio = true;
            var PersonaAfiliacionBD = await _personaAfiliacionRepository.obtenerPorPersonaId(personaId);
            if(PersonaAfiliacionBD != null)
            {
                cambio = !( personaAfiliacion.RegimenId == PersonaAfiliacionBD.RegimenId &&
                            personaAfiliacion.AseguradoraId == PersonaAfiliacionBD.AseguradoraId &&
                            personaAfiliacion.EstadoAfiliacionId == PersonaAfiliacionBD.EstadoAfiliacionId &&
                            personaAfiliacion.NivelSisbenId == PersonaAfiliacionBD.NivelSisbenId);
            }

            return cambio;
        }
        
    }
}
