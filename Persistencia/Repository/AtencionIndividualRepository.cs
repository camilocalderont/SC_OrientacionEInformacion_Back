using Dominio.Models.AtencionesIndividuales;
using Dominio.Mapper.AtencionesIndividuales;
using Microsoft.EntityFrameworkCore;
using Persistencia.Context;
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

        public async Task<IEnumerable<AtencionIndividualReporteResponse>> obtenerPorRangoFechas(DateTime DtFechaInicio, DateTime DtFechaFin)
        {

            DateTime fechafinalContemplandoHorasMinutosSegundos = Convert.ToDateTime(DtFechaFin.ToString("yyyy-MM-dd") + " 23:59:59");

            IEnumerable<AtencionIndividual> atencionQuery = await (from at in _context.AtencionIndividual.Where(p => p.DtFechaRegistro >= DtFechaInicio && p.DtFechaRegistro <= fechafinalContemplandoHorasMinutosSegundos)
                                                                   join pe in _context.Persona on at.PersonaId equals pe.Id
                                                                   select new AtencionIndividual
                                                                   {
                                                                       Id = at.Id,
                                                                       DtFechaRegistro = at.DtFechaRegistro,
                                                                       EstadoId = at.EstadoId,
                                                                       MotivoId = at.MotivoId,
                                                                       SubMotivoId = at.SubMotivoId,
                                                                       TxAclaracionMotivo = at.TxAclaracionMotivo,
                                                                       TxGestionRealizada = at.TxGestionRealizada,
                                                                       AtencionReasignaciones = at.AtencionReasignaciones,
                                                                       AtencionSeguimientos = at.AtencionSeguimientos,
                                                                       UsuarioId = at.UsuarioId,
                                                                       PersonaId = at.PersonaId,
                                                                       Persona = pe
                                                                   }).ToListAsync();

            var atenciones = atencionQuery.Select(at => new AtencionIndividualReporteResponse
            {
                Id = at.Id,
                DtFechaRegistro = at.DtFechaRegistro,
                EstadoId = at.EstadoId,
                MotivoId = at.MotivoId,
                SubMotivoId = at.SubMotivoId,
                TxAclaracionMotivo = at.TxAclaracionMotivo,
                TxGestionRealizada = at.TxGestionRealizada,
                UsuarioId = at.UsuarioId,
                UsuarioActualId = at.AtencionReasignaciones.Any() ? at.AtencionReasignaciones.OrderBy(x => x.Id).LastOrDefault().UsuarioActualId : at.UsuarioId,
                PersonaId = at.PersonaId,
                TipoDocumentoId = at.Persona.TipoDocumentoId,
                EnfoquePoblacionalId = at.Persona.EnfoquePoblacionalId,
                PoblacionPrioritariaId = at.Persona.PoblacionPrioritariaId,
                SubPoblacionPrioritariaId = at.Persona.SubPoblacionPrioritariaId ?? 0,
                VcDocumento = at.Persona.VcDocumento ?? string.Empty,
                VcPrimerApellido = at.Persona.VcPrimerApellido ?? string.Empty,
                VcPrimerNombre = at.Persona.VcPrimerNombre ?? string.Empty,
                VcSegundoApellido = at.Persona.VcSegundoApellido ?? string.Empty,
                VcSegundoNombre = at.Persona.VcSegundoNombre ?? string.Empty,
                FechaCambioEstadoCaso = at.AtencionSeguimientos.Any() ? at.AtencionSeguimientos.OrderBy(x => x.Id).LastOrDefault().DtFechaRegistro.ToString("yyyy-MM-dd") : string.Empty,
                FechaUltimoSeguimientoCaso = at.AtencionSeguimientos.Any() ? at.AtencionSeguimientos.OrderBy(x => x.Id).LastOrDefault().DtFechaRegistro.ToString("yyyy-MM-dd") : string.Empty
            }).ToList();

            return atenciones;
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



            var atencionesIndividuales = atencionesIndividualesQuery.Select(a=>new BandejaIndividualDTO
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

            });

            if (usuarioId > 0)
            {
                atencionesIndividuales = atencionesIndividuales.Where(p => p.UsuarioActualId == usuarioId);
            }

            return await atencionesIndividuales.ToListAsync();


        }

        public async Task<IEnumerable<AtencionIndividualReporteDto>> obtenerPorRangoFechasParaReporteOYP(DateTime fechaInicio, DateTime fechaFinal)
        {
            IQueryable<AtencionIndividual> query = _context.AtencionIndividual.AsQueryable();

            DateTime fechafinalContemplandoHorasMinutosSegundos = new DateTime(fechaFinal.Year, fechaFinal.Month, fechaFinal.Day, 23, 59, 59);

            query = query
                .Where(p => (DateTime.Compare(p.DtFechaRegistro, fechaInicio) >= 0) && (DateTime.Compare(p.DtFechaRegistro, fechafinalContemplandoHorasMinutosSegundos) <= 0));

            query = query
                .Include(atencionIndividual => atencionIndividual.Persona)
                .ThenInclude(persona => persona.PersonaContactos.OrderByDescending(personaCont => personaCont.DtFechaRegistro))
                .Include(atencionIndividual => atencionIndividual.Persona)
                .ThenInclude(persona => persona.PersonaAfiliaciones.OrderByDescending(personaAfi => personaAfi.DtFechaRegistro));


            var atencionesIndividuales = await query.
                Select(item =>
                new AtencionIndividualReporteDto
                {
                    Consecutivo = item.PersonaId + "",
                    Cas_Id = item.Id + "",
                    Mes = item.DtFechaRegistro.Month,
                    FechaAlmacenamiento = item.DtFechaRegistro.ToString("yyyy-MM-dd"),
                    TipoDocumentoId = item.Persona.TipoDocumentoId,
                    NumeroIdent = item.Persona.VcDocumento,
                    PrimerApellido = item.Persona.VcPrimerApellido,
                    SegundoApellido = item.Persona.VcSegundoApellido == null ? "" : item.Persona.VcSegundoApellido,
                    PrimerNombre = item.Persona.VcPrimerNombre,
                    SegundoNombre = item.Persona.VcSegundoNombre == null ? "" : item.Persona.VcSegundoNombre,
                    EdadAlRegistroDelCaso = OperacionesFechas.restar(item.DtFechaRegistro, item.Persona.DtFechaNacimiento).ToString(),
                    RangoDeEdadAlRegistroDelCaso = (DateTime.Now.Year - item.Persona.DtFechaNacimiento.Value.Year) + "",
                    FechaNacimiento = item.Persona.DtFechaNacimiento == null ? "" : item.Persona.DtFechaNacimiento.Value.ToString("yyyy-MM-dd"),
                    SexoId = item.Persona.SexoId,
                    GeneroId = item.Persona.GeneroId,
                    OrientacionSexualId = item.Persona.OrientacionSexualId,
                    EnfoquePoblacionalId = item.Persona.EnfoquePoblacionalId,
                    PoblacionPrioritariaId = item.Persona.PoblacionPrioritariaId,
                    SubPoblacionPrioritariaId = item.Persona.SubPoblacionPrioritariaId == null ? -1 : item.Persona.SubPoblacionPrioritariaId.Value,
                    SubEtniaId = item.Persona.SubEtniaId == null ? -1 : item.Persona.SubEtniaId.Value,
                    EtniaId = item.Persona.EtniaId,
                    RegimenId = item.Persona.PersonaAfiliaciones.First().RegimenId,
                    AseguradoraId = item.Persona.PersonaAfiliaciones.First().AseguradoraId,
                    NivelSisbenId = item.Persona.PersonaAfiliaciones.First().NivelSisbenId,
                    DepartamentoId = item.Persona.PersonaContactos.First().DepartamentoId,
                    MunicipioId = item.Persona.PersonaContactos.First().MunicipioId,
                    Direccion = item.Persona.PersonaContactos.First().VcDireccion,
                    LocalidadId = item.Persona.PersonaContactos.First().LocalidadId,
                    BarrioId = item.Persona.PersonaContactos.First().BarrioId == null ? -1 : item.Persona.PersonaContactos.First().BarrioId.Value,
                    UpzId = item.Persona.PersonaContactos.First().UpzId == null ? -1 : item.Persona.PersonaContactos.First().UpzId.Value,
                    CorreoElectronico = item.Persona.VcCorreo == null ? "" : item.Persona.VcCorreo,
                    Telefono1 = item.Persona.PersonaContactos.First().VcTelefono1 == null ? "" : item.Persona.PersonaContactos.First().VcTelefono1,
                    Telefono2 = item.Persona.PersonaContactos.First().VcTelefono2 == null ? "" : item.Persona.PersonaContactos.First().VcTelefono2,
                    AclaracionesTelfDir = item.Persona.PersonaContactos.First().TxDatosContactoAclaraciones,
                    TipoOrientacion = item.TipoSolicitudId,
                    MotivoId = item.MotivoId,
                    SubMotivoId = item.SubMotivoId,
                    AclaracionMotivoOrientacion = item.TxAclaracionMotivo,
                    GestionResolucionPAcceso = item.TxGestionRealizada == null ? "" : item.TxGestionRealizada,
                    DatosDeContactoAclaración = item.Persona.PersonaContactos.First().TxDatosContactoAclaraciones,
                    CanalAtencionId = item.CanalAtencionId,
                    EstadoCasoId = item.EstadoId,
                    UsuarioId = item.UsuarioId
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