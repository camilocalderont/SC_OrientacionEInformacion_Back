using Dominio.Models.AtencionesIndividuales;
using Dominio.Mapper.AtencionesIndividuales;
using Microsoft.EntityFrameworkCore;
using Persistencia.Context;
using System.Reflection.Metadata;

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


        public async Task<IEnumerable<AtencionIndividualDTO>> obtenerPorRangoFechasEstadoUsuarioYDocumento(
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
                var personaWeb = _context.Persona.Where(p=>p.VcDocumento== VcDocumento).FirstOrDefault();
                _context.Entry(personaWeb).State = EntityState.Detached;
                if (personaWeb != null)
                {
                    atencionesIndividualesQuery = atencionesIndividualesQuery.Where(g => g.PersonaId == personaWeb.Id);
                }

            }



            var atencionesIndividuales = await atencionesIndividualesQuery.Select(a=>new AtencionIndividualDTO
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

            }).ToListAsync();

            return atencionesIndividuales;


        }


        public async Task<IEnumerable<AtencionIndividualDTO>> obtenerPorPersonaYExcluyeCaso(long PersonaId,long AtencionIndividualId)
        {
            IQueryable<AtencionIndividual> atencionesIndividualesQuery = _context.AtencionIndividual.AsQueryable();

            atencionesIndividualesQuery = atencionesIndividualesQuery.Where(p => p.PersonaId == PersonaId && p.Id != AtencionIndividualId);
           



            var atencionesIndividuales = await atencionesIndividualesQuery.Select(a => new AtencionIndividualDTO
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

            }).ToListAsync();

            return atencionesIndividuales;


        }


        public async Task<AtencionIndividualDTO> obtenerPorId(long atencionIndividualId)
        {
            var atencionIndividualDto = _context.AtencionIndividual.Where(p => p.Id == atencionIndividualId)
                .Select(a => new AtencionIndividualDTO
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

                }).FirstOrDefault();
            return atencionIndividualDto;
        }


    }


}