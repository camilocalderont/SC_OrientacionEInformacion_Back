using Dominio.Models.AtencionesGrupales;
using Persistencia.Repository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Aplicacion.Services
{
    public class AtencionGrupalService : GenericService<AtencionGrupal>
    {


        public AtencionGrupalRepository _atencionGrupalRepository { get; }
        public IGenericRepository<AtencionGrupal> _genericRepository { get; }
        public AtencionGrupalService(IGenericRepository<AtencionGrupal> genericRepository, AtencionGrupalRepository atencionGrupalRepository): base(genericRepository)
        {
            this._atencionGrupalRepository = atencionGrupalRepository;
            this._genericRepository = genericRepository;
        }

        public async Task<IEnumerable<AtencionGrupal>> obtenerPorRangoFechasYUsuario(DateTime DtFechaInicio, DateTime DtFechaFin, long usuarioId)
        {
            return await _atencionGrupalRepository.obtenerPorRangoFechasYUsuario(DtFechaInicio, DtFechaFin, usuarioId);
        }

    }
}
