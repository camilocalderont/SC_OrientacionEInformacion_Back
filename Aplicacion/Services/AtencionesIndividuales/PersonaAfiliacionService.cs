
using Dominio.Models.AtencionesIndividuales;
using Persistencia.Repository;


namespace Aplicacion.Services.AtencionesIndividuales
{
    public class PersonaAfiliacionService : GenericService<PersonaAfiliacion>
    {

        public IGenericRepository<PersonaAfiliacion> _genericRepository { get; }

        public PersonaAfiliacionService(IGenericRepository<PersonaAfiliacion> genericRepository ) : base(genericRepository)
        {
            _genericRepository = genericRepository;
        }
        
    }
}
