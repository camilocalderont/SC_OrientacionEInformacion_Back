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
            // Inicializamos los valores de la diferencia en 0.
            int anios = 0;
            int meses = 0;
            int dias = 0;

            // Si la fechaInicio es null o si es mayor que la fecha final, retornamos "0 años, 0 meses, 0 días".
            if (fechaInicio == null || fechaInicio.Value > fechaFin)
            {
                return new DiferenciaEntreFechas(anios, meses, dias);
            }

            // Calculamos la diferencia en años, meses y días.
            anios = fechaFin.Year - fechaInicio.Value.Year;
            meses = fechaFin.Month - fechaInicio.Value.Month;
            dias = fechaFin.Day - fechaInicio.Value.Day;

            // Si el día de la fecha final es menor al día de la fecha de inicio,
            // entonces debemos "pedir prestado" un mes de los meses.
            if (dias < 0)
            {
                meses--;
                dias += DateTime.DaysInMonth(fechaInicio.Value.Year, fechaInicio.Value.Month);
            }

            // Si el mes de la fecha final es menor al mes de la fecha de inicio,
            // entonces debemos "pedir prestado" un año de los años.
            if (meses < 0)
            {
                anios--;
                meses += 12;
            }

            // Retornamos el resultado.
            return new DiferenciaEntreFechas(anios, meses, dias);
        }
    }
}
