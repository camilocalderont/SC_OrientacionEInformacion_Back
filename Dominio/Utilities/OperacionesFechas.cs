using Dominio.Models.General;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Utilities
{
    public class OperacionesFechas
    {
        public static DiferenciaEntreFechas restar(DateTime fechaFin, DateTime? fechaInicio)
        {
            /*
            * 1. Si retorna -1  entonces la fecha inicial es mayor que la fecha final, entonces no se debe hacer la resta de fechas.
            * 2. Si retonra 0 entonces la fecha incial es igual a la fecha final
            * 3. Si alguno de los atributos de DiferenciaEntreFechas es mayor que cero, entonces se realizó la operacion.
            */
            

            DiferenciaEntreFechas fechaResultado = new DiferenciaEntreFechas();

            if (fechaInicio != null)
            {
                if (fechaInicio.Value.Year > fechaFin.Year)
                {
                    fechaResultado.anios = -1;
                    fechaResultado.meses = -1;
                    fechaResultado.dias = -1;
                    return fechaResultado;
                }

                if ((fechaInicio.Value.Year == fechaFin.Year) && (fechaInicio.Value.Month == fechaFin.Month) && (fechaInicio.Value.Day == fechaFin.Day))
                {
                    return fechaResultado;
                }

                fechaResultado.anios = fechaFin.Year - fechaInicio.Value.Year;

                if (fechaFin.Month >= fechaInicio.Value.Month)
                {
                    fechaResultado.meses = fechaFin.Month - fechaInicio.Value.Month;
                    if (fechaFin.Day >= fechaInicio.Value.Day)
                    {
                        fechaResultado.dias = fechaFin.Day - fechaInicio.Value.Day;
                    }
                }
                else
                {
                    fechaResultado.meses = 0;
                    if (fechaFin.Day >= fechaInicio.Value.Day)
                    {
                        fechaResultado.dias = fechaFin.Day - fechaInicio.Value.Day;
                    }
                    else
                    {
                        fechaResultado.dias = 0;
                    }
                }
                return fechaResultado;
            }
            else
            {
                return fechaResultado;
            }
        }
    }
}
