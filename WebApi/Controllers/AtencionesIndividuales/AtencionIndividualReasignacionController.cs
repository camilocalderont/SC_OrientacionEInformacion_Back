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
    public class AtencionIndividualReasignacionController : ControllerBase
    {
        private readonly IGenericService<AtencionIndividualReasignacion> _service;
        public AtencionIndividualReasignacionController(IGenericService<AtencionIndividualReasignacion> service)
        {
            this._service = service;
        }


        [HttpPost("crear")]
        public async Task<IActionResult> crear(AtencionIndividualReasignacion atencionIndividualReasignacion)
        {

            var response = new { Titulo = "Bien Hecho!", Mensaje = "Reasignación del caso creada de forma correcta", Codigo = HttpStatusCode.Created };
            AtencionIndividualReasignacion atencionIndividualReasignacionModel = null;

            var asignacionesCasoWeb = await _service.GetAsync(e => e.AtencionIndividualId == atencionIndividualReasignacion.AtencionIndividualId, e => e.OrderBy(e => e.Id), "");

            bool validaUsuarioAnterior = true;
            atencionIndividualReasignacion.DtFechaAsignacion = DateTime.Now;
            if (asignacionesCasoWeb.Any())
            {
                var ultimaAsignacion = asignacionesCasoWeb.Last();
                if(ultimaAsignacion.UsuarioActualId != atencionIndividualReasignacion.UsuarioActualId)
                {
                    ultimaAsignacion.DtFechaReAsignacion = atencionIndividualReasignacion.DtFechaAsignacion;
                    atencionIndividualReasignacion.UsuarioAsignaId = ultimaAsignacion.UsuarioActualId;
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
                bool guardo = await _service.CreateAsync(atencionIndividualReasignacion);
                if (!guardo)
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "No se pudo guardar la reasignación caso", Codigo = HttpStatusCode.BadRequest };
                }
                else
                {
                    atencionIndividualReasignacionModel = atencionIndividualReasignacion;
                }
            }

            var modelResponse = new ModelResponse<AtencionIndividualReasignacion>(response.Codigo, response.Titulo, response.Mensaje, atencionIndividualReasignacionModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);

        }



        [HttpGet("porAtencionIndividualId/{atencionIndividualId}")]
        public async Task<ActionResult<IEnumerable<AtencionIndividualReasignacion>>> GetActividadesPorModulo(long atencionIndividualId)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = $"Se encontraron los asignaciones correspondientes a la atención individual con id: {atencionIndividualId}", Codigo = HttpStatusCode.OK };
            IEnumerable<AtencionIndividualReasignacion> AtencionIndividualReasignaciones = await _service.GetAsync(e => e.AtencionIndividualId == atencionIndividualId, e => e.OrderBy(e => e.Id), "");

            if (!AtencionIndividualReasignaciones.Any())
            {
                response = new { Titulo = "No hay registros", Mensaje = $"No se encontraron asignaciones correspondientes a la atención individual con id: {atencionIndividualId}", Codigo = HttpStatusCode.OK };
            }

            var listModelResponse = new ListModelResponse<AtencionIndividualReasignacion>(response.Codigo, response.Titulo, response.Mensaje, AtencionIndividualReasignaciones);
            return StatusCode((int)listModelResponse.Codigo, listModelResponse);
        }

    }
}
