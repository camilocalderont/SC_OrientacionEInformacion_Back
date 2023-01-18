using Aplicacion.Services;
using Dominio.Models.AtencionesWeb;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Responses;
using WebApi.Requests.AtencionesWeb;
using AutoMapper;

namespace WebApi.Controllers.AtencionesWeb
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtencionWebSeguimientoController : ControllerBase
    {
        private readonly IGenericService<AtencionWebSeguimiento> _service;
        private readonly IGenericService<AtencionWeb> _atencionWebservice;
        private readonly IMapper _mapper;
        public AtencionWebSeguimientoController(
            IGenericService<AtencionWebSeguimiento> service,
            IGenericService<AtencionWeb> atencionWebservice,
            IMapper mapper
        )
        {
            this._service = service;
            this._atencionWebservice = atencionWebservice;
            this._mapper = mapper;
        }

        [HttpPost("crear")]
        public async Task<IActionResult> crear(AtencionWebSeguimientoRequest atencionWebSeguimientoRequest)
        {

            var response = new { Titulo = "Bien Hecho!", Mensaje = "Seguimiento creado de forma correcta", Codigo = HttpStatusCode.Created };
            AtencionWebSeguimiento atencionWebSeguimientoModel = null;

            AtencionWebSeguimiento atencionWebSeguimiento = _mapper.Map<AtencionWebSeguimiento>(atencionWebSeguimientoRequest);
            atencionWebSeguimiento.DtFechaRegistro = DateTime.Now;
            bool guardo = await _service.CreateAsync(atencionWebSeguimiento);
            if (!guardo)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No se pudo guardar el seguimiento", Codigo = HttpStatusCode.BadRequest };
            }
            else
            {
                if (atencionWebSeguimiento.BCierraCaso)
                {
                    AtencionWeb atencionWeb = await _atencionWebservice.FindAsync(atencionWebSeguimiento.AtencionWebId);
                    if (atencionWeb != null)
                    {
                        if (atencionWeb.EstadoId != atencionWebSeguimientoRequest.EstadoId)
                        {
                            atencionWeb.EstadoId = atencionWebSeguimientoRequest.EstadoId;
                            bool actualizo = await _atencionWebservice.UpdateAsync(atencionWeb.Id, atencionWeb);
                            if (actualizo)
                            {
                                response = new { Titulo = "Bien Hecho!", Mensaje = "Seguimiento creado de forma correcta, se ha cambiado el estado de caso a cerrado", Codigo = HttpStatusCode.OK };
                            }
                        }
                    }
                }                
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
