using Dominio.Models.AtencionesGrupales;
using Dominio.Models.AtencionesWeb;
using Microsoft.EntityFrameworkCore;
using Persistencia.Repository;

namespace Aplicacion.Services.AtencionesWeb
{
    public class PersonaWebService : GenericService<PersonaWeb>
    {

        public PersonaWebRepository _personaWebRepository { get; }
        public IGenericRepository<PersonaWeb> _genericRepository { get; }
        public PersonaWebService(IGenericRepository<PersonaWeb> genericRepository, PersonaWebRepository personaWebRepository) : base(genericRepository)
        {
            _genericRepository = genericRepository;
            _personaWebRepository = personaWebRepository;
        }

        public PersonaWeb obtenerporCorreo(string correo)
        {
            return _personaWebRepository.obtenerporCorreo(correo);
        }

    }
}
