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
        public static Regex DEFINICION_CLASE_HEREDADA = new Regex(@"^class ([a-z]|[A-Z])+[0-9]* : ([a-z]|[A-Z])+[0-9]* ");
        public static Regex DEFINICION_VARIABLE = new Regex(@"^(int|double|bool|string) ([a-z]|[A-Z])+([0-9])* ; $");
        public static Regex DEFINICION_VARIABLE_VECTOR = new Regex(@"^(int|double|bool|string) (\[\])? ([a-z]|[A-Z])+([0-9])* ; $");
        public static Regex DEFINICION_VARIABLE_PARAMETRO = new Regex(@"^(int|double|bool|string) ([a-z]|[A-Z])+([0-9])* $");
        public static Regex DEFINICION_VARIABLE_VECTOR_PARAMETRO = new Regex(@"^(int|double|bool|string) (\[\])? ([a-z]|[A-Z])+([0-9])* $");
        public static Regex DEFINICION_METODO = new Regex(@"^void ([a-z]|[A-Z])+[0-9]* ");
        public static Regex DEFINICION_INTERFAZ = new Regex(@"^interface ([a-z]|[A-Z])+[0-9]* ");
        public static Regex DEFINICION_METODO_INTERFAZ = new Regex(@"^void ([a-z]|[A-Z])+[0-9]* (\() ");
        public static Regex DEFINICION_FUNCION = new Regex(@"^(int|double|bool|string) ([a-z]|[A-Z])+([0-9])* (\()");
        public static Regex DEFINICION_FUNCION_EN_INTERFAZ = new Regex(@"^(int|double|bool|string) ([a-z]|[A-Z])+([0-9])* (\() ");
        public static Regex DEFINICION_CONSTANTE = new Regex(@"^const ");
        //public static Regex DEFINICION_CONSTANTE_INT = new Regex(@"^const (int) ([a-z]|[A-Z])+[0-9]* (=)  ; $");
        //public static Regex DEFINICION_CONSTANTE_DOUBLE = new Regex(@"^const (double) ([a-z]|[A-Z])+[0-9]* (=)  ; $");
        //public static Regex DEFINICION_CONSTANTE_BOOL = new Regex(@"^const (bool) ([a-z]|[A-Z])+[0-9]* (=)  ; $");
        //public static Regex DEFINICION_CONSTANTE_STRING = new Regex(@"^const (string) ([a-z]|[A-Z])+[0-9]* (=)  ; $");
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

        public List<Ambitos> RegresarAmbitos()
        {
            return LISTA_DE_AMBITOS;
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
                if (CuantasVariablesMeComi == 0 && item.Cadena != "$")
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
                        LECTOR_DE_CADENA = string.Empty;
                        MetodoDeclaracion(item.Cadena);
                    }
                    else if (DEFINICION_INTERFAZ.IsMatch(LECTOR_DE_CADENA))
                    {
                        LECTOR_DE_CADENA = string.Empty;
                        InterfazDeclaracion(item.Cadena);
                    }
                    else if (DEFINICION_CONSTANTE.IsMatch(LECTOR_DE_CADENA))
                    {
                        LECTOR_DE_CADENA = string.Empty;
                        ConstanteDeclaracion();
                    }
                    else if (DEFINICION_FUNCION.IsMatch(LECTOR_DE_CADENA))
                    {
                        FuncionDeclaracion(LECTOR_DE_CADENA);
                        LECTOR_DE_CADENA = string.Empty;
                    }
                    else if (DEFINICION_VARIABLE.IsMatch(LECTOR_DE_CADENA))
                    {
                        VariableDeclaracion(LECTOR_DE_CADENA);
                        LECTOR_DE_CADENA = string.Empty;
                    }
                    else if (DEFINICION_VARIABLE_VECTOR.IsMatch(LECTOR_DE_CADENA))
                    {
                        VariableVecDeclaracion(LECTOR_DE_CADENA);
                        LECTOR_DE_CADENA = string.Empty;
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
            var quitarTokens = 0;
            CuantasVariablesMeComi++;
            var lista_Variables_Declaradas = new List<string>();
            //recorrer la lista global de tokens hasta encontrar un }
            if (LISTA_TOKENS.Peek().Cadena == ":")
            {
                ClaseHerDeclaracion(ID_Clase);
            }
            else
            {
                LISTA_TOKENS.Dequeue();//remover {
                foreach (var item in LISTA_TOKENS)
                {
                    if (item.Cadena != "}")
                    {
                        LECTOR_DE_CADENA += item.Cadena;
                        LECTOR_DE_CADENA += " ";
                        CuantasVariablesMeComi++;
                        if (DEFINICION_VARIABLE.IsMatch(LECTOR_DE_CADENA) || DEFINICION_VARIABLE_VECTOR.IsMatch(LECTOR_DE_CADENA) || DEFINICION_CONSTANTE.IsMatch(LECTOR_DE_CADENA))
                        {
                            lista_Variables_Declaradas.Add("Atributo: " + LECTOR_DE_CADENA + "\n");
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
                while (quitarTokens-1 > 0)//puede ser 1 o 0, porque consume de ultimo el dolar
                {
                    LISTA_TOKENS.Dequeue();
                    quitarTokens--;
                }
                Ambitos NuevoAmbitoClase = new Ambitos();
                NuevoAmbitoClase.AMBITO.Add("Class: " + ID_Clase + "\n", lista_Variables_Declaradas);
                LISTA_DE_AMBITOS.Add(NuevoAmbitoClase);
            }
        }

        public void ClaseHerDeclaracion(string ID_Clase)
        {
            //aqui voy a definir puras variables y crear ambito
            LISTA_TOKENS.Dequeue();//remover :
            var ID_Clase_Hereda = LISTA_TOKENS.Peek().Cadena;
            var quitarTokens = 0;
            LISTA_TOKENS.Dequeue();//remover id de la clase heredada
            LISTA_TOKENS.Dequeue();//remover {
            CuantasVariablesMeComi++;
            CuantasVariablesMeComi++;
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
                    if (DEFINICION_VARIABLE.IsMatch(LECTOR_DE_CADENA) || DEFINICION_VARIABLE_VECTOR.IsMatch(LECTOR_DE_CADENA) || DEFINICION_CONSTANTE.IsMatch(LECTOR_DE_CADENA))
                    {
                        lista_Variables_Declaradas.Add("Atributo: " + LECTOR_DE_CADENA + "\n");
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
            while (quitarTokens-3 > 0)//puede ser 1 o 0, porque consume de ultimo el dolar
            {
                LISTA_TOKENS.Dequeue();
                quitarTokens--;
            }
            Ambitos NuevoAmbitoClase = new Ambitos();
            NuevoAmbitoClase.AMBITO.Add("Class: " + ID_Clase + " Hereda: " + ID_Clase_Hereda + "\n", lista_Variables_Declaradas);
            LISTA_DE_AMBITOS.Add(NuevoAmbitoClase);
        }

        public void MetodoDeclaracion(string ID_Metodo)
        {
            //aqui voy a definir puras variables y crear ambito
            LISTA_TOKENS.Dequeue();//remover (
            var quitarTokens = 0;
            CuantasVariablesMeComi++;//remover (
            var Parametros_Declarados = new List<string>();//REVISAR SI SE PUEDE HACER UN METODO PARA AHORRAR CODIGO
            //recorrer la lista global de tokens hasta encontrar un }
            foreach (var item in LISTA_TOKENS)
            {
                if (item.Cadena != ")" && item.Cadena != ",")
                {
                    LECTOR_DE_CADENA += item.Cadena;
                    LECTOR_DE_CADENA += " ";
                    CuantasVariablesMeComi++;
                    if (DEFINICION_VARIABLE_PARAMETRO.IsMatch(LECTOR_DE_CADENA) || DEFINICION_VARIABLE_VECTOR_PARAMETRO.IsMatch(LECTOR_DE_CADENA))
                    {
                        Parametros_Declarados.Add("Parametro: " + LECTOR_DE_CADENA + "\n");
                        LECTOR_DE_CADENA = string.Empty;
                    }
                }
                else if (item.Cadena == ",")
                {
                    CuantasVariablesMeComi++;
                }
                else if (item.Cadena == ")")
                {
                    CuantasVariablesMeComi++;
                    break;
                }
            }
            //el siguiente token es un {
            //revisar que hago adentro del metodo
            CuantasVariablesMeComi++;
            CuantasVariablesMeComi++;
            quitarTokens = CuantasVariablesMeComi;
            while (quitarTokens-1 > 0)//puede ser 1 o 0, porque consume de ultimo el dolar
            {
                LISTA_TOKENS.Dequeue();
                quitarTokens--;
            }
            Ambitos NuevoAmbitoMetodo = new Ambitos();
            NuevoAmbitoMetodo.AMBITO.Add("Metodo: " + ID_Metodo + "\n", Parametros_Declarados);
            LISTA_DE_AMBITOS.Add(NuevoAmbitoMetodo);
        }

        public void InterfazDeclaracion(string ID_Interfaz)
        {
            //aqui voy a definir puras variables y crear ambito
            LISTA_TOKENS.Dequeue();//remover {
            var quitarTokens = 0;
            CuantasVariablesMeComi++;
            var lista_Variables_Declaradas = new List<string>();
            var lista_Variables_De_SubMetodos = new Stack<string>();
            //recorrer la lista global de tokens hasta encontrar un }
            foreach (var item in LISTA_TOKENS)
            {
                if (item.Cadena != "}")
                {
                    if (item.Cadena != ")" && item.Cadena != "," && item.Cadena != ";")
                    {
                        LECTOR_DE_CADENA += item.Cadena;
                        LECTOR_DE_CADENA += " ";
                        CuantasVariablesMeComi++;
                        if (DEFINICION_METODO_INTERFAZ.IsMatch(LECTOR_DE_CADENA))
                        {
                            lista_Variables_De_SubMetodos.Push("Metodo: " + LECTOR_DE_CADENA.Substring(0, LECTOR_DE_CADENA.Length - 2) + "\n");
                            LECTOR_DE_CADENA = string.Empty;
                        }
                        else if (DEFINICION_FUNCION_EN_INTERFAZ.IsMatch(LECTOR_DE_CADENA))
                        {
                            lista_Variables_De_SubMetodos.Push("Funcion: " + LECTOR_DE_CADENA.Substring(0, LECTOR_DE_CADENA.Length - 2) + "\n");
                            LECTOR_DE_CADENA = string.Empty;
                        }
                        else if (lista_Variables_De_SubMetodos.Count > 0 && (DEFINICION_VARIABLE_PARAMETRO.IsMatch(LECTOR_DE_CADENA) || DEFINICION_VARIABLE_VECTOR_PARAMETRO.IsMatch(LECTOR_DE_CADENA)))
                        {
                            var topeDeCola = lista_Variables_De_SubMetodos.Pop() + "Parametro: " + LECTOR_DE_CADENA + "\n";
                            lista_Variables_De_SubMetodos.Push(topeDeCola);
                            // topeDeCola += "Parametro: "+LECTOR_DE_CADENA+"\n";
                            LECTOR_DE_CADENA = string.Empty;
                        }
                    }
                    else if (item.Cadena == "," || item.Cadena == ";")
                    {
                        CuantasVariablesMeComi++;
                    }
                    else if (item.Cadena == ")")
                    {
                        lista_Variables_Declaradas.Add(lista_Variables_De_SubMetodos.Pop());
                        CuantasVariablesMeComi++;
                    }
                }
                else if (item.Cadena == "}")
                {
                    CuantasVariablesMeComi++;
                    break;
                }
            }
            quitarTokens = CuantasVariablesMeComi;
            while (quitarTokens-1 > 0)//puede ser 1 o 0, porque consume de ultimo el dolar
            {
                LISTA_TOKENS.Dequeue();
                quitarTokens--;
            }
            Ambitos NuevoAmbitoInterfaz = new Ambitos();
            NuevoAmbitoInterfaz.AMBITO.Add("Interface: " + ID_Interfaz + "\n", lista_Variables_Declaradas);//si tengo parametros adentro de los metodos, escribirlos
            LISTA_DE_AMBITOS.Add(NuevoAmbitoInterfaz);
        }

        public void ConstanteDeclaracion()
        {
            //aqui voy a definir puras variables y crear ambito
            var TipoConstante = LISTA_TOKENS.Peek().Cadena;
            LISTA_TOKENS.Dequeue(); // remover tipo
            CuantasVariablesMeComi++;
            var IDConstante = LISTA_TOKENS.Peek().Cadena;
            LISTA_TOKENS.Dequeue(); // remover id
            CuantasVariablesMeComi++;
            LISTA_TOKENS.Dequeue(); // remover =
            CuantasVariablesMeComi++;
            var quitarTokens = 0;
            //CuantasVariablesMeComi++;
            var lista_Variables_Declaradas = new List<string>();
            //recorrer la lista global de tokens hasta encontrar un ;
            //foreach (var item in LISTA_TOKENS)
            //{
            //    if (item.Cadena != ";")
            //    {
            //        LECTOR_DE_CADENA += item.Cadena;
            //        var VecDouble = LECTOR_DE_CADENA.Split('.');
            //        var valorDeConstante = "";
            //        var IntConst = 0;
            //        CuantasVariablesMeComi++;
            //        if (VecDouble.Length > 1)
            //        {
            //            valorDeConstante = "double";
            //        }
            //        else if (LECTOR_DE_CADENA == "false" || LECTOR_DE_CADENA == "true")
            //        {
            //            valorDeConstante = LECTOR_DE_CADENA;
            //        }
            //        else
            //        {
            //            try
            //            {
            //                IntConst = int.Parse(LECTOR_DE_CADENA);
            //                valorDeConstante = "int";
            //            }
            //            catch (Exception)
            //            {
            //                valorDeConstante = "string";
            //            }
            //        }
            //        if (TipoConstante == valorDeConstante)
            //        {
            //            lista_Variables_Declaradas.Add("Valor: " + LECTOR_DE_CADENA);//verifico que la cadena sea el mismo tipo que el constante
            //            LECTOR_DE_CADENA = string.Empty;
            //        }
            //        else
            //        {
            //            //error semantico de asignacion de constante
            //        }


            //    }
            //    else if (item.Cadena == ";")
            //    {
            //        CuantasVariablesMeComi++;
            //        break;
            //    }
            //}
            quitarTokens = CuantasVariablesMeComi;
            while (quitarTokens - 3 > 0)//puede ser 1 o 0, porque consume de ultimo el dolar
            {
                LISTA_TOKENS.Dequeue();
                quitarTokens--;
            }
            Ambitos NuevoAmbitoClase = new Ambitos();
            NuevoAmbitoClase.AMBITO.Add("Constante: " + IDConstante + " Tipo: " + TipoConstante + "\n", lista_Variables_Declaradas);
            LISTA_DE_AMBITOS.Add(NuevoAmbitoClase);
        }

        public void FuncionDeclaracion(string LectorCadena)
        {
            //aqui voy a definir puras variables y crear ambito
            //LISTA_TOKENS.Dequeue();//remover (
            LECTOR_DE_CADENA = string.Empty;
            var vecLectorCad = LectorCadena.Split(' ');
            var Tipo_Metodo = vecLectorCad[0];
            var ID_Metodo = vecLectorCad[1];
            var quitarTokens = 0;
            //CuantasVariablesMeComi++;
            var Parametros_Declarados = new List<string>();//REVISAR SI SE PUEDE HACER UN METODO PARA AHORRAR CODIGO
            //recorrer la lista global de tokens hasta encontrar un }
            foreach (var item in LISTA_TOKENS)
            {
                if (item.Cadena != ")" && item.Cadena != ",")
                {
                    LECTOR_DE_CADENA += item.Cadena;
                    LECTOR_DE_CADENA += " ";
                    CuantasVariablesMeComi++;
                    if (DEFINICION_VARIABLE_PARAMETRO.IsMatch(LECTOR_DE_CADENA) || DEFINICION_VARIABLE_VECTOR_PARAMETRO.IsMatch(LECTOR_DE_CADENA))
                    {
                        Parametros_Declarados.Add("Parametro: " + LECTOR_DE_CADENA + "\n");
                        LECTOR_DE_CADENA = string.Empty;
                    }
                }
                else if (item.Cadena == ",")
                {
                    CuantasVariablesMeComi++;
                }
                else if (item.Cadena == ")")
                {
                    CuantasVariablesMeComi++;
                    break;
                }
            }
            //el siguiente token es un {
            //revisar que hago adentro del metodo
            CuantasVariablesMeComi++;
            CuantasVariablesMeComi++;
            quitarTokens = CuantasVariablesMeComi;
            while (quitarTokens > 0)//puede ser 1 o 0, porque consume de ultimo el dolar
            {
                LISTA_TOKENS.Dequeue();
                quitarTokens--;
            }
            Ambitos NuevoAmbitoMetodo = new Ambitos();
            NuevoAmbitoMetodo.AMBITO.Add("Funcion: " + ID_Metodo + " Tipo: " + Tipo_Metodo + "\n", Parametros_Declarados);
            LISTA_DE_AMBITOS.Add(NuevoAmbitoMetodo);
        }

        public void VariableDeclaracion(string LectorCadena)
        {
            //aqui voy a definir puras variables y crear ambito
            LECTOR_DE_CADENA = string.Empty;
            var vecLectorCad = LectorCadena.Split(' ');
            var Tipo_Variable = vecLectorCad[0];
            var ID_Variable = vecLectorCad[1];
            var lista_Variables_Declaradas = new List<string>();
            Ambitos NuevoAmbitoClase = new Ambitos();
            NuevoAmbitoClase.AMBITO.Add("Variable: " + ID_Variable + " Tipo: " + Tipo_Variable + "\n", lista_Variables_Declaradas);
            LISTA_DE_AMBITOS.Add(NuevoAmbitoClase);
        }

        public void VariableVecDeclaracion(string LectorCadena)
        {
            LECTOR_DE_CADENA = string.Empty;
            var vecLectorCad = LectorCadena.Split(' ');
            var Tipo_Variable = vecLectorCad[0];
            var ID_Variable = vecLectorCad[2];
            var lista_Variables_Declaradas = new List<string>();
            Ambitos NuevoAmbitoClase = new Ambitos();
            NuevoAmbitoClase.AMBITO.Add("Variable: " + ID_Variable + " Tipo: " + Tipo_Variable + "[]" + "\n", lista_Variables_Declaradas);
            LISTA_DE_AMBITOS.Add(NuevoAmbitoClase);
        }

    }
}
