using Dominio.Mapper.AtencionesIndividuales;
using Dominio.Mapper.AtencionesWeb;
using Dominio.Models.AtencionesIndividuales;
using Dominio.Models.AtencionesWeb;
using Microsoft.EntityFrameworkCore;
using Persistencia.Context;
using System.Reflection.Metadata;

namespace Persistencia.Repository
{
    public class AtencionWebRepository : GenericRepository<AtencionWeb>
    {

        public IGenericRepository<AtencionWebAnexo> _atencionWebRepository { get; }
        public OrientacionDbContext _context;

        public AtencionWebRepository(OrientacionDbContext context, IGenericRepository<AtencionWebAnexo> atencionWebRepository) : base(context)
        {
            this._atencionWebRepository = atencionWebRepository;
            this._context = context;
        }


        public async Task<IEnumerable<AtencionWebDTO>> obtenerPorRangoFechasEstadoUsuarioYCorreo(
            long EstadoId, 
            DateTime DtFechaInicio, 
            DateTime DtFechaFin, 
            long usuarioId, 
            string VcCorreo
        )
        {
            IQueryable<AtencionWeb> atencionesWebQuery = _context.AtencionWeb.AsQueryable();

            atencionesWebQuery = atencionesWebQuery.Where(p => p.DtFechaRegistro >= DtFechaInicio &&
                                            p.DtFechaRegistro <= DtFechaFin);
            if (usuarioId > 0)
            {
                atencionesWebQuery = atencionesWebQuery.Where(p => p.UsuarioId == usuarioId);
            }

            if (EstadoId > 0)
            {
                atencionesWebQuery = atencionesWebQuery.Where(p => p.EstadoId == EstadoId);
            }

            if (VcCorreo.Length > 0)
            {
                var personaWeb = _context.PersonaWeb.Where(p=>p.VcCorreo== VcCorreo).FirstOrDefault();
                _context.Entry(personaWeb).State = EntityState.Detached;
                if (personaWeb != null)
                {
                    atencionesWebQuery = atencionesWebQuery.Where(g => g.PersonaWebId == personaWeb.Id);
                }

            }



            var atencionesWeb = await atencionesWebQuery.Select(a=>new AtencionWebDTO
            {
                Id= a.Id,
                DtFechaRegistro = a.DtFechaRegistro,
                CanalAtencionId= a.CanalAtencionId,
                DtFechaOrientacion = a.DtFechaOrientacion,
                EstadoId= a.EstadoId,
                IAnexos = a.AtencionAnexos.Count,
                MotivoId= a.MotivoId,
                SubMotivoId= a.SubMotivoId,
                PersonaWebId= a.PersonaWebId,   
                TipoGestionId= a.TipoGestionId,
                BProcesoFallido = a.BProcesoFallido,
                TipoProcesoFallidoId = a.TipoProcesoFallidoId,
                TipoSolicitudId = a.TipoSolicitudId,
                TxAclaracionMotivo= a.TxAclaracionMotivo,
                TxAsuntoCorreo = a.TxAsuntoCorreo,
                UsuarioId= a.UsuarioId,
                VcCorreo= a.PersonasWeb.VcCorreo,
                VcNombreCompleto = $"{a.PersonasWeb.VcPrimerNombre ?? string.Empty} {a.PersonasWeb.VcSegundoNombre ?? string.Empty} {a.PersonasWeb.VcPrimerApellido ?? string.Empty} {a.PersonasWeb.VcSegundoApellido ?? string.Empty}",
                VcTelefono1 = a.PersonasWeb.VcTelefono1,
                VcTelefono2 = a.PersonasWeb.VcTelefono2,
                UsuarioActualId = a.AtencionReasignaciones.Any() ? a.AtencionReasignaciones.OrderBy(a=>a.Id).Last().UsuarioActualId : a.UsuarioId,

            }).ToListAsync();

            return atencionesWeb;


        }


        public async Task<IEnumerable<AtencionWebDTO>> obtenerPorPersonaWebYExcluyeCaso(long PersonaWebId,long AtencionWebId)
        {
            IQueryable<AtencionWeb> atencionesWebQuery = _context.AtencionWeb.AsQueryable();

            atencionesWebQuery = atencionesWebQuery.Where(p => p.PersonaWebId == PersonaWebId && p.Id != AtencionWebId);
           



            var atencionesWeb = await atencionesWebQuery.Select(a => new AtencionWebDTO
            {
                Id = a.Id,
                DtFechaRegistro = a.DtFechaRegistro,
                CanalAtencionId = a.CanalAtencionId,
                DtFechaOrientacion = a.DtFechaOrientacion,
                EstadoId = a.EstadoId,
                IAnexos = a.AtencionAnexos.Count,
                MotivoId = a.MotivoId,
                SubMotivoId = a.SubMotivoId,
                PersonaWebId = a.PersonaWebId,
                TipoGestionId = a.TipoGestionId,
                BProcesoFallido = a.BProcesoFallido,
                TipoProcesoFallidoId = a.TipoProcesoFallidoId,
                TipoSolicitudId = a.TipoSolicitudId,
                TxAclaracionMotivo = a.TxAclaracionMotivo,
                TxAsuntoCorreo = a.TxAsuntoCorreo,
                UsuarioId = a.UsuarioId,
                VcCorreo = a.PersonasWeb.VcCorreo,
                VcNombreCompleto = $"{a.PersonasWeb.VcPrimerNombre} {a.PersonasWeb.VcSegundoNombre} {a.PersonasWeb.VcPrimerApellido} {a.PersonasWeb.VcSegundoApellido}",
                VcTelefono1 = a.PersonasWeb.VcTelefono1,
                VcTelefono2 = a.PersonasWeb.VcTelefono2,
                UsuarioActualId = a.AtencionReasignaciones.Any() ? a.AtencionReasignaciones.OrderBy(a => a.Id).Last().UsuarioActualId : a.UsuarioId,

            }).ToListAsync();

            return atencionesWeb;


        }


