using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;

namespace Proyecto_Compis
{
    class AnalizadorSemántico
    {
        public static Regex DEFINICION_CLASE = new Regex(@"^class ([a-z]|[A-Z])+[0-9]* ");
        public static Regex DEFINICION_VARIABLE = new Regex(@"^(int|double|bool|string) ([a-z]|[A-Z])+([0-9])* ; $");
        public static Regex DEFINICION_VARIABLE_VECTOR = new Regex(@"^(int|double|bool|string) (\[\])? ([a-z]|[A-Z])+([0-9])* ; $");
        public static Regex DEFINICION_METODO = new Regex(@"^void ([a-z]|[A-Z])+[0-9]* ");
        public static Regex DEFINICION_INTERFAZ= new Regex(@"^interface ([a-z]|[A-Z])+[0-9]* ");
        public static Regex DEFINICION_FUNCION = new Regex(@"^(int|double|bool|string) ([a-z]|[A-Z])+([0-9])* (\() ");
        public static Regex DEFINICION_CONSTANTE = new Regex(@"^const (int|double|bool|string) ([a-z]|[A-Z])+[0-9]* ");
        public static Queue<PropiedadesDePalabras> LISTA_TOKENS = new Queue<PropiedadesDePalabras>();
        public static List<Ambitos> LISTA_DE_AMBITOS = new List<Ambitos>();
        public static string LECTOR_DE_CADENA;
        public static bool MeComiVariables = false;
        public static int CuantasVariablesMeComi = 0;

        public AnalizadorSemántico(List<PropiedadesDePalabras> Tokns)
        {
            foreach (var item in Tokns)
            {
                LISTA_TOKENS.Enqueue(item);
            }
            PropiedadesDePalabras Dolar = new PropiedadesDePalabras("ESPECIAL", "$", 0, 0, 1);
            LISTA_TOKENS.Enqueue(Dolar);
        }

        public void DefinirAmbitos()
        {
            List<PropiedadesDePalabras> Lista_tkns = new List<PropiedadesDePalabras>();
            foreach (var item in LISTA_TOKENS)
            {
                Lista_tkns.Add(item);
            }
            foreach (var item in Lista_tkns)
            {
                if (CuantasVariablesMeComi == 0)
                {
                    LECTOR_DE_CADENA += item.Cadena;
                    LECTOR_DE_CADENA += " ";
                    LISTA_TOKENS.Dequeue();
                    if (DEFINICION_CLASE.IsMatch(LECTOR_DE_CADENA))
                    {
                        LECTOR_DE_CADENA = string.Empty;
                        ClaseDeclaracion(item.Cadena);
                    }
                    else if (DEFINICION_METODO.IsMatch(LECTOR_DE_CADENA))
                    {

                    }
                    else if (DEFINICION_INTERFAZ.IsMatch(LECTOR_DE_CADENA))
                    {

                    }
                    else if (DEFINICION_CONSTANTE.IsMatch(LECTOR_DE_CADENA))
                    {

                    }
                    else if (DEFINICION_FUNCION.IsMatch(LECTOR_DE_CADENA))
                    {

                    }
                    else if (DEFINICION_VARIABLE.IsMatch(LECTOR_DE_CADENA))
                    {

                    }
                    else if (DEFINICION_VARIABLE_VECTOR.IsMatch(LECTOR_DE_CADENA))
                    {

                    }
                }
                else if (CuantasVariablesMeComi != 0)
                {
                    CuantasVariablesMeComi--;
                }
            }
        }

        public void ClaseDeclaracion(string ID_Clase)
        {
            //aqui voy a definir puras variables y crear ambito
            LISTA_TOKENS.Dequeue();//remover {
            var quitarTokens = 0;
            //CuantasVariablesMeComi++;
            var lista_Variables_Declaradas = new List<string>();
            //recorrer la lista global de tokens hasta encontrar un }
            foreach (var item in LISTA_TOKENS)
            {
                if (item.Cadena != "}")
                {
                    LECTOR_DE_CADENA += item.Cadena;
                    LECTOR_DE_CADENA += " ";
                    CuantasVariablesMeComi++;
                    if (DEFINICION_VARIABLE.IsMatch(LECTOR_DE_CADENA) || DEFINICION_VARIABLE_VECTOR.IsMatch(LECTOR_DE_CADENA))
                    {
                        lista_Variables_Declaradas.Add(LECTOR_DE_CADENA);
                        LECTOR_DE_CADENA = string.Empty;
                    }
                }
                else if (item.Cadena == "}")
                {
                    CuantasVariablesMeComi++;
                    break;
                }
            }
            quitarTokens = CuantasVariablesMeComi;
            while (quitarTokens > 0)//puede ser 1 o 0, porque consume de ultimo el dolar
            {
                LISTA_TOKENS.Dequeue();
                quitarTokens--;
            }
            Ambitos NuevoAmbitoClase = new Ambitos();
            NuevoAmbitoClase.AMBITO.Add(ID_Clase, lista_Variables_Declaradas);
            LISTA_DE_AMBITOS.Add(NuevoAmbitoClase);
        }

    }
}
