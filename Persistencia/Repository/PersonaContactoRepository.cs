using Dominio.Models.AtencionesIndividuales;
using Microsoft.EntityFrameworkCore;
using Persistencia.Context;


namespace Persistencia.Repository
{
    public class PersonaContactoRepository : GenericRepository<PersonaContacto>
    {
        public IGenericRepository<PersonaContacto> _personaContactoRepository { get; }
        public OrientacionDbContext _context;

        public PersonaContactoRepository(IGenericRepository<PersonaContacto> personaContactoRepository, OrientacionDbContext context):base(context)
        {
            _personaContactoRepository = personaContactoRepository;
            _context = context;
        }

        public async Task<PersonaContacto> obtenerPorPersonaId(long personaId){
            return await _context.PersonaContacto.Where(p=>p.PersonaId == personaId).OrderBy(P=>P.Id).LastOrDefaultAsync();
        }
    }
}