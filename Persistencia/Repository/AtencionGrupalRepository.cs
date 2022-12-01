using Dominio.Mapper.AtencionesGrupales;
using Dominio.Models.AtencionesGrupales;
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




        //public IEnumerable<AtencionGrupalAnexoDTO> getAtencionGrupaAnexolDTO()

        //{
        //    var parametro = _context.AtencionGrupal
        //            //.Where(p => p.BEstado == true && p.VcCodigoInterno == codigoIterno)
        //            .Select(d => new AtencionGrupalDTO
        //            {

        //                DtFechaRegistro = d.DtFechaRegistro,
        //                UsuarioId = d.UsuarioId,
        //                INumeroUsuarios = d.INumeroUsuarios.

        //                    Select(d => new AtencionGrupalAnexoDTO
        //                    {
        //                        VcNombre        = d.VcNombre,
        //                        UsuarioId       = d.UsuarioId,
        //                        DtFechaRegistro = d.DtFechaRegistro,
        //                        DtFechaRegistro = d.DtFechaRegistro,
        //                        VcDescripcion   = d.VcDescripcion,
        //                        IBytes          = d.IBytes
        //                    }).ToList()
        //            })
        //            .FirstOrDefault();

        //    return parametro.ParametroDetalles;

        //}


    }


}