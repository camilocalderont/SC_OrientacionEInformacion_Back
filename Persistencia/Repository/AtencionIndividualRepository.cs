using Dominio.Models.AtencionesIndividuales;
using Dominio.Mapper.AtencionesIndividuales;
using Microsoft.EntityFrameworkCore;
using Persistencia.Context;
using System.Reflection.Metadata;
using System.Linq;
using Microsoft.EntityFrameworkCore.Query;
using Microsoft.Build.ObjectModelRemoting;
using Dominio.Utilities;

namespace Persistencia.Repository
{
    public class AtencionIndividualRepository : GenericRepository<AtencionIndividual>
    {

        public IGenericRepository<AtencionIndividualAnexo> _atencionIndividualAnexoRepository { get; }
        public OrientacionDbContext _context;

        public AtencionIndividualRepository(OrientacionDbContext context, IGenericRepository<AtencionIndividualAnexo> atencionIndividualAnexoRepository) : base(context)
        {
            this._atencionIndividualAnexoRepository = atencionIndividualAnexoRepository;
            this._context = context;
        }


        public async Task<IEnumerable<BandejaIndividualDTO>> obtenerPorRangoFechasEstadoUsuarioYDocumento(
            long EstadoId, 
            DateTime DtFechaInicio, 
            DateTime DtFechaFin, 
            long usuarioId, 
            string VcDocumento
        )
        {
            IQueryable<AtencionIndividual> atencionesIndividualesQuery = _context.AtencionIndividual.AsQueryable();

            atencionesIndividualesQuery = atencionesIndividualesQuery.Where(p => p.DtFechaRegistro >= DtFechaInicio &&
                                            p.DtFechaRegistro <= DtFechaFin);
            if (usuarioId > 0)
            {
                atencionesIndividualesQuery = atencionesIndividualesQuery.Where(p => p.UsuarioId == usuarioId);
            }

            if (EstadoId > 0)
            {
                atencionesIndividualesQuery = atencionesIndividualesQuery.Where(p => p.EstadoId == EstadoId);
            }

            if (VcDocumento.Length > 0)
            {
                var persona = _context.Persona.Where(p=>p.VcDocumento== VcDocumento).FirstOrDefault();
                
                if (persona != null)
                {
                    _context.Entry(persona).State = EntityState.Detached;
                    atencionesIndividualesQuery = atencionesIndividualesQuery.Where(g => g.PersonaId == persona.Id);
                }

            }



            var atencionesIndividuales = await atencionesIndividualesQuery.Select(a=>new BandejaIndividualDTO
            {
                Id = a.Id,
                DtFechaRegistro = a.DtFechaRegistro,
                CanalAtencionId = a.CanalAtencionId,
                EstadoId = a.EstadoId,
                IAnexos = a.AtencionAnexos.Count,
                MotivoId = a.MotivoId,
                SubMotivoId = a.SubMotivoId,
                PersonaId = a.PersonaId,
                TipoSolicitudId = a.TipoSolicitudId,
                TxAclaracionMotivo = a.TxAclaracionMotivo,
                TxGestionRealizada = a.TxGestionRealizada,
                UsuarioId = a.UsuarioId,
                UsuarioActualId = a.AtencionReasignaciones.Any() ? a.AtencionReasignaciones.OrderBy(a => a.Id).Last().UsuarioActualId : a.UsuarioId,
                VcTurnoSat = a.VcTurnoSat,
                TipoDocumentoId = a.Persona.TipoDocumentoId,
                VcDocumento =   a.Persona.VcDocumento,
                VcNombrecompleto = $"{a.Persona.VcPrimerNombre ?? string.Empty} {a.Persona.VcSegundoNombre ?? string.Empty} {a.Persona.VcPrimerApellido ?? string.Empty} {a.Persona.VcSegundoApellido ?? string.Empty}",
                GeneroId = a.Persona.GeneroId,
                VcOtroGenero = a.Persona.VcOtroGenero,
                VcNombreIdentitario = a.Persona.VcNombreIdentitario,
                OrientacionSexualId = a.Persona.OrientacionSexualId,
                VcOtraOrientacionSexual = a.Persona.VcOtraOrientacionSexual,
                SexoId = a.Persona.SexoId,
                DtFechaNacimiento = a.Persona.DtFechaNacimiento,
                EnfoquePoblacionalId = a.Persona.EnfoquePoblacionalId,
                HechoVictimizanteId = a.Persona.HechoVictimizanteId,
                DepartamentoOrigenVictimaId = a.Persona.DepartamentoOrigenVictimaId,
                MunicipioOrigenVictimaId = a.Persona.MunicipioOrigenVictimaId,
                EtniaId = a.Persona.EtniaId,
                SubEtniaId = a.Persona.SubEtniaId,
                PoblacionPrioritariaId = a.Persona.PoblacionPrioritariaId,
                SubPoblacionPrioritariaId = a.Persona.SubPoblacionPrioritariaId,
                VcCorreo = a.Persona.VcCorreo,
                DtFechaActualizacion = a.Persona.DtFechaActualizacion,
                UsuarioActualizacionId = a.Persona.UsuarioActualizacionId,

                RegimenId = a.Persona.PersonaAfiliaciones.Any() ? a.Persona.PersonaAfiliaciones.OrderBy(a => a.Id).Last().RegimenId : 0,
                AseguradoraId = a.Persona.PersonaAfiliaciones.Any() ? a.Persona.PersonaAfiliaciones.OrderBy(a => a.Id).Last().AseguradoraId : 0,
                EstadoAfiliacionId = a.Persona.PersonaAfiliaciones.Any() ? a.Persona.PersonaAfiliaciones.OrderBy(a => a.Id).Last().EstadoAfiliacionId : 0,
                NivelSisbenId = a.Persona.PersonaAfiliaciones.Any() ? a.Persona.PersonaAfiliaciones.OrderBy(a => a.Id).Last().NivelSisbenId : 0,
                InstitucionInstrumentoVinculadoId = a.Persona.PersonaAfiliaciones.Any() ? a.Persona.PersonaAfiliaciones.OrderBy(a => a.Id).Last().InstitucionInstrumentoVinculadoId : 0,
                
                PaisId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().PaisId : 0,
                DepartamentoId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().DepartamentoId : 0,
                MunicipioId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().MunicipioId : 0,
                LocalidadId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().LocalidadId : 0,
                UpzId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().UpzId : 0,
                BarrioId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().BarrioId : 0,
                ZonaId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().ZonaId : 0,
                VcDireccion = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().VcDireccion : "",
                TxDatosContactoAclaraciones = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().TxDatosContactoAclaraciones : "",
                VcTelefono1 = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().VcTelefono1 : "",
                VcTelefono2 = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().VcTelefono2 : "",                

            }).ToListAsync();

            return atencionesIndividuales;


        }

