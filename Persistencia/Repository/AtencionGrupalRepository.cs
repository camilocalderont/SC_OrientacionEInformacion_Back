using Dominio.Mapper.AtencionesGrupales;
using Dominio.Models.AtencionesGrupales;
using Microsoft.EntityFrameworkCore;
using Persistencia.Context;

namespace Persistencia.Repository
{
    public class AtencionGrupalRepository : GenericRepository<AtencionGrupal>
    {

        public IGenericRepository<AtencionGrupalAnexo> _atencionGrupalRepository { get; }
        public OrientacionDbContext _context;

        public AtencionGrupalRepository(OrientacionDbContext context, IGenericRepository<AtencionGrupalAnexo> atencionGrupalRepository) : base(context)
        {
            this._atencionGrupalRepository = atencionGrupalRepository;
            this._context = context;
        }


        public async Task<IEnumerable<AtencionGrupal>> obtenerPorRangoFechasYUsuario(DateTime DtFechaInicio, DateTime DtFechaFin, long usuarioId)
        {
            IQueryable<AtencionGrupal> atencionesGrupalQuery = _context.AtencionGrupal
                .Include(p => p.AtencionGrupalesAnexos)
                .Where(p => p.DtFechaRegistro >= DtFechaInicio && p.DtFechaRegistro <= DtFechaFin)
                .AsQueryable();

            if (usuarioId > 0)
            {
                atencionesGrupalQuery = atencionesGrupalQuery.Where(p => p.UsuarioId == usuarioId);
            }
            return await atencionesGrupalQuery.ToListAsync();
        }

        public async Task<IEnumerable<AtencionGrupal>> obtenerPorRangoFechas(DateTime DtFechaInicio, DateTime DtFechaFin)
        {
            IQueryable<AtencionGrupal> atencionesGrupalQuery = _context.AtencionGrupal.AsQueryable();

            DateTime fechafinalContemplandoHorasMinutosSegundos = new DateTime(DtFechaFin.Year, DtFechaFin.Month, DtFechaFin.Day, 23, 59, 59);

            atencionesGrupalQuery = atencionesGrupalQuery.Where(p =>  (DateTime.Compare(p.DtFechaRegistro, DtFechaInicio)>=0) &&
                                            (DateTime.Compare(p.DtFechaRegistro, fechafinalContemplandoHorasMinutosSegundos) <=0) );
           
            return await atencionesGrupalQuery.ToListAsync();
        }

        public async Task<AtencionGrupalDTO> obtenerPorId(long atencionGrupalId)
        {
            var atencionGrupalDto = _context.AtencionGrupal.Where(p => p.Id == atencionGrupalId)
                .Select(a => new AtencionGrupalDTO
                {
                    Id= a.Id,
                    DtFechaRegistro= a.DtFechaRegistro,
                    DtFechaOrientacion = a.DtFechaOrientacion,
                    INumeroUsuarios = a.INumeroUsuarios,
                    LocalidadId= a.LocalidadId,
                    MotivoId= a.MotivoId,
                    SubMotivoId = a.SubMotivoId,
                    TiempoDuracionId = a.TiempoDuracionId,
                    TipoActividadId= a.TipoActividadId,
                    TipoSolicitudId = a.TipoSolicitudId,
                    TxAclaracionMotivo = a.TxAclaracionMotivo,
                    UsuarioId= a.UsuarioId,
                    VcLugar = a.VcLugar,
                    IAnexos = a.AtencionGrupalesAnexos.Count()

                }).FirstOrDefault();
            return atencionGrupalDto;
        }


    }


}