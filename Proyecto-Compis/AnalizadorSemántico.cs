using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
using System.IO;

namespace Proyecto_Compis
{
    class AnalizadorSemántico
    {
        public static Regex DEFINICION_CLASE = new Regex(@"^class ([a-z]|[A-Z])+[0-9]* ");
        public static Regex DEFINICION_CLASE_HEREDADA = new Regex(@"^class ([a-z]|[A-Z])+[0-9]* : ([a-z]|[A-Z])+[0-9]* ");
        public static Regex DEFINICION_VARIABLE = new Regex(@"^(int|double|bool|string) ([a-z]|[A-Z])+([0-9])* ; $");
        //public static Regex DEFINICION_VARIABLE_MULTIPLE = new Regex(@"^(int|double|bool|string) ([a-z]|[A-Z])+([0-9])* , ");
        public static Regex DEFINICION_VARIABLE_VECTOR = new Regex(@"^(int|double|bool|string) (\[\])? (([a-z]|[A-Z])+([0-9])*)+ ; $");
        //public static Regex DEFINICION_VARIABLE_VECTOR_MULTIPLE = new Regex(@"^(int|double|bool|string) (\[\])? ([a-z]|[A-Z])+([0-9])* , ");
        public static Regex DEFINICION_VARIABLE_PARAMETRO = new Regex(@"^(int|double|bool|string) ([a-z]|[A-Z])+([0-9])* $");
        public static Regex DEFINICION_VARIABLE_VECTOR_PARAMETRO = new Regex(@"^(int|double|bool|string) (\[\])? ([a-z]|[A-Z])+([0-9])* $");
        public static Regex DEFINICION_METODO = new Regex(@"^void ([a-z]|[A-Z])+[0-9]* ");
        public static Regex DEFINICION_INTERFAZ = new Regex(@"^interface ([a-z]|[A-Z])+[0-9]* ");
        public static Regex DEFINICION_METODO_INTERFAZ = new Regex(@"^void ([a-z]|[A-Z])+[0-9]* (\() ");
        public static Regex DEFINICION_FUNCION = new Regex(@"^(int|double|bool|string) ([a-z]|[A-Z])+([0-9])* (\()");
        public static Regex DEFINICION_FUNCION_EN_INTERFAZ = new Regex(@"^(int|double|bool|string) ([a-z]|[A-Z])+([0-9])* (\() ");
        public static Regex DEFINICION_CONSTANTE = new Regex(@"^const ");
        public static Regex DEFINICION_CONSTANTE_METODO = new Regex(@"^const (int|double|bool|string) ([a-z]|[A-Z])+([0-9])* ; $");
        public static Regex DEFINICION_ASIGNACION_DE_VALOR = new Regex(@"^([a-z]|[A-Z])+([0-9])* (\=) ");
        //public static Regex DEFINICION_CONSTANTE_INT = new Regex(@"^const (int) ([a-z]|[A-Z])+[0-9]* (=)  ; $");
        //public static Regex DEFINICION_CONSTANTE_DOUBLE = new Regex(@"^const (double) ([a-z]|[A-Z])+[0-9]* (=)  ; $");
        //public static Regex DEFINICION_CONSTANTE_BOOL = new Regex(@"^const (bool) ([a-z]|[A-Z])+[0-9]* (=)  ; $");
        //public static Regex DEFINICION_CONSTANTE_STRING = new Regex(@"^const (string) ([a-z]|[A-Z])+[0-9]* (=)  ; $");
        public static Queue<PropiedadesDePalabras> LISTA_TOKENS = new Queue<PropiedadesDePalabras>();
        public static List<Ambitos> LISTA_DE_AMBITOS = new List<Ambitos>();
        public static string LECTOR_DE_CADENA;
        //public static bool MeComiVariables = false;
        public static int CuantasVariablesMeComi = 0;
        public static Dictionary<string, string> LISTA_ID_VARIABLES = new Dictionary<string, string>();
        public static int CorrimientoTkMetodo = 0;
        public static int CorrimientoTkMetodoFaseAnterior = 0;
        public static bool PrimeraVezMetodo = true;
        public static int CorriminetoNuevosTokens = 0;
        public static List<string> LISTA_DE_ERRORES = new List<string>();
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

