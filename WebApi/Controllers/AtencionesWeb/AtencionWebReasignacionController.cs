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
    public class AtencionWebReasignacionController : ControllerBase
    {
        private readonly IGenericService<AtencionWebReasignacion> _service;
        public AtencionWebReasignacionController(IGenericService<AtencionWebReasignacion> service)
        {
            this._service = service;
        }


        [HttpPost("crear")]
        public async Task<IActionResult> crear(AtencionWebReasignacion atencionWebReasignacion)
        {

            var response = new { Titulo = "Bien Hecho!", Mensaje = "Reasignación del caso creada de forma correcta", Codigo = HttpStatusCode.Created };
            AtencionWebReasignacion atencionWebReasignacionModel = null;

            var asignacionesCasoWeb = await _service.GetAsync(e => e.AtencionWebId == atencionWebReasignacion.AtencionWebId, e => e.OrderBy(e => e.Id), "");

            bool validaUsuarioAnterior = true;
            atencionWebReasignacion.DtFechaAsignacion = DateTime.Now;
            if (asignacionesCasoWeb.Any())
            {
                var ultimaAsignacion = asignacionesCasoWeb.Last();
                if(ultimaAsignacion.UsuarioActualId != atencionWebReasignacion.UsuarioActualId)
                {
                    ultimaAsignacion.DtFechaReAsignacion = atencionWebReasignacion.DtFechaAsignacion;
                    atencionWebReasignacion.UsuarioAsignaId = ultimaAsignacion.UsuarioActualId;
                    await _service.UpdateAsync(ultimaAsignacion.Id, ultimaAsignacion);
                }
                else
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "El último usuario asignado al caso es el mismo que se quiere asignar", Codigo = HttpStatusCode.BadRequest };
                    validaUsuarioAnterior = false;
                }

            }
            if (validaUsuarioAnterior)
            {
                bool guardo = await _service.CreateAsync(atencionWebReasignacion);
                if (!guardo)
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "No se pudo guardar la reasignación caso", Codigo = HttpStatusCode.BadRequest };
                }
                else
                {
                    atencionWebReasignacionModel = atencionWebReasignacion;
                }
            }

            var modelResponse = new ModelResponse<AtencionWebReasignacion>(response.Codigo, response.Titulo, response.Mensaje, atencionWebReasignacionModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);

        }



        [HttpGet("porAtencionWebId/{atencionWebId}")]
        public async Task<ActionResult<IEnumerable<AtencionWebReasignacion>>> GetActividadesPorModulo(long atencionWebId)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = $"Se encontraron los seguimientos correspondientes a la atención web con id: {atencionWebId}", Codigo = HttpStatusCode.OK };
            IEnumerable<AtencionWebReasignacion> AtencionWebReasignaciones = null;
            if (!await _service.ExistsAsync(e => e.Id > 0))
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existen actividades", Codigo = HttpStatusCode.Accepted };
            }

            AtencionWebReasignaciones = await _service.GetAsync(e => e.AtencionWebId == atencionWebId, e => e.OrderBy(e => e.Id), "");

            if (!AtencionWebReasignaciones.Any())
            {
                response = new { Titulo = "No hay registros", Mensaje = $"No se encontraron seguimientos correspondientes a la atención web con id: {atencionWebId}", Codigo = HttpStatusCode.OK };
            }

            var listModelResponse = new ListModelResponse<AtencionWebReasignacion>(response.Codigo, response.Titulo, response.Mensaje, AtencionWebReasignaciones);
            return StatusCode((int)listModelResponse.Codigo, listModelResponse);
        }

    }
}
