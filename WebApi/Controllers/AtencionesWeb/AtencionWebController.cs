using Aplicacion.Services;
using AutoMapper;
using Dominio.Mapper.AtencionesGrupales;
using Dominio.Models.AtencionesGrupales;
using Dominio.Models.AtencionesWeb;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Requests.AtencionesWeb;
using WebApi.Responses;

namespace WebApi.Controllers.AtencionesWeb
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtencionWebController : ControllerBase
    {

        private readonly PersonaWebService _service;
        private readonly IGenericService<AtencionWeb> _atencionWeb;
        private readonly IGenericService<AtencionWebAnexo> _anexo;
        private readonly IMapper _mapper;

        public AtencionWebController(PersonaWebService service, IGenericService<AtencionWeb> atencionWeb, IMapper mapper, IGenericService<AtencionWebAnexo> anexo)
        {
            this._service = service;
            this._atencionWeb = atencionWeb;
            this._mapper = mapper;
            this._anexo = anexo;
        }



        [HttpPost("crear")]
        public async Task<ActionResult<AtencionWeb>> CrearPersonaWeb([FromForm] AtencionWebRequest atencionWebRequest)
        {

            var response = new { Titulo = "Bien echo!", Mensaje = "Datos cargados", Codigo = HttpStatusCode.OK };
            PersonaWeb personaWeb = _mapper.Map<PersonaWeb>(atencionWebRequest);
            AtencionWeb atencionWeb = _mapper.Map<AtencionWeb>(atencionWebRequest);

            try
            {
                if (validarAnexo(atencionWebRequest.Anexo))
                {

                    personaWeb.DtFechaActualizacion = DateTime.Now;
                    personaWeb.DtFechaRegistro = DateTime.Now;

                    var personaWebCorreo = _service.obtenerporCorreo(personaWeb.VcCorreo);

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
                            var responseCargo = CargarAnexo(anexo, atencionWeb);


                            if (responseCargo.Result.Codigo == HttpStatusCode.OK)
                            {
                                AtencionWebAnexo atencionwebAnexo = new AtencionWebAnexo
                                {
                                    AtencionWebId = atencionWeb.Id,
                                    IBytes = (int)anexo.Length,
                                    VcNombre = anexo.FileName,
                                    UsuarioId = atencionWeb.UsuarioId,
                                    DtFechaRegistro = atencionWeb.DtFechaRegistro,
                                    VcRuta = responseCargo.Result.Codigo == HttpStatusCode.OK ? responseCargo.Result.Mensaje : "//",
                                    VcUrl = "//"

                                };

                                bool guardoAnexo = await _anexo.CreateAsync(atencionwebAnexo);

                                if (!guardoAnexo)
                                {
                                    //Rollback
                                }
                            }


                        }

                        else if (!guardoatencionWeb && guardopersonaWeb)
                        {
                            response = new { Titulo = "Algo salio mal", Mensaje = "No se pudo guardar persona web", Codigo = HttpStatusCode.BadRequest };
                        }

                    }

                    //else
                    //{
                    //    response = new { Titulo = "Algo salio mal", Mensaje = "No se pudo guardar persona web", Codigo = HttpStatusCode.BadRequest };
                    //}
                }


                else
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "El archivo PDF no debe superar los 2 megabytes y tiene que ser de tipo pdf", Codigo = HttpStatusCode.BadRequest };
                    return StatusCode((int)response.Codigo, response);
                }

            }

            catch (Exception ex )
            {
                throw;
                //response = new { Titulo = "Algo salio mal", Mensaje = "Error cargando el archivo PDf", Codigo = HttpStatusCode.BadRequest };
                //var modelResponseError = new ModelResponse<AtencionWeb>(response.Codigo, response.Titulo, response.Mensaje, null);
                //return StatusCode((int)modelResponseError.Codigo, modelResponseError);
            }


            var modelResponse = new ModelResponse<PersonaWeb>(response.Codigo, response.Titulo, response.Mensaje, personaWeb);
            return StatusCode((int)modelResponse.Codigo, modelResponse);

        }

        private bool validarAnexo(IFormFile anexo)
        {
            string nombreArchivo = anexo.FileName;
            var archivoArray = nombreArchivo.Split(".");
            var extension = archivoArray[archivoArray.Length - 1];

            return anexo.Length <= 2097152 && extension == "pdf";
        }


        private async Task<GenericResponse> CargarAnexo(IFormFile anexo, AtencionWeb atencionWeb)
        {
            var response = new { Titulo = "Bien hecho", Mensaje = "Ruta", Codigo = HttpStatusCode.BadRequest };
            string rutaInicial = Environment.CurrentDirectory;
            string nombreArchivo = anexo.FileName;
            string ruta = rutaInicial + "\\Upload\\AtencionWeb\\" + atencionWeb.Id + "\\";
            var rutaArchivo = ruta + nombreArchivo;

            var guardoAnexo = false;
            Directory.CreateDirectory(Path.GetDirectoryName(ruta));

            using (var str = System.IO.File.Create(rutaArchivo))
            {
                str.Position = 0;

                await anexo.CopyToAsync(str);
                guardoAnexo = System.IO.File.Exists(rutaArchivo);
            }
            if (guardoAnexo)
            {
                response = new { Titulo = "Bien hecho", Mensaje = rutaArchivo, Codigo = HttpStatusCode.OK };
            }

            var genericResponse = new GenericResponse(response.Codigo, response.Titulo, response.Mensaje);

            return genericResponse;
        }





        [HttpGet("GetAtencionWeb")]
        public async Task<ActionResult<IEnumerable<AtencionWebRequest>>> GetAtencionWeb()
        {
            try
            {
                var response = new { Titulo = "Bien Hecho!", Mensaje = "Se encontraron atenciones web", Codigo = HttpStatusCode.OK };

                IEnumerable<AtencionWeb> AtencionWebModel = null;

                AtencionWebModel = (IEnumerable<AtencionWeb>)await _service.GetAsync();

                List<AtencionWebRequest> atencionwebDTO = _mapper.Map<List<AtencionWebRequest>>(AtencionWebModel);

                if (!await _service.ExistsAsync(e => e.Id > 0))
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
