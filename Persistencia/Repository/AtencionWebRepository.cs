using Dominio.Mapper.AtencionesGrupales;
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


    }


}