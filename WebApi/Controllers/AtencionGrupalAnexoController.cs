using Aplicacion.Services;
using AutoMapper;
using Dominio.Mapper.AtencionesGrupales;
using Dominio.Models.AtencionesGrupales;
using Microsoft.AspNetCore.Mvc;
using System.IO.Compression;
using System.Net;
using WebApi.Responses;
using IHostingEnvironment = Microsoft.AspNetCore.Hosting.IHostingEnvironment;

namespace WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class AtencionGrupalAnexoController : ControllerBase
    {
        private readonly IGenericService<AtencionGrupalAnexo> _service;
        private readonly IMapper _mapper;

        private const string rutaArchivo = @"C:\Users\erojas\3D Objects\AtencionGrupalBack\ORIENTACION_INFORMACION_Back\WebApi\Upload";

        public AtencionGrupalAnexoController(IGenericService<AtencionGrupalAnexo> service, IMapper mapper)
        {
            this._service = service;
            this._mapper= mapper;
        }


      
        [HttpGet("GetAnexoCaso")]
        public async Task<ActionResult<IEnumerable<AnexosCasosDTO>>> GetAnexoCaso()
        {
            try
            {
                var response = new { Titulo = "Bien Hecho!", Mensaje = "Se encontraron anexos caso", Codigo = HttpStatusCode.OK };

                IEnumerable<AtencionGrupalAnexo> AtencionActorModel = null;

                AtencionActorModel = await _service.GetAsync();
         
                List<AnexosCasosDTO> atencionactorDTO = _mapper.Map<List<AnexosCasosDTO>>(AtencionActorModel);
                
                //var atencionactorDTO = _mapper.Map<List<AnexosCasosDTO>>(AtencionActorModel);

                if (!await _service.ExistsAsync(e => e.Id > 0))
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "No existen anexos caso", Codigo = HttpStatusCode.Accepted };
                }

                var listModelResponse = new ListModelResponse<AnexosCasosDTO>(response.Codigo, response.Titulo, response.Mensaje, atencionactorDTO);
                return StatusCode((int)listModelResponse.Codigo, listModelResponse);

            }

            catch (Exception)
            {
                var response = new { Titulo = "Algo salio mal", Mensaje = "Mostrando anexos caso", Codigo = HttpStatusCode.RequestedRangeNotSatisfiable };
                return StatusCode((int)response.Codigo, response);
            }

           
        }




        [HttpPost("PostAtencionGrupalAnexo")]
        public async Task<IActionResult> PostAtencionGrupalAnexo(AtencionGrupalAnexoDTO atenciongrupalanexo )
        {

            try
            {
                var response = new { Titulo = "Bien Hecho!", Mensaje = "Atencion grupal anexo creado de forma correcta", Codigo = HttpStatusCode.Created };
                var atenciongrupalanexoDTO = _mapper.Map<AtencionGrupalAnexo>(atenciongrupalanexo);

                atenciongrupalanexoDTO.DtFechaRegistro = DateTime.Now;

                bool guardo = await _service.CreateAsync(atenciongrupalanexoDTO);

                if (!guardo)
                {
                    response = new { Titulo = "Algo salio mal", Mensaje = "No se pudo guardar atención grupal anexo", Codigo = HttpStatusCode.BadRequest };
                }
                var modelResponse = new ModelResponse<AtencionGrupalAnexoDTO>(response.Codigo, response.Titulo, response.Mensaje, atenciongrupalanexo);
                return StatusCode((int)modelResponse.Codigo, modelResponse);
            }
            catch (Exception)
            {
                var response = new { Titulo = "Algo salio mal", Mensaje = "Creando atención grupal anexo", Codigo = HttpStatusCode.RequestedRangeNotSatisfiable };
                return StatusCode((int)response.Codigo, response); throw;
            }
          
        }

    }

}
