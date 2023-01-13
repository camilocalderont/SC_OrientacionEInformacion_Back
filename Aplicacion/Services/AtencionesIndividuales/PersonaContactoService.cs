
using Dominio.Models.AtencionesIndividuales;
using Persistencia.Repository;


namespace Aplicacion.Services.AtencionesIndividuales
{
    public class PersonaContactoService : GenericService<PersonaContacto>
    {

        public IGenericRepository<PersonaContacto> _genericRepository { get; }
        public PersonaContactoRepository _personaContactoRepository { get; }

        public PersonaContactoService(
            IGenericRepository<PersonaContacto> genericRepository,
            PersonaContactoRepository personaContactoRepository ) : base(genericRepository)
        {
            _genericRepository = genericRepository;
            _personaContactoRepository = personaContactoRepository;
        }

        public async Task<bool> CambioModelo(PersonaContacto personaContacto, long personaId)
        {
            bool cambio = true;
            var PersonaContactoBD = await _personaContactoRepository.obtenerPorPersonaId(personaId);
            if(PersonaContactoBD != null ) {
               cambio = !(
                        personaContacto.PaisId == PersonaContactoBD.PaisId &&
                        personaContacto.DepartamentoId == PersonaContactoBD.DepartamentoId &&
                        personaContacto.MunicipioId == PersonaContactoBD.MunicipioId &&
                        personaContacto.LocalidadId == PersonaContactoBD.LocalidadId &&
                        personaContacto.UpzId == PersonaContactoBD.UpzId &&
                        personaContacto.BarrioId == PersonaContactoBD.BarrioId &&
                        personaContacto.VcDireccion == PersonaContactoBD.VcDireccion &&
                        personaContacto.VcTelefono1 == PersonaContactoBD.VcTelefono1 &&
                        personaContacto.VcTelefono2 == PersonaContactoBD.VcTelefono2
              );
            }

            return cambio;
        }
        
    }
}
