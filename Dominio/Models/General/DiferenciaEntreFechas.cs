using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Dominio.Models.General
{
    public class DiferenciaEntreFechas
    {
        public int anios = 0;
        public int meses = 0;
        public int dias = 0;

        public DiferenciaEntreFechas()
        {

        }

        public DiferenciaEntreFechas(int anios, int meses, int dias)
        {
            this.anios = anios;
            this.meses = meses;
            this.dias = dias;
        }

        public override string ToString()
        {
            return anios+" Años, "+meses+" Meses, "+dias+" Dias";
        }

    }
}