        public async Task<IEnumerable<AtencionWebReporteDTO>> obtenerPorRangoFechasParaReporteOW(DateTime fechaInicio, DateTime fechaFinal)
        {
            IQueryable<AtencionWeb> query = _context.AtencionWeb.AsQueryable();

            query = query
                .Where(p => (DateTime.Compare(p.DtFechaRegistro, fechaInicio) >= 0) && (DateTime.Compare(p.DtFechaRegistro, fechaFinal) <= 0));

            query = query.Include(atencionWeb => atencionWeb.PersonasWeb);

            var atencionWeb = await query.Select(atencionWeb => new AtencionWebReporteDTO
            {
                Id = atencionWeb.Id,
                Mes = atencionWeb.DtFechaRegistro.Month,
                FechaAlmacenamiento = atencionWeb.DtFechaRegistro,
                FechaOrientacion = atencionWeb.DtFechaOrientacion,
                PrimerApellido = atencionWeb.PersonasWeb.VcPrimerApellido == null ? "" : atencionWeb.PersonasWeb.VcPrimerApellido,
                SegundoApellido = atencionWeb.PersonasWeb.VcSegundoApellido == null ? "" : atencionWeb.PersonasWeb.VcSegundoApellido,
                PrimerNombre = atencionWeb.PersonasWeb.VcPrimerNombre == null ? "" : atencionWeb.PersonasWeb.VcPrimerNombre,
                SegundoNombre = atencionWeb.PersonasWeb.VcSegundoNombre == null ? "" : atencionWeb.PersonasWeb.VcSegundoNombre,
                CorreoElectronico = atencionWeb.PersonasWeb.VcCorreo,
                Telefono1 = atencionWeb.PersonasWeb.VcTelefono1 == null ? "" : atencionWeb.PersonasWeb.VcTelefono1,
                Celular = atencionWeb.PersonasWeb.VcTelefono2 == null ? "" : atencionWeb.PersonasWeb.VcTelefono2,
                MotivoId = atencionWeb.MotivoId,
                SubMotivoId = atencionWeb.SubMotivoId,
                CanalAtencionId = atencionWeb.CanalAtencionId,
                TipoGestionId = atencionWeb.TipoGestionId == null ? -1 : atencionWeb.TipoGestionId.Value,
                TipoProcesoFallidoId = atencionWeb.TipoProcesoFallidoId,
                AsuntoCorreoElectronico = atencionWeb.TxAsuntoCorreo,
                AclaracionMotivoOrientacion = atencionWeb.TxAclaracionMotivo,
                UsuarioId = atencionWeb.PersonasWeb.UsuarioId
            }).ToListAsync();

            return atencionWeb;
        }

            public async Task<AtencionWebDTO> obtenerPorId(long atencionWebId)
        {
            var atencionWebDto = _context.AtencionWeb.Where(p => p.Id == atencionWebId)
                .Select(a => new AtencionWebDTO
                {
                    Id = a.Id,
                    DtFechaRegistro = a.DtFechaRegistro,
                    CanalAtencionId = a.CanalAtencionId,
                    DtFechaOrientacion = a.DtFechaOrientacion,
                    EstadoId = a.EstadoId,
                    IAnexos = a.AtencionAnexos.Count,
                    MotivoId = a.MotivoId,
                    SubMotivoId = a.SubMotivoId,
                    PersonaWebId = a.PersonaWebId,
                    TipoGestionId = a.TipoGestionId,
                    BProcesoFallido = a.BProcesoFallido,
                    TipoProcesoFallidoId = a.TipoProcesoFallidoId,
                    TipoSolicitudId = a.TipoSolicitudId,
                    TxAclaracionMotivo = a.TxAclaracionMotivo,
                    TxAsuntoCorreo = a.TxAsuntoCorreo,
                    UsuarioId = a.UsuarioId,
                    VcCorreo = a.PersonasWeb.VcCorreo,
                    VcNombreCompleto = $"{a.PersonasWeb.VcPrimerNombre} {a.PersonasWeb.VcSegundoNombre} {a.PersonasWeb.VcPrimerApellido} {a.PersonasWeb.VcSegundoApellido}",
                    VcTelefono1 = a.PersonasWeb.VcTelefono1,
                    VcTelefono2 = a.PersonasWeb.VcTelefono2,
                    UsuarioActualId = a.AtencionReasignaciones.Any() ? a.AtencionReasignaciones.OrderBy(a => a.Id).Last().UsuarioActualId : a.UsuarioId,
                    Seguimientos = a.AtencionSeguimientos.Select(a => new AtencionWebSeguimientoDTO
                    {
                        AtencionWebId = a.AtencionWebId,
                        BCierraCaso = a.BCierraCaso,
                        DtFechaRegistro = a.DtFechaRegistro,
                        UsuarioId = a.UsuarioId,
                        VcDescripcion = a.VcDescripcion,
                    }).ToList(),
                    Reasignaciones = a.AtencionReasignaciones.Select(a => new AtencionWebReasignacionDTO
                    {
                        AtencionWebId = a.AtencionWebId,
                        DtFechaAsignacion = a.DtFechaAsignacion,
                        DtFechaReAsignacion = a.DtFechaAsignacion,
                        UsuarioActualId = a.UsuarioActualId,
                        UsuarioAsignaId = a.UsuarioAsignaId,
                        VcDescripcion = a.VcDescripcion
                    }).ToList()
                }).FirstOrDefault();
            return atencionWebDto;
        }

    }


}