using Dominio.Models.AtencionesGrupales;
using Dominio.Models.AtencionesWeb;
using Microsoft.EntityFrameworkCore;
using Persistencia.Context;


namespace Persistencia.Repository
{
    public class PersonaWebRepository : GenericRepository<PersonaWeb>
    {
        public IGenericRepository<AtencionWeb> _atencionWebRepository { get; }
        public OrientacionDbContext _context;

        public PersonaWebRepository(OrientacionDbContext context, IGenericRepository<AtencionWeb> atencionWebRepository) : base(context)
        {
            this._atencionWebRepository = atencionWebRepository;
            this._context = context;
        }

        public PersonaWeb obtenerporCorreo(string correo)
        {
            return _context.PersonaWeb.Where(p => p.VcCorreo == correo.ToLower()).FirstOrDefault();
        }
    }
}
    

