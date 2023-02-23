using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Mapper.AtencionesIndividuales
{
    public class AtencionIndividualReporteDto
    {
        public long consecutivo { get; set; }
        public long cas_Id { get; set; }
        public int mes { get; set; }
        public DateTime fechaAlmacenamiento { get; set; }
        public long tipoDocumentoId { get; set; }
        public string numeroIdent { get; set; }
        public string primerApellido { get; set; }
        public string segundoApellido { get; set; }
        public string primerNombre { get; set; }
        public string segundoNombre { get; set; }
        public string edadAlRegistroDelCaso { get; set; }
        public string fechaNacimiento { get; set; }
        public long sexoId { get; set; }
        public long generoId { get; set; }
        public long orientacionSexualId { get; set; }
        public long enfoquePoblacionalId { get; set; }
        public long poblacionPrioritariaId { get; set; }
        public long subPoblacionPrioritariaId { get; set; }
        public long subEtniaId { get; set; }
        public long etniaId { get; set; }
        public long regimenId { get; set; }
        public long aseguradoraId { get; set; }
        public long nivelSisbenId { get; set; }
        public long departamentoId { get; set; }
        public long municipioId { get; set; }
        public string direccion { get; set; }
        public long localidadId { get; set; }
        public long barrioId { get; set; }
        public long upzId { get; set; }
        public string  correoElectronico { get; set; }
        public string telefono1 { get; set; }
        public string Telefono2 { get; set; }
        public string aclaracionesTelfDir { get; set; }
        public long tipoOrientacion { get; set; }
        public long motivoId { get; set; }
        public long subMotivoId { get; set; }
        public string aclaracionMotivoOrientacion { get; set; }
        public string gestionResolucionPAcceso { get; set; }
        public string datosDeContactoAclaración { get; set; }
        public long canalAtencionId { get; set; }
        public long estadoCasoId { get; set; }
        public long usuarioId { get; set; }
    }
}