        public List<string> RegresarErrores()
        {
            return LISTA_DE_ERRORES;
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
                    //else if (DEFINICION_VARIABLE_MULTIPLE.IsMatch(LECTOR_DE_CADENA))
                    //{
                    //    VariableMultiDeclaracion(LECTOR_DE_CADENA);
                    //    LECTOR_DE_CADENA = string.Empty;
                    //}
                    //else if (DEFINICION_VARIABLE_VECTOR_MULTIPLE.IsMatch(LECTOR_DE_CADENA))
                    //{
                    //    VariableVecMultiDeclaracion(LECTOR_DE_CADENA);
                    //    LECTOR_DE_CADENA = string.Empty;
                    //}
                }
                else if (CuantasVariablesMeComi != 0)
                {
                    CuantasVariablesMeComi--;
                }
                else if (item.Cadena == "$")
                {
                    //termine de ver todos los tokens
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
                while (quitarTokens - 1 > 0)//puede ser 1 o 0, porque consume de ultimo el dolar
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
            while (quitarTokens - 3 > 0)//puede ser 1 o 0, porque consume de ultimo el dolar
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
            var LugarDeAmbito = new List<string>();
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
                        Ambitos NuevoAmbito = new Ambitos();
                        LugarDeAmbito.Add("Ambito: " + ID_Metodo);
                        NuevoAmbito.AMBITO.Add("Parametro: " + LECTOR_DE_CADENA, LugarDeAmbito);
                        LISTA_DE_AMBITOS.Add(NuevoAmbito);
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
            quitarTokens = CuantasVariablesMeComi;
            while (quitarTokens - 1 > 0)//puede ser 1 o 0, porque consume de ultimo el dolar
            {
                LISTA_TOKENS.Dequeue();
                quitarTokens--;
            }
            //el siguiente token es un {
            //revisar que hago adentro del metodo
            var quitarTokens2 = 0;
            var CorrerVariables2 = 0;
            LISTA_TOKENS.Dequeue();//{//hasta aqui voy bien
            var correrEnAsignacionDeVar = 0;
            var lista_tks = new Queue<PropiedadesDePalabras>();
            foreach (var item in LISTA_TOKENS)
            {
                lista_tks.Enqueue(item);
            }
            foreach (var item in lista_tks)
            {
                if (correrEnAsignacionDeVar == 0)
                {
                    if (item.Cadena != "}")//correr tokens por comer
                    {
                        //verificar variables que se van a crear
                        LECTOR_DE_CADENA += item.Cadena;
                        LECTOR_DE_CADENA += " ";
                        //LISTA_TOKENS.Dequeue();
                        CuantasVariablesMeComi++;
                        quitarTokens2++;
                        CorrerVariables2++;
                        CorrimientoTkMetodo++;
                        CorrimientoTkMetodoFaseAnterior++;
                        if (DEFINICION_ASIGNACION_DE_VALOR.IsMatch(LECTOR_DE_CADENA))
                        {
                            if (ComprobarExistencia(LECTOR_DE_CADENA, ID_Metodo))
                            {
                                correrEnAsignacionDeVar = ComprobacionDeTipos(LECTOR_DE_CADENA, ID_Metodo);
                                CuantasVariablesMeComi += correrEnAsignacionDeVar;
                                quitarTokens2 += correrEnAsignacionDeVar;
                                //correrEnAsignacionDeVar = quitarTokens2;

                                LECTOR_DE_CADENA = string.Empty;
                                //quitarTokens2 = CorrerVariables2;

                            }
                            else
                            {
                                var vec = LECTOR_DE_CADENA.Split(' ');
                                LISTA_DE_ERRORES.Add("No se encontro el id: " + vec[0] + " en el metodo: " + ID_Metodo);
                                LECTOR_DE_CADENA = string.Empty;
                                //error no existe el id ingresado

                            }
                        }
                        else if (DEFINICION_VARIABLE.IsMatch(LECTOR_DE_CADENA))
                        {
                            if (ComprobarExistenciaCreacionVar(LECTOR_DE_CADENA, ID_Metodo))
                            {
                                VariableDeclaracionMetodo(LECTOR_DE_CADENA, ID_Metodo);
                                LECTOR_DE_CADENA = string.Empty;
                                //quitarTokens2 = CorrerVariables2;
                                //correrEnAsignacionDeVar += 3;
                            }
                            else
                            {
                                var vec = LECTOR_DE_CADENA.Split(' ');
                                LISTA_DE_ERRORES.Add("No se encontro el id: " + vec[1] + " en el metodo: " + ID_Metodo);
                                LECTOR_DE_CADENA = string.Empty;
                                //error
                            }

                        }
                        else if (DEFINICION_VARIABLE_VECTOR.IsMatch(LECTOR_DE_CADENA))
                        {
                            if (ComprobarExistenciaCreacionVar(LECTOR_DE_CADENA, ID_Metodo))
                            {
                                VariableVecDeclaracionMetodo(LECTOR_DE_CADENA, ID_Metodo);
                                LECTOR_DE_CADENA = string.Empty;
                                //quitarTokens2 = CorrerVariables2;
                                //correrEnAsignacionDeVar += 3;
                            }
                            else
                            {
                                var vec = LECTOR_DE_CADENA.Split(' ');
                                LISTA_DE_ERRORES.Add("No se encontro el id: " + vec[2] + " en el metodo: " + ID_Metodo);
                                LECTOR_DE_CADENA = string.Empty;
                                //error
                            }
                        }
                        else if (DEFINICION_CONSTANTE_METODO.IsMatch(LECTOR_DE_CADENA))
                        {
                            if (ComprobarExistenciaCreacionConst(LECTOR_DE_CADENA, ID_Metodo))
                            {
                                ConstanteDeclaracionMetodo(LECTOR_DE_CADENA, ID_Metodo);
                                LECTOR_DE_CADENA = string.Empty;
                                //quitarTokens2 = CorrerVariables2;
                                //correrEnAsignacionDeVar += 3;
                            }
                            else
                            {
                                var vec = LECTOR_DE_CADENA.Split(' ');
                                LISTA_DE_ERRORES.Add("No se encontro el id: " + vec[2] + " en el metodo: " + ID_Metodo);
                                LECTOR_DE_CADENA = string.Empty;
                                //error
                            }
                        }
                        //instanciar variables creadas para operaciones
                    }
                    else if (item.Cadena == "}")
                    {
                        CuantasVariablesMeComi++;
                        CorrerVariables2++;
                        //CorrimientoTkMetodo++;
                        break;
                    }
                }
                else if (item.Cadena == ";")
                {
                    correrEnAsignacionDeVar--;
                    //    quitarTokens2--;
                    //CorrimientoTkMetodo++;
                }
                else
                {
                    correrEnAsignacionDeVar--;
                }
            }
            quitarTokens++;
            //CuantasVariablesMeComi++;//{
            CuantasVariablesMeComi++;//}
            quitarTokens += quitarTokens2;
            while (quitarTokens - 1 > 0)//puede ser 1 o 0, porque consume de ultimo el dolar
            {
                LISTA_TOKENS.Dequeue();
                quitarTokens--;
            }
            Ambitos NuevoAmbitoMetodo = new Ambitos();
            NuevoAmbitoMetodo.AMBITO.Add("Metodo: " + ID_Metodo + " ", Parametros_Declarados);
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
            while (quitarTokens - 1 > 0)//puede ser 1 o 0, porque consume de ultimo el dolar
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

            //AgregarVariablesOVerExistencia();

            CuantasVariablesMeComi++;//}
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
            lista_Variables_Declaradas.Add("Ambito: Global");
            Ambitos NuevoAmbitoClase = new Ambitos();
            NuevoAmbitoClase.AMBITO.Add("Variable: " + ID_Variable + " Tipo: " + Tipo_Variable + "\n", lista_Variables_Declaradas);
            LISTA_DE_AMBITOS.Add(NuevoAmbitoClase);
            LISTA_ID_VARIABLES.Add(ID_Variable, "Global");
        }

        public void VariableVecDeclaracion(string LectorCadena)
        {
            LECTOR_DE_CADENA = string.Empty;
            var vecLectorCad = LectorCadena.Split(' ');
            var Tipo_Variable = vecLectorCad[0];
            var ID_Variable = vecLectorCad[2];
            var lista_Variables_Declaradas = new List<string>();
            lista_Variables_Declaradas.Add("Ambito: Global");
            Ambitos NuevoAmbitoClase = new Ambitos();
            NuevoAmbitoClase.AMBITO.Add("Variable: " + ID_Variable + " Tipo: " + Tipo_Variable + "[]" + "\n", lista_Variables_Declaradas);
            LISTA_DE_AMBITOS.Add(NuevoAmbitoClase);
            LISTA_ID_VARIABLES.Add(ID_Variable, "Global");
        }

        public void VariableDeclaracionMetodo(string LectorCadena, string IDMetodo)
        {
            //aqui voy a definir puras variables y crear ambito
            LECTOR_DE_CADENA = string.Empty;
            var vecLectorCad = LectorCadena.Split(' ');
            var Tipo_Variable = vecLectorCad[0];
            var ID_Variable = vecLectorCad[1];
            var lista_Variables_Declaradas = new List<string>();
            lista_Variables_Declaradas.Add("Ambito: " + IDMetodo);
            Ambitos NuevoAmbitoClase = new Ambitos();
            NuevoAmbitoClase.AMBITO.Add("Variable: " + ID_Variable + " Tipo: " + Tipo_Variable + "\n", lista_Variables_Declaradas);
            LISTA_DE_AMBITOS.Add(NuevoAmbitoClase);
            LISTA_ID_VARIABLES.Add(ID_Variable, IDMetodo);
        }

        public void VariableVecDeclaracionMetodo(string LectorCadena, string IDMetodo)
        {
            LECTOR_DE_CADENA = string.Empty;
            var vecLectorCad = LectorCadena.Split(' ');
            var Tipo_Variable = vecLectorCad[0];
            var ID_Variable = vecLectorCad[2];
            var lista_Variables_Declaradas = new List<string>();
            lista_Variables_Declaradas.Add("Ambito: " + IDMetodo);
            Ambitos NuevoAmbitoClase = new Ambitos();
            NuevoAmbitoClase.AMBITO.Add("Variable: " + ID_Variable + " Tipo: " + Tipo_Variable + "[]" + "\n", lista_Variables_Declaradas);
            LISTA_DE_AMBITOS.Add(NuevoAmbitoClase);
            LISTA_ID_VARIABLES.Add(ID_Variable, IDMetodo);
        }

