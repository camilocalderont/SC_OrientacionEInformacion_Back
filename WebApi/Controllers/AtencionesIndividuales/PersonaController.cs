using Aplicacion.Services;
using AutoMapper;
using Dominio.Models.AtencionesIndividuales;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Requests.AtencionesIndividuales;
using WebApi.Responses;
using WebApi.Storage;
using WebApi.Validaciones;
using Dominio.Utilities;
using Aplicacion.Services.AtencionesIndividuales;

namespace WebApi.Controllers.AtencionesWeb
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaController : ControllerBase
    {
        private readonly PersonaService  _personaService;
        private readonly PersonaAfiliacionService  _personaAfiliacionService;
        private readonly PersonaContactoService  _personaContactoService;
        private readonly ValidacionCorreo _validacorreo;
        private readonly IGenericService<AtencionIndividualAnexo> _anexoIndividualService;
        private readonly IMapper _mapper;

        private readonly AzureStorage _azureStorage;

        public PersonaController(
            PersonaService personaService,
            PersonaAfiliacionService personaAfiliacionService,
            PersonaContactoService personaContactoService,
            IMapper mapper, 
            IGenericService<AtencionIndividualAnexo> anexo, 
            ValidacionCorreo validacorreo,
            AzureStorage azureStorage
        )
        {
            this._personaService = personaService;
            this._mapper = mapper;
            this._anexoIndividualService = anexo;
            this._validacorreo=validacorreo;
            this._azureStorage=azureStorage;
            this._personaAfiliacionService = personaAfiliacionService;
            this._personaContactoService = personaContactoService;
        }

        [HttpGet("{tipoDocumentoId}/{vcDocumento}")]
        public async Task<IActionResult> obtenerPorTipoDocumentoyDocumento(long tipoDocumentoId,string vcDocumento)
        {
            var response = new { Titulo = "", Mensaje = "", Codigo = HttpStatusCode.Accepted };
            Persona PersonaModel = null;


            var persona = await _personaService.obtenerPorTipoDocumentoyDocumento(tipoDocumentoId,vcDocumento);

            if (persona == null)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = $"No existe la persona Web con tipo de documento: {tipoDocumentoId} y documento {vcDocumento}  "  , Codigo = HttpStatusCode.OK };
            }
            else
            {
                PersonaModel = persona;
                response = new { Titulo = "Bien Hecho!", Mensaje = "Se obtuvo atención Web con el tipo de documento y documento solicitado", Codigo = HttpStatusCode.OK };
            }

            var modelResponse = new ModelResponse<Persona>(response.Codigo, response.Titulo, response.Mensaje, PersonaModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);
        }

        [HttpPost("crear")]
        public async Task<ActionResult<Persona>> CrearPersona(PersonaRequest personaRequest)
        {
            var response = new { Titulo = "Bien Hecho!", Mensaje = "Datos cargados", Codigo = HttpStatusCode.OK };
            Persona persona = _mapper.Map<Persona>(personaRequest);
            PersonaAfiliacion personaAfiliacion = _mapper.Map<PersonaAfiliacion>(personaRequest);
            PersonaContacto personaContacto = _mapper.Map<PersonaContacto>(personaRequest);

            try
            {
                persona.DtFechaActualizacion = DateTime.Now;
                persona.DtFechaRegistro      = DateTime.Now;

                bool guardoPersona = false;
                bool guardoPersonaAfiliacionContacto = false;
                var personaBusqueda = await _personaService.obtenerPorTipoDocumentoyDocumento(persona.TipoDocumentoId,persona.VcDocumento);
                if (personaBusqueda == null)
                {
                    guardoPersona = await _personaService.CreateAsync(persona);
                }
                else
                {
                    persona.Id = personaBusqueda.Id;
                    guardoPersona = await _personaService.UpdateAsync(persona.Id,persona);

                }

                if (guardoPersona)
                {
                    personaAfiliacion.PersonaId = persona.Id;
                    personaAfiliacion.DtFechaRegistro = persona.DtFechaRegistro;

                    personaContacto.PersonaId = persona.Id;
                    personaContacto.DtFechaRegistro = persona.DtFechaRegistro;        

                    //Obtener el último persona afiliacion
                    //Si existe compare con los datos que vienen del request
                    bool guardoPersonaAfiliacion = await _personaAfiliacionService.CreateAsync(personaAfiliacion);


                    //Obtener el último persona contacto
                    //Si existe compare con los datos que vienen del request                     
                    bool guardoPersonaContacto = await _personaContactoService.CreateAsync(personaContacto);
                    guardoPersonaAfiliacionContacto = guardoPersonaAfiliacion && guardoPersonaContacto;
                }
                else{
                    guardoPersonaAfiliacionContacto = false;
                }   

                if(!guardoPersonaAfiliacionContacto)
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "Error creando el registro de persona web", Codigo = HttpStatusCode.OK };
                    var modelResponseError = new ModelResponse<Persona>(response.Codigo, response.Titulo, response.Mensaje, null);
                    return StatusCode((int)modelResponseError.Codigo, modelResponseError);
                }

            }
            catch (Exception)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "Error creando el registro de persona web", Codigo = HttpStatusCode.OK };
                var modelResponseError = new ModelResponse<Persona>(response.Codigo, response.Titulo, response.Mensaje, null);
                return StatusCode((int)modelResponseError.Codigo, modelResponseError);
            }

            var modelResponse = new ModelResponse<Persona>(response.Codigo, response.Titulo, response.Mensaje, persona);
            return StatusCode((int)modelResponse.Codigo, modelResponse);
          
        }


        /*
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
                response = new { Titulo = "No hay registros", Mensaje = "No se encontraron actividades con el fitro indicado", Codigo = HttpStatusCode.OK };
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

            if (!AtencionesWeb.Any())
            {
                response = new { Titulo = "No hay registros", Mensaje = "No se encontraron actividades con el fitro indicado", Codigo = HttpStatusCode.OK };
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

        */


    }
}