        public async Task<IEnumerable<AtencionIndividualReporteDto>> obtenerPorRangoFechas(DateTime fechaInicio, DateTime fechaFinal)
        {
            IQueryable<AtencionIndividual> query = _context.AtencionIndividual.AsQueryable();

            query = query
                .Where(p => (DateTime.Compare(p.DtFechaRegistro, fechaInicio) >= 0) && (DateTime.Compare(p.DtFechaRegistro, fechaFinal) <= 0));

            query = query
                .Include(atencionIndividual => atencionIndividual.Persona)
                .ThenInclude(persona => persona.PersonaContactos.OrderByDescending(personaCont => personaCont.DtFechaRegistro))
                .Include(atencionIndividual => atencionIndividual.Persona)
                .ThenInclude(persona => persona.PersonaAfiliaciones.OrderByDescending(personaAfi => personaAfi.DtFechaRegistro));


            var atencionesIndividuales = await query.
                Select(item =>
                new AtencionIndividualReporteDto
                {
                    consecutivo = item.PersonaId,
                    cas_Id = item.Id,
                    mes = item.DtFechaRegistro.Month,
                    fechaAlmacenamiento = item.DtFechaRegistro,
                    tipoDocumentoId= item.Persona.TipoDocumentoId,
                    numeroIdent = item.Persona.VcDocumento,
                    primerApellido = item.Persona.VcPrimerApellido,
                    segundoApellido = item.Persona.VcSegundoApellido == null ? "" : item.Persona.VcSegundoApellido,
                    primerNombre = item.Persona.VcPrimerNombre,
                    segundoNombre = item.Persona.VcSegundoNombre == null ? "" : item.Persona.VcSegundoNombre,
                    edadAlRegistroDelCaso = OperacionesFechas.restar(item.DtFechaRegistro, item.Persona.DtFechaNacimiento).ToString(),
                    fechaNacimiento = item.Persona.DtFechaNacimiento == null ? "" : item.Persona.DtFechaNacimiento.Value.ToString("yyyy-MM-dd"),
                    sexoId = item.Persona.SexoId,
                    generoId = item.Persona.GeneroId,
                    orientacionSexualId = item.Persona.OrientacionSexualId,
                    enfoquePoblacionalId = item.Persona.EnfoquePoblacionalId,
                    poblacionPrioritariaId = item.Persona.PoblacionPrioritariaId,
                    subPoblacionPrioritariaId = item.Persona.SubPoblacionPrioritariaId == null ? -1 : item.Persona.SubPoblacionPrioritariaId.Value,
                    subEtniaId = item.Persona.SubEtniaId == null ? -1 : item.Persona.SubEtniaId.Value,
                    etniaId = item.Persona.EtniaId,
                    regimenId = item.Persona.PersonaAfiliaciones.First().RegimenId,
                    aseguradoraId = item.Persona.PersonaAfiliaciones.First().AseguradoraId,
                    nivelSisbenId = item.Persona.PersonaAfiliaciones.First().NivelSisbenId,
                    departamentoId = item.Persona.PersonaContactos.First().DepartamentoId,
                    municipioId = item.Persona.PersonaContactos.First().MunicipioId,
                    direccion = item.Persona.PersonaContactos.First().VcDireccion,
                    localidadId = item.Persona.PersonaContactos.First().LocalidadId,
                    barrioId = item.Persona.PersonaContactos.First().BarrioId == null ? -1 : item.Persona.PersonaContactos.First().BarrioId.Value,
                    upzId = item.Persona.PersonaContactos.First().UpzId == null ? -1 : item.Persona.PersonaContactos.First().UpzId.Value,
                    correoElectronico = item.Persona.VcCorreo == null ? "" : item.Persona.VcCorreo,
                    telefono1 = item.Persona.PersonaContactos.First().VcTelefono1 == null ? "" : item.Persona.PersonaContactos.First().VcTelefono1,
                    Telefono2 = item.Persona.PersonaContactos.First().VcTelefono2 == null ? "" : item.Persona.PersonaContactos.First().VcTelefono2,
                    aclaracionesTelfDir = item.Persona.PersonaContactos.First().TxDatosContactoAclaraciones,
                    tipoOrientacion = item.TipoSolicitudId,
                    motivoId = item.MotivoId,
                    subMotivoId = item.SubMotivoId,
                    aclaracionMotivoOrientacion = item.TxAclaracionMotivo,
                    gestionResolucionPAcceso = item.TxGestionRealizada == null ? "" : item.TxGestionRealizada,
                    datosDeContactoAclaración = item.Persona.PersonaContactos.First().TxDatosContactoAclaraciones,
                    canalAtencionId = item.CanalAtencionId,
                    estadoCasoId = item.EstadoId,
                    usuarioId = item.Persona.UsuarioId
                }
            ).ToListAsync<AtencionIndividualReporteDto>();


            return atencionesIndividuales;
        }