        public void ConstanteDeclaracionMetodo(string LectorDeCadena, string ID_Metodo)
        {
            var VecConst = LectorDeCadena.Split(' ');//const int id;
            var IDConstante = VecConst[2];
            var lista_Variables_Declaradas = new List<string>();
            lista_Variables_Declaradas.Add("Ambito: " + ID_Metodo);
            Ambitos NuevoAmbitoClase = new Ambitos();
            NuevoAmbitoClase.AMBITO.Add("Constante: " + IDConstante + " Tipo: " + VecConst[1] + "\n", lista_Variables_Declaradas);
            LISTA_DE_AMBITOS.Add(NuevoAmbitoClase);
            LISTA_ID_VARIABLES.Add(IDConstante, ID_Metodo);
        }

        public bool ComprobarExistenciaCreacionVar(string LectorCadena, string IDMetodo)
        {
            var NoHayError = false;
            var vecLectorCad = LectorCadena.Split(' ');
            var Tipo_Variable = vecLectorCad[0];
            var ID_Variable = vecLectorCad[1];
            var listaVariables = new List<Ambitos>();
            var lista_Variables_Declaradas = new List<string>();
            lista_Variables_Declaradas.Add("Ambito: " + IDMetodo);
            foreach (var item in LISTA_DE_AMBITOS)
            {
                var ambito = item.AMBITO.ElementAt(0);
                var vecK = ambito.Key.Split(' ');
                var ComprobarVar = vecK[0];
                if (ComprobarVar == "Variable:")
                {
                    listaVariables.Add(item);
                }
            }

            //foreach (var item in listaVariables)//aqui ya tengo todas las variables creadas en el archivo
            //{

            //    var Vec = item.AMBITO.ElementAt(0).Value.ElementAt(0).Split(' ');//me separa el ambito: global
            //    var Vec2 = item.AMBITO.ElementAt(0).Key.Split(' ');// me separa Variable: idVariable
            //    var IDaProbar = Vec2[1];// guardo id
            //    var NombreInstancia = Vec[1];//guardo Global
            foreach (var item2 in LISTA_ID_VARIABLES)
            {
                var IDGuardado = item2.Key;
                var InstanciaGuardado = item2.Value;
                //var NombreInstancia2 = Vec2[1];//guardo Global
                if (ID_Variable == IDGuardado)//tienen el mismo ID
                {
                    if (IDMetodo != InstanciaGuardado)
                    {
                        //Ambitos NuevoAmbito = new Ambitos();
                        //NuevoAmbito.AMBITO.Add("Variable: " + ID_Variable + " Tipo: " + Tipo_Variable + "\n", lista_Variables_Declaradas);
                        //LISTA_DE_AMBITOS.Add(NuevoAmbito);
                        //LISTA_ID_VARIABLES.Add(ID_Variable, IDMetodo);
                        ////guardo la variable
                        NoHayError = true;
                        return NoHayError;
                    }
                    else
                    {
                        //Error
                        return NoHayError;
                    }
                }
                else
                {
                    //Ambitos NuevoAmbito = new Ambitos();
                    //NuevoAmbito.AMBITO.Add("Variable: " + ID_Variable + " Tipo: " + Tipo_Variable + "\n", lista_Variables_Declaradas);
                    //LISTA_DE_AMBITOS.Add(NuevoAmbito);
                    //LISTA_ID_VARIABLES.Add(ID_Variable, IDMetodo);
                    ////guardo la variable
                    NoHayError = true;
                    return NoHayError;
                }
            }
            //}

            return NoHayError;
        }

