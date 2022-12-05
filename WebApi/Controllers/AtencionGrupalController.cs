using Aplicacion.Services;
using AutoMapper;
using Dominio.Mapper.AtencionesGrupales;
using Dominio.Models.AtencionesGrupales;
using Microsoft.AspNetCore.Mvc;
using System.IO;
using System.Net;

using WebApi.Responses;
using WebApi.Requests;
using Microsoft.AspNetCore.Mvc.ApplicationModels;
using System.Reflection.Metadata;
using static System.Net.WebRequestMethods;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtencionGrupalController : ControllerBase
    {
        private readonly IGenericService<AtencionGrupal> _service;
        private readonly IGenericService<AtencionGrupalAnexo> _anexo;
        private readonly IMapper _mapper;
        public AtencionGrupalController(IGenericService<AtencionGrupal> service, IGenericService<AtencionGrupalAnexo> anexo, IMapper mapper)
        {
            this._service = service;
            this._mapper  = mapper;
            this._anexo   = anexo;
        }


        [HttpPost("crear")]
        public async Task<ActionResult<AtencionGrupal>>  CrearAtencionGrupal([FromForm] AtencionGrupalUnifiedDTO atenciongrupalRequest)
        {
            var response = new { Titulo = "Bien echo!", Mensaje = "Datos cargados", Codigo = HttpStatusCode.OK };
            AtencionGrupal atencionGrupal = _mapper.Map<AtencionGrupal>(atenciongrupalRequest);

            try
            {
                if(validarAnexo(atenciongrupalRequest.Anexo))
                {
                    atencionGrupal.DtFechaRegistro = DateTime.Now;
                    bool guardo = await _service.CreateAsync(atencionGrupal);

                    if (guardo)
                    {
                        var anexo = atenciongrupalRequest.Anexo;
                        var responseCargo = CargarAnexo(anexo, atencionGrupal);

                        if (responseCargo.Result.Codigo == HttpStatusCode.OK)
                        {
                            AtencionGrupalAnexo atencionGrupalAnexo = new AtencionGrupalAnexo
                            {
                                AtencionGrupalId = atencionGrupal.Id,
                                IBytes = (int)anexo.Length,
                                VcNombre = anexo.FileName,
                                UsuarioId = atencionGrupal.UsuarioId,
                                DtFechaRegistro = atencionGrupal.DtFechaRegistro,
                                VcRuta = responseCargo.Result.Codigo == HttpStatusCode.OK ? responseCargo.Result.Mensaje : "//",
                                VcUrl = "//"

                            };

                            bool guardoAnexo = await _anexo.CreateAsync(atencionGrupalAnexo);
                            if (!guardoAnexo)
                            {
                                //Rollback
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


            var modelResponse = new ModelResponse<AtencionGrupal>(response.Codigo, response.Titulo, response.Mensaje, atencionGrupal);
            return StatusCode((int)modelResponse.Codigo, modelResponse);

        }

        private async Task<GenericResponse> CargarAnexo(IFormFile anexo,AtencionGrupal atencionGrupal)
        {
            var  response = new { Titulo = "Bien hecho", Mensaje = "Ruta", Codigo = HttpStatusCode.BadRequest };
            string rutaInicial = Environment.CurrentDirectory;
            string nombreArchivo = anexo.FileName;
            string ruta = rutaInicial + "\\Upload\\AtencionGrupal\\" + atencionGrupal.Id + "\\";
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


        private bool validarAnexo(IFormFile anexo)
        {
            string nombreArchivo = anexo.FileName;
            var archivoArray = nombreArchivo.Split(".");
            var extension = archivoArray[archivoArray.Length - 1];

            return anexo.Length <= 2097152 && extension == "pdf";
          
        }


    }
}




    
