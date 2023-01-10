
using Aplicacion.Services.AtencionesWeb;
using Dominio.Models.AtencionesWeb;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using WebApi.Responses;

namespace WebApi.Controllers.AtencionesWeb
{
    [Route("api/[controller]")]
    [ApiController]
    public class PersonaWebController : ControllerBase
    {

        private readonly PersonaWebService _service;


        public PersonaWebController(PersonaWebService service)
        {
            this._service = service;
        }


        [HttpGet("{VcCorreo}")]
        public async Task<IActionResult> ObtenerporCorreo(string VcCorreo)
        {
            var response = new { Titulo = "", Mensaje = "", Codigo = HttpStatusCode.Accepted };
            PersonaWeb PersonaWebModel = null;


            var personaWeb = _service.obtenerporCorreo(VcCorreo);

            if (personaWeb == null)
            {
                response = new { Titulo = "Algo salio mal", Mensaje = "No existe la persona Web con correo " + VcCorreo, Codigo = HttpStatusCode.OK };
            }
            else
            {
                PersonaWebModel = personaWeb;
                response = new { Titulo = "Bien Hecho!", Mensaje = "Se obtuvo atención Web con el Id solicitado", Codigo = HttpStatusCode.OK };
            }


            var modelResponse = new ModelResponse<PersonaWeb>(response.Codigo, response.Titulo, response.Mensaje, PersonaWebModel);
            return StatusCode((int)modelResponse.Codigo, modelResponse);
        }


    }
}
