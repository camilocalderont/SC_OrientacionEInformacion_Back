using Aplicacion.Services;
using Dominio.Models.AtencionesIndividuales;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Responses;

namespace WebApi.Controllers.AtencionesIndividuales
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtencionIndividualSeguimientoController : ControllerBase
    {
        private readonly IGenericService<AtencionIndividualSeguimiento> _service;
        public AtencionIndividualSeguimientoController(IGenericService<AtencionIndividualSeguimiento> service)
        {
            this._service = service;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> crear(AtencionIndividualSeguimiento atencionIndividualSeguimiento)
        {

            var response = new { Titulo = "Bien Hecho!", Mensaje = "Seguimiento creado de forma correcta", Codigo = HttpStatusCode.Created };
            AtencionIndividualSeguimiento atencionIndividualSeguimientoModel = null;

            atencionIndividualSeguimiento.DtFechaRegistro = DateTime.Now;
            bool guardo = await _service.CreateAsync(atencionIndividualSeguimiento);
            if (!guardo)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No se pudo guardar el seguimiento", Codigo = HttpStatusCode.BadRequest };
            }
            else
            {
                atencionIndividualSeguimientoModel = atencionIndividualSeguimiento;
            }

            var modelResponse = new ModelResponse<AtencionIndividualSeguimiento>(response.Codigo, response.Titulo, response.Mensaje, atencionIndividualSeguimientoModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);

        }



        [HttpGet("porAtencionIndividualId/{atencionIndividualId}")]
        public async Task<ActionResult<IEnumerable<AtencionIndividualSeguimiento>>> GetActividadesPorModulo(long atencionIndividualId)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = $"Se encontraron los seguimientos correspondientes a la atención individual con id: {atencionIndividualId}", Codigo = HttpStatusCode.OK };
            IEnumerable<AtencionIndividualSeguimiento> AtencionIndividualSeguimientos = null;
            if (!await _service.ExistsAsync(e => e.Id > 0))
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existen actividades", Codigo = HttpStatusCode.Accepted };
            }

            AtencionIndividualSeguimientos = await _service.GetAsync(e => e.AtencionIndividualId == atencionIndividualId, e => e.OrderBy(e => e.Id), "");
          
            if (!AtencionIndividualSeguimientos.Any())
            {
                response = new { Titulo = "No hay registros", Mensaje = $"No se encontraron seguimientos correspondientes a la atención individual con id: {atencionIndividualId}", Codigo = HttpStatusCode.OK };
            }

            var listModelResponse = new ListModelResponse<AtencionIndividualSeguimiento>(response.Codigo, response.Titulo, response.Mensaje, AtencionIndividualSeguimientos);
            return StatusCode((int)listModelResponse.Codigo, listModelResponse);
        }




    }
}
