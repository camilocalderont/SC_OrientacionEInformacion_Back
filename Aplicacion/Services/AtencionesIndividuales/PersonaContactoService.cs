
using Dominio.Models.AtencionesIndividuales;
using Persistencia.Repository;


namespace Aplicacion.Services.AtencionesIndividuales
{
    public class PersonaContactoService : GenericService<PersonaContacto>
    {

        public IGenericRepository<PersonaContacto> _genericRepository { get; }

        public PersonaContactoService(IGenericRepository<PersonaContacto> genericRepository ) : base(genericRepository)
        {
            _genericRepository = genericRepository;
        }
        
    }
}
