using Dominio.Models.AtencionesGrupales;
using Dominio.Models.AtencionesWeb;
using Microsoft.EntityFrameworkCore;
using Persistencia.Repository;

namespace Aplicacion.Services
{
    public class PersonaWebService : GenericService<PersonaWeb>
    {

        public PersonaWebRepository _personaWebRepository { get; }
        public IGenericRepository<PersonaWeb> _genericRepository { get; }
        public PersonaWebService(IGenericRepository<PersonaWeb> genericRepository, PersonaWebRepository personaWebRepository): base(genericRepository) 
        {
            this._genericRepository = genericRepository;
            this._personaWebRepository = personaWebRepository;
        }

        public async Task<IEnumerable<PersonaWeb>> obtenerPorRangoFechasYUsuario(DateTime DtFechaInicio, DateTime DtFechaFin, long usuarioId,string correo)
        {
            return await _personaWebRepository.obtenerPorRangoFechasYUsuario(DtFechaInicio, DtFechaFin, usuarioId,correo);
        }


        public PersonaWeb obtenerporCorreo(string correo)
        {
            return _personaWebRepository.obtenerporCorreo(correo);
        }

    }
}
