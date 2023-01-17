using Aplicacion.Services;
using AutoMapper;
using Dominio.Mapper.AtencionesGrupales;
using Dominio.Models.AtencionesGrupales;
using Dominio.Models.AtencionesWeb;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Responses;


namespace WebApi.Controllers.AtencionesGrupales
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtencionGrupalAnexoController : ControllerBase
    {
        private readonly IGenericService<AtencionGrupalAnexo> _service;
        private readonly IMapper _mapper;


        public AtencionGrupalAnexoController(IGenericService<AtencionGrupalAnexo> service, IMapper mapper)
        {
            _service = service;
            _mapper = mapper;
        }


        [HttpGet("porAtencionGrupalId/{atencionGrupalId}")]
        public async Task<ActionResult<IEnumerable<AtencionGrupalAnexo>>> GetActividadesPorModulo(long atencionGrupalId)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = $"Se encontraron los anexos correspondientes a la atención grupal con id: {atencionGrupalId}", Codigo = HttpStatusCode.OK };
            IEnumerable<AtencionGrupalAnexo> AtencioGrupalAnexos = null;
            if (!await _service.ExistsAsync(e => e.Id > 0))
            {
                response = new { Titulo = "Algo salio mal", Mensaje = $"No existen anexos asociados a la atención grupal con id: {atencionGrupalId}", Codigo = HttpStatusCode.Accepted };
            }

            AtencioGrupalAnexos = await _service.GetAsync(e => e.AtencionGrupalId == atencionGrupalId, e => e.OrderBy(e => e.Id), "");

            if (AtencioGrupalAnexos.Count() == 0)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = $"No se encontraron anexos correspondientes a la atención grupal con id: {atencionGrupalId}", Codigo = HttpStatusCode.NotFound };
            }

            var listModelResponse = new ListModelResponse<AtencionGrupalAnexo>(response.Codigo, response.Titulo, response.Mensaje, AtencioGrupalAnexos);
            return StatusCode((int)listModelResponse.Codigo, listModelResponse);
        }

        /*
         * Se debe agregar funcionalidad de almacenamiento en Azure
        [HttpPost("PostAtencionGrupalAnexo")]
        public async Task<IActionResult> PostAtencionGrupalAnexo(AtencionGrupalAnexoDTO atenciongrupalanexo)
        {
            try
            {
                var response = new { Titulo = "Bien Hecho!", Mensaje = "Atencion grupal anexo creado de forma correcta", Codigo = HttpStatusCode.Created };
                var atenciongrupalanexoDTO = _mapper.Map<AtencionGrupalAnexo>(atenciongrupalanexo);

                atenciongrupalanexoDTO.DtFechaRegistro = DateTime.Now;

                bool guardo = await _service.CreateAsync(atenciongrupalanexoDTO);

                if (!guardo)
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "No se pudo guardar atención grupal anexo", Codigo = HttpStatusCode.BadRequest };
                }
                var modelResponse = new ModelResponse<AtencionGrupalAnexoDTO>(response.Codigo, response.Titulo, response.Mensaje, atenciongrupalanexo);
                return StatusCode((int)modelResponse.Codigo, modelResponse);
            }
            catch (Exception)
            {
                var response = new { Titulo = "Algo salio mal", Mensaje = "Creando atención grupal anexo", Codigo = HttpStatusCode.RequestedRangeNotSatisfiable };
                return StatusCode((int)response.Codigo, response); throw;
            }

        }
        */
    }

}
