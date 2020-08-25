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

        //Método para agregar las reglas y reconocer los tokens
        public void NuevaReglaDeTokens(string patron_Nuevo, string token_Nombre, bool ignorar = false)
        {
            if (string.IsNullOrWhiteSpace(token_Nombre))
            {
                throw new ArgumentException(string.Format("{0} no es un nombre válido.", token_Nombre));
            }

            if (string.IsNullOrEmpty(patron_Nuevo))
            {
                throw new ArgumentException(string.Format("El patrón {0} no es válido.", patron_Nuevo));
            }

            if (pattern == null)
            {
                pattern = new StringBuilder(string.Format("(?<{0}>{1})", token_Nombre, patron_Nuevo));
            }
            else
            {
                pattern.Append(string.Format("|(?<{0}>{1})", token_Nombre, patron_Nuevo));
            }

            if (!ignorar)
            { 
                TNombres.Add(token_Nombre); 
            }          

            compilar = true;
        }      

        //Conteo de lineas
        public int contarLineas(int indice, ref int lineaComienzo, string token)
        {
            int linea = 0;
            for (int i = 0; i < token.Length; i++)
                if (token[i] == '\n')
                {
                    linea++;
                    lineaComienzo = indice + i + 1;
                }
            return linea;
        }



    }
}
