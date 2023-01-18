using Aplicacion.Services;
using AutoMapper;
using Dominio.Models.AtencionesIndividuales;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Requests.AtencionesWeb;
using WebApi.Responses;

namespace WebApi.Controllers.AtencionesIndividuales
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtencionIndividualAnexoController : ControllerBase
    {

        private readonly IGenericService<AtencionIndividualAnexo> _service;
        private readonly IMapper _mapper;

        public AtencionIndividualAnexoController(IGenericService<AtencionIndividualAnexo> service, IMapper _mapper)
        {
            this._service = service;
            this._mapper  = _mapper; 
        }



        [HttpGet("porAtencionIndividualId/{atencionIndividualId}")]
        public async Task<ActionResult<IEnumerable<AtencionIndividualAnexo>>> GetAtencionIndividualAnexoPorId(long atencionIndividualId)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = $"Se encontraron los anexos correspondientes a la atención individual con id: {atencionIndividualId}", Codigo = HttpStatusCode.OK };
            IEnumerable<AtencionIndividualAnexo> AtencionIndividualAnexos = await _service.GetAsync(e => e.AtencionIndividualId == atencionIndividualId, e => e.OrderBy(e => e.Id), "");

            if (!AtencionIndividualAnexos.Any())
            {
                response = new { Titulo = "No hay registros", Mensaje = $"No se encontraron seguimientos correspondientes a la atención individual con id: {atencionIndividualId}", Codigo = HttpStatusCode.OK };
            }

            var listModelResponse = new ListModelResponse<AtencionIndividualAnexo>(response.Codigo, response.Titulo, response.Mensaje, AtencionIndividualAnexos);
            return StatusCode((int)listModelResponse.Codigo, listModelResponse);
        }




    }
}
