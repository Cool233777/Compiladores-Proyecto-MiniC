using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using Proyecto_Compis.Properties;
namespace Proyecto_Compis
{
    public class AnalizadorLex
    {
        public Regex gex;
        public StringBuilder pattern;
        public List<string> TNombres;
        public int[] numeros;
        public bool compilar;

        public AnalizadorLex()
        {
            compilar = true;
            TNombres = new List<string>();
            
        }



        //Conteo de lineas
        public int contarLineas(int indice,ref int lineaComienzo, string token)
        {
            int linea = 0;
            for (int i=0; i<token.Length;i++)
                if(token[i]=='\n')
                {
                    linea++;
                    lineaComienzo = indice + i + 1;
                }
            return linea;
        }
        


    }
}
