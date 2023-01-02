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
            IQueryable<AtencionGrupal> atencionesGrupalQuery = _context.AtencionGrupal.AsQueryable();
            DtFechaFin = DtFechaFin.AddDays(1).AddSeconds(-1);

            atencionesGrupalQuery.Where(p => p.DtFechaOrientacion >= DtFechaInicio &&
                                            p.DtFechaOrientacion <= DtFechaFin);

            if (usuarioId > 0)
            {
                atencionesGrupalQuery = atencionesGrupalQuery.Where(p => p.UsuarioId == usuarioId);
            }
            return await atencionesGrupalQuery.ToListAsync();
        }


    }


}