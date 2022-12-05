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
            if (usuarioId > 0)
            {
                return await _context.AtencionGrupal
                            .Where(g => g.DtFechaRegistro >= DtFechaInicio && 
                                        g.DtFechaRegistro <= DtFechaFin &&
                                        g.UsuarioId == usuarioId
                            ).ToListAsync();
            }
            else
            {
                return await _context.AtencionGrupal
                            .Where(g => g.DtFechaRegistro >= DtFechaInicio &&
                                        g.DtFechaRegistro <= DtFechaFin
                            ).ToListAsync();
            }
           
        }


    }


}