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
        //cambiar nombres a mayus
        public Regex REGEX;
        public StringBuilder PATTERN;
        public List<string> TNOMBRES = new List<string>();
        public int[] NUMEROS;
        public bool DEBUG;
        //public PropiedadesDePalabras TokenAnterior = new PropiedadesDePalabras("", "", 0, 0, 0);
        //public PropiedadesDePalabras TokenActual= new PropiedadesDePalabras("", "", 0, 0, 0);

        //Método para agregar las reglas y reconocer los tokens
        public void NuevaReglaDeTokens(string patron_Nuevo, string token_Nombre, bool ignorar = false)
        {
            if (PATTERN == null)
            {
                PATTERN = new StringBuilder(string.Format("(?<{0}>{1})", token_Nombre, patron_Nuevo));
            }
            else
            {
                PATTERN.Append(string.Format("|(?<{0}>{1})", token_Nombre, patron_Nuevo));
            }

            if (!ignorar)
            {
                TNOMBRES.Add(token_Nombre);
            }

            DEBUG = true;
        }

        //Recibir tokens
        public IEnumerable<PropiedadesDePalabras> Tokens(string Texto_A_Compilar)
        {
            Match match = REGEX.Match(Texto_A_Compilar);
            if (!match.Success)
            {
                yield break;
            }
            var linea = 1;
            var comienzo = 0;
            var indice = 0;

            while (match.Success)
            {
                if (match.Index > indice)
                {
                    var token = Texto_A_Compilar.Substring(indice, match.Index - indice);
                    yield return new PropiedadesDePalabras("ERROR", token, indice, linea, (indice - comienzo) + 1);
                    linea += ContarLineas(indice, ref comienzo, token);
                }

                for (int i = 0; i < NUMEROS.Length; i++)
                {
                    if (match.Groups[NUMEROS[i]].Success)
                    {
                        var nombre = REGEX.GroupNameFromNumber(NUMEROS[i]);
                        yield return new PropiedadesDePalabras(nombre, match.Value, match.Index, linea, (match.Index - comienzo) + 1);
                        break;
                    }
                }
                linea += ContarLineas(match.Index, ref comienzo, match.Value);
                indice = match.Index + match.Length;
                match = match.NextMatch();
                //if (TokenActual.Cadena == "void")
                //{
                //    TokenAnterior = TokenActual;
                //}
                //if (TokenActual.Nombre == "IDENTIFICADOR" && TokenAnterior.Cadena == "void")
                //{
                //    //tabla de simbolos.add (identificador , atributos ej: int a, string b, bool c)
                //}
            }

            if (Texto_A_Compilar.Length > indice)
            {
                yield return new PropiedadesDePalabras("ERROR", Texto_A_Compilar.Substring(indice), indice, linea, (indice - comienzo) + 1);
            }
        }

        public void Debuggear(RegexOptions opciones)
        {
            if (DEBUG)
            {
                try
                {
                    REGEX = new Regex(PATTERN.ToString(), opciones);
                    NUMEROS = new int[TNOMBRES.Count];
                    var obtenerNombre = REGEX.GetGroupNames();
                    for (int i = 0, index = 0; i < obtenerNombre.Length; i++)
                    {
                        if (TNOMBRES.Contains(obtenerNombre[i]))
                        {
                            NUMEROS[index++] = REGEX.GroupNumberFromName(obtenerNombre[i]);
                        }
                    }
                    DEBUG = false;

                }
                catch (Exception ex) { throw ex; }
            }
        }

        //Conteo de lineas
        public int ContarLineas(int indice, ref int lineaComienzo, string token)
        {
            var linea = 0;
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

