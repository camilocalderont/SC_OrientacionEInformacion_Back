using Aplicacion.Services;
using Aplicacion.Services.AtencionesGrupales;
using AutoMapper;
using Dominio.Models.AtencionesGrupales;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Responses;
using WebApi.Requests.AtencionesGrupales;
using Dominio.Utilities;
using WebApi.Storage;
using Dominio.Mapper.AtencionesGrupales;

namespace WebApi.Controllers.AtencionesGrupales
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtencionGrupalController : ControllerBase
    {
        private readonly AtencionGrupalService _service;
        private readonly IGenericService<AtencionGrupalAnexo> _anexo;
        private readonly IMapper _mapper;

        private readonly AzureStorage _azureStorage;
        private readonly TimeZoneInfo _timeZone;
        public AtencionGrupalController(
            AtencionGrupalService service, 
            IGenericService<AtencionGrupalAnexo> anexo, 
            IMapper mapper, 
            AzureStorage azureStorage,
            TimeZoneInfo timeZone
        )
        {
            _service = service;
            _mapper = mapper;
            _anexo = anexo;
            _azureStorage = azureStorage;
            _timeZone = timeZone;
        }


        [HttpPost("crear")]
        public async Task<ActionResult<AtencionGrupal>> CrearAtencionGrupal([FromForm] AtencionGrupalRequest atenciongrupalRequest)
        {
            var response = new { Titulo = "Bien hecho!", Mensaje = "La atención grupal ha sido registrada con código No. {0}", Codigo = HttpStatusCode.OK };
            AtencionGrupal atencionGrupal = _mapper.Map<AtencionGrupal>(atenciongrupalRequest);
            //using (var transaction = new TransactionScope())
            //{

                try
                {
                    if (_azureStorage.validarAnexo(atenciongrupalRequest.Anexo,Constants.DOSMB,"pdf"))
                    {
                        atencionGrupal.DtFechaRegistro = TimeZoneInfo.ConvertTimeFromUtc(DateTime.UtcNow, _timeZone);
                        bool guardo = await _service.CreateAsync(atencionGrupal);

                        if (guardo)
                        {
                            var anexo = atenciongrupalRequest.Anexo;
                            var nombreEntidad = atencionGrupal.GetType().Name;
                            string rutaRemota = nombreEntidad +"/" + atencionGrupal.Id + "/" + anexo.FileName;
                            ArchivoCarga archivoCarga = await _azureStorage.CargarArchivoStream(anexo, rutaRemota);

                            if (archivoCarga.rutaLocal.Length > 0)
                            {
                                AtencionGrupalAnexo atencionGrupalAnexo = new AtencionGrupalAnexo
                                {
                                    AtencionGrupalId = atencionGrupal.Id,
                                    IBytes = (int)anexo.Length,
                                    VcNombre = anexo.FileName,
                                    UsuarioId = atencionGrupal.UsuarioId,
                                    DtFechaRegistro = atencionGrupal.DtFechaRegistro,
                                    VcRuta = archivoCarga.rutaLocal

                                };

                                bool guardoAnexo = await _anexo.CreateAsync(atencionGrupalAnexo);
                                if (guardoAnexo)
                                {
                                    //transaction.Complete();
                                }
                            }
                            else
                            {
                                //Rollback
                            }



                        }
                        else
                        {
                            //Rollback
                            response = new { Titulo = "Algo salio mal", Mensaje = "No se pudo guardar la atención grupal", Codigo = HttpStatusCode.BadRequest };
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
                    var modelResponseError = new ModelResponse<AtencionGrupal>(response.Codigo, response.Titulo, response.Mensaje, null);
                    return StatusCode((int)modelResponseError.Codigo, modelResponseError);
                }

            //}

            var modelResponse = new ModelResponse<AtencionGrupal>(response.Codigo, response.Titulo, string.Format(response.Mensaje, atencionGrupal.Id), atencionGrupal);
            return StatusCode((int)modelResponse.Codigo, modelResponse);

        }

        [HttpPost("bandeja")]
        public async Task<ActionResult<ListModelResponse<BandejaGrupalDto>>> obtenerPorRangoFechasYUsuario(BandejaCasosGrupalRequest bandejaCasosGrupal)
        {

            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se encontraron los casos de atención grupal", Codigo = HttpStatusCode.OK };
            IEnumerable<BandejaGrupalDto> AtencionesGrupales = null;
            AtencionesGrupales = await _service.obtenerPorRangoFechasYUsuario(bandejaCasosGrupal.DtFechaInicio, bandejaCasosGrupal.DtFechaFin, bandejaCasosGrupal.UsuarioId);

            if (AtencionesGrupales.Count() == 0)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No se encontraron casos de atención grupal con el fitro indicado", Codigo = HttpStatusCode.NoContent };

            }
            var listModelResponse = new ListModelResponse<BandejaGrupalDto>(response.Codigo, response.Titulo, response.Mensaje, AtencionesGrupales);

            return StatusCode((int)listModelResponse.Codigo, listModelResponse);

        }



        [HttpGet("obtenerPorRangoFechas")]
        public async Task<ActionResult<ListModelResponse<AtencionGrupal>>> obtenerPorRangoFechas([FromQuery(Name = "fechaInicio")] DateTime fechaInicio, [FromQuery(Name = "fechaFinal")] DateTime fechaFinal)
        {

            var response = new { Titulo = "Bien Hecho!", Mensaje = "Se encontraron los casos de atención grupal", Codigo = HttpStatusCode.OK };
            IEnumerable<AtencionGrupal> AtencionesGrupales = null;
            AtencionesGrupales = await _service.obtenerPorRangoFechas(fechaInicio, fechaFinal);

            if (AtencionesGrupales.Count() == 0)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No se encontraron casos de atención grupal con el fitro indicado", Codigo = HttpStatusCode.NoContent };

            }
            var listModelResponse = new ListModelResponse<AtencionGrupal>(response.Codigo, response.Titulo, response.Mensaje, AtencionesGrupales);

            return StatusCode((int)listModelResponse.Codigo, listModelResponse);

        }





        [HttpGet("{Id}")]
        public async Task<IActionResult> GetAtencionGrupal(long Id)
        {
            var response = new { Titulo = "", Mensaje = "", Codigo = HttpStatusCode.Accepted };
            AtencionGrupalDTO AtencionGrupalModel = null;

            var atenciongrupal = await _service.obtenerPorId(Id);

            if (atenciongrupal == null)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existe atención grupal con id " + Id, Codigo = HttpStatusCode.OK };
            }
            else
            {
                AtencionGrupalModel = atenciongrupal;
                response = new { Titulo = "Bien Hecho!", Mensaje = "Se obtuvo atención grupal con el Id solicitado", Codigo = HttpStatusCode.OK };
            }


            var modelResponse = new ModelResponse<AtencionGrupalDTO>(response.Codigo, response.Titulo, response.Mensaje, AtencionGrupalModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);
        }


        [HttpPost("anexo")]
        public  ActionResult cargarAnexo([FromForm] List<IFormFile> anexo)
        {
            var response = "";


            try
            {
                using (var ms = new MemoryStream())
                {

                    anexo[0].CopyTo(ms);
                    var fileBytes = ms.ToArray();
                    response = Convert.ToBase64String(fileBytes);
                }
                return StatusCode(200, response);
            }
            catch (Exception ex)
            {
                response = ex.Message;
                
                return StatusCode(200, response);
            }          
        }


        [HttpPost("cargar")]
        public async Task<ActionResult> Cargar([FromForm] List<IFormFile> files)
        {
            

            var response = "";
            try
            {
                var errores = new List<string>();
                var registros = 0;

                if (files.Count == 0)
                {
                    response = "Algo salio mal";
                }

                var nombreArchivo = files[0].FileName;  

                response = nombreArchivo;

            }
            catch (Exception ex)
            {
                response =ex.Message;
            }
           

            return StatusCode(200, response);
        }


    }

}





