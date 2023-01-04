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

        public AtencionWebController(
            AtencionWebService service,
            PersonaWebService personaWebService,
            IMapper mapper, 
            IGenericService<AtencionWebAnexo> anexo, 
            ValidacionCorreo validacorreo,
            AzureStorage azureStorage
        )
        {
            this._personaWebService = personaWebService;
            this._atencionWebservice = service;
            this._mapper = mapper;
            this._anexoWebService = anexo;
            this._validacorreo=validacorreo;
            this._azureStorage=azureStorage;
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

                    personaWeb.DtFechaActualizacion = DateTime.Now;
                    personaWeb.DtFechaRegistro      = DateTime.Now;

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
                        atencionWeb.DtFechaRegistro = DateTime.Now;

                        bool guardoatencionWeb = await _atencionWebservice.CreateAsync(atencionWeb);


                        if (guardoatencionWeb && atencionWebRequest.Anexo != null)
                        {
                            var anexo = atencionWebRequest.Anexo;
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
                                    DtFechaRegistro = atencionWeb.DtFechaRegistro,
                                    VcRuta = archivoCarga.rutaLocal
                                };

                                bool guardoAnexo = await _anexoWebService.CreateAsync(atencionwebAnexo);
                            }

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
                response = new { Titulo = "Algo salio mal", Mensaje = "Error cargando el archivo PDf", Codigo = HttpStatusCode.BadRequest };
                var modelResponseError = new ModelResponse<AtencionWeb>(response.Codigo, response.Titulo, response.Mensaje, null);
                return StatusCode((int)modelResponseError.Codigo, modelResponseError);
            }

            var modelResponse = new ModelResponse<PersonaWeb>(response.Codigo, response.Titulo, response.Mensaje, personaWeb);
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

            if (AtencionesWeb.Count() == 0)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No se encontraron actividades con el fitro indicado", Codigo = HttpStatusCode.Accepted };

            }
            var listModelResponse = new ListModelResponse<AtencionWebDTO>(response.Codigo, response.Titulo, response.Mensaje, AtencionesWeb);

            return StatusCode((int)listModelResponse.Codigo, listModelResponse);
        }

        
        [HttpGet("otrosCasos/{PersonaWebId}/{AtencionWebId}")]
        public async Task<ActionResult<ListModelResponse<AtencionWebDTO>>> obtenerOtrosCasosPersonaWeb(long PersonaWebId, long AtencionWebId)
        {

            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se encontraron los casos de atención grupal", Codigo = HttpStatusCode.OK };
            IEnumerable<AtencionWebDTO> AtencionesWeb = null;
            AtencionesWeb = await _atencionWebservice.obtenerPorPersonaWebYExcluyeCaso(PersonaWebId, AtencionWebId);

            if (AtencionesWeb.Count() == 0)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No se encontraron actividades con el fitro indicado", Codigo = HttpStatusCode.Accepted };

            }
            var listModelResponse = new ListModelResponse<AtencionWebDTO>(response.Codigo, response.Titulo, response.Mensaje, AtencionesWeb);

            return StatusCode((int)listModelResponse.Codigo, listModelResponse);

        }
        

        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAtencionWeb(long Id)
        {
            var response = new { Titulo = "", Mensaje = "", Codigo = HttpStatusCode.Accepted };
            AtencionWeb AtencionWebModelModel = null;

            if (!await _atencionWebservice.ExistsAsync(e => e.Id > 0))
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existen atención Web", Codigo = HttpStatusCode.BadRequest };
            }

            var atencionweb = await _atencionWebservice.GetAsync(e => e.Id == Id, e => e.OrderBy(e => e.Id), "");

            if (atencionweb.Count < 1)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existe atención Web con id " + Id, Codigo = HttpStatusCode.NotFound };
            }
            else
            {
                AtencionWebModelModel = atencionweb.First();
                response = new { Titulo = "Bien Hecho!", Mensaje = "Se obtuvo atención Web con el Id solicitado", Codigo = HttpStatusCode.OK };
            }


            var modelResponse = new ModelResponse<AtencionWeb>(response.Codigo, response.Titulo, response.Mensaje, AtencionWebModelModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);
        }


    }
}
