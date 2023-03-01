using ClosedXML.Excel;
using Dominio.Models.AtencionesIndividuales;
using Dominio.Request;
using Microsoft.Win32;
using Persistencia.Repository;
using DateTime = System.DateTime;

namespace Aplicacion.AgregarExcel
{
    public interface IAgregarExcel
    {
        Task<PersonaRequest> procesarArchivo(Stream ruta, long UsuarioId);
    }

    public class AgregarExcel   : IAgregarExcel 
    {
        public PersonaRepository _personaRepository { get; }

        public AgregarExcel(PersonaRepository personaRepository)
        {
            this._personaRepository = personaRepository;
        }

        public async Task<PersonaRequest> procesarArchivo(Stream ruta, long UsuarioId)
        {
            List<Persona> personas = new List<Persona>();
            List<string> errores = new List<string>();
            int registros = 0;

            using (XLWorkbook workBook = new XLWorkbook(ruta))
            {
                var worksheet = workBook.Worksheet(1);

                try
                {
                    int fila = 2;

                    foreach (var row in worksheet.RowsUsed().Skip(1))
                    {
                        long TipoDocumento = (long)worksheet.Cell(fila, 1).Value;


                        string? Documento = worksheet.Cell(fila, 2).Value + "";


                        string? PrimerNombre = worksheet.Cell(fila, 3).Value + "";


                        string? SegundoNombre = (string)worksheet.Cell(fila, 4).Value;


                        string? PrimerApellido = (string)worksheet.Cell(fila, 5).Value;


                        string? SegundoApellido = (string)worksheet.Cell(fila, 6).Value;

                        if (TipoDocumento <= 0)
                        {
                            errores.Add("El tipo de documento, no puede estar vacio o tener texto en la celda (A" + row.ToString() + ")");
                            break;
                        }

                        if (String.IsNullOrEmpty(Documento))
                        {
                            errores.Add("El parámetro 'DOCUMENTO_IDENTIDAD', no puede ser vacio en la celda (B" + row.ToString() + ")");
                            break;
                        }

                        if (String.IsNullOrEmpty(PrimerNombre))
                        {
                            errores.Add("El parámetro 'PRIMER_NOMBRE', no puede ser vacio en la celda (C" + row.ToString() + ")");
                            break;
                        }

                        if (String.IsNullOrEmpty(SegundoNombre))
                        {
                            errores.Add("El parámetro 'SEGUNDO_NOMBRE', no puede ser vacio en la celda (D" + row.ToString() + ")");
                            break;
                        }

                        if (String.IsNullOrEmpty(PrimerApellido))
                        {
                            errores.Add("El parámetro 'PRIMER_APELLIDO', no puede ser vacio en la celda (E" + row.ToString() + ")");
                            break;
                        }

                        if (String.IsNullOrEmpty(SegundoApellido))
                        {
                            errores.Add("El parámetro 'SEGUNDO_APELLIDO', no puede ser vacio en la celda (F" + row.ToString() + ")");
                            break;
                        }

                        Persona persona = new Persona
                        {
                            TipoDocumentoId = TipoDocumento,
                            VcDocumento = Documento,
                            VcPrimerNombre = PrimerNombre,
                            VcSegundoNombre = SegundoNombre,
                            VcPrimerApellido = PrimerApellido,
                            VcSegundoApellido = SegundoApellido,
                            GeneroId = 1593,
                            OrientacionSexualId = 1599,
                            SexoId = 1602,
                            EnfoquePoblacionalId = 1239,
                            EtniaId = 1608,
                            PoblacionPrioritariaId = 1624,
                            DtFechaRegistro = DateTime.Now,
                            UsuarioId = UsuarioId,
                        };

                        personas.Add(persona);

                        fila++;
                    }

                    if(errores.Count>0)
                    {
                        personas = new List<Persona>();
                        registros = 0;
                    }
                    else
                    {
                       registros = await _personaRepository.agregarVarios(personas);
                    }

                }
                catch (Exception ex)
                {
                    Console.WriteLine("Error: "+ex.Message);
                    errores.Add(ex.Message);
                }

                return new PersonaRequest
                {
                    Errores = errores,
                    Registros = registros
                };
            } 
        }
    }
}
        







