using Dominio.Mapper.AtencionesIndividuales;
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

        public async Task<PersonaDTO> obtenerPorTipoDocumentoyDocumento(long tipoDocumentoId, string vcDocumento)
        {
            
            var persona = await _context.Persona.Where(p => p.TipoDocumentoId == tipoDocumentoId && p.VcDocumento == vcDocumento)
            .Select(a => new PersonaDTO
            {
                Id = a.Id,
                TipoDocumentoId = a.TipoDocumentoId,
                VcDocumento = a.VcDocumento,
                VcPrimerNombre = a.VcPrimerNombre,
                VcSegundoNombre = a.VcSegundoNombre,
                VcPrimerApellido = a.VcPrimerApellido,
                VcSegundoApellido = a.VcSegundoApellido,
                GeneroId = a.GeneroId,
                VcOtroGenero = a.VcOtroGenero,
                VcNombreIdentitario = a.VcNombreIdentitario,
                OrientacionSexualId = a.OrientacionSexualId,
                VcOtraOrientacionSexual = a.VcOtraOrientacionSexual,
                SexoId = a.SexoId,
                DtFechaNacimiento = a.DtFechaNacimiento,
                EnfoquePoblacionalId = a.EnfoquePoblacionalId,
                HechoVictimizanteId = a.HechoVictimizanteId,
                DepartamentoOrigenVictimaId = a.DepartamentoOrigenVictimaId,
                MunicipioOrigenVictimaId = a.MunicipioOrigenVictimaId,
                EtniaId = a.EtniaId,
                SubEtniaId = a.SubEtniaId,
                PoblacionPrioritariaId = a.PoblacionPrioritariaId,
                SubPoblacionPrioritariaId = a.SubPoblacionPrioritariaId,
                VcCorreo = a.VcCorreo,
                DtFechaRegistro = a.DtFechaRegistro,
                UsuarioId = a.UsuarioId,
                DtFechaActualizacion = a.DtFechaActualizacion,
                UsuarioActualizacionId = a.UsuarioActualizacionId,

                RegimenId = a.PersonaAfiliaciones.Any() ? a.PersonaAfiliaciones.OrderBy(a => a.Id).Last().RegimenId : 0,
                AseguradoraId = a.PersonaAfiliaciones.Any() ? a.PersonaAfiliaciones.OrderBy(a => a.Id).Last().AseguradoraId : 0,
                EstadoAfiliacionId = a.PersonaAfiliaciones.Any() ? a.PersonaAfiliaciones.OrderBy(a => a.Id).Last().EstadoAfiliacionId : 0,
                NivelSisbenId = a.PersonaAfiliaciones.Any() ? a.PersonaAfiliaciones.OrderBy(a => a.Id).Last().NivelSisbenId : 0,
                InstitucionInstrumentoVinculadoId = a.PersonaAfiliaciones.Any() ? a.PersonaAfiliaciones.OrderBy(a => a.Id).Last().InstitucionInstrumentoVinculadoId : 0,
                
                PaisId = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().PaisId : 0,
                DepartamentoId = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().DepartamentoId : 0,
                MunicipioId = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().MunicipioId : 0,
                LocalidadId = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().LocalidadId : 0,
                UpzId = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().UpzId : 0,
                BarrioId = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().BarrioId : 0,
                ZonaId = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().ZonaId : 0,
                VcDireccion = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().VcDireccion : "",
                TxDatosContactoAclaraciones = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().TxDatosContactoAclaraciones : "",
                VcTelefono1 = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().VcTelefono1 : "",
                VcTelefono2 = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().VcTelefono2 : "",
                
            }).FirstOrDefaultAsync();


            return persona;
        }

        public async Task<PersonaDTO> obtenerPorId(long personaId)
        {

            var persona = await _context.Persona.Where(p => p.Id == personaId)
            .Select(a => new PersonaDTO
            {
                Id = a.Id,
                TipoDocumentoId = a.TipoDocumentoId,
                VcDocumento = a.VcDocumento,
                VcPrimerNombre = a.VcPrimerNombre,
                VcSegundoNombre = a.VcSegundoNombre,
                VcPrimerApellido = a.VcPrimerApellido,
                VcSegundoApellido = a.VcSegundoApellido,
                GeneroId = a.GeneroId,
                VcOtroGenero = a.VcOtroGenero,
                VcNombreIdentitario = a.VcNombreIdentitario,
                OrientacionSexualId = a.OrientacionSexualId,
                VcOtraOrientacionSexual = a.VcOtraOrientacionSexual,
                SexoId = a.SexoId,
                DtFechaNacimiento = a.DtFechaNacimiento,
                EnfoquePoblacionalId = a.EnfoquePoblacionalId,
                HechoVictimizanteId = a.HechoVictimizanteId,
                DepartamentoOrigenVictimaId = a.DepartamentoOrigenVictimaId,
                MunicipioOrigenVictimaId = a.MunicipioOrigenVictimaId,
                EtniaId = a.EtniaId,
                SubEtniaId = a.SubEtniaId,
                PoblacionPrioritariaId = a.PoblacionPrioritariaId,
                SubPoblacionPrioritariaId = a.SubPoblacionPrioritariaId,
                VcCorreo = a.VcCorreo,
                DtFechaRegistro = a.DtFechaRegistro,
                UsuarioId = a.UsuarioId,
                DtFechaActualizacion = a.DtFechaActualizacion,
                UsuarioActualizacionId = a.UsuarioActualizacionId,

                RegimenId = a.PersonaAfiliaciones.Any() ? a.PersonaAfiliaciones.OrderBy(a => a.Id).Last().RegimenId : 0,
                AseguradoraId = a.PersonaAfiliaciones.Any() ? a.PersonaAfiliaciones.OrderBy(a => a.Id).Last().AseguradoraId : 0,
                EstadoAfiliacionId = a.PersonaAfiliaciones.Any() ? a.PersonaAfiliaciones.OrderBy(a => a.Id).Last().EstadoAfiliacionId : 0,
                NivelSisbenId = a.PersonaAfiliaciones.Any() ? a.PersonaAfiliaciones.OrderBy(a => a.Id).Last().NivelSisbenId : 0,
                InstitucionInstrumentoVinculadoId = a.PersonaAfiliaciones.Any() ? a.PersonaAfiliaciones.OrderBy(a => a.Id).Last().InstitucionInstrumentoVinculadoId : 0,

                PaisId = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().PaisId : 0,
                DepartamentoId = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().DepartamentoId : 0,
                MunicipioId = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().MunicipioId : 0,
                LocalidadId = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().LocalidadId : 0,
                UpzId = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().UpzId : 0,
                BarrioId = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().BarrioId : 0,
                ZonaId = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().ZonaId : 0,
                VcDireccion = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().VcDireccion : "",
                TxDatosContactoAclaraciones = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().TxDatosContactoAclaraciones : "",
                VcTelefono1 = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().VcTelefono1 : "",
                VcTelefono2 = a.PersonaContactos.Any() ? a.PersonaContactos.OrderBy(a => a.Id).Last().VcTelefono2 : "",

            }).FirstOrDefaultAsync();


            return persona;
        }
    }
}