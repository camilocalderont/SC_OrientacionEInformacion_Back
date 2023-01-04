using Aplicacion.Services;
using Dominio.Models.AtencionesWeb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Responses;

namespace WebApi.Controllers.AtencionesWeb
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtencionWebSeguimientoController : ControllerBase
    {
        private readonly IGenericService<AtencionWebSeguimiento> _service;
        public AtencionWebSeguimientoController(IGenericService<AtencionWebSeguimiento> service)
        {
            this._service = service;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> crear(AtencionWebSeguimiento atencionWebSeguimiento)
        {

            var response = new { Titulo = "Bien Hecho!", Mensaje = "Seguimiento creado de forma correcta", Codigo = HttpStatusCode.Created };
            AtencionWebSeguimiento atencionWebSeguimientoModel = null;

            atencionWebSeguimiento.DtFechaRegistro = DateTime.Now;
            bool guardo = await _service.CreateAsync(atencionWebSeguimiento);
            if (!guardo)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No se pudo guardar el seguimiento", Codigo = HttpStatusCode.BadRequest };
            }
            else
            {
                atencionWebSeguimientoModel = atencionWebSeguimiento;
            }

            var modelResponse = new ModelResponse<AtencionWebSeguimiento>(response.Codigo, response.Titulo, response.Mensaje, atencionWebSeguimientoModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);

        }



        [HttpGet("porAtencionWebId/{atencionWebId}")]
        public async Task<ActionResult<IEnumerable<AtencionWebSeguimiento>>> GetActividadesPorModulo(long atencionWebId)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = $"Se encontraron los seguimientos correspondientes a la atención web con id: {atencionWebId}", Codigo = HttpStatusCode.OK };
            IEnumerable<AtencionWebSeguimiento> AtencionWebSeguimientos = null;
            if (!await _service.ExistsAsync(e => e.Id > 0))
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existen actividades", Codigo = HttpStatusCode.Accepted };
            }

            AtencionWebSeguimientos = await _service.GetAsync(e => e.AtencionWebId == atencionWebId, e => e.OrderBy(e => e.Id), "");
          
            if (!AtencionWebSeguimientos.Any())
            {
                response = new { Titulo = "No hay registros", Mensaje = $"No se encontraron seguimientos correspondientes a la atención web con id: {atencionWebId}", Codigo = HttpStatusCode.OK };
            }

            var listModelResponse = new ListModelResponse<AtencionWebSeguimiento>(response.Codigo, response.Titulo, response.Mensaje, AtencionWebSeguimientos);
            return StatusCode((int)listModelResponse.Codigo, listModelResponse);
        }




    }
}