        public async Task<IEnumerable<BandejaIndividualDTO>> obtenerPorPersonaYExcluyeCaso(long PersonaId,long AtencionIndividualId)
        {
            IQueryable<AtencionIndividual> atencionesIndividualesQuery = _context.AtencionIndividual.AsQueryable();

            atencionesIndividualesQuery = atencionesIndividualesQuery.Where(p => p.PersonaId == PersonaId && p.Id != AtencionIndividualId);
           



            var atencionesIndividuales = await atencionesIndividualesQuery.Select(a => new BandejaIndividualDTO
            {
                Id = a.Id,
                DtFechaRegistro = a.DtFechaRegistro,
                CanalAtencionId = a.CanalAtencionId,
                EstadoId = a.EstadoId,
                IAnexos = a.AtencionAnexos.Count,
                MotivoId = a.MotivoId,
                SubMotivoId = a.SubMotivoId,
                PersonaId = a.PersonaId,
                TipoSolicitudId = a.TipoSolicitudId,
                TxAclaracionMotivo = a.TxAclaracionMotivo,
                TxGestionRealizada = a.TxGestionRealizada,
                UsuarioId = a.UsuarioId,
                UsuarioActualId = a.AtencionReasignaciones.Any() ? a.AtencionReasignaciones.OrderBy(a => a.Id).Last().UsuarioActualId : a.UsuarioId,
                VcTurnoSat = a.VcTurnoSat,
                TipoDocumentoId = a.Persona.TipoDocumentoId,
                VcDocumento =   a.Persona.VcDocumento,
                VcNombrecompleto = $"{a.Persona.VcPrimerNombre ?? string.Empty} {a.Persona.VcSegundoNombre ?? string.Empty} {a.Persona.VcPrimerApellido ?? string.Empty} {a.Persona.VcSegundoApellido ?? string.Empty}",
                GeneroId = a.Persona.GeneroId,
                VcOtroGenero = a.Persona.VcOtroGenero,
                VcNombreIdentitario = a.Persona.VcNombreIdentitario,
                OrientacionSexualId = a.Persona.OrientacionSexualId,
                VcOtraOrientacionSexual = a.Persona.VcOtraOrientacionSexual,
                SexoId = a.Persona.SexoId,
                DtFechaNacimiento = a.Persona.DtFechaNacimiento,
                EnfoquePoblacionalId = a.Persona.EnfoquePoblacionalId,
                HechoVictimizanteId = a.Persona.HechoVictimizanteId,
                DepartamentoOrigenVictimaId = a.Persona.DepartamentoOrigenVictimaId,
                MunicipioOrigenVictimaId = a.Persona.MunicipioOrigenVictimaId,
                EtniaId = a.Persona.EtniaId,
                SubEtniaId = a.Persona.SubEtniaId,
                PoblacionPrioritariaId = a.Persona.PoblacionPrioritariaId,
                SubPoblacionPrioritariaId = a.Persona.SubPoblacionPrioritariaId,
                VcCorreo = a.Persona.VcCorreo,
                DtFechaActualizacion = a.Persona.DtFechaActualizacion,
                UsuarioActualizacionId = a.Persona.UsuarioActualizacionId,

                RegimenId = a.Persona.PersonaAfiliaciones.Any() ? a.Persona.PersonaAfiliaciones.OrderBy(a => a.Id).Last().RegimenId : 0,
                AseguradoraId = a.Persona.PersonaAfiliaciones.Any() ? a.Persona.PersonaAfiliaciones.OrderBy(a => a.Id).Last().AseguradoraId : 0,
                EstadoAfiliacionId = a.Persona.PersonaAfiliaciones.Any() ? a.Persona.PersonaAfiliaciones.OrderBy(a => a.Id).Last().EstadoAfiliacionId : 0,
                NivelSisbenId = a.Persona.PersonaAfiliaciones.Any() ? a.Persona.PersonaAfiliaciones.OrderBy(a => a.Id).Last().NivelSisbenId : 0,
                InstitucionInstrumentoVinculadoId = a.Persona.PersonaAfiliaciones.Any() ? a.Persona.PersonaAfiliaciones.OrderBy(a => a.Id).Last().InstitucionInstrumentoVinculadoId : 0,
                
                PaisId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().PaisId : 0,
                DepartamentoId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().DepartamentoId : 0,
                MunicipioId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().MunicipioId : 0,
                LocalidadId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().LocalidadId : 0,
                UpzId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().UpzId : 0,
                BarrioId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().BarrioId : 0,
                ZonaId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().ZonaId : 0,
                VcDireccion = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().VcDireccion : "",
                TxDatosContactoAclaraciones = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().TxDatosContactoAclaraciones : "",
                VcTelefono1 = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().VcTelefono1 : "",
                VcTelefono2 = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().VcTelefono2 : "",
            
            }).ToListAsync();

            return atencionesIndividuales;


        }


