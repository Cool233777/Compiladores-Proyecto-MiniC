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
        public static Stack<string> SIMBOLO_PARSER = new Stack<string>();
        public static Queue<PropiedadesDePalabras> CADENA = new Queue<PropiedadesDePalabras>();
        public static string ACCION = "";
        public static Dictionary<int, string[]> DIC_DE_NO_TERMINALES;

        public AnalizadorSintactico(List<PropiedadesDePalabras> Tokns)
        {
            foreach (var item in Tokns)
            {
                CADENA.Enqueue(item);
            }
        }

        public void CrearDicNoTerminales()
        {
            var DicARegresar = new Dictionary<int, string[]>();
            //S'-> Program
            DicARegresar.Add(0, new string[] { "1", "S'" });//Numero de produccion, {cuantos produce, nombre de produccion }
            //Program-> Decl Program
            //DicARegresar.Add(1, 2);
            ////Program-> Decl
            //DicARegresar.Add(2, 1);
            ////Decl-> VariableDecl
            //DicARegresar.Add(3, 1);
            ////Decl-> FunctionsDecl
            //DicARegresar.Add(4, 1);
            ////Decl->  ConstDecl
            //DicARegresar.Add(5, 1);
            ////Decl-> ClassDecl
            //DicARegresar.Add(6, 1);
            ////Decl-> InterfaceDecl
            //DicARegresar.Add(7, 1);
            ////VariableDecl-> Variable
            //DicARegresar.Add(8, 2);
            ////Variable> Type ident
            //DicARegresar.Add(9, 2);
            ////10. ConstDecl-> const ConstType ident ;
            //DicARegresar.Add(10, 4);
            ////11.ConstType -> int
            //DicARegresar.Add(11, 1);
            ////12.	ConstType -> double 
            //DicARegresar.Add(12, 1);
            ////13.	ConstType -> bool 
            //DicARegresar.Add(13, 1);
            ////14.	ConstType -> string
            //DicARegresar.Add(14, 1);
            ////15.Type->Type_P Type_R
            //DicARegresar.Add(15, 2);
            ////16.Type_P-> int
            //DicARegresar.Add(16, 1);
            ////17.Type_P-> double
            //DicARegresar.Add(17, 1);
            //18.Type_P-> bool
            DicARegresar.Add(18, new string[] { "1", "Type_P" });
            ////19.Type_P-> string
            //DicARegresar.Add(19, 1);
            ////20.Type_P->ident
            //DicARegresar.Add(20, 1);
            ////21.Type_R-> []
            //DicARegresar.Add(21, 1);
            ////22.Type_R-> ''
            //DicARegresar.Add(22, 0);
            ////23.FunctionsDecl->Type ident(Formals) StmtBlock
            //DicARegresar.Add(23, 6);
            ////24.FunctionsDecl-> void ident(Formals ) StmtBlock
            //DicARegresar.Add(24, 6);
            ////25.Formals->Variable Formals_P
            //DicARegresar.Add(25, 2);
            ////26.Formals_P-> , Formals
            //DicARegresar.Add(26, 2);
            ////27.Formals_P-> ''
            //DicARegresar.Add(27, 0);
            ////28.ClassDecl-> class ident ClassDecl_P { ClassDecl_Q }
            //DicARegresar.Add(28, 6);
            ////29.	ClassDecl_P -> : ident ClassDecl_R
            //DicARegresar.Add(29, 3);
            ////30.	ClassDecl_P -> ''
            //DicARegresar.Add(30, 0);
            ////31.	ClassDecl_R -> ClassDecl_O
            //DicARegresar.Add(31, 1);
            ////32.	ClassDecl_R -> ''
            //DicARegresar.Add(32, 0);
            ////33.	ClassDecl_O -> , ident
            //DicARegresar.Add(33, 2);
            ////34.	ClassDecl_O -> , ident ClassDecl_O
            //DicARegresar.Add(34, 3);
            ////35.	ClassDecl_Q -> Field ClassDecl_Q
            //DicARegresar.Add(35, 2);
            ////36.	ClassDecl_Q -> ''
            //DicARegresar.Add(36, 0);
            ////37.	Field -> VariableDecl 
            //DicARegresar.Add(37, 1);
            ////38.	Field -> FunctionsDecl
            //DicARegresar.Add(38, 1);
            ////39.	Field -> ConstDecl
            //DicARegresar.Add(39, 1);
            ////40.	InterfaceDecl -> interface ident { InterfaceDecl_P }
            //DicARegresar.Add(40, 5);
            ////41.	InterfaceDecl_P -> Prototype InterfaceDecl_P
            //DicARegresar.Add(41, 2);
            ////42.	InterfaceDecl_P -> ''
            //DicARegresar.Add(42, 0);
            ////43.	Prototype -> Type ident(Formals );
            //DicARegresar.Add(43, 6);
            ////44.	Prototype -> void ident(Formals );
            //DicARegresar.Add(44, 6);
            ////45.	StmtBlock -> { StmtBlock_P StmtBlock_R StmtBlock_O}
            //DicARegresar.Add(45, 5);
            ////46.	StmtBlock_P -> VariableDecl StmtBlock_P
            //DicARegresar.Add(46, 2);
            ////47.	StmtBlock_P -> ''
            //DicARegresar.Add(47, 0);
            ////48.	StmtBlock_R -> ConstDecl StmtBlock_R
            //DicARegresar.Add(48, 2);
            ////49.	StmtBlock_R -> ''
            //DicARegresar.Add(49, 0);
            ////50.	StmtBlock_O -> Stmt StmtBlock_O
            //DicARegresar.Add(50, 2);
            ////51.	StmtBlock_O -> ''
            //DicARegresar.Add(51, 0);
            ////52.	Stmt -> IfStmt
            //DicARegresar.Add(52, 1);
            ////53.	Stmt -> WhileStmt  
            //DicARegresar.Add(53, 1);
            ////54.	Stmt -> Stmt_P ;
            //DicARegresar.Add(54, 2);
            ////55.	Stmt -> ForStmt 
            //DicARegresar.Add(55, 1);
            ////56.	Stmt -> BreakStmt 
            //DicARegresar.Add(56, 1);
            ////57.	Stmt -> ReturnStmt 
            //DicARegresar.Add(57, 1);
            ////58.	Stmt -> PrintStmt 
            //DicARegresar.Add(58, 1);
            ////59.	Stmt -> StmtBlock 
            //DicARegresar.Add(59, 1);
            ////60.	Stmt_P -> Expr
            //DicARegresar.Add(60, 1);
            ////61.	Stmt_P -> ''
            //DicARegresar.Add(61, 0);
            ////62.	IfStmt -> if (Expr ) Stmt IfStmt_P 
            //DicARegresar.Add(62, 6);
            ////63.	IfStmt_P -> else Stmt 
            //DicARegresar.Add(63, 2);
            ////64.	IfStmt_P -> ''
            //DicARegresar.Add(64, 0);
            ////65.	WhileStmt -> while (Expr ) Stmt
            //DicARegresar.Add(65, 5);
            ////66.	ForStmt -> for (Expr ; Expr ; Expr ) Stmt
            //DicARegresar.Add(66, 9);
            ////67.	ReturnStmt -> return Expr ;
            //DicARegresar.Add(67, 3);
            ////68.	BreakStmt -> break ;
            //DicARegresar.Add(68, 2);
            ////69.	PrintStmt -> Console.Writeline(Expr PrintStmt_P ) ;
            //DicARegresar.Add(69, 8);
            ////70.	PrintStmt_P -> , Expr PrintStmt_P
            //DicARegresar.Add(70, 3);
            ////71.	PrintStmt_P -> ''
            //DicARegresar.Add(71, 0);
            ////72.	Expr -> ident = ExprOr
            //DicARegresar.Add(72, 3);
            ////73.	Expr -> ExprOr
            //DicARegresar.Add(73, 1);
            ////74.	ExprOr -> ExprOr == ExprOrP
            //DicARegresar.Add(74, 3);
            ////75.	ExprOr -> ExprOrP
            //DicARegresar.Add(75, 1);
            ////76.	ExprOrP -> ExprOrP && ExprAnd
            //DicARegresar.Add(76, 3);
            ////77.	ExprOrP -> ExprAnd
            //DicARegresar.Add(77, 1);
            ////78.	ExprAnd -> ExprAnd<ExprAndP
            //DicARegresar.Add(78, 3);
            ////79.	ExprAnd -> ExprAnd <= ExprAndP
            //DicARegresar.Add(79, 3);
            ////80.	ExprAnd -> ExprAndP
            //DicARegresar.Add(80, 1);
            ////81.	ExprAndP -> ExprAndP + ExprEquals
            //DicARegresar.Add(81, 3);
            ////82.	ExprAndP -> ExprEquals
            //DicARegresar.Add(82, 1);
            ////83.	ExprEquals -> ExprEquals* ExprEqualsP
            //DicARegresar.Add(83, 3);
            ////84.	ExprEquals -> ExprEquals % ExprEqualsP
            //DicARegresar.Add(84, 3);
            ////85.	ExprEquals -> ExprEqualsP
            //DicARegresar.Add(85, 1);
            ////86.	ExprEqualsP -> - ExprComp
            //DicARegresar.Add(86, 2);
            ////87.	ExprEqualsP -> ! ExprComp
            //DicARegresar.Add(87, 2);
            ////88.	ExprEqualsP -> ExprComp
            //DicARegresar.Add(88, 1);
            ////89.	ExprComp -> ExprComp.ident = ExprCompP
            //DicARegresar.Add(89, 5);
            ////90.	ExprComp -> ExprComp.ident
            //DicARegresar.Add(90, 3);
            ////91.	ExprComp -> ExprCompP
            //DicARegresar.Add(91, 1);
            ////92.	ExprCompP -> (Expr )
            //DicARegresar.Add(92, 3);
            ////93.	ExprCompP -> this
            //DicARegresar.Add(93, 1);
            ////94.	ExprCompP -> ident
            //DicARegresar.Add(94, 1);
            ////95.	ExprCompP -> New (ident )
            //DicARegresar.Add(95, 4);
            ////96.	ExprCompP -> intConstant
            //DicARegresar.Add(96, 1);
            ////97.	ExprCompP -> doubleConstant
            //DicARegresar.Add(97, 1);
            ////98.	ExprCompP -> boolConstant
            //DicARegresar.Add(98, 1);
            ////99.	ExprCompP -> stringConstant
            //DicARegresar.Add(99, 1);
            ////100.	ExprCompP -> null
            //DicARegresar.Add(100, 1);
            //terminar aqui

            DIC_DE_NO_TERMINALES = DicARegresar;
        }

        public void Crear_Tabla()
        {
            CrearDicNoTerminales();
            var Simbolos = new string[89] { ";", "id", "const", "int", "double", "bool", "string", "[]", "(", ")", "void", ",", "class", "{", "}", ":", "interface", "if", "else", "while", "for", "return", "break", "Console", ".", "WriteLine", "=", "==", "&&", "<", "<=", "+", "*", "%", "-", "!", "this", "New", "intConstant", "doubleConstant", "boolConstant", "stringConstant", "null", "$", "S'", "Program", "Decl", "VariableDecl", "Variable", "ConstDecl", "ConstType", "Type", "Type_P", "Type_R", "FunctionDecl", "Formals", "Formals_P", "ClassDecl", "ClassDecl_P", "ClassDecl_R", "ClassDecl_O", "ClassDecl_Q", "Field", "InterfaceDecl", "InterfaceDecl_P", "Prototype", "StmtBlock", "StmtBlock_P", "StmtBlock_R", "StmtBlock_O", "Stmt", "Stmt_P", "IfStmt", "IfStmt_P", "WhileStmt", "ForStmt", "ReturnStmt", "BreakStmt", "PrintStmt", "PrintStmt_P", "Expr", "ExprOr", "ExprOrP", "ExprAnd", "ExprAndP", "ExprEquals", "ExprEqualsP", "ExprComp", "ExprCompP" };
            //var Archivo = new FileStream(@"D:\Descargas\SLR.txt", FileMode.Open);
            var Archivo = new FileStream(Path.GetFullPath("SLR.txt"), FileMode.Open);
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
            List<PropiedadesDePalabras> Aux_Lista_De_Cadena = new List<PropiedadesDePalabras>();
            //agregar a la lista auxiliar para usar el foreach
            foreach (var item in CADENA)
            {
                Aux_Lista_De_Cadena.Add(item);
            }

            foreach (var Cadena_A_Evaluar in Aux_Lista_De_Cadena)
            {

                var AccionConLetra = "";
                if (Cadena_A_Evaluar.Nombre == "IDENTIFICADOR")
                {
                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "id");
                }
                else
                {
                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), Cadena_A_Evaluar.Cadena);
                }

                var SplitSinDivisor = AccionConLetra.Split('/');
                var SplitESE = AccionConLetra.Split('s');
                var SplitR = AccionConLetra.Split('r');
                if (SplitSinDivisor.Length > 1)//quiere decir que tiene conflicto
                {

                }
                else if (SplitR.Length > 1)//quiere decir que es una reduccion lo que viene
                {
                    var Estado_A_Desplazarse = int.Parse(SplitR[1]);
                    var Devolver_Dic_De_No_Terminal = DIC_DE_NO_TERMINALES.First(x => x.Key == Estado_A_Desplazarse);
                    var Numeros_A_Desapilar = int.Parse(Devolver_Dic_De_No_Terminal.Value[0]);
                    var Aux_Simbolo_Parser = SIMBOLO_PARSER;
                    while (Numeros_A_Desapilar > 0)//quitar los primeros simbolos, colocar en el primero el simbolo reducido y correr la lista
                    {
                        Aux_Simbolo_Parser.Pop();
                        LA_PILA.Pop();
                        Numeros_A_Desapilar--;
                    }
                    
                    Aux_Simbolo_Parser.Push(Devolver_Dic_De_No_Terminal.Value[1]);
                    SIMBOLO_PARSER = Aux_Simbolo_Parser;
                }
                else if (SplitESE.Length > 1)//quiere decir que viene un desplazamiento
                {
                    var Estado_A_Desplazarse = int.Parse(SplitESE[1]);
                    LA_PILA.Push(Estado_A_Desplazarse);//meto a la pila el numero del estado a desplazar
                    SIMBOLO_PARSER.Push(Cadena_A_Evaluar.Cadena);//agrego a simbolo
                    CADENA.Dequeue();//quito de entrada
                    //var Devolver_Dic_De_No_Terminal = DICCIONARIO_DE_ESTADOS.First(x => x.Key ==  Estado_A_Desplazarse);
                }
                else//quiere decir que fue error de sintaxis
                {

                }

            }
            //manejo de errores: seguir compilando hasta encontrar un ; , { , } , )

            //En las columnas va: Iteracion, Tope de pila, Simbolo, Cadena, Acción
            //Leo primer token
            //Voy al primer estado de la pila(Guardar)

            //me voy a la tabla de análisis con el numero de la pila guardado***
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
            Tabla_De_Parser();
        }

    }
}
