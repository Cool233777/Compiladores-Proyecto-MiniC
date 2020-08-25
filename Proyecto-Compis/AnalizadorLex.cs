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


        //Recibir tokens
        public IEnumerable<PropiedadesDePalabras> Tokens(string texto)
        {
            if (compilar) throw new Exception("requiere compilar");
            Match match = gex.Match(texto);
            if (!match.Success) yield break;
            int linea = 1, comienzo = 0, indice = 0;

            while (match.Success)
            {
                if (match.Index > indice)
                {
                    string token = texto.Substring(indice, match.Index - indice);
                    yield return new PropiedadesDePalabras("ERROR", token, indice, linea, (indice - comienzo) + 1);
                    linea += contarLineas(indice, ref comienzo, token);
                }

                for (int i = 0; i < numeros.Length; i++)
                {
                    if (match.Groups[numeros[i]].Success)
                    {
                        string nombre = gex.GroupNameFromNumber(numeros[i]);
                        yield return new PropiedadesDePalabras(nombre, match.Value, match.Index, linea, (match.Index - comienzo) + 1);
                        break;

                    }
                }
                linea += contarLineas(match.Index, ref comienzo, match.Value);
                indice = match.Index + match.Length;
                match = match.NextMatch();


            }
            if (texto.Length >indice)
            {
                yield return new PropiedadesDePalabras("ERROR", texto.Substring(indice), indice, linea, (indice - comienzo) + 1);
            }
            }

        public void Compilar(RegexOptions opciones)
        {
            if (compilar)
            {
                try
                {
                    gex = new Regex(pattern.ToString(), opciones);
                    numeros = new int[TNombres.Count];
                    string[] obtenerNombre = gex.GetGroupNames();
                    for (int i = 0, index = 0; i<obtenerNombre.Length; i++)
                    {
                        if (TNombres.Contains(obtenerNombre[i]))
                        {
                            numeros[index++] = gex.GroupNumberFromName(obtenerNombre[i]);

                        }
                    }
                    compilar = false;

                }
                catch (Exception ex) { throw ex; }
            }
        }
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