        public async Task<BandejaIndividualDTO> obtenerPorId(long atencionIndividualId)
        {
            var atencionIndividualDto = _context.AtencionIndividual.Where(p => p.Id == atencionIndividualId)
                .Select(a => new BandejaIndividualDTO
                {
                    Id = a.Id,
                    DtFechaRegistro = a.DtFechaRegistro,
                    CanalAtencionId = a.CanalAtencionId,
                    EstadoId = a.EstadoId,
                    IAnexos = a.AtencionAnexos.Count,
                    MotivoId = a.MotivoId,
                    SubMotivoId = a.SubMotivoId,
                    PersonaId = a.PersonaId,
                    TipoSolicitudId = a.TipoSolicitudId,
                    TxAclaracionMotivo = a.TxAclaracionMotivo,
                    TxGestionRealizada = a.TxGestionRealizada,
                    UsuarioId = a.UsuarioId,
                    UsuarioActualId = a.AtencionReasignaciones.Any() ? a.AtencionReasignaciones.OrderBy(a => a.Id).Last().UsuarioActualId : a.UsuarioId,
                    VcTurnoSat = a.VcTurnoSat,
                    TipoDocumentoId = a.Persona.TipoDocumentoId,
                    VcDocumento =   a.Persona.VcDocumento,
                    VcNombrecompleto = $"{a.Persona.VcPrimerNombre ?? string.Empty} {a.Persona.VcSegundoNombre ?? string.Empty} {a.Persona.VcPrimerApellido ?? string.Empty} {a.Persona.VcSegundoApellido ?? string.Empty}",
                    GeneroId = a.Persona.GeneroId,
                    VcOtroGenero = a.Persona.VcOtroGenero,
                    VcNombreIdentitario = a.Persona.VcNombreIdentitario,
                    OrientacionSexualId = a.Persona.OrientacionSexualId,
                    VcOtraOrientacionSexual = a.Persona.VcOtraOrientacionSexual,
                    SexoId = a.Persona.SexoId,
                    DtFechaNacimiento = a.Persona.DtFechaNacimiento,
                    EnfoquePoblacionalId = a.Persona.EnfoquePoblacionalId,
                    HechoVictimizanteId = a.Persona.HechoVictimizanteId,
                    DepartamentoOrigenVictimaId = a.Persona.DepartamentoOrigenVictimaId,
                    MunicipioOrigenVictimaId = a.Persona.MunicipioOrigenVictimaId,
                    EtniaId = a.Persona.EtniaId,
                    SubEtniaId = a.Persona.SubEtniaId,
                    PoblacionPrioritariaId = a.Persona.PoblacionPrioritariaId,
                    SubPoblacionPrioritariaId = a.Persona.SubPoblacionPrioritariaId,
                    VcCorreo = a.Persona.VcCorreo,
                    DtFechaActualizacion = a.Persona.DtFechaActualizacion,
                    UsuarioActualizacionId = a.Persona.UsuarioActualizacionId,

                    RegimenId = a.Persona.PersonaAfiliaciones.Any() ? a.Persona.PersonaAfiliaciones.OrderBy(a => a.Id).Last().RegimenId : 0,
                    AseguradoraId = a.Persona.PersonaAfiliaciones.Any() ? a.Persona.PersonaAfiliaciones.OrderBy(a => a.Id).Last().AseguradoraId : 0,
                    EstadoAfiliacionId = a.Persona.PersonaAfiliaciones.Any() ? a.Persona.PersonaAfiliaciones.OrderBy(a => a.Id).Last().EstadoAfiliacionId : 0,
                    NivelSisbenId = a.Persona.PersonaAfiliaciones.Any() ? a.Persona.PersonaAfiliaciones.OrderBy(a => a.Id).Last().NivelSisbenId : 0,
                    InstitucionInstrumentoVinculadoId = a.Persona.PersonaAfiliaciones.Any() ? a.Persona.PersonaAfiliaciones.OrderBy(a => a.Id).Last().InstitucionInstrumentoVinculadoId : 0,
                    
                    PaisId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().PaisId : 0,
                    DepartamentoId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().DepartamentoId : 0,
                    MunicipioId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().MunicipioId : 0,
                    LocalidadId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().LocalidadId : 0,
                    UpzId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().UpzId : 0,
                    BarrioId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().BarrioId : 0,
                    ZonaId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().ZonaId : 0,
                    VcDireccion = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().VcDireccion : "",
                    TxDatosContactoAclaraciones = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().TxDatosContactoAclaraciones : "",
                    VcTelefono1 = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().VcTelefono1 : "",
                    VcTelefono2 = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().VcTelefono2 : "",
                    
                    Actores = a.AtencionActores.Select( a=> new AtencionIndividualActorDTO{
                            AtencionIndividualId = a.AtencionIndividualId,
                            DtFechaRegistro = a.DtFechaRegistro,
                            SedeId = a.SedeId,
                            TipoActorId = a.TipoActorId,
                            TipoId = a.TipoId,
                            UsuarioId = a.UsuarioId,
                        }).ToList(),
                    
                    Seguimientos = a.AtencionSeguimientos.Select(a=> new AtencionIndividualSeguimientoDTO{
                        AtencionIndividualId = a.AtencionIndividualId,
                        BCierraCaso = a.BCierraCaso,
                        DtFechaRegistro = a.DtFechaRegistro,
                        UsuarioId = a.UsuarioId,
                        VcDescripcion = a.VcDescripcion,                        
                    }).ToList(),
                    Reasignaciones = a.AtencionReasignaciones.Select(a => new AtencionIndividualReasignacionDTO{
                        AtencionIndividualId = a.AtencionIndividualId,
                        DtFechaAsignacion = a.DtFechaAsignacion,
                        DtFechaReAsignacion = a.DtFechaAsignacion,
                        UsuarioActualId = a.UsuarioActualId,
                        UsuarioAsignaId = a.UsuarioAsignaId,
                        VcDescripcion = a.VcDescripcion
                    }).ToList()

                }).FirstOrDefault();
            return atencionIndividualDto;
        }