        public bool ComprobarExistenciaDefinicionVar(string ID_Variable, string IDMetodo)
        {
            var NoHayError = false;
            foreach (var item in LISTA_DE_AMBITOS)
            {
                var ambito = item.AMBITO.ElementAt(0);
                var vecK = ambito.Key.Split(' ');
                var vecV = ambito.Value.ElementAt(0).Split(' ');//ambito: global
                var ComprobarVar = vecK[0];
                if (ComprobarVar == "Variable:")
                {
                    if (ID_Variable == vecK[1])
                    {
                        if (IDMetodo == vecV[1] || IDMetodo == "Global")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    //else
                    //{
                    //    return false;
                    //}
                }
            }

            return NoHayError;
        }

        public bool ComprobarExistenciaDefinicionConst(string ID_Variable, string IDMetodo)
        {
            var NoHayError = false;
            foreach (var item in LISTA_DE_AMBITOS)
            {
                var ambito = item.AMBITO.ElementAt(0);
                var vecK = ambito.Key.Split(' ');
                var vecV = ambito.Value.ElementAt(0).Split(' ');//ambito: global
                var ComprobarVar = vecK[0];
                if (ComprobarVar == "Constante:")
                {
                    if (ID_Variable == vecK[1])
                    {
                        if (IDMetodo == vecV[1] || IDMetodo == "Global")
                        {
                            return true;
                        }
                        else
                        {
                            return false;
                        }
                    }
                    //else
                    //{
                    //    return false;
                    //}
                }
            }

            return NoHayError;
        }

        public bool ComprobarExistenciaCreacionConst(string LectorCadena, string IDMetodo)
        {
            var NoHayError = false;
            var vecLectorCad = LectorCadena.Split(' ');
            var Tipo_Variable = vecLectorCad[0];
            var ID_Variable = vecLectorCad[1];
            var listaVariables = new List<Ambitos>();
            var lista_Variables_Declaradas = new List<string>();
            lista_Variables_Declaradas.Add("Ambito: " + IDMetodo);
            foreach (var item in LISTA_DE_AMBITOS)
            {
                var ambito = item.AMBITO.ElementAt(0);
                var vecK = ambito.Key.Split(' ');
                var ComprobarVar = vecK[0];
                if (ComprobarVar == "Const:")
                {
                    listaVariables.Add(item);
                }
            }
            foreach (var item2 in LISTA_ID_VARIABLES)
            {
                var IDGuardado = item2.Key;
                var InstanciaGuardado = item2.Value;
                //var NombreInstancia2 = Vec2[1];//guardo Global
                if (ID_Variable == IDGuardado)//tienen el mismo ID
                {
                    if (IDMetodo != InstanciaGuardado)
                    {
                        NoHayError = true;
                        return NoHayError;
                    }
                    else
                    {
                        //Error
                        return NoHayError;
                    }
                }
                else
                {
                    NoHayError = true;
                    return NoHayError;
                }
            }
            return NoHayError;
        }

        public bool ComprobarExistencia(string LectorCadena, string IDMetodo)
        {
            var Existe = false;
            var Vec = LectorCadena.Split(' ');
            var IDvariable = Vec[0];
            foreach (var item in LISTA_ID_VARIABLES)
            {
                if (IDvariable == item.Key)//existe
                {
                    Existe = true;
                    return Existe;
                }
                //else
                //{
                //    //ERROR asigno a una variable desconocida
                //    return Existe;
                //}
            }
            return Existe;
        }

        public int ComprobacionDeTipos(string LectorCadena, string ID_Metodo)
        {
            var lista_tks = new Queue<PropiedadesDePalabras>();
            var correrEnAsignacionDeVar = 0;
            var correrMetodo = 0;
            var auxCorrimientos = 0;
            foreach (var item in LISTA_TOKENS)
            {
                lista_tks.Enqueue(item);
            }
            if (PrimeraVezMetodo)
            {

                while (CorrimientoTkMetodo > 0)// crear una auxiliar de la lista de tokens y otra auxiliar para el contador de asignacion
                {
                    lista_tks.Dequeue();
                    CorrimientoTkMetodo--;
                }
                PrimeraVezMetodo = false;

            }
            else
            {
                auxCorrimientos = CorrimientoTkMetodoFaseAnterior + CorriminetoNuevosTokens;
                while (auxCorrimientos > 0)// crear una auxiliar de la lista de tokens y otra auxiliar para el contador de asignacion
                {
                    lista_tks.Dequeue();
                    auxCorrimientos--;
                }
                //PrimeraVezMetodo = true;
            }



            var Vec = LectorCadena.Split(' ');
            var IdVariable = Vec[0];
            var ValorVariable = "";
            var ListaAUXAmbito = new List<string>();
            //var ListaAUXSubAmbito = new List<string>();
            var TipoDeVar = "";
            foreach (var item in LISTA_DE_AMBITOS)
            {
                var VecAmbito = item.AMBITO.ElementAt(0).Key.Split(' ');//VARIABLE: id TIPO: int

                if (IdVariable == VecAmbito[1])
                {
                    TipoDeVar = VecAmbito[3].Substring(0, VecAmbito[3].Length - 1);
                    //var VecAmbitoVal = item.AMBITO.ElementAt(0).Value.ElementAt(0).Split(' ');//Ambito: ID_Metodo
                    ListaAUXAmbito = item.AMBITO.ElementAt(0).Value; //Ambito: ID_Metodo
                                                                     //var VecAmbitoVal = item.AMBITO.ElementAt(0).Key
                    break;
                }
            }

            switch (TipoDeVar)
            {
                case "int"://solo puede ser suma de numeros
                    LECTOR_DE_CADENA = string.Empty;
                    var cantidadDeTokensAoperar = 0;
                    var listaTokensOperando = new List<PropiedadesDePalabras>();
                    var Error4 = true;
                    var existeUnVar = true;
                    var ListaIDNoDeclarados = new List<string>();
                    foreach (var item in lista_tks)
                    {

                        if (item.Cadena != ";" && Error4 && (item.Nombre == "NUMERO" || item.Nombre == "OPERADOR" || item.Nombre == "DECIMAL" || item.Cadena == ")" || item.Cadena == "("))
                        {
                            //LECTOR_DE_CADENA += item.Cadena;
                            listaTokensOperando.Add(item);
                            //CuantasVariablesMeComi++;
                            correrEnAsignacionDeVar++;
                            CorrimientoTkMetodo++;
                            CorriminetoNuevosTokens++;
                            cantidadDeTokensAoperar++;
                            //CorrimientoTkMetodoFaseAnterior++;
                        }
                        else if (item.Cadena == ";" && Error4 == true)
                        {
                            //CuantasVariablesMeComi++;
                            correrEnAsignacionDeVar++;
                            CorrimientoTkMetodo++;
                            CorriminetoNuevosTokens++;
                            break;
                            //CorrimientoTkMetodoFaseAnterior++;
                        }
                        else if (item.Cadena == ";" && Error4 == false && existeUnVar == true)
                        {
                            //CuantasVariablesMeComi++;
                            correrEnAsignacionDeVar++;
                            CorrimientoTkMetodo++;
                            CorriminetoNuevosTokens++;
                            LISTA_DE_ERRORES.Add("Error de asignacion, se esperaba int en: " + IdVariable + ", en el metodo: " + ID_Metodo);
                            break;
                            //CorrimientoTkMetodoFaseAnterior++;
                        }
                        else if (item.Cadena == ";" && Error4 == false && existeUnVar == false)
                        {
                            //CuantasVariablesMeComi++;
                            correrEnAsignacionDeVar++;
                            CorrimientoTkMetodo++;
                            CorriminetoNuevosTokens++;
                            foreach (var item2 in ListaIDNoDeclarados)
                            {
                                LISTA_DE_ERRORES.Add("El ID: " + item2 + ",no esta declarada");
                            }
                            break;
                            //CorrimientoTkMetodoFaseAnterior++;
                        }
                        else if (item.Nombre == "IDENTIFICADOR")//Como es un ID, voy a ver si existe y luego su valor
                        {
                            //var existeUnVar = ComprobarExistenciaDefinicionVar(item.Cadena, ID_Metodo);

                            if (ComprobarExistenciaDefinicionVar(item.Cadena, ID_Metodo))
                            {
                                correrEnAsignacionDeVar++;
                                CorrimientoTkMetodo++;
                                CorriminetoNuevosTokens++;
                                foreach (var item2 in LISTA_DE_AMBITOS)
                                {
                                    var VecAmbito = item2.AMBITO.ElementAt(0).Key.Split(' ');//VARIABLE: id TIPO: int
                                    //var ValorDeVAR = new string[2];
                                    if (item.Cadena == VecAmbito[1])
                                    {
                                        //TipoDeVar = VecAmbito[3].Substring(0, VecAmbito[3].Length - 1);
                                        var VecAmbitoKey = item2.AMBITO.ElementAt(0).Key.Split(' ');//Var: id Tipo: int
                                        var VecAmbitoVal = item2.AMBITO.ElementAt(0).Value.ElementAt(0).Split(' ');//Ambito: ID_Metodo
                                        if (VecAmbitoKey[3] == "int\n")
                                        {
                                            var ultimoIndice = item2.AMBITO.ElementAt(0).Value.Count;
                                            var ListaAUXSubAmbito = item2.AMBITO.ElementAt(0).Value.ElementAt(ultimoIndice - 1); //Val: ultimo valor asignado
                                            var ValorDeVAR = ListaAUXSubAmbito.Split(' ');
                                            item.Cadena = ValorDeVAR[1];
                                            item.Nombre = "NUMERO";
                                            listaTokensOperando.Add(item);
                                            break;
                                        }
                                        else
                                        {
                                            LISTA_DE_ERRORES.Add("Error de asignacion, el ID: " + VecAmbitoKey[1] + ", no es de tipo int" + ", en el metodo: " + ID_Metodo);
                                            Error4 = false;
                                            break;
                                        }
                                        //ValorDeVAR = item2.AMBITO.ElementAt().v.Split(0);
                                        //var VecAmbitoVal = item.AMBITO.ElementAt(0).Key
                                    }
                                }

                            }
                            else if (ComprobarExistenciaDefinicionConst(item.Cadena, ID_Metodo))
                            {
                                correrEnAsignacionDeVar++;
                                CorrimientoTkMetodo++;
                                CorriminetoNuevosTokens++;
                                foreach (var item2 in LISTA_DE_AMBITOS)
                                {
                                    var VecAmbito = item2.AMBITO.ElementAt(0).Key.Split(' ');//VARIABLE: id TIPO: int
                                    //var ValorDeVAR = new string[2];
                                    if (item.Cadena == VecAmbito[1])
                                    {
                                        //TipoDeVar = VecAmbito[3].Substring(0, VecAmbito[3].Length - 1);
                                        var VecAmbitoKey = item2.AMBITO.ElementAt(0).Key.Split(' ');//Var: id Tipo: int
                                        var VecAmbitoVal = item2.AMBITO.ElementAt(0).Value.ElementAt(0).Split(' ');//Ambito: ID_Metodo
                                        if (VecAmbitoKey[3] == "int\n" || VecAmbitoKey[3] == "double\n")
                                        {
                                            var ultimoIndice = item2.AMBITO.ElementAt(0).Value.Count;
                                            var ListaAUXSubAmbito = item2.AMBITO.ElementAt(0).Value.ElementAt(ultimoIndice - 1); //Val: ultimo valor asignado
                                            var ValorDeVAR = ListaAUXSubAmbito.Split(' ');
                                            item.Cadena = ValorDeVAR[1];
                                            item.Nombre = "NUMERO";
                                            listaTokensOperando.Add(item);
                                            break;
                                        }
                                        else
                                        {
                                            LISTA_DE_ERRORES.Add("Error de operacion, el ID: " + VecAmbitoKey[1] + ", no es de tipo int" + ", en el metodo: " + ID_Metodo);
                                            Error4 = false;
                                            break;
                                        }
                                        //ValorDeVAR = item2.AMBITO.ElementAt().v.Split(0);
                                        //var VecAmbitoVal = item.AMBITO.ElementAt(0).Key
                                    }
                                }
                            }
                            else
                            {
                                //error no existe el identificador
                                LISTA_DE_ERRORES.Add("El ID: " + item.Cadena + ", no es de tipo int" + ", en el metodo: " + ID_Metodo);
                                Error4 = false;
                                correrEnAsignacionDeVar++;
                                CorrimientoTkMetodo++;
                                CorriminetoNuevosTokens++;
                            }
                            //Error2 = false;
                        }
                        else
                        {
                            Error4 = false;
                            correrEnAsignacionDeVar++;
                            CorrimientoTkMetodo++;
                            CorriminetoNuevosTokens++;
                        }
                    }
                    if (Error4)
                    {
                        //LECTOR_DE_CADENA = LECTOR_DE_CADENA.Trim();
                        Resolver Postfijo = new Resolver();
                        var Post = Postfijo.CambiarPostfijo(listaTokensOperando);
                        LECTOR_DE_CADENA = string.Empty;
                        if (Post.ElementAt(0) != "Error")
                        {
                            var Resultado = Postfijo.evaluarResultado(Post);
                            ListaAUXAmbito.Add("Valor: " + Resultado.ToString());
                        }
                        else
                        {
                            //var vec = LECTOR_DE_CADENA.Split(' ');
                            //LISTA_DE_ERRORES.Add("Error de operacion, se esperaba int en: " + IdVariable + " en el metodo: " + ID_Metodo);
                            ////error
                        }
                    }

                    break;
                case "double":
                    LECTOR_DE_CADENA = string.Empty;
                    listaTokensOperando = new List<PropiedadesDePalabras>();
                    var Error3 = true;
                    var existeUnVar3 = true;
                    var ListaIDNoDeclarados3 = new List<string>();
                    foreach (var item in lista_tks)
                    {
                        if (item.Cadena != ";" && Error3 && (item.Nombre == "DECIMAL" || item.Nombre == "OPERADOR" || item.Nombre == "NUMERO" || item.Cadena == ")" || item.Cadena == "("))
                        {
                            //LECTOR_DE_CADENA += item.Cadena;
                            listaTokensOperando.Add(item);
                            //CuantasVariablesMeComi++;
                            correrEnAsignacionDeVar++;
                            CorrimientoTkMetodo++;
                            CorriminetoNuevosTokens++;
                            //cantidadDeTokensAoperar++;
                            //CorrimientoTkMetodoFaseAnterior++;
                        }
                        else if (item.Cadena == ";" && Error3 == true)
                        {
                            //CuantasVariablesMeComi++;
                            correrEnAsignacionDeVar++;
                            CorrimientoTkMetodo++;
                            CorriminetoNuevosTokens++;
                            break;
                            //CorrimientoTkMetodoFaseAnterior++;
                        }
                        else if (item.Cadena == ";" && Error3 == false)
                        {
                            //CuantasVariablesMeComi++;
                            correrEnAsignacionDeVar++;
                            CorrimientoTkMetodo++;
                            CorriminetoNuevosTokens++;
                            LISTA_DE_ERRORES.Add("Error de asignacion, se esperaba double en: " + IdVariable + ", en el metodo: " + ID_Metodo);
                            break;
                            //CorrimientoTkMetodoFaseAnterior++;
                        }
                        else if (item.Nombre == "IDENTIFICADOR")//Como es un ID, voy a ver si existe y luego su valor
                        {
                            if (ComprobarExistenciaDefinicionVar(item.Cadena, ID_Metodo))
                            {
                                correrEnAsignacionDeVar++;
                                CorrimientoTkMetodo++;
                                CorriminetoNuevosTokens++;
                                foreach (var item2 in LISTA_DE_AMBITOS)
                                {
                                    var VecAmbito = item2.AMBITO.ElementAt(0).Key.Split(' ');//VARIABLE: id TIPO: int
                                    //var ValorDeVAR = new string[2];
                                    if (item.Cadena == VecAmbito[1])
                                    {
                                        //TipoDeVar = VecAmbito[3].Substring(0, VecAmbito[3].Length - 1);
                                        var VecAmbitoKey = item2.AMBITO.ElementAt(0).Key.Split(' ');//Var: id Tipo: int
                                        var VecAmbitoVal = item2.AMBITO.ElementAt(0).Value.ElementAt(0).Split(' ');//Ambito: ID_Metodo
                                        if (VecAmbitoKey[3] == "int\n" || VecAmbitoKey[3] == "double\n")
                                        {
                                            var ultimoIndice = item2.AMBITO.ElementAt(0).Value.Count;
                                            var ListaAUXSubAmbito = item2.AMBITO.ElementAt(0).Value.ElementAt(ultimoIndice - 1); //Val: ultimo valor asignado
                                            var ValorDeVAR = ListaAUXSubAmbito.Split(' ');
                                            item.Cadena = ValorDeVAR[1];
                                            item.Nombre = "DECIMAL";
                                            listaTokensOperando.Add(item);
                                            break;
                                        }
                                        else
                                        {
                                            LISTA_DE_ERRORES.Add("Error de asignacion, el ID: " + VecAmbitoKey[1] + ", no es de tipo double" + ", en el metodo: " + ID_Metodo);
                                            break;
                                        }
                                        //ValorDeVAR = item2.AMBITO.ElementAt().v.Split(0);
                                        //var VecAmbitoVal = item.AMBITO.ElementAt(0).Key
                                    }
                                }

                            }
                            else if (ComprobarExistenciaDefinicionConst(item.Cadena, ID_Metodo))
                            {
                                correrEnAsignacionDeVar++;
                                CorrimientoTkMetodo++;
                                CorriminetoNuevosTokens++;
                                foreach (var item2 in LISTA_DE_AMBITOS)
                                {
                                    var VecAmbito = item2.AMBITO.ElementAt(0).Key.Split(' ');//VARIABLE: id TIPO: int
                                    //var ValorDeVAR = new string[2];
                                    if (item.Cadena == VecAmbito[1])
                                    {
                                        //TipoDeVar = VecAmbito[3].Substring(0, VecAmbito[3].Length - 1);
                                        var VecAmbitoKey = item2.AMBITO.ElementAt(0).Key.Split(' ');//Var: id Tipo: int
                                        var VecAmbitoVal = item2.AMBITO.ElementAt(0).Value.ElementAt(0).Split(' ');//Ambito: ID_Metodo
                                        if (VecAmbitoKey[3] == "int\n" || VecAmbitoKey[3] == "double\n")
                                        {
                                            var ultimoIndice = item2.AMBITO.ElementAt(0).Value.Count;
                                            var ListaAUXSubAmbito = item2.AMBITO.ElementAt(0).Value.ElementAt(ultimoIndice - 1); //Val: ultimo valor asignado
                                            var ValorDeVAR = ListaAUXSubAmbito.Split(' ');
                                            item.Cadena = ValorDeVAR[1];
                                            item.Nombre = "DECIMAL";
                                            listaTokensOperando.Add(item);
                                            break;
                                        }
                                        else
                                        {
                                            LISTA_DE_ERRORES.Add("Error de operacion, el ID: " + VecAmbitoKey[1] + ", no es de tipo double" + ", en el metodo: " + ID_Metodo);
                                            break;
                                        }
                                        //ValorDeVAR = item2.AMBITO.ElementAt().v.Split(0);
                                        //var VecAmbitoVal = item.AMBITO.ElementAt(0).Key
                                    }
                                }
                            }
                            else
                            {
                                //error no existe el identificador
                                LISTA_DE_ERRORES.Add("El ID: " + item.Cadena + ", no es de tipo double" + ", en el metodo: " + ID_Metodo);
                                Error3 = false;
                                correrEnAsignacionDeVar++;
                                CorrimientoTkMetodo++;
                                CorriminetoNuevosTokens++;
                            }
                            //Error3 = false;
                            ////CuantasVariablesMeComi++;
                            //correrEnAsignacionDeVar++;
                            //CorrimientoTkMetodo++;
                            //CorriminetoNuevosTokens++;
                            ////CorrimientoTkMetodoFaseAnterior++;
                        }
                        else if (item.Cadena == ";" && Error3 == false && existeUnVar3 == true)
                        {
                            //CuantasVariablesMeComi++;
                            correrEnAsignacionDeVar++;
                            CorrimientoTkMetodo++;
                            CorriminetoNuevosTokens++;
                            LISTA_DE_ERRORES.Add("Error de asignacion, se esperaba double en: " + IdVariable + ", en el metodo: " + ID_Metodo);
                            break;
                            //CorrimientoTkMetodoFaseAnterior++;
                        }
                        else if (item.Cadena == ";" && Error3 == false && existeUnVar3 == false)
                        {
                            //CuantasVariablesMeComi++;
                            correrEnAsignacionDeVar++;
                            CorrimientoTkMetodo++;
                            CorriminetoNuevosTokens++;
                            foreach (var item2 in ListaIDNoDeclarados3)
                            {
                                LISTA_DE_ERRORES.Add("El ID: " + item2 + ",no esta declarada");
                            }
                            break;
                            //CorrimientoTkMetodoFaseAnterior++;
                        }
                        else if (item.Nombre == "IDENTIFICADOR" && Error3 == false)
                        {
                            Error3 = false;
                            correrEnAsignacionDeVar++;
                            CorrimientoTkMetodo++;
                            CorriminetoNuevosTokens++;
                            LISTA_DE_ERRORES.Add("El ID: " + item.Cadena + ", no es de tipo double, en el metodo: " + ID_Metodo);
                        }
                        else if ((item.Nombre == "OPERADOR" || item.Nombre=="DELIMITADOR"|| item.Nombre == "CADENA"))
                        {
                            Error3 = false;
                            correrEnAsignacionDeVar++;
                            CorrimientoTkMetodo++;
                            CorriminetoNuevosTokens++;
                        }
                        else
                        {
                            //error no existe el identificador
                            ListaIDNoDeclarados3.Add(item.Cadena);
                            existeUnVar = false;
                            Error4 = false;
                            correrEnAsignacionDeVar++;
                            CorrimientoTkMetodo++;
                            CorriminetoNuevosTokens++;

                        }

                    }
                    if (Error3)
                    {
                        //LECTOR_DE_CADENA = LECTOR_DE_CADENA.Trim();
                        ResolverDecimal Postfijo2 = new ResolverDecimal();
                        var Post2 = Postfijo2.CambiarPostfijo(listaTokensOperando);
                        LECTOR_DE_CADENA = string.Empty;
                        if (Post2.ElementAt(0) != "Error")
                        {
                            var Resultado = Postfijo2.evaluarResultado(Post2);
                            ListaAUXAmbito.Add("Valor: " + Resultado.ToString());
                        }
                        //else
                        //{
                        //    LISTA_DE_ERRORES.Add("Error de operacion, se esperaba double en: " + IdVariable + " en el metodo: " + ID_Metodo);
                        //    //error
                        //}
                    }

                    break;
                case "string":
                    LECTOR_DE_CADENA = string.Empty;
                    var Error2 = true;
                    var existeUnVar2 = true;
                    var ListaIDNoDeclarados2 = new List<string>();
                    var Concat = false;
                    foreach (var item in lista_tks)
                    {
                        if (item.Cadena != ";" && Error2)
                        {
                            try
                            {
                                int Er = int.Parse(item.Cadena);
                                Error2 = false;
                                if (item.Cadena != ";")
                                {
                                    correrEnAsignacionDeVar++;
                                    CorrimientoTkMetodo++;
                                    //CorrimientoTkMetodoFaseAnterior++;
                                    CorriminetoNuevosTokens++;
                                    // Ignorar = true;
                                }
                                else if (item.Cadena == ";")
                                {
                                    correrEnAsignacionDeVar++;
                                    CorrimientoTkMetodo++;
                                    //CorrimientoTkMetodoFaseAnterior++;
                                    CorriminetoNuevosTokens++;
                                    LISTA_DE_ERRORES.Add("Error de asignacion, se esperaba string en: " + IdVariable + ", en el metodo: " + ID_Metodo);
                                    break;
                                }
                                //error
                            }
                            catch (Exception)
                            {
                                if (item.Nombre == "OPERADOR" && item.Cadena == "+")
                                {
                                    Concat = true;
                                    correrEnAsignacionDeVar++;
                                    CorrimientoTkMetodo++;
                                    //CorrimientoTkMetodoFaseAnterior++;
                                    CorriminetoNuevosTokens++;
                                }
                                else if (Concat)
                                {
                                    Concat = false;
                                    if (item.Nombre == "IDENTIFICADOR")//Como es un ID, voy a ver si existe y luego su valor
                                    {
                                        if (ComprobarExistenciaDefinicionVar(item.Cadena, ID_Metodo))
                                        {
                                            correrEnAsignacionDeVar++;
                                            CorrimientoTkMetodo++;
                                            CorriminetoNuevosTokens++;
                                            foreach (var item2 in LISTA_DE_AMBITOS)
                                            {
                                                var VecAmbito = item2.AMBITO.ElementAt(0).Key.Split(' ');//VARIABLE: id TIPO: string
                                                                                                         //var ValorDeVAR = new string[2];
                                                if (item.Cadena == VecAmbito[1])
                                                {
                                                    //TipoDeVar = VecAmbito[3].Substring(0, VecAmbito[3].Length - 1);
                                                    var VecAmbitoKey = item2.AMBITO.ElementAt(0).Key.Split(' ');//Var: id Tipo: int
                                                    var VecAmbitoVal = item2.AMBITO.ElementAt(0).Value.ElementAt(0).Split(' ');//Ambito: ID_Metodo
                                                    if (VecAmbitoKey[3] == "string\n")
                                                    {
                                                        var ultimoIndice = item2.AMBITO.ElementAt(0).Value.Count;
                                                        var ListaAUXSubAmbito = item2.AMBITO.ElementAt(0).Value.ElementAt(ultimoIndice - 1); //Val: ultimo valor asignado
                                                        var ValorDeVAR = ListaAUXSubAmbito.Split(' ');
                                                        item.Cadena = ValorDeVAR[1];
                                                        LECTOR_DE_CADENA += item.Cadena;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        LISTA_DE_ERRORES.Add("Error de concatenacion, el ID: " + VecAmbitoKey[1] + ", no es de tipo string" + ", en el metodo: " + ID_Metodo);
                                                        Error2 = false;
                                                        break;
                                                    }
                                                    //ValorDeVAR = item2.AMBITO.ElementAt().v.Split(0);
                                                    //var VecAmbitoVal = item.AMBITO.ElementAt(0).Key
                                                }
                                            }
                                            //Error2 = false;
                                        }
                                        else if (ComprobarExistenciaDefinicionConst(item.Cadena, ID_Metodo))
                                        {
                                            correrEnAsignacionDeVar++;
                                            CorrimientoTkMetodo++;
                                            CorriminetoNuevosTokens++;
                                            foreach (var item2 in LISTA_DE_AMBITOS)
                                            {
                                                var VecAmbito = item2.AMBITO.ElementAt(0).Key.Split(' ');//VARIABLE: id TIPO: int
                                                                                                         //var ValorDeVAR = new string[2];
                                                if (item.Cadena == VecAmbito[1])
                                                {
                                                    //TipoDeVar = VecAmbito[3].Substring(0, VecAmbito[3].Length - 1);
                                                    var VecAmbitoKey = item2.AMBITO.ElementAt(0).Key.Split(' ');//Var: id Tipo: int
                                                    var VecAmbitoVal = item2.AMBITO.ElementAt(0).Value.ElementAt(0).Split(' ');//Ambito: ID_Metodo
                                                    if (VecAmbitoKey[3] == "string\n")
                                                    {
                                                        var ultimoIndice = item2.AMBITO.ElementAt(0).Value.Count;
                                                        var ListaAUXSubAmbito = item2.AMBITO.ElementAt(0).Value.ElementAt(ultimoIndice - 1); //Val: ultimo valor asignado
                                                        var ValorDeVAR = ListaAUXSubAmbito.Split(' ');
                                                        item.Cadena = ValorDeVAR[1];
                                                        LECTOR_DE_CADENA += item.Cadena;
                                                        break;
                                                    }
                                                    else
                                                    {
                                                        LISTA_DE_ERRORES.Add("Error de concatenacion, el ID: " + VecAmbitoKey[1] + ", no es de tipo string" + ", en el metodo: " + ID_Metodo);
                                                        Error2=false;
                                                        break;
                                                    }
                                                    //ValorDeVAR = item2.AMBITO.ElementAt().v.Split(0);
                                                    //var VecAmbitoVal = item.AMBITO.ElementAt(0).Key
                                                }
                                            }
                                            //Error2 = false;
                                        }
                                        else
                                        {
                                            LISTA_DE_ERRORES.Add("El ID: " + item.Cadena + ", no es de tipo string" + ", en el metodo: " + ID_Metodo);
                                            Error2 = false;
                                            correrEnAsignacionDeVar++;
                                            CorrimientoTkMetodo++;
                                            CorriminetoNuevosTokens++;
                                        }
                                        ////CuantasVariablesMeComi++;
                                        //correrEnAsignacionDeVar++;
                                        //CorrimientoTkMetodo++;
                                        //CorriminetoNuevosTokens++;
                                        ////CorrimientoTkMetodoFaseAnterior++;
                                    }
                                    else if (item.Cadena == ";" && Error2 == false && existeUnVar2 == true)
                                    {
                                        //CuantasVariablesMeComi++;
                                        correrEnAsignacionDeVar++;
                                        CorrimientoTkMetodo++;
                                        CorriminetoNuevosTokens++;
                                        LISTA_DE_ERRORES.Add("Error de asignacion, se esperaba string en: " + IdVariable + ", en el metodo: " + ID_Metodo);
                                        break;
                                        //CorrimientoTkMetodoFaseAnterior++;
                                    }
                                    else if (item.Cadena == ";" && Error2 == false && existeUnVar2 == false)
                                    {
                                        //CuantasVariablesMeComi++;
                                        correrEnAsignacionDeVar++;
                                        CorrimientoTkMetodo++;
                                        CorriminetoNuevosTokens++;
                                        foreach (var item2 in ListaIDNoDeclarados2)
                                        {
                                            LISTA_DE_ERRORES.Add("El ID: " + item2 + ",no esta declarada");
                                        }
                                        break;
                                        //CorrimientoTkMetodoFaseAnterior++;
                                    }
                                    else if (item.Nombre == "CADENA")
                                    {
                                        correrEnAsignacionDeVar++;
                                        CorrimientoTkMetodo++;
                                        CorriminetoNuevosTokens++;
                                        LECTOR_DE_CADENA += item.Cadena;
                                    }
                                    else
                                    {
                                        //error no existe el identificador
                                        ListaIDNoDeclarados2.Add(item.Cadena);
                                        existeUnVar = false;
                                        Error2 = false;
                                        correrEnAsignacionDeVar++;
                                        CorrimientoTkMetodo++;
                                        CorriminetoNuevosTokens++;

                                    }
                                }
                                else if (item.Nombre == "CADENA" && Error2&&existeUnVar2==true)
                                {
                                    LECTOR_DE_CADENA += item.Cadena;
                                    //ListaAUXAmbito.Add("Valor: " + LECTOR_DE_CADENA);
                                    //CuantasVariablesMeComi++;
                                    correrEnAsignacionDeVar++;
                                    CorrimientoTkMetodo++;
                                    CorriminetoNuevosTokens++;
                                    //CorrimientoTkMetodoFaseAnterior++;
                                }
                                else
                                {
                                    Error2 = false;
                                    if (item.Cadena != ";")
                                    {
                                        correrEnAsignacionDeVar++;
                                        CorrimientoTkMetodo++;
                                        //CorrimientoTkMetodoFaseAnterior++;
                                        CorriminetoNuevosTokens++;
                                        // Ignorar = true;
                                    }
                                    else if (item.Cadena == ";")
                                    {
                                        correrEnAsignacionDeVar++;
                                        CorrimientoTkMetodo++;
                                        //CorrimientoTkMetodoFaseAnterior++;
                                        CorriminetoNuevosTokens++;
                                        LISTA_DE_ERRORES.Add("Error de asignacion, se esperaba string en: " + IdVariable + ", en el metodo: " + ID_Metodo);
                                        break;
                                    }
                                }
                            }
                        }
                        else if (item.Cadena == ";" && Error2 == false)
                        {
                            //CuantasVariablesMeComi++;
                            correrEnAsignacionDeVar++;
                            CorrimientoTkMetodo++;
                            CorriminetoNuevosTokens++;
                            LISTA_DE_ERRORES.Add("Error de asignacion, se esperaba string en: " + IdVariable + ", en el metodo: " + ID_Metodo);
                            break;
                            //CorrimientoTkMetodoFaseAnterior++;
                        }
                        else if (item.Cadena == ";" && Error2 == true)
                        {
                            ListaAUXAmbito.Add("Valor: " + LECTOR_DE_CADENA);
                            LECTOR_DE_CADENA = string.Empty;
                            correrEnAsignacionDeVar++;
                            CorrimientoTkMetodo++;
                            CorriminetoNuevosTokens++;
                            break;
                        }
                        else
                        {
                            correrEnAsignacionDeVar++;
                            CorrimientoTkMetodo++;
                            CorriminetoNuevosTokens++;
                        }
                    }

                    break;
                case "bool":
                    LECTOR_DE_CADENA = string.Empty;
                    var Error = true;
                    var Ignorar = false;
                    var existeUnVar4 = true;
                    var ListaIDNoDeclarados4 = new List<string>();
                    foreach (var item in lista_tks)// no termine de sacar los tokens ya leidos
                    {
                        if (item.Cadena != ";" && Error)
                        {
                            if (item.Cadena == "true" || item.Cadena == "false")
                            {
                                LECTOR_DE_CADENA += item.Cadena;
                                //CuantasVariablesMeComi++;
                                correrEnAsignacionDeVar++;
                                CorrimientoTkMetodo++;
                                //CorrimientoTkMetodoFaseAnterior++;
                                CorriminetoNuevosTokens++;
                                ListaAUXAmbito.Add("Valor: " + LECTOR_DE_CADENA);
                            }
                            else
                            {
                                Error = false;
                                if (item.Cadena != ";")
                                {
                                    correrEnAsignacionDeVar++;
                                    CorrimientoTkMetodo++;
                                    //CorrimientoTkMetodoFaseAnterior++;
                                    CorriminetoNuevosTokens++;
                                    Ignorar = true;
                                }
                                else if (item.Cadena == ";")
                                {
                                    correrEnAsignacionDeVar++;
                                    CorrimientoTkMetodo++;
                                    //CorrimientoTkMetodoFaseAnterior++;
                                    CorriminetoNuevosTokens++;
                                    LISTA_DE_ERRORES.Add("Error de asignacion, se esperaba bool en: " + IdVariable + " en el metodo: " + ID_Metodo);
                                    break;
                                }

                                //error
                            }
                        }
                        else if (item.Cadena == ";" && Error == false)
                        {
                            //CuantasVariablesMeComi++;
                            correrEnAsignacionDeVar++;
                            CorrimientoTkMetodo++;
                            CorriminetoNuevosTokens++;
                            LISTA_DE_ERRORES.Add("Error de asignacion, se esperaba bool en: " + IdVariable + " en el metodo: " + ID_Metodo);
                            break;
                            //CorrimientoTkMetodoFaseAnterior++;
                        }
                        else if (item.Cadena == ";" && Error == true)
                        {
                            //CuantasVariablesMeComi++;
                            correrEnAsignacionDeVar++;
                            CorrimientoTkMetodo++;
                            CorriminetoNuevosTokens++;
                            break;
                            //CorrimientoTkMetodoFaseAnterior++;
                        }
                        else
                        {
                            correrEnAsignacionDeVar++;
                            CorrimientoTkMetodo++;
                            CorriminetoNuevosTokens++;
                        }
                    }
                    break;
                default:
                    break;
            }
            return correrEnAsignacionDeVar;
        }

        //public void AgregarVariables(string LectorCadena)
        //{

        //}



        //public void VariableMultiDeclaracion(string LectorCadena)
        //{
        //    //aqui voy a definir puras variables y crear ambito

        //    var quitarTokns = 0;
        //    foreach (var item in LISTA_TOKENS)
        //    {
        //        if (item.Cadena != ";")
        //        {
        //            LectorCadena += item.Cadena+" ";
        //            CuantasVariablesMeComi++;
        //        }
        //        else
        //        {
        //            CuantasVariablesMeComi++;
        //            break;
        //        }
        //    }

        //    var Lista_Variables = new List<string>();
        //    var vecLectorCad = LectorCadena.Split(' ');
        //    var Tipo_Variable = vecLectorCad[0];
        //    for (int i = 1; i < vecLectorCad.Length; i++)
        //    {
        //        if (vecLectorCad[i] != "," && vecLectorCad[i] != "")
        //        {
        //            Lista_Variables.Add(vecLectorCad[i]);
        //        }
        //    }
        //    quitarTokns = CuantasVariablesMeComi;
        //    while (quitarTokns>0)
        //    {
        //        LISTA_TOKENS.Dequeue();
        //        quitarTokns--;
        //    }
        //    foreach (var item in Lista_Variables)
        //    {
        //        var lista_Variables_Declaradas = new List<string>();
        //        Ambitos NuevoAmbitoVariable = new Ambitos();
        //        NuevoAmbitoVariable.AMBITO.Add("Variable: "+item+" Tipo: "+Tipo_Variable+"\n", lista_Variables_Declaradas);
        //        LISTA_DE_AMBITOS.Add(NuevoAmbitoVariable);
        //    }

        //}

        //public void VariableVecMultiDeclaracion(string LectorCadena)
        //{
        //    //aqui voy a definir puras variables y crear ambito

        //    var quitarTokns = 0;
        //    foreach (var item in LISTA_TOKENS)
        //    {
        //        if (item.Cadena != ";" && item.Cadena != "[]")
        //        {
        //            LectorCadena += item.Cadena + " ";
        //            quitarTokns++;
        //        }
        //        else if (item.Cadena == "[]")
        //        {
        //            quitarTokns++;
        //        }
        //        else
        //        {
        //            quitarTokns++;
        //            break;
        //        }
        //    }

        //    var Lista_Variables = new List<string>();
        //    var vecLectorCad = LectorCadena.Split(' ');
        //    var Tipo_Variable = vecLectorCad[0];
        //    for (int i = 1; i < vecLectorCad.Length; i++)
        //    {
        //        if (vecLectorCad[i] != "," && vecLectorCad[i] != "" && vecLectorCad[i] != "[]")
        //        {
        //            Lista_Variables.Add(vecLectorCad[i]);
        //        }
        //    }

        //    while (quitarTokns > 0)
        //    {
        //        LISTA_TOKENS.Dequeue();
        //        quitarTokns--;
        //    }
        //    LECTOR_DE_CADENA = string.Empty;
        //    foreach (var item in Lista_Variables)
        //    {
        //        var lista_Variables_Declaradas = new List<string>();
        //        Ambitos NuevoAmbitoVariable = new Ambitos();
        //        NuevoAmbitoVariable.AMBITO.Add("Variable: " + item + " Tipo: " + Tipo_Variable+"[]" + "\n", lista_Variables_Declaradas);
        //        LISTA_DE_AMBITOS.Add(NuevoAmbitoVariable);
        //    }
        //}

    }
}
