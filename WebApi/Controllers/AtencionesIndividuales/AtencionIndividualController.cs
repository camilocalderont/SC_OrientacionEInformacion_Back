using Aplicacion.Services;
using AutoMapper;
using Dominio.Models.AtencionesIndividuales;
using Dominio.Mapper.AtencionesIndividuales;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Requests.AtencionesIndividuales;
using WebApi.Responses;
using WebApi.Storage;
using WebApi.Validaciones;
using Dominio.Utilities;
using Aplicacion.Services.AtencionesIndividuales;

namespace WebApi.Controllers.AtencionesIndividuales
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtencionIndividualController : ControllerBase
    {
        private readonly PersonaService  _personaService;
        private readonly AtencionIndividualService _atencionIndividualservice;
        private readonly ValidacionCorreo _validacorreo;
        private readonly IGenericService<AtencionIndividualAnexo> _anexoIndividualService;
        private readonly IMapper _mapper;

        private readonly AzureStorage _azureStorage;

        public AtencionIndividualController(
            AtencionIndividualService service,
            PersonaService personaService,
            IMapper mapper, 
            IGenericService<AtencionIndividualAnexo> anexo, 
            ValidacionCorreo validacorreo,
            AzureStorage azureStorage
        )
        {
            this._personaService = personaService;
            this._atencionIndividualservice = service;
            this._mapper = mapper;
            this._anexoIndividualService = anexo;
            this._validacorreo=validacorreo;
            this._azureStorage=azureStorage;
        }


        [HttpPost("crear")]
        public async Task<ActionResult<AtencionIndividual>> CrearAtencionIndividual([FromForm] AtencionIndividualRequest atencionIndividualRequest)
        {

            var response = new { Titulo = "Bien Hecho!", Mensaje = "Datos cargados", Codigo = HttpStatusCode.OK };
            AtencionIndividual atencionIndividual = _mapper.Map<AtencionIndividual>(atencionIndividualRequest);

            try
            {
                if (_azureStorage.validarAnexo(atencionIndividualRequest.Anexo, Constants.DOSMB, "pdf"))
                {                   
                    bool guardoatencionIndividual = await _atencionIndividualservice.CreateAsync(atencionIndividual);
                    if (guardoatencionIndividual && atencionIndividualRequest.Anexo != null)
                    {
                        var anexo = atencionIndividualRequest.Anexo;
                        var nombreEntidad = atencionIndividual.GetType().Name;
                        string rutaRemota = nombreEntidad + "/" + atencionIndividual.Id + "/" + anexo.FileName;
                        ArchivoCarga archivoCarga = await _azureStorage.CargarArchivoStream(anexo, rutaRemota);                          

                        if (archivoCarga.rutaLocal.Length > 0)
                        {
                            AtencionIndividualAnexo atencionIndividualAnexo = new AtencionIndividualAnexo
                            {
                                AtencionIndividualId = atencionIndividual.Id,
                                IBytes = (int)anexo.Length,
                                VcNombre = anexo.FileName,
                                UsuarioId = atencionIndividual.UsuarioId,
                                DtFechaRegistro = atencionIndividual.DtFechaRegistro,
                                VcRuta = archivoCarga.rutaLocal
                            };

                            bool guardoAnexo = await _anexoIndividualService.CreateAsync(atencionIndividualAnexo);
                        }

                    }
                    else
                    {
                        response = new { Titulo = "Algo salio mal", Mensaje = "No se pudo guardar el caso atención individual", Codigo = HttpStatusCode.BadRequest };
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
                response = new { Titulo = "Algo salio mal", Mensaje = "Error creando el registro de caso atención individual", Codigo = HttpStatusCode.BadRequest };
                var modelResponseError = new ModelResponse<AtencionIndividual>(response.Codigo, response.Titulo, response.Mensaje, null);
                return StatusCode((int)modelResponseError.Codigo, modelResponseError);
            }

            var modelResponse = new ModelResponse<AtencionIndividual>(response.Codigo, response.Titulo, response.Mensaje, atencionIndividual);
            return StatusCode((int)modelResponse.Codigo, modelResponse);

        }


        [HttpPost("bandeja")]
        public async Task<ActionResult<ListModelResponse<AtencionIndividualDTO>>> obtenerPorRangoFechasYUsuario(BandejaCasosIndividualRequest bandejaCasosIndividual)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se encontraron los casos de atención individual", Codigo = HttpStatusCode.OK };
            IEnumerable<AtencionIndividualDTO> AtencionesIndividuales = null;
            AtencionesIndividuales = await _atencionIndividualservice.obtenerPorRangoFechasEstadoUsuarioYDocumento(
                    bandejaCasosIndividual.EstadoId,
                    bandejaCasosIndividual.DtFechaInicio,
                    bandejaCasosIndividual.DtFechaFin,
                    bandejaCasosIndividual.UsuarioId,
                    bandejaCasosIndividual.VcDocumento
            );

            if (!AtencionesIndividuales.Any())
            {
                response = new { Titulo = "No hay registros", Mensaje = "No se encontraron casos de atención individual con el fitro indicado", Codigo = HttpStatusCode.OK };
            }
            var listModelResponse = new ListModelResponse<AtencionIndividualDTO>(response.Codigo, response.Titulo, response.Mensaje, AtencionesIndividuales);

            return StatusCode((int)listModelResponse.Codigo, listModelResponse);
        }

        
        [HttpGet("otrosCasos/{PersonaId}/{AtencionIndividualId}")]
        public async Task<ActionResult<ListModelResponse<AtencionIndividualDTO>>> obtenerOtrosCasosPersona(long PersonaId, long AtencionIndividualId)
        {

            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se encontraron los casos de atención grupal", Codigo = HttpStatusCode.OK };
            IEnumerable<AtencionIndividualDTO> AtencionesIndividuales = null;
            AtencionesIndividuales = await _atencionIndividualservice.obtenerPorPersonaYExcluyeCaso(PersonaId, AtencionIndividualId);

            if (!AtencionesIndividuales.Any())
            {
                response = new { Titulo = "No hay registros", Mensaje = "No se encontraron actividades con el fitro indicado", Codigo = HttpStatusCode.OK };
            }
            var listModelResponse = new ListModelResponse<AtencionIndividualDTO>(response.Codigo, response.Titulo, response.Mensaje, AtencionesIndividuales);

            return StatusCode((int)listModelResponse.Codigo, listModelResponse);

        }
        

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAtencionIndividual(long Id)
        {
            var response = new { Titulo = "", Mensaje = "", Codigo = HttpStatusCode.Accepted };
            AtencionIndividualDTO AtencionIndividualModelModel = null;


            var atencionIndividual = await _atencionIndividualservice.obtenerPorId(Id);

            if (atencionIndividual == null)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existe atención individual con id " + Id, Codigo = HttpStatusCode.NoContent };
            }
            else
            {
                AtencionIndividualModelModel = atencionIndividual;
                response = new { Titulo = "Bien Hecho!", Mensaje = "Se obtuvo atención individual con el Id solicitado", Codigo = HttpStatusCode.OK };
            }


            var modelResponse = new ModelResponse<AtencionIndividualDTO>(response.Codigo, response.Titulo, response.Mensaje, AtencionIndividualModelModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);
        }




    }
}
