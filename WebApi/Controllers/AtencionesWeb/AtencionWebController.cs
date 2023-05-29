using Aplicacion.Services;
using AutoMapper;
using Dominio.Models.AtencionesWeb;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Requests.AtencionesWeb;
using WebApi.Responses;
using WebApi.Storage;
using WebApi.Validaciones;
using Dominio.Utilities;
using Aplicacion.Services.AtencionesWeb;
using Aplicacion.Services.AtencionesIndividuales;
using Dominio.Mapper.AtencionesIndividuales;
using Dominio.Mapper.AtencionesWeb;
using Dominio.Models.AtencionesIndividuales;
using WebApi.Requests.AtencionesIndividuales;

namespace WebApi.Controllers.AtencionesWeb
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtencionWebController : ControllerBase
    {
        private readonly PersonaWebService  _personaWebService;
        private readonly AtencionWebService _atencionWebservice;
        private readonly ValidacionCorreo _validacorreo;
        private readonly IGenericService<AtencionWebAnexo> _anexoWebService;
        private readonly IMapper _mapper;

        private readonly AzureStorage _azureStorage;

        private readonly TimeZoneInfo _timeZone;

        public AtencionWebController(
            AtencionWebService service,
            PersonaWebService personaWebService,
            IMapper mapper, 
            IGenericService<AtencionWebAnexo> anexo, 
            ValidacionCorreo validacorreo,
            AzureStorage azureStorage,
            TimeZoneInfo timeZone
        )
        {
            this._personaWebService = personaWebService;
            this._atencionWebservice = service;
            this._mapper = mapper;
            this._anexoWebService = anexo;
            this._validacorreo=validacorreo;
            this._azureStorage=azureStorage;
            this._timeZone = timeZone;
        }


        [HttpPost("crear")]
        public async Task<ActionResult<AtencionWeb>> CrearPersonaWeb([FromForm] AtencionWebRequest atencionWebRequest)
        {

            var response = new { Titulo = "Bien Hecho!", Mensaje = "Datos cargados", Codigo = HttpStatusCode.OK };
            PersonaWeb personaWeb = _mapper.Map<PersonaWeb>(atencionWebRequest);
            AtencionWeb atencionWeb = _mapper.Map<AtencionWeb>(atencionWebRequest);

            try
            {
                if (_azureStorage.validarAnexo(atencionWebRequest.Anexo, Constants.DOSMB, "pdf"))
                {

                    personaWeb.DtFechaActualizacion = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
                    personaWeb.DtFechaRegistro      = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);

                    var personaWebCorreo = _personaWebService.obtenerporCorreo(personaWeb.VcCorreo);

                    var validacorreo = _validacorreo.ValidarEmail(personaWeb.VcCorreo);

                    if (!validacorreo)
                    {
                        response = new { Titulo = "Algo salio mal", Mensaje = "Por favor digita un correo valido", Codigo = HttpStatusCode.BadRequest };
                        return StatusCode((int)response.Codigo, response);
                    }


                    bool guardopersonaWeb = false;

                    if (personaWebCorreo == null)
                    {
                        guardopersonaWeb = await _personaWebService.CreateAsync(personaWeb);
                    }
                    else
                    {
                        personaWeb.Id = personaWebCorreo.Id;
                        guardopersonaWeb = await _personaWebService.UpdateAsync(personaWeb.Id,personaWeb);

                    }

                    if (guardopersonaWeb)
                    {
                        atencionWeb.PersonaWebId = personaWeb.Id;
                        atencionWeb.DtFechaRegistro = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);

                        bool guardoatencionWeb = await _atencionWebservice.CreateAsync(atencionWeb);


                        if (guardoatencionWeb && atencionWebRequest.Anexo != null)
                        {
                            bool guardoAnexo = await guardarAnexo(atencionWebRequest.Anexo, atencionWeb);

                        }

                        else if (!guardoatencionWeb && guardopersonaWeb)
                        {
                            response = new { Titulo = "Algo salio mal", Mensaje = "No se pudo guardar persona web", Codigo = HttpStatusCode.BadRequest };
                        }

                    }
                }
                else
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "El archivo PDF no debe superar los 2 megabytes y tiene que ser de tipo pdf", Codigo = HttpStatusCode.BadRequest };
                    return StatusCode((int)response.Codigo, response);
                }

            }

            catch (Exception)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "Error creando el registro de atención web", Codigo = HttpStatusCode.BadRequest };
                var modelResponseError = new ModelResponse<AtencionWeb>(response.Codigo, response.Titulo, response.Mensaje, null);
                return StatusCode((int)modelResponseError.Codigo, modelResponseError);
            }

            var modelResponse = new ModelResponse<AtencionWeb>(response.Codigo, response.Titulo, response.Mensaje, atencionWeb);
            return StatusCode((int)modelResponse.Codigo, modelResponse);

        }


        [HttpPost("bandeja")]
        public async Task<ActionResult<ListModelResponse<AtencionWebDTO>>> obtenerPorRangoFechasYUsuario(BandejaCasosWebRequest bandejaCasosWeb)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se encontraron los casos de atención grupal", Codigo = HttpStatusCode.OK };
            IEnumerable<AtencionWebDTO> AtencionesWeb = null;
            AtencionesWeb = await _atencionWebservice.obtenerPorRangoFechasEstadoUsuarioYCorreo(
                    bandejaCasosWeb.EstadoId,
                    bandejaCasosWeb.DtFechaInicio,
                    bandejaCasosWeb.DtFechaFin,
                    bandejaCasosWeb.UsuarioId,
                    bandejaCasosWeb.VcCorreo
            );

            if (!AtencionesWeb.Any())
            {
                response = new { Titulo = "No hay registros", Mensaje = "No se encontraron casos de atenciones web  con el fitro indicado", Codigo = HttpStatusCode.OK };
            }
            var listModelResponse = new ListModelResponse<AtencionWebDTO>(response.Codigo, response.Titulo, response.Mensaje, AtencionesWeb);

            return StatusCode((int)listModelResponse.Codigo, listModelResponse);
        }

        [HttpGet("obtenerPorRangoFechasParaReporteOW")]
        public async Task<ActionResult<ListModelResponse<AtencionWebReporteDTO>>> obtenerPorRangoFechasParaReporteOW([FromQuery(Name = "fechaInicio")] DateTime fechaInicio, [FromQuery(Name = "fechaFinal")] DateTime fechaFinal)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se encontraron registros de Atencion Web", Codigo = HttpStatusCode.OK };
            IEnumerable<AtencionWebReporteDTO> atencionIndividualReporteDto = null;

            atencionIndividualReporteDto = await _atencionWebservice.obtenerPorRangoFechasParaReporteOW(fechaInicio, fechaFinal);

            if (!atencionIndividualReporteDto.Any())
            {
                response = new { Titulo = "No hay registros", Mensaje = "No se encontraron registros de Atencion Web con el fitro indicado", Codigo = HttpStatusCode.OK };
            }

            var listModelResponse = new ListModelResponse<AtencionWebReporteDTO>(response.Codigo, response.Titulo, response.Mensaje, atencionIndividualReporteDto);

            return StatusCode((int)listModelResponse.Codigo, listModelResponse);
        }


        [HttpGet("otrosCasos/{PersonaWebId}/{AtencionWebId}")]
        public async Task<ActionResult<ListModelResponse<AtencionWebDTO>>> obtenerOtrosCasosPersonaWeb(long PersonaWebId, long AtencionWebId)
        {

            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se encontraron los casos de atención web", Codigo = HttpStatusCode.OK };
            IEnumerable<AtencionWebDTO> AtencionesWeb = null;
            AtencionesWeb = await _atencionWebservice.obtenerPorPersonaWebYExcluyeCaso(PersonaWebId, AtencionWebId);

            if (!AtencionesWeb.Any())
            {
                response = new { Titulo = "No hay registros", Mensaje = "No se encontraron casos de atenciones web con el fitro indicado", Codigo = HttpStatusCode.OK };
            }
            var listModelResponse = new ListModelResponse<AtencionWebDTO>(response.Codigo, response.Titulo, response.Mensaje, AtencionesWeb);

            return StatusCode((int)listModelResponse.Codigo, listModelResponse);

        }
        

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAtencionWeb(long Id)
        {
            var response = new { Titulo = "", Mensaje = "", Codigo = HttpStatusCode.Accepted };
            AtencionWebDTO AtencionWebModelModel = null;


            var atencionweb = await _atencionWebservice.obtenerPorId(Id);

            if (atencionweb == null)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existe atención Web con id " + Id, Codigo = HttpStatusCode.NoContent };
            }
            else
            {
                AtencionWebModelModel = atencionweb;
                response = new { Titulo = "Bien Hecho!", Mensaje = "Se obtuvo atención Web con el Id solicitado", Codigo = HttpStatusCode.OK };
            }


            var modelResponse = new ModelResponse<AtencionWebDTO>(response.Codigo, response.Titulo, response.Mensaje, AtencionWebModelModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);
        }

        [HttpPost("asociarAnexoCaso")]
        public async Task<ActionResult<bool>> asociarAnexoCasoAtencionIndividual([FromForm] AtencionWebRequest atencionWebRequest)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Datos cargados", Codigo = HttpStatusCode.OK };
            AtencionWeb atencionWeb = _mapper.Map<AtencionWeb>(atencionWebRequest);
            bool guardoAnexo = await guardarAnexo(atencionWebRequest.Anexo, atencionWeb);
            if (!guardoAnexo)
            {
                response = new { Titulo = "Algo salió mal!", Mensaje = "No se pudo cargar el anexo", Codigo = HttpStatusCode.OK };
            }
            var modelResponse = new ModelResponse<bool>(response.Codigo, response.Titulo, response.Mensaje, guardoAnexo);
            return StatusCode((int)modelResponse.Codigo, modelResponse);
        }

        [NonAction]
        public async Task<bool> guardarAnexo(IFormFile Anexo, AtencionWeb atencionWeb)
        {
            bool guardoAnexo = false;
            var anexo = Anexo;
            var nombreEntidad = atencionWeb.GetType().Name;
            string rutaRemota = nombreEntidad + "/" + atencionWeb.Id + "/" + anexo.FileName;
            ArchivoCarga archivoCarga = await _azureStorage.CargarArchivoStream(anexo, rutaRemota);

            if (archivoCarga.rutaLocal.Length > 0)
            {
                AtencionWebAnexo atencionwebAnexo = new AtencionWebAnexo
                {
                    AtencionWebId = atencionWeb.Id,
                    IBytes = (int)anexo.Length,
                    VcNombre = anexo.FileName,
                    UsuarioId = atencionWeb.UsuarioId,
                    DtFechaRegistro = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone),
                    VcRuta = archivoCarga.rutaLocal
                };

                guardoAnexo = await _anexoWebService.CreateAsync(atencionwebAnexo);
            }

            return guardoAnexo;
        }




    }
}
