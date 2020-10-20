using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace Proyecto_Compis
{
    public class AnalizadorSintactico
    {
        //public static Dictionary<string, string> DiCCIONARIO_DE_SIMBOLOS = new Dictionary<string, string>();

        public static Dictionary<int, Dictionary<string, string>> DICCIONARIO_DE_ESTADOS = new Dictionary<int, Dictionary<string, string>>();
        public static Stack<int> LA_PILA = new Stack<int>();
        public static List<string> SIMBOLO_PARSER = new List<string>();
        public static Queue<PropiedadesDePalabras> CADENA;
        public static string ACCION = "";
        public static Dictionary<int, int> DIC_DE_NO_TERMINALES;

        public AnalizadorSintactico(List<PropiedadesDePalabras> Tokns)
        {
            foreach (var item in Tokns)
            {
                CADENA.Enqueue(item);
            }
        }

        public void CrearDicNoTerminales()
        {
            var DicARegresar = new Dictionary<int, int>();
            //empezas aqui
            DicARegresar.Add(1, 1);//numero de produccion y cuantos produce
            //terminar aqui

            DIC_DE_NO_TERMINALES = DicARegresar;
        }

        public void Crear_Tabla()
        {
            CrearDicNoTerminales();
            var Simbolos = new string[89] { ";", "id", "const", "int", "double", "bool", "string", "[]", "(", ")", "void", ",", "class", "{", "}", ":", "interface", "if", "else", "while", "for", "return", "break", "Console", ".", "WriteLine", "=", "==", "&&", "<", "<=", "+", "*", "%", "-", "!", "this", "New", "intConstant", "doubleConstant", "boolConstant", "stringConstant", "null", "$", "S'", "Program", "Decl", "VariableDecl", "Variable", "ConstDecl", "ConstType", "Type", "Type_P", "Type_R", "FunctionDecl", "Formals", "Formals_P", "ClassDecl", "ClassDecl_P", "ClassDecl_R", "ClassDecl_O", "ClassDecl_Q", "Field", "InterfaceDecl", "InterfaceDecl_P", "Prototype", "StmtBlock", "StmtBlock_P", "StmtBlock_R", "StmtBlock_O", "Stmt", "Stmt_P", "IfStmt", "IfStmt_P", "WhileStmt", "ForStmt", "ReturnStmt", "BreakStmt", "PrintStmt", "PrintStmt_P", "Expr", "ExprOr", "ExprOrP", "ExprAnd", "ExprAndP", "ExprEquals", "ExprEqualsP", "ExprComp", "ExprCompP" };
            var Archivo = new FileStream(@"D:\Descargas\SLR.txt", FileMode.Open);
            var Lector = new StreamReader(Archivo);
            var PosEstados = 0;
            while (!Lector.EndOfStream)
            {
                Dictionary<string, string> Dic_simbolos = new Dictionary<string, string>();
                var SplitSinPuntoYcoma = Lector.ReadLine().Split(';');
                var PosSimbolos = 0;
                foreach (var item in SplitSinPuntoYcoma)
                {
                    Dic_simbolos.Add(Simbolos[PosSimbolos], item);
                    PosSimbolos++;
                }
                DICCIONARIO_DE_ESTADOS.Add(PosEstados, Dic_simbolos);
                PosEstados++;
            }
        }

        public string RegresarAccion(int Tope_De_Pila, string Cadena_A_Evaluar)
        {
            var Devolver_Dic_De_Estado = DICCIONARIO_DE_ESTADOS.First(x => x.Key == Tope_De_Pila);
            var Devolver_Dic_De_Simbolo = Devolver_Dic_De_Estado.Value.First(x => x.Key == Cadena_A_Evaluar);
            return Devolver_Dic_De_Simbolo.Value;
        }

        public void Tabla_De_Parser()
        {
            Crear_Tabla();
            LA_PILA.Push(0);//TOPE DE PILA 0
            foreach (var Cadena_A_Evaluar in CADENA)
            {
                var AccionConLetra = RegresarAccion(LA_PILA.Peek(), Cadena_A_Evaluar.Cadena);
                var SplitSinDivisor = AccionConLetra.Split('/');
                var SplitESE = AccionConLetra.Split('s');
                var SplitR = AccionConLetra.Split('r');
                if (SplitSinDivisor.Length > 1)//quiere decir que tiene conflicto
                {

                }
                else if (SplitR.Length > 1)//quiere decir que es una reduccion lo que viene
                {
                    var Estado_A_Desplazarse = int.Parse(SplitR[2]);
                    var Devolver_Dic_De_Estado = DIC_DE_NO_TERMINALES.First(x => x.Key == Estado_A_Desplazarse);
                    var Numeros_A_Desapilar = Devolver_Dic_De_Estado.Value;
                    while (Numeros_A_Desapilar > 0)//quitar los primeros simbolos, colocar en el primero el simbolo reducido y correr la lista
                    {
                        SIMBOLO_PARSER.RemoveRange(0, Numeros_A_Desapilar);
                        LA_PILA.Pop();
                        Numeros_A_Desapilar--;
                    }
                }
                else if (SplitESE.Length > 1)//quiere decir que viene un desplazamiento
                {
                    var Estado_A_Desplazarse = int.Parse(SplitESE[2]);
                    LA_PILA.Push(Estado_A_Desplazarse);//meto a la pila el numero del estado a desplazar
                    SIMBOLO_PARSER.Add(Cadena_A_Evaluar.Cadena);//agrego a simbolo
                    CADENA.Dequeue();//quito de entrada
                    //var Devolver_Dic_De_Estado = DICCIONARIO_DE_ESTADOS.First(x => x.Key ==  Estado_A_Desplazarse);
                }
                else//quiere decir que fue error de sintaxis
                {

                }

            }
            //manejo de errores: seguir compilando hasta encontrar un ; , { , } , )

            //En las columnas va: Iteracion, Tope de pila, Simbolo, Cadena, Acción
            //Leo primer token
            //Voy al primer estado de la pila(Guardar)

            //me voy a la tabla de análisis con el numero de la pila guardado
            //Verificar la acción
            //Si es un desplazamiento, Ej: D5, Guardo el 5
            //Se apila el numero en la PILA
            //El primer token pasa al simbolo
            //repetir

            //Si es una reducción, se desapila estados como simbolos tenga el lado derecho la produccion de la reduccion
            //Reducir los simbolos(al papa de la produccion)
            //Tope de pila y tomar el simbolo No Terminal reducido, la accion seria el IrA
            //apilar el IrA y vuelvo a preguntar el tope de pila con la cadena
            //repetir

            //si es n, es error de sintaxis

        }

        public void Ejecutar_Analizador()
        {
            Crear_Tabla();
            Tabla_De_Parser();
        }

    }
}
