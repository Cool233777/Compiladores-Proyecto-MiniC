using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compis
{
   public class PropiedadesDePalabras
    {
        public PropiedadesDePalabras(string nombre, string cadena, int index, int linea, int columna)
        {
            Nombre = nombre;
            Cadena = cadena;
            Index = index;
            Linea = linea;
            Columna = columna;

        }

        public string Nombre { get; set; }
        public string Cadena { get; set; }
        public int Index { get; set; }
        public int Linea { get; set; }
        public int Columna { get; set; }

        public int Lenght { get { return Cadena.Length; } }
    }
}
