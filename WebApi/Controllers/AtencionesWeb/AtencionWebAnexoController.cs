using Aplicacion.Services;
using AutoMapper;
using Dominio.Models.AtencionesGrupales;
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



        [HttpGet("porAtencionWebId/{atencionWebId}")]
        public async Task<ActionResult<IEnumerable<AtencionWebAnexo>>> GetActividadesPorModulo(long atencionWebId)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = $"Se encontraron los anexos correspondientes a la atención web con id: {atencionWebId}", Codigo = HttpStatusCode.OK };
            IEnumerable<AtencionWebAnexo> AtencionWebAnexos = null;
            if (!await _service.ExistsAsync(e => e.Id > 0))
            {
                response = new { Titulo = "Algo salio mal", Mensaje = $"No existen anexos asociados a la atención web con id: {atencionWebId}", Codigo = HttpStatusCode.Accepted };
            }

            AtencionWebAnexos = await _service.GetAsync(e => e.AtencionWebId == atencionWebId, e => e.OrderBy(e => e.Id), "");

            if (!AtencionWebAnexos.Any())
            {
                response = new { Titulo = "No hay registros", Mensaje = $"No se encontraron seguimientos correspondientes a la atención web con id: {atencionWebId}", Codigo = HttpStatusCode.OK };
            }

            var listModelResponse = new ListModelResponse<AtencionWebAnexo>(response.Codigo, response.Titulo, response.Mensaje, AtencionWebAnexos);
            return StatusCode((int)listModelResponse.Codigo, listModelResponse);
        }




    }
}
