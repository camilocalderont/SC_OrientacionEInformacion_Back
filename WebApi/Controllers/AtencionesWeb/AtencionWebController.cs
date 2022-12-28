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
using Dominio.Models.AtencionesGrupales;
using WebApi.Requests.AtencionesGrupales;

namespace WebApi.Controllers.AtencionesWeb
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtencionWebController : ControllerBase
    {

        private readonly PersonaWebService _service;
        private readonly ValidacionCorreo _validacorreo;
        private readonly IGenericService<AtencionWeb> _atencionWeb;
        private readonly IGenericService<AtencionWebAnexo> _anexo;
        private readonly IMapper _mapper;

        private readonly AzureStorage _azureStorage;

        public AtencionWebController(
            PersonaWebService service, 
            IGenericService<AtencionWeb> atencionWeb, 
            IMapper mapper, 
            IGenericService<AtencionWebAnexo> anexo, 
            ValidacionCorreo validacorreo,
            AzureStorage azureStorage
        )
        {
            this._service = service;
            this._atencionWeb = atencionWeb;
            this._mapper = mapper;
            this._anexo = anexo;
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

                    var personaWebCorreo = _service.obtenerporCorreo(personaWeb.VcCorreo);

                    var validacorreo = _validacorreo.ValidarEmail(personaWeb.VcCorreo);

                    if (!validacorreo)
                    {
                        response = new { Titulo = "Algo salio mal", Mensaje = "Por favor digita un correo valido", Codigo = HttpStatusCode.BadRequest };
                        return StatusCode((int)response.Codigo, response);
                    }


                    bool guardopersonaWeb = false;

                    if (personaWebCorreo == null)
                    {
                        guardopersonaWeb = await _service.CreateAsync(personaWeb);
                    }
                    else
                    {
                        personaWeb.Id = personaWebCorreo.Id;
                        guardopersonaWeb = true;
                    }

                    if (guardopersonaWeb)
                    {
                        atencionWeb.PersonaWebId = personaWeb.Id;
                        atencionWeb.DtFechaRegistro = DateTime.Now;

                        bool guardoatencionWeb = await _atencionWeb.CreateAsync(atencionWeb);


                        if (guardoatencionWeb)
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

                                bool guardoAnexo = await _anexo.CreateAsync(atencionwebAnexo);
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

       
        [HttpGet("GetAtencionWeb")]
        public async Task<ActionResult<IEnumerable<AtencionWebRequest>>> GetAtencionWeb()
        {
            try
            {
                var response = new { Titulo = "Bien Hecho!", Mensaje = "Se encontraron atenciones web", Codigo = HttpStatusCode.OK };

                IEnumerable<AtencionWeb> AtencionWebModel = null;

                AtencionWebModel = await _atencionWeb.GetAsync();

                List<AtencionWebRequest> atencionwebDTO = _mapper.Map<List<AtencionWebRequest>>(AtencionWebModel);

                if (!await _atencionWeb.ExistsAsync(e => e.Id > 0))
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "No existen atenciones web", Codigo = HttpStatusCode.Accepted };
                }

                var listModelResponse = new ListModelResponse<AtencionWebRequest>(response.Codigo, response.Titulo, response.Mensaje, atencionwebDTO);
                return StatusCode((int)listModelResponse.Codigo, listModelResponse);

            }

            catch (Exception)
            {
                var response = new { Titulo = "Algo salio mal", Mensaje = "Mostrando atenciones web", Codigo = HttpStatusCode.RequestedRangeNotSatisfiable };
                return StatusCode((int)response.Codigo, response);
            }

        }

    }
}
