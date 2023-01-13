using Dominio.Models.AtencionesIndividuales;
using Microsoft.EntityFrameworkCore;
using Persistencia.Context;


namespace Persistencia.Repository
{
    public class PersonaAfiliacionRepository : GenericRepository<PersonaAfiliacion>
    {
        public IGenericRepository<PersonaAfiliacion> _personaAfiliacionRepository { get; }
        public OrientacionDbContext _context;

        public PersonaAfiliacionRepository(IGenericRepository<PersonaAfiliacion> personaAfiliacionRepository, OrientacionDbContext context):base(context)
        {
            _personaAfiliacionRepository = personaAfiliacionRepository;
            _context = context;
        }

        public async Task<PersonaAfiliacion> obtenerPorPersonaId(long personaId){
            return await _context.PersonaAfiliacion.Where(p=>p.PersonaId == personaId).OrderBy(P => P.Id).LastOrDefaultAsync();
        }
    }
}