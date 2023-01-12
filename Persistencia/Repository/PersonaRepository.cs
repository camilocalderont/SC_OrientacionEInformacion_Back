using Dominio.Models.AtencionesIndividuales;
using Microsoft.EntityFrameworkCore;
using Persistencia.Context;


namespace Persistencia.Repository
{
    public class PersonaRepository : GenericRepository<Persona>
    {
        public IGenericRepository<Persona> _personaRepository { get; }
        public OrientacionDbContext _context;

        public PersonaRepository(IGenericRepository<Persona> personaRepository, OrientacionDbContext context):base(context)
        {
            _personaRepository = personaRepository;
            _context = context;
        }

        public async Task<Persona> obtenerPorTipoDocumentoyDocumento(long tipoDocumentoId,string vcDocumento){
            return await _context.Persona.Where(p=>p.TipoDocumentoId == tipoDocumentoId && p.VcDocumento == vcDocumento).FirstOrDefaultAsync();
        }
    }
}