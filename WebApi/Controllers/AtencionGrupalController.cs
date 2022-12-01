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


        [HttpPost("PostAtencionGrupal")]
        public async Task<ActionResult> PostAtencionGrupal( [FromForm] AtencionGrupalDTO atenciongrupal, [FromForm] List<IFormFile> files)
        {
            var response = new { Titulo = "Bien echo!", Mensaje = "Datos cargados", Codigo = HttpStatusCode.OK};

            try
            { 
                AtencionGrupalAnexo atenciongrupalanexo = new AtencionGrupalAnexo();
          

                AtencionGrupal atenciongrupalDTO = _mapper.Map<AtencionGrupal>(atenciongrupal);
                atenciongrupalDTO.DtFechaRegistro = DateTime.Now;


                //bool grupalanexo = await _anexo.CreateAsync(atenciongrupalanexo);
                bool guardo = await _service.CreateAsync(atenciongrupalDTO);
              

                if (!guardo)
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "No se pudo guardar atención grupal", Codigo = HttpStatusCode.BadRequest };
                }
                var modelResponse = new ModelResponse<AtencionGrupalDTO>(response.Codigo, response.Titulo, response.Mensaje, atenciongrupal);
                return StatusCode((int)modelResponse.Codigo, modelResponse);


                string rutaInicial = Environment.CurrentDirectory;
                var nombreArchivo = files[0].FileName;
                var rutaArchivo = rutaInicial + "/Upload/" + nombreArchivo;
                var archivoArray = nombreArchivo.Split(".");
                var extencion = archivoArray[archivoArray.Length - 1];

                

                //byte[] tamañoArchivo = System.IO.File.ReadAllBytes(rutaArchivo);

                //atenciongrupalanexo.IBytes = tamañoArchivo;
                //atenciongrupalanexo.VcNombre = nombreArchivo;

                // FileInfo file = new FileInfo(rutaArchivo);

                //if (file.Length > 1048576)
                //{
                //    response = new { Titulo = "Algo salio mal", Mensaje = "El archio PDF no debe superar los 2 megabytes", Codigo = HttpStatusCode.BadRequest };
                //    return StatusCode((int)response.Codigo, response);
                //}

                if (files.Count == 0)
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "No se recibio el archivo ", Codigo = HttpStatusCode.BadRequest };
                }


                if (extencion != "pdf")
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "El archio no contiene el formato PDF", Codigo = HttpStatusCode.BadRequest };
                    return StatusCode((int)response.Codigo, response);
                }
                else
                {
                   
                    if (files.Count == 1)
                    {
                        System.IO.File.Delete(rutaArchivo);
                    }

                    using (var str = System.IO.File.Create(rutaArchivo))
                    {
                        str.Position = 0;
                        await files[0].CopyToAsync(str);
                    }
                }

            }
            catch (Exception)
            {
               response = new { Titulo = "Algo salio mal", Mensaje = "Error cargando el archivo PDf", Codigo = HttpStatusCode.BadRequest };
                return StatusCode((int)response.Codigo, response);
            }
          
        }

        [HttpPost("crear")]
        public async Task<ActionResult<AtencionGrupal>>  CrearAtencionGrupal([FromForm] AtencionGrupalUnifiedDTO atenciongrupalRequest)
        {
            var response = new { Titulo = "Bien echo!", Mensaje = "Datos cargados", Codigo = HttpStatusCode.OK };
            AtencionGrupal atencionGrupal = _mapper.Map<AtencionGrupal>(atenciongrupalRequest);

            try
            {
                
                atencionGrupal.DtFechaRegistro = DateTime.Now;
                bool guardo = await _service.CreateAsync(atencionGrupal);

                if (guardo)
                {
                    var anexo = atenciongrupalRequest.Anexo;
                    var responseCargo = CargarAnexo(anexo, atencionGrupal);
                    
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
                }
                else
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "No se pudo guardar la atención grupal", Codigo = HttpStatusCode.BadRequest };
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

        public async Task<GenericResponse> CargarAnexo(IFormFile anexo,AtencionGrupal atencionGrupal)
        {
            var  response = new { Titulo = "Bien hecho", Mensaje = "Ruta", Codigo = HttpStatusCode.BadRequest };
            string rutaInicial = Environment.CurrentDirectory;
            string nombreArchivo = anexo.FileName;
            string ruta = rutaInicial + "\\Upload\\AtencionGrupal\\" + atencionGrupal.Id + "\\";
            var rutaArchivo = ruta + nombreArchivo;    
            var archivoArray = nombreArchivo.Split(".");
            var extension = archivoArray[archivoArray.Length - 1];

            if (anexo.Length > 1048576)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "El archio PDF no debe superar los 2 megabytes", Codigo = HttpStatusCode.BadRequest };
            }

            if (extension != "pdf")
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "El archio no contiene el formato PDF", Codigo = HttpStatusCode.BadRequest };

            }
            else
            {
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
            }

            var genericResponse = new GenericResponse(response.Codigo, response.Titulo, response.Mensaje);

            return genericResponse;
        }


    }
}




    
