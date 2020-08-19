using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace Proyecto_Compis
{
    class AnalizadorLex
    {
        Regex regex;
        StringBuilder pattern;
        bool compilar;
        List<string> TNombres;
        int[] numeros;

        public AnalizadorLex()
        {
            compilar = true;
            TNombres = new List<string>();
        }
    }
}