        public async Task<IEnumerable<BandejaIndividualDTO>> obtenerPorTipoDocumentoDocumentoYEstado(
            long tipoDocumentoId, 
            string VcDocumento,
            long EstadoId
        )
        {
            IQueryable<AtencionIndividual> atencionesIndividualesQuery = _context.AtencionIndividual.AsQueryable();

            atencionesIndividualesQuery = atencionesIndividualesQuery.Where(p => p.EstadoId == EstadoId);

            if (VcDocumento.Length > 0 && tipoDocumentoId>0)
            {
                var persona = _context.Persona.Where(p=>p.VcDocumento== VcDocumento && p.TipoDocumentoId == tipoDocumentoId).FirstOrDefault();               
                if (persona != null)
                {
                    _context.Entry(persona).State = EntityState.Detached;
                    atencionesIndividualesQuery = atencionesIndividualesQuery.Where(g => g.PersonaId == persona.Id);
                }

            }

            var atencionesIndividuales = await atencionesIndividualesQuery.Select(a=>new BandejaIndividualDTO
            {
                Id = a.Id,
                DtFechaRegistro = a.DtFechaRegistro,
                CanalAtencionId = a.CanalAtencionId,
                EstadoId = a.EstadoId,
                IAnexos = a.AtencionAnexos.Count,
                MotivoId = a.MotivoId,
                SubMotivoId = a.SubMotivoId,
                PersonaId = a.PersonaId,
                TipoSolicitudId = a.TipoSolicitudId,
                TxAclaracionMotivo = a.TxAclaracionMotivo,
                TxGestionRealizada = a.TxGestionRealizada,
                UsuarioId = a.UsuarioId,
                UsuarioActualId = a.AtencionReasignaciones.Any() ? a.AtencionReasignaciones.OrderBy(a => a.Id).Last().UsuarioActualId : a.UsuarioId,
                VcTurnoSat = a.VcTurnoSat,
                TipoDocumentoId = a.Persona.TipoDocumentoId,
                VcDocumento =   a.Persona.VcDocumento,
                VcNombrecompleto = $"{a.Persona.VcPrimerNombre ?? string.Empty} {a.Persona.VcSegundoNombre ?? string.Empty} {a.Persona.VcPrimerApellido ?? string.Empty} {a.Persona.VcSegundoApellido ?? string.Empty}",
                GeneroId = a.Persona.GeneroId,
                VcOtroGenero = a.Persona.VcOtroGenero,
                VcNombreIdentitario = a.Persona.VcNombreIdentitario,
                OrientacionSexualId = a.Persona.OrientacionSexualId,
                VcOtraOrientacionSexual = a.Persona.VcOtraOrientacionSexual,
                SexoId = a.Persona.SexoId,
                DtFechaNacimiento = a.Persona.DtFechaNacimiento,
                EnfoquePoblacionalId = a.Persona.EnfoquePoblacionalId,
                HechoVictimizanteId = a.Persona.HechoVictimizanteId,
                DepartamentoOrigenVictimaId = a.Persona.DepartamentoOrigenVictimaId,
                MunicipioOrigenVictimaId = a.Persona.MunicipioOrigenVictimaId,
                EtniaId = a.Persona.EtniaId,
                SubEtniaId = a.Persona.SubEtniaId,
                PoblacionPrioritariaId = a.Persona.PoblacionPrioritariaId,
                SubPoblacionPrioritariaId = a.Persona.SubPoblacionPrioritariaId,
                VcCorreo = a.Persona.VcCorreo,
                DtFechaActualizacion = a.Persona.DtFechaActualizacion,
                UsuarioActualizacionId = a.Persona.UsuarioActualizacionId,

                RegimenId = a.Persona.PersonaAfiliaciones.Any() ? a.Persona.PersonaAfiliaciones.OrderBy(a => a.Id).Last().RegimenId : 0,
                AseguradoraId = a.Persona.PersonaAfiliaciones.Any() ? a.Persona.PersonaAfiliaciones.OrderBy(a => a.Id).Last().AseguradoraId : 0,
                EstadoAfiliacionId = a.Persona.PersonaAfiliaciones.Any() ? a.Persona.PersonaAfiliaciones.OrderBy(a => a.Id).Last().EstadoAfiliacionId : 0,
                NivelSisbenId = a.Persona.PersonaAfiliaciones.Any() ? a.Persona.PersonaAfiliaciones.OrderBy(a => a.Id).Last().NivelSisbenId : 0,
                InstitucionInstrumentoVinculadoId = a.Persona.PersonaAfiliaciones.Any() ? a.Persona.PersonaAfiliaciones.OrderBy(a => a.Id).Last().InstitucionInstrumentoVinculadoId : 0,
                
                PaisId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().PaisId : 0,
                DepartamentoId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().DepartamentoId : 0,
                MunicipioId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().MunicipioId : 0,
                LocalidadId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().LocalidadId : 0,
                UpzId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().UpzId : 0,
                BarrioId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().BarrioId : 0,
                ZonaId = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().ZonaId : 0,
                VcDireccion = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().VcDireccion : "",
                TxDatosContactoAclaraciones = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().TxDatosContactoAclaraciones : "",
                VcTelefono1 = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().VcTelefono1 : "",
                VcTelefono2 = a.Persona.PersonaContactos.Any() ? a.Persona.PersonaContactos.OrderBy(a => a.Id).Last().VcTelefono2 : "",               

            }).ToListAsync();

            return atencionesIndividuales;
        }




    }


}