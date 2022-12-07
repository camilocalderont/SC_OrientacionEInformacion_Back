using Aplicacion.Services;
using AutoMapper;
using Dominio.Models.AtencionesWeb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Requests.AtencionesWeb;
using WebApi.Responses;

namespace WebApi.Controllers.AtencionesWeb
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtencionWebAnexoController : ControllerBase
    {

        private readonly IGenericService<AtencionWebAnexo> _service;
        private readonly IMapper _mapper;

        public AtencionWebAnexoController(IGenericService<AtencionWebAnexo> service, IMapper _mapper)
        {
            this._service = service;
            this._mapper  = _mapper; 
        }



        [HttpGet("GetAtencionAnexo")]
        public async Task<ActionResult<IEnumerable<AtencionWenAnexoRequest>>> GetAtencionAnexo()
        {
            try
            {
                var response = new { Titulo = "Bien Hecho!", Mensaje = "Se encontraron atenciones anexo", Codigo = HttpStatusCode.OK };

                IEnumerable<AtencionWebAnexo> AtencionAnexoModel = null;

                AtencionAnexoModel = await _service.GetAsync();

                List<AtencionWenAnexoRequest> atencionanexoDTO = _mapper.Map<List<AtencionWenAnexoRequest>>(AtencionAnexoModel);

                if (!await _service.ExistsAsync(e => e.Id > 0))
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "No existen atenciones anexo", Codigo = HttpStatusCode.Accepted };
                }

                var listModelResponse = new ListModelResponse<AtencionWenAnexoRequest>(response.Codigo, response.Titulo, response.Mensaje, atencionanexoDTO);
                return StatusCode((int)listModelResponse.Codigo, listModelResponse);

            }

            catch (Exception)
            {
                var response = new { Titulo = "Algo salio mal", Mensaje = "Mostrando atenciones anexo", Codigo = HttpStatusCode.RequestedRangeNotSatisfiable };
                return StatusCode((int)response.Codigo, response);
            }


        }



        
    }
}
