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
    public class AtencionIndividualActorController : ControllerBase
    {

        private readonly IGenericService<AtencionIndividualActor> _service;
        private readonly IMapper _mapper;

        public AtencionIndividualActorController(IGenericService<AtencionIndividualActor> service, IMapper _mapper)
        {
            this._service = service;
            this._mapper  = _mapper; 
        }

        [HttpPost("crear")]
        public async Task<bool> crearPorListado(IEnumerable<AtencionIndividualActor> AtencionesIndividualesActoresList)
        {
            bool guardo = true;
            var fechaRegistro = DateTime.Now;
            foreach (AtencionIndividualActor atencionIndividualActor in AtencionesIndividualesActoresList)
            {
                atencionIndividualActor.DtFechaRegistro = fechaRegistro;
                guardo = await crear(atencionIndividualActor);
            }
            return guardo;
        }

        [NonAction]    
        public async Task<bool> crear(AtencionIndividualActor atencionIndividualActor)
        {
            return await _service.CreateAsync(atencionIndividualActor);
        }


        [HttpGet("porAtencionIndividualId/{atencionIndividualId}")]
        public async Task<ActionResult<IEnumerable<AtencionIndividualActor>>> GetAtencionesIndividualesActor(long atencionIndividualId)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = $"Se encontraron los actores correspondientes a la atención individual con id: {atencionIndividualId}", Codigo = HttpStatusCode.OK };
            IEnumerable<AtencionIndividualActor> AtencionIndividualActors = null;
            if (!await _service.ExistsAsync(e => e.Id > 0))
            {
                response = new { Titulo = "Algo salio mal", Mensaje = $"No existen actores asociados a la atención individual con id: {atencionIndividualId}", Codigo = HttpStatusCode.Accepted };
            }

            AtencionIndividualActors = await _service.GetAsync(e => e.AtencionIndividualId == atencionIndividualId, e => e.OrderBy(e => e.Id), "");

            if (!AtencionIndividualActors.Any())
            {
                response = new { Titulo = "No hay registros", Mensaje = $"No se encontraron seguimientos correspondientes a la atención individual con id: {atencionIndividualId}", Codigo = HttpStatusCode.OK };
            }

            var listModelResponse = new ListModelResponse<AtencionIndividualActor>(response.Codigo, response.Titulo, response.Mensaje, AtencionIndividualActors);
            return StatusCode((int)listModelResponse.Codigo, listModelResponse);
        }




    }
}
