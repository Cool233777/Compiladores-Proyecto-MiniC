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
        public static bool YA_ENCONTRE_UN_DESPLAZAMIENTO = false;
        bool ERROR_PRIMER_CAMINO = false;
        bool ERROR_SEGUNDO_CAMINO = false;
        public static bool SINTAXIS_CORRECTA = false;
        public static string MENSAJE_RESULTANTE = string.Empty;

        public AnalizadorSintactico(List<PropiedadesDePalabras> Tokns)
        {
            foreach (var item in Tokns)
            {
                CADENA.Enqueue(item);
            }
            PropiedadesDePalabras Dolar = new PropiedadesDePalabras("ESPECIAL", "$", 0, 0, 1);
            CADENA.Enqueue(Dolar);
        }

        public void CrearDicNoTerminales()
        {
            var DicARegresar = new Dictionary<int, string[]>();
            //0.S'-> Program
            DicARegresar.Add(0, new string[] { "1", "S'" });//Numero de produccion, {cuantos produce, nombre de produccion }
            //1.Program-> Decl Program
            DicARegresar.Add(1, new string[] { "2", "Program" });
            //DicARegresar.Add(1, 2);
            //2.Program-> Decl
            DicARegresar.Add(2, new string[] { "1", "Program" });
            //DicARegresar.Add(2, 1);
            ////3.Decl-> VariableDecl
            DicARegresar.Add(3, new string[] { "1", "Decl" });
            //DicARegresar.Add(3, 1);
            ////4.Decl-> FunctionsDecl
            DicARegresar.Add(4, new string[] { "1", "Decl" });
            //DicARegresar.Add(4, 1);
            ////5.Decl->  ConstDecl
            DicARegresar.Add(5, new string[] { "1", "Decl" });
            //DicARegresar.Add(5, 1);
            ////6.Decl-> ClassDecl
            DicARegresar.Add(6, new string[] { "1", "Decl" });
            //DicARegresar.Add(6, 1);
            ////7.Decl-> InterfaceDecl
            DicARegresar.Add(7, new string[] { "1", "Decl" });
            //DicARegresar.Add(7, 1);
            ////8.VariableDecl-> Variable ;
            DicARegresar.Add(8, new string[] { "2", "VariableDecl" });
            //DicARegresar.Add(8, 2);
            ////9.Variable> Type ident
            DicARegresar.Add(9, new string[] { "2", "Variable" });
            //DicARegresar.Add(9, 2);
            ////10. ConstDecl-> const ConstType ident ;
            DicARegresar.Add(10, new string[] { "4", "ConstDecl" });
            //DicARegresar.Add(10, 4);
            ////11.ConstType -> int
            DicARegresar.Add(11, new string[] { "1", "ConstType" });
            //DicARegresar.Add(11, 1);
            ////12.	ConstType -> double 
            DicARegresar.Add(12, new string[] { "1", "ConstType" });
            //DicARegresar.Add(12, 1);
            ////13.	ConstType -> bool 
            DicARegresar.Add(13, new string[] { "1", "ConstType" });
            //DicARegresar.Add(13, 1);
            ////14.	ConstType -> string
            DicARegresar.Add(14, new string[] { "1", "ConstType" });
            //DicARegresar.Add(14, 1);
            ////15.Type->Type_P Type_R
            DicARegresar.Add(15, new string[] { "2", "Type" });
            //DicARegresar.Add(15, 2);
            ////16.Type_P-> int
            DicARegresar.Add(16, new string[] { "1", "Type_P" });
            //DicARegresar.Add(16, 1);
            ////17.Type_P-> double
            DicARegresar.Add(17, new string[] { "1", "Type_P" });
            //DicARegresar.Add(17, 1);
            //18.Type_P-> bool
            DicARegresar.Add(18, new string[] { "1", "Type_P" });
            ////19.Type_P-> string
            DicARegresar.Add(19, new string[] { "1", "Type_P" });
            //DicARegresar.Add(19, 1);
            ////20.Type_P->ident
            DicARegresar.Add(20, new string[] { "1", "Type_P" });
            //DicARegresar.Add(20, 1);
            ////21.Type_R-> []
            DicARegresar.Add(21, new string[] { "1", "Type_R" });
            //DicARegresar.Add(21, 1);
            ////22.Type_R-> ''
            DicARegresar.Add(22, new string[] { "0", "Type_R" });
            //DicARegresar.Add(22, 0);
            ////23.FunctionsDecl->Type ident(Formals) StmtBlock
            DicARegresar.Add(23, new string[] { "6", "FunctionsDecl" });
            //DicARegresar.Add(23, 6);
            ////24.FunctionsDecl-> void ident(Formals ) StmtBlock
            DicARegresar.Add(24, new string[] { "6", "FunctionsDecl" });
            //DicARegresar.Add(24, 6);
            ////25.Formals->Variable Formals_P
            DicARegresar.Add(25, new string[] { "2", "Formals" });
            //DicARegresar.Add(25, 2);
            ////26.Formals_P-> , Formals
            DicARegresar.Add(26, new string[] { "2", "Formals_P" });
            //DicARegresar.Add(26, 2);
            ////27.Formals_P-> ''
            DicARegresar.Add(27, new string[] { "0", "Formals_P" });
            //DicARegresar.Add(27, 0);
            ////28.ClassDecl-> class ident ClassDecl_P { ClassDecl_Q }
            DicARegresar.Add(28, new string[] { "6", "ClassDecl" });
            //DicARegresar.Add(28, 6);
            ////29.	ClassDecl_P -> : ident ClassDecl_R
            DicARegresar.Add(29, new string[] { "3", "ClassDecl_P" });
            //DicARegresar.Add(29, 3);
            ////30.	ClassDecl_P -> ''
            DicARegresar.Add(30, new string[] { "0", "ClassDecl_P" });
            //DicARegresar.Add(30, 0);
            ////31.	ClassDecl_R -> ClassDecl_O
            DicARegresar.Add(31, new string[] { "1", "ClassDecl_R" });
            //DicARegresar.Add(31, 1);
            ////32.	ClassDecl_R -> ''
            DicARegresar.Add(32, new string[] { "0", "ClassDecl_R" });
            //DicARegresar.Add(32, 0);
            ////33.	ClassDecl_O -> , ident
            DicARegresar.Add(33, new string[] { "2", "ClassDecl_O" });
            //DicARegresar.Add(33, 2);
            ////34.	ClassDecl_O -> , ident ClassDecl_O
            DicARegresar.Add(34, new string[] { "3", "ClassDecl_O" });
            //DicARegresar.Add(34, 3);
            ////35.	ClassDecl_Q -> Field ClassDecl_Q
            DicARegresar.Add(35, new string[] { "2", "ClassDecl_Q" });
            //DicARegresar.Add(35, 2);
            ////36.	ClassDecl_Q -> ''
            DicARegresar.Add(36, new string[] { "0", "ClassDecl_Q" });
            //DicARegresar.Add(36, 0);
            ////37.	Field -> VariableDecl 
            DicARegresar.Add(37, new string[] { "1", "Field" });
            //DicARegresar.Add(37, 1);
            ////38.	Field -> FunctionsDecl
            DicARegresar.Add(38, new string[] { "1", "Field" });
            //DicARegresar.Add(38, 1);
            ////39.	Field -> ConstDecl
            DicARegresar.Add(39, new string[] { "1", "Field" });
            //DicARegresar.Add(39, 1);
            ////40.	InterfaceDecl -> interface ident { InterfaceDecl_P }
            DicARegresar.Add(40, new string[] { "5", "InterfaceDecl" });
            //DicARegresar.Add(40, 5);
            ////41.	InterfaceDecl_P -> Prototype InterfaceDecl_P
            DicARegresar.Add(41, new string[] { "2", "InterfaceDecl_P" });
            //DicARegresar.Add(41, 2);
            ////42.	InterfaceDecl_P -> ''
            DicARegresar.Add(42, new string[] { "0", "InterfaceDecl_P" });
            //DicARegresar.Add(42, 0);
            ////43.	Prototype -> Type ident(Formals );
            DicARegresar.Add(43, new string[] { "6", "Prototype" });
            //DicARegresar.Add(43, 6);
            ////44.	Prototype -> void ident(Formals );
            DicARegresar.Add(44, new string[] { "6", "Prototype" });
            //DicARegresar.Add(44, 6);
            ////45.	StmtBlock -> { StmtBlock_P StmtBlock_R StmtBlock_O}
            DicARegresar.Add(45, new string[] { "5", "StmtBlock" });
            //DicARegresar.Add(45, 5);
            ////46.	StmtBlock_P -> VariableDecl StmtBlock_P
            DicARegresar.Add(46, new string[] { "2", "StmtBlock_P" });
            //DicARegresar.Add(46, 2);
            ////47.	StmtBlock_P -> ''
            DicARegresar.Add(47, new string[] { "0", "StmtBlock_P" });
            //DicARegresar.Add(47, 0);
            ////48.	StmtBlock_R -> ConstDecl StmtBlock_R
            DicARegresar.Add(48, new string[] { "2", "StmtBlock_R" });
            //DicARegresar.Add(48, 2);
            ////49.	StmtBlock_R -> ''
            DicARegresar.Add(49, new string[] { "0", "StmtBlock_R" });
            //DicARegresar.Add(49, 0);
            ////50.	StmtBlock_O -> Stmt StmtBlock_O
            DicARegresar.Add(50, new string[] { "2", "StmtBlock_O" });
            //DicARegresar.Add(50, 2);
            ////51.	StmtBlock_O -> ''
            DicARegresar.Add(51, new string[] { "0", "StmtBlock_O" });
            //DicARegresar.Add(51, 0);
            ////52.	Stmt -> IfStmt
            DicARegresar.Add(52, new string[] { "1", "Stmt" });
            //DicARegresar.Add(52, 1);
            ////53.	Stmt -> WhileStmt  
            DicARegresar.Add(53, new string[] { "1", "Stmt" });
            //DicARegresar.Add(53, 1);
            ////54.	Stmt -> Stmt_P ;
            DicARegresar.Add(54, new string[] { "2", "Stmt" });
            //DicARegresar.Add(54, 2);
            ////55.	Stmt -> ForStmt 
            DicARegresar.Add(55, new string[] { "1", "Stmt" });
            //DicARegresar.Add(55, 1);
            ////56.	Stmt -> BreakStmt 
            DicARegresar.Add(56, new string[] { "1", "Stmt" });
            //DicARegresar.Add(56, 1);
            ////57.	Stmt -> ReturnStmt 
            DicARegresar.Add(57, new string[] { "1", "Stmt" });
            //DicARegresar.Add(57, 1);
            ////58.	Stmt -> PrintStmt 
            DicARegresar.Add(58, new string[] { "1", "Stmt" });
            //DicARegresar.Add(58, 1);
            ////59.	Stmt -> StmtBlock 
            DicARegresar.Add(59, new string[] { "1", "Stmt" });
            //DicARegresar.Add(59, 1);
            ////60.	Stmt_P -> Expr
            DicARegresar.Add(60, new string[] { "1", "Stmt_P" });
            //DicARegresar.Add(60, 1);
            ////61.	Stmt_P -> ''
            DicARegresar.Add(61, new string[] { "0", "Stmt_P" });
            //DicARegresar.Add(61, 0);
            ////62.	IfStmt -> if (Expr ) Stmt IfStmt_P 
            DicARegresar.Add(62, new string[] { "6", "IfStmt" });
            //DicARegresar.Add(62, 6);
            ////63.	IfStmt_P -> else Stmt 
            DicARegresar.Add(63, new string[] { "2", "IfStmt_P" });
            //DicARegresar.Add(63, 2);
            ////64.	IfStmt_P -> ''
            DicARegresar.Add(64, new string[] { "0", "IfStmt_P" });
            //DicARegresar.Add(64, 0);
            ////65.	WhileStmt -> while (Expr ) Stmt
            DicARegresar.Add(65, new string[] { "5", "WhileStmt" });
            //DicARegresar.Add(65, 5);
            ////66.	ForStmt -> for (Expr ; Expr ; Expr ) Stmt
            DicARegresar.Add(66, new string[] { "9", "ForStmt" });
            //DicARegresar.Add(66, 9);
            ////67.	ReturnStmt -> return Expr ;
            DicARegresar.Add(67, new string[] { "3", "ReturnStmt" });
            //DicARegresar.Add(67, 3);
            ////68.	BreakStmt -> break ;
            DicARegresar.Add(68, new string[] { "2", "BreakStmt" });
            //DicARegresar.Add(68, 2);
            ////69.	PrintStmt -> Console.Writeline(Expr PrintStmt_P ) ;
            DicARegresar.Add(69, new string[] { "8", "PrintStmt" });
            //DicARegresar.Add(69, 8);
            ////70.	PrintStmt_P -> , Expr PrintStmt_P
            DicARegresar.Add(70, new string[] { "3", "PrintStmt_P" });
            //DicARegresar.Add(70, 3);
            ////71.	PrintStmt_P -> ''
            DicARegresar.Add(71, new string[] { "0", "PrintStmt_P" });
            //DicARegresar.Add(71, 0);
            ////72.	Expr -> ident = ExprOr
            DicARegresar.Add(72, new string[] { "3", "Expr" });
            //DicARegresar.Add(72, 3);
            ////73.	Expr -> ExprOr
            DicARegresar.Add(73, new string[] { "1", "Expr" });
            //DicARegresar.Add(73, 1);
            ////74.	ExprOr -> ExprOr == ExprOrP
            DicARegresar.Add(74, new string[] { "3", "ExprOr" });
            //DicARegresar.Add(74, 3);
            ////75.	ExprOr -> ExprOrP
            DicARegresar.Add(75, new string[] { "1", "ExprOr" });
            //DicARegresar.Add(75, 1);
            ////76.	ExprOrP -> ExprOrP && ExprAnd
            DicARegresar.Add(76, new string[] { "3", "ExprOrP" });
            //DicARegresar.Add(76, 3);
            ////77.	ExprOrP -> ExprAnd
            DicARegresar.Add(77, new string[] { "1", "ExprOrP" });
            //DicARegresar.Add(77, 1);
            ////78.	ExprAnd -> ExprAnd<ExprAndP
            DicARegresar.Add(78, new string[] { "3", "ExprAnd" });
            //DicARegresar.Add(78, 3);
            ////79.	ExprAnd -> ExprAnd <= ExprAndP
            DicARegresar.Add(79, new string[] { "3", "ExprAnd" });
            //DicARegresar.Add(79, 3);
            ////80.	ExprAnd -> ExprAndP
            DicARegresar.Add(80, new string[] { "1", "ExprAnd" });
            //DicARegresar.Add(80, 1);
            ////81.	ExprAndP -> ExprAndP + ExprEquals
            DicARegresar.Add(81, new string[] { "3", "ExprAndP" });
            //DicARegresar.Add(81, 3);
            ////82.	ExprAndP -> ExprEquals
            DicARegresar.Add(82, new string[] { "1", "ExprAndP" });
            //DicARegresar.Add(82, 1);
            ////83.	ExprEquals -> ExprEquals* ExprEqualsP
            DicARegresar.Add(83, new string[] { "3", "ExprEquals" });
            //DicARegresar.Add(83, 3);
            ////84.	ExprEquals -> ExprEquals % ExprEqualsP
            DicARegresar.Add(84, new string[] { "3", "ExprEquals" });
            //DicARegresar.Add(84, 3);
            ////85.	ExprEquals -> ExprEqualsP
            DicARegresar.Add(85, new string[] { "1", "ExprEquals" });
            //DicARegresar.Add(85, 1);
            ////86.	ExprEqualsP -> - ExprComp
            DicARegresar.Add(86, new string[] { "2", "ExprEqualsP" });
            //DicARegresar.Add(86, 2);
            ////87.	ExprEqualsP -> ! ExprComp
            DicARegresar.Add(87, new string[] { "2", "ExprEqualsP" });
            //DicARegresar.Add(87, 2);
            ////88.	ExprEqualsP -> ExprComp
            DicARegresar.Add(88, new string[] { "1", "ExprEqualsP" });
            //DicARegresar.Add(88, 1);
            ////89.	ExprComp -> ExprComp.ident = ExprCompP
            DicARegresar.Add(89, new string[] { "5", "ExprComp" });
            //DicARegresar.Add(89, 5);
            ////90.	ExprComp -> ExprComp.ident
            DicARegresar.Add(90, new string[] { "3", "ExprComp" });
            //DicARegresar.Add(90, 3);
            ////91.	ExprComp -> ExprCompP
            DicARegresar.Add(91, new string[] { "1", "ExprComp" });
            //DicARegresar.Add(91, 1);
            ////92.	ExprCompP -> (Expr )
            DicARegresar.Add(92, new string[] { "3", "ExprCompP" });
            //DicARegresar.Add(92, 3);
            ////93.	ExprCompP -> this
            DicARegresar.Add(93, new string[] { "1", "ExprCompP" });
            //DicARegresar.Add(93, 1);
            ////94.	ExprCompP -> ident
            DicARegresar.Add(94, new string[] { "1", "ExprCompP" });
            //DicARegresar.Add(94, 1);
            ////95.	ExprCompP -> New (ident )
            DicARegresar.Add(95, new string[] { "4", "ExprCompP" });
            //DicARegresar.Add(95, 4);
            ////96.	ExprCompP -> intConstant
            DicARegresar.Add(96, new string[] { "1", "ExprCompP" });
            //DicARegresar.Add(96, 1);
            ////97.	ExprCompP -> doubleConstant
            DicARegresar.Add(97, new string[] { "1", "ExprCompP" });
            //DicARegresar.Add(97, 1);
            ////98.	ExprCompP -> boolConstant
            DicARegresar.Add(98, new string[] { "1", "ExprCompP" });
            //DicARegresar.Add(98, 1);
            ////99.	ExprCompP -> stringConstant
            DicARegresar.Add(99, new string[] { "1", "ExprCompP" });
            //DicARegresar.Add(99, 1);
            ////100.	ExprCompP -> null
            DicARegresar.Add(100, new string[] { "|", "ExprCompP" });
            //DicARegresar.Add(100, 1);
            //terminar aqui

            DIC_DE_NO_TERMINALES = DicARegresar;
        }

        public void Crear_Tabla()
        {
            CrearDicNoTerminales();
            var Simbolos = new string[89] { ";", "id", "const", "int", "double", "bool", "string", "[]", "(", ")", "void", ",", "class", "{", "}", ":", "interface", "if", "else", "while", "for", "return", "break", "Console", ".", "WriteLine", "=", "==", "&&", "<", "<=", "+", "*", "%", "-", "!", "this", "New", "intConstant", "doubleConstant", "boolConstant", "stringConstant", "null", "$", "S'", "Program", "Decl", "VariableDecl", "Variable", "ConstDecl", "ConstType", "Type", "Type_P", "Type_R", "FunctionsDecl", "Formals", "Formals_P", "ClassDecl", "ClassDecl_P", "ClassDecl_R", "ClassDecl_O", "ClassDecl_Q", "Field", "InterfaceDecl", "InterfaceDecl_P", "Prototype", "StmtBlock", "StmtBlock_P", "StmtBlock_R", "StmtBlock_O", "Stmt", "Stmt_P", "IfStmt", "IfStmt_P", "WhileStmt", "ForStmt", "ReturnStmt", "BreakStmt", "PrintStmt", "PrintStmt_P", "Expr", "ExprOr", "ExprOrP", "ExprAnd", "ExprAndP", "ExprEquals", "ExprEqualsP", "ExprComp", "ExprCompP" };
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

        public void RecursivoReducirEIrA(int EstadoADesplazarse, PropiedadesDePalabras TopeDeCadena)
        {
            if (!YA_ENCONTRE_UN_DESPLAZAMIENTO)
            {
                var Devolver_Dic_De_No_Terminal = DIC_DE_NO_TERMINALES.First(x => x.Key == EstadoADesplazarse);
                var Numeros_A_Desapilar = int.Parse(Devolver_Dic_De_No_Terminal.Value[0]);
                var Aux_Simbolo_Parser = SIMBOLO_PARSER;
                if (Numeros_A_Desapilar == 0)
                {
                    Aux_Simbolo_Parser.Push(Devolver_Dic_De_No_Terminal.Value[1]);
                    SIMBOLO_PARSER = Aux_Simbolo_Parser;
                }
                else
                {
                    while (Numeros_A_Desapilar > 0)//quitar los primeros simbolos, colocar en el primero el simbolo reducido y correr la lista
                    {
                        Aux_Simbolo_Parser.Pop();
                        LA_PILA.Pop();
                        Numeros_A_Desapilar--;
                    }
                    Aux_Simbolo_Parser.Push(Devolver_Dic_De_No_Terminal.Value[1]);
                    SIMBOLO_PARSER = Aux_Simbolo_Parser;
                }

                //empieza IrA

                var TopeDePilaIrA = LA_PILA.Peek();
                var AccionIrA = RegresarAccion(TopeDePilaIrA, Devolver_Dic_De_No_Terminal.Value[1]);//siempre va a ser un No Terminal
                var AccionConLetra = "";
                if (AccionIrA != "n")
                {
                    LA_PILA.Push(int.Parse(AccionIrA));//agrego el numero del IrA a la pila
                    if (TopeDeCadena.Nombre == "IDENTIFICADOR")//quiere decir que viene un id
                    {
                        AccionConLetra = RegresarAccion(LA_PILA.Peek(), "id");//pregunto si su casilla es una reduccion o desplazamiento, si es reduccion, vuelvo a llamar a este método, si no, termino
                        var SplitSinDivisor = AccionConLetra.Split('/');
                        var SplitESE = AccionConLetra.Split('s');
                        var SplitR = AccionConLetra.Split('r');
                        if (SplitSinDivisor.Length > 1)//quiere decir que tiene conflicto
                        {
                            //primero me voy a el desplazamineto, luego a la reduccion 
                            bool Error_Primer_Camino = false;
                            ERROR_PRIMER_CAMINO = false;
                            ERROR_SEGUNDO_CAMINO = false;
                            Queue<PropiedadesDePalabras> auxCadenasDespla = new Queue<PropiedadesDePalabras>();
                            Queue<PropiedadesDePalabras> auxCadenaRedux = new Queue<PropiedadesDePalabras>();
                            foreach (var item in CADENA)
                            {
                                auxCadenaRedux.Enqueue(item);
                                auxCadenasDespla.Enqueue(item);
                            }
                            if (!Error_Primer_Camino)//desplazar
                            {
                                SplitESE = SplitSinDivisor[0].Split('s');
                                var Estado_A_Desplazarse = int.Parse(SplitESE[1]);
                                LA_PILA.Push(Estado_A_Desplazarse);//meto a la pila el numero del estado a desplazar
                                SIMBOLO_PARSER.Push(TopeDeCadena.Cadena);//agrego a simbolo
                                auxCadenasDespla.Dequeue();//quito de entrada
                                                           //termine de hacer el proceso normal de desplazamineto, ahora tengo que ver si el siguiente me da error
                                AccionConLetra = string.Empty;

                                if (auxCadenasDespla.Peek().Nombre == "IDENTIFICADOR")
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "id");
                                }
                                else if (auxCadenasDespla.Peek().Nombre == "NUMERO")
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "intConstant");
                                }
                                else if (auxCadenasDespla.Peek().Nombre == "DECIMAL")
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "doubleConstant");
                                }
                                else if (auxCadenasDespla.Peek().Nombre == "CADENA")
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "stringConstant");
                                }
                                else if (auxCadenasDespla.Peek().Cadena == "true" || auxCadenasDespla.Peek().Cadena == "false")
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "boolConstant");
                                }
                                else
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), auxCadenasDespla.Peek().Cadena);
                                }

                                if (AccionConLetra == "n")//como ya se encontre su accion, veo si da error si no, sigo normal
                                {
                                    Error_Primer_Camino = true;
                                    ERROR_PRIMER_CAMINO = true;
                                    CADENA = auxCadenaRedux;
                                }
                                else
                                {
                                    CADENA = auxCadenasDespla;
                                }
                            }
                            if (Error_Primer_Camino)//reducir
                            {
                                SplitR = SplitSinDivisor[1].Split('r');
                                LA_PILA.Pop();//quito el desplazar de antes, tengo que poner en los demas
                                SIMBOLO_PARSER.Pop();
                                RecursivoReducirEIrA(int.Parse(SplitR[1]), auxCadenaRedux.Peek());//le paso la cadena a reducir, tengo que ver si es error o no en el método
                            }
                            if (ERROR_PRIMER_CAMINO && ERROR_SEGUNDO_CAMINO)//Error de sintaxis, ya probe con ambos caminos
                            {
                                SINTAXIS_CORRECTA = false;
                                MENSAJE_RESULTANTE = "///////// ERROR DE SINTAXIS EN LA LINEA: " + TopeDeCadena.Linea + "     ///////////" + "\n" + "Se encontró: " + TopeDeCadena.Cadena;
                                //recuperarse
                                while (LA_PILA.Count > 1)//limpio la pila, la dejo en 0
                                {
                                    LA_PILA.Pop();
                                    SIMBOLO_PARSER.Pop();
                                }
                                while (AccionConLetra == "n")// que pase de cadena y de accion hasta de reconozca algo con el estado 0
                                {
                                    if (CADENA.Peek().Nombre == "IDENTIFICADOR")
                                    {
                                        AccionConLetra = RegresarAccion(LA_PILA.Peek(), "id");
                                    }
                                    else if (CADENA.Peek().Nombre == "NUMERO")
                                    {
                                        AccionConLetra = RegresarAccion(LA_PILA.Peek(), "intConstant");
                                    }
                                    else if (CADENA.Peek().Nombre == "DECIMAL")
                                    {
                                        AccionConLetra = RegresarAccion(LA_PILA.Peek(), "doubleConstant");
                                    }
                                    else if (CADENA.Peek().Nombre == "CADENA")
                                    {
                                        AccionConLetra = RegresarAccion(LA_PILA.Peek(), "stringConstant");
                                    }
                                    else if (CADENA.Peek().Cadena == "true" || CADENA.Peek().Cadena == "false")
                                    {
                                        AccionConLetra = RegresarAccion(LA_PILA.Peek(), "boolConstant");
                                    }
                                    else
                                    {
                                        AccionConLetra = RegresarAccion(LA_PILA.Peek(), CADENA.Peek().Cadena);
                                    }
                                    SplitESE = AccionConLetra.Split('s');
                                    if (SplitESE.Length > 1)
                                    {
                                        var Estado_A_Desplazarse = int.Parse(SplitESE[1]);
                                        LA_PILA.Push(Estado_A_Desplazarse);//meto a la pila el numero del estado a desplazar
                                        SIMBOLO_PARSER.Push(TopeDeCadena.Cadena);//agrego a simbolo
                                        CADENA.Dequeue();//quito de entrada
                                    }
                                    else
                                    {
                                        CADENA.Dequeue();//quito de entrada
                                        SIMBOLO_PARSER.Pop();
                                        break;
                                    }
                                }
                            }
                            //aqui termina la verificación de conflictos de la tabla de análisis, colocar también en el método recursivo
                        }
                        else if (SplitR.Length > 1)//quiere decir que es una reduccion lo que viene
                        {

                            RecursivoReducirEIrA(int.Parse(SplitR[1]), TopeDeCadena);

                        }
                        else if (SplitESE.Length > 1)//quiere decir que viene un desplazamiento//CADENA.Dequeue
                        {
                            YA_ENCONTRE_UN_DESPLAZAMIENTO = true;
                            var Estado_A_Desplazarse = int.Parse(SplitESE[1]);
                            LA_PILA.Push(Estado_A_Desplazarse);//meto a la pila el numero del estado a desplazar
                            SIMBOLO_PARSER.Push(TopeDeCadena.Cadena);
                            CADENA.Dequeue();
                        }
                        else if (AccionConLetra == "n")//quiere decir que fue error de sintaxis
                        {
                            SINTAXIS_CORRECTA = false;
                            MENSAJE_RESULTANTE = "///////// ERROR DE SINTAXIS EN LA LINEA: " + TopeDeCadena.Linea + "     ///////////" + "\n" + "Se encontró: " + TopeDeCadena.Cadena;
                            while (LA_PILA.Count > 1)//limpio la pila, la dejo en 0
                            {
                                LA_PILA.Pop();
                                SIMBOLO_PARSER.Pop();
                            }
                            while (AccionConLetra == "n")// que pase de cadena y de accion hasta de reconozca algo con el estado 0
                            {
                                if (CADENA.Peek().Nombre == "IDENTIFICADOR")
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "id");
                                }
                                else if (CADENA.Peek().Nombre == "NUMERO")
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "intConstant");
                                }
                                else if (CADENA.Peek().Nombre == "DECIMAL")
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "doubleConstant");
                                }
                                else if (CADENA.Peek().Nombre == "CADENA")
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "stringConstant");
                                }
                                else if (CADENA.Peek().Cadena == "true" || CADENA.Peek().Cadena == "false")
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "boolConstant");
                                }
                                else
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), CADENA.Peek().Cadena);
                                }
                                SplitESE = AccionConLetra.Split('s');
                                if (SplitESE.Length > 1)
                                {
                                    var Estado_A_Desplazarse = int.Parse(SplitESE[1]);
                                    LA_PILA.Push(Estado_A_Desplazarse);//meto a la pila el numero del estado a desplazar
                                    SIMBOLO_PARSER.Push(TopeDeCadena.Cadena);//agrego a simbolo
                                    CADENA.Dequeue();//quito de entrada
                                }
                                else
                                {
                                    CADENA.Dequeue();//quito de entrada
                                    SIMBOLO_PARSER.Pop();
                                    break;
                                }
                            }
                        }
                        else // aceptar
                        {
                            SINTAXIS_CORRECTA = true;
                            MENSAJE_RESULTANTE = "/////////// COMPILACION TERMINADA, CODIGO SINTACTICAMENTE CORRECTO :) ///////";
                        }
                    }
                    else//viene una palabra reconocible
                    {
                        AccionConLetra = RegresarAccion(LA_PILA.Peek(), TopeDeCadena.Cadena);//pregunto si su casilla es una reduccion o desplazamiento, si es reduccion, vuelvo a llamar a este método, si no, termino
                        var SplitSinDivisor = AccionConLetra.Split('/');
                        var SplitESE = AccionConLetra.Split('s');
                        var SplitR = AccionConLetra.Split('r');
                        if (SplitSinDivisor.Length > 1)//quiere decir que tiene conflicto, dado que venia de algo que no es ID
                        {
                            //primero me voy a el desplazamineto, luego a la reduccion 
                            bool Error_Primer_Camino = false;
                            ERROR_PRIMER_CAMINO = false;
                            ERROR_SEGUNDO_CAMINO = false;
                            Queue<PropiedadesDePalabras> auxCadenasDespla = new Queue<PropiedadesDePalabras>();
                            Queue<PropiedadesDePalabras> auxCadenaRedux = new Queue<PropiedadesDePalabras>();
                            foreach (var item in CADENA)
                            {
                                auxCadenaRedux.Enqueue(item);
                                auxCadenasDespla.Enqueue(item);
                            }
                            if (!Error_Primer_Camino)//desplazar
                            {
                                SplitESE = SplitSinDivisor[0].Split('s');
                                var Estado_A_Desplazarse = int.Parse(SplitESE[1]);
                                LA_PILA.Push(Estado_A_Desplazarse);//meto a la pila el numero del estado a desplazar
                                SIMBOLO_PARSER.Push(TopeDeCadena.Cadena);//agrego a simbolo
                                auxCadenasDespla.Dequeue();//quito de entrada
                                                           //termine de hacer el proceso normal de desplazamineto, ahora tengo que ver si el siguiente me da error
                                AccionConLetra = string.Empty;

                                if (auxCadenasDespla.Peek().Nombre == "IDENTIFICADOR")
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "id");
                                }
                                else if (auxCadenasDespla.Peek().Nombre == "NUMERO")
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "intConstant");
                                }
                                else if (auxCadenasDespla.Peek().Nombre == "DECIMAL")
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "doubleConstant");
                                }
                                else if (auxCadenasDespla.Peek().Nombre == "CADENA")
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "stringConstant");
                                }
                                else if (auxCadenasDespla.Peek().Cadena == "true" || auxCadenasDespla.Peek().Cadena == "false")
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "boolConstant");
                                }
                                else
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), auxCadenasDespla.Peek().Cadena);
                                }


                                if (AccionConLetra == "n")//como ya se encontre su accion, veo si da error si no, sigo normal
                                {
                                    Error_Primer_Camino = true;
                                    ERROR_PRIMER_CAMINO = true;
                                    CADENA = auxCadenaRedux;
                                }
                                else
                                {
                                    CADENA = auxCadenasDespla;
                                }
                            }
                            if (Error_Primer_Camino)//reducir
                            {
                                SplitR = SplitSinDivisor[1].Split('r');
                                LA_PILA.Pop();//quito el desplazar de antes, tengo que poner en los demas
                                SIMBOLO_PARSER.Pop();
                                RecursivoReducirEIrA(int.Parse(SplitR[1]), auxCadenaRedux.Peek());//le paso la cadena a reducir, tengo que ver si es error o no en el método
                            }
                            if (ERROR_PRIMER_CAMINO && ERROR_SEGUNDO_CAMINO)//Error de sintaxis, ya probe con ambos caminos
                            {
                                //recuperarse
                                while (LA_PILA.Count > 1)//limpio la pila, la dejo en 0
                                {
                                    LA_PILA.Pop();
                                    SIMBOLO_PARSER.Pop();
                                }
                                while (AccionConLetra == "n")// que pase de cadena y de accion hasta de reconozca algo con el estado 0
                                {
                                    if (CADENA.Peek().Nombre == "IDENTIFICADOR")
                                    {
                                        AccionConLetra = RegresarAccion(LA_PILA.Peek(), "id");
                                    }
                                    else if (CADENA.Peek().Nombre == "NUMERO")
                                    {
                                        AccionConLetra = RegresarAccion(LA_PILA.Peek(), "intConstant");
                                    }
                                    else if (CADENA.Peek().Nombre == "DECIMAL")
                                    {
                                        AccionConLetra = RegresarAccion(LA_PILA.Peek(), "doubleConstant");
                                    }
                                    else if (CADENA.Peek().Nombre == "CADENA")
                                    {
                                        AccionConLetra = RegresarAccion(LA_PILA.Peek(), "stringConstant");
                                    }
                                    else if (CADENA.Peek().Cadena == "true" || CADENA.Peek().Cadena == "false")
                                    {
                                        AccionConLetra = RegresarAccion(LA_PILA.Peek(), "boolConstant");
                                    }
                                    else
                                    {
                                        AccionConLetra = RegresarAccion(LA_PILA.Peek(), CADENA.Peek().Cadena);
                                    }
                                    SplitESE = AccionConLetra.Split('s');
                                    if (SplitESE.Length > 1)
                                    {
                                        var Estado_A_Desplazarse = int.Parse(SplitESE[1]);
                                        LA_PILA.Push(Estado_A_Desplazarse);//meto a la pila el numero del estado a desplazar
                                        SIMBOLO_PARSER.Push(TopeDeCadena.Cadena);//agrego a simbolo
                                        CADENA.Dequeue();//quito de entrada
                                    }
                                    else
                                    {
                                        CADENA.Dequeue();//quito de entrada
                                        SIMBOLO_PARSER.Pop();
                                        break;
                                    }
                                }
                            }
                            //aqui termina la verificación de conflictos de la tabla de análisis, colocar también en el método recursivo
                        }
                        else if (SplitR.Length > 1)//quiere decir que es una reduccion lo que viene
                        {

                            RecursivoReducirEIrA(int.Parse(SplitR[1]), TopeDeCadena);

                        }
                        else if (SplitESE.Length > 1)//quiere decir que viene un desplazamiento//CADENA.dequeue
                        {
                            YA_ENCONTRE_UN_DESPLAZAMIENTO = true;
                            var Estado_A_Desplazarse = int.Parse(SplitESE[1]);
                            LA_PILA.Push(Estado_A_Desplazarse);//meto a la pila el numero del estado a desplazar
                            SIMBOLO_PARSER.Push(TopeDeCadena.Cadena);
                            CADENA.Dequeue();
                        }
                        else if (AccionConLetra == "n")//quiere decir que fue error de sintaxis
                        {
                            SINTAXIS_CORRECTA = false;
                            MENSAJE_RESULTANTE = "///////// ERROR DE SINTAXIS EN LA LINEA: " + TopeDeCadena.Linea + "     ///////////" + "\n" + "Se encontró: " + TopeDeCadena.Cadena;
                            while (LA_PILA.Count > 1)//limpio la pila, la dejo en 0
                            {
                                LA_PILA.Pop();
                                SIMBOLO_PARSER.Pop();
                            }
                            while (AccionConLetra == "n")// que pase de cadena y de accion hasta de reconozca algo con el estado 0
                            {
                                if (CADENA.Peek().Nombre == "IDENTIFICADOR")
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "id");
                                }
                                else if (CADENA.Peek().Nombre == "NUMERO")
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "intConstant");
                                }
                                else if (CADENA.Peek().Nombre == "DECIMAL")
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "doubleConstant");
                                }
                                else if (CADENA.Peek().Nombre == "CADENA")
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "stringConstant");
                                }
                                else if (CADENA.Peek().Cadena == "true" || CADENA.Peek().Cadena == "false")
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "boolConstant");
                                }
                                else
                                {
                                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), CADENA.Peek().Cadena);
                                }
                                SplitESE = AccionConLetra.Split('s');
                                if (SplitESE.Length > 1)
                                {
                                    var Estado_A_Desplazarse = int.Parse(SplitESE[1]);
                                    LA_PILA.Push(Estado_A_Desplazarse);//meto a la pila el numero del estado a desplazar
                                    SIMBOLO_PARSER.Push(TopeDeCadena.Cadena);//agrego a simbolo
                                    CADENA.Dequeue();//quito de entrada
                                }
                                else
                                {
                                    CADENA.Dequeue();//quito de entrada
                                    SIMBOLO_PARSER.Pop();
                                    break;
                                }
                            }
                        }
                        else // aceptar
                        {
                            SINTAXIS_CORRECTA = true;
                            MENSAJE_RESULTANTE = "/////////// COMPILACION TERMINADA, CODIGO SINTACTICAMENTE CORRECTO :) ///////";
                        }
                    }
                }
                else
                {
                    //encontre un error luego de entrar a una reduccion de un conflicto... cierto? puede ser si no pues ya se que es error
                    //tengo que seguir compilando, pero tengo que preguntar si venia de un conflicto para hacer true el error segundo
                    if (ERROR_PRIMER_CAMINO)
                    {
                        ERROR_SEGUNDO_CAMINO = true;
                    }
                    else // error de sintaxis, codigo mal escrito
                    {
                        SINTAXIS_CORRECTA = false;
                        MENSAJE_RESULTANTE = "///////// ERROR DE SINTAXIS EN LA LINEA: " + TopeDeCadena.Linea + "     ///////////" + "\n" + "Se encontró: " + TopeDeCadena.Cadena;
                        //recuperarse
                        while (LA_PILA.Count > 1)//limpio la pila, la dejo en 0
                        {
                            LA_PILA.Pop();
                            SIMBOLO_PARSER.Pop();
                        }
                        while (AccionConLetra == "n")// que pase de cadena y de accion hasta de reconozca algo con el estado 0
                        {
                            if (CADENA.Peek().Nombre == "IDENTIFICADOR")
                            {
                                AccionConLetra = RegresarAccion(LA_PILA.Peek(), "id");
                            }
                            else if (CADENA.Peek().Nombre == "NUMERO")
                            {
                                AccionConLetra = RegresarAccion(LA_PILA.Peek(), "intConstant");
                            }
                            else if (CADENA.Peek().Nombre == "DECIMAL")
                            {
                                AccionConLetra = RegresarAccion(LA_PILA.Peek(), "doubleConstant");
                            }
                            else if (CADENA.Peek().Nombre == "CADENA")
                            {
                                AccionConLetra = RegresarAccion(LA_PILA.Peek(), "stringConstant");
                            }
                            else if (CADENA.Peek().Cadena == "true" || CADENA.Peek().Cadena == "false")
                            {
                                AccionConLetra = RegresarAccion(LA_PILA.Peek(), "boolConstant");
                            }
                            else
                            {
                                AccionConLetra = RegresarAccion(LA_PILA.Peek(), CADENA.Peek().Cadena);
                            }
                            var SplitESE = AccionConLetra.Split('s');
                            if (SplitESE.Length > 1)
                            {
                                var Estado_A_Desplazarse = int.Parse(SplitESE[1]);
                                LA_PILA.Push(Estado_A_Desplazarse);//meto a la pila el numero del estado a desplazar
                                SIMBOLO_PARSER.Push(TopeDeCadena.Cadena);//agrego a simbolo
                                CADENA.Dequeue();//quito de entrada
                            }
                            else
                            {
                                CADENA.Dequeue();//quito de entrada
                                SIMBOLO_PARSER.Pop();
                                break;
                            }
                        }
                    }
                }
            }
            YA_ENCONTRE_UN_DESPLAZAMIENTO = false;
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

                var AccionConLetra = string.Empty;
                if (Cadena_A_Evaluar.Nombre == "IDENTIFICADOR")
                {
                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "id");
                }
                else if (Cadena_A_Evaluar.Nombre == "NUMERO")
                {
                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "intConstant");
                }
                else if (Cadena_A_Evaluar.Nombre == "DECIMAL")
                {
                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "doubleConstant");
                }
                else if (CADENA.Peek().Nombre  == "CADENA")
                {
                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "stringConstant");
                }
                else if (CADENA.Peek().Cadena == "true" || CADENA.Peek().Cadena == "false")
                {
                    AccionConLetra = RegresarAccion(LA_PILA.Peek(), "boolConstant");
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
                    //primero me voy a el desplazamineto, luego a la reduccion 
                    bool Error_Primer_Camino = false;
                    ERROR_PRIMER_CAMINO = false;
                    ERROR_SEGUNDO_CAMINO = false;
                    Queue<PropiedadesDePalabras> auxCadenasDespla = new Queue<PropiedadesDePalabras>();
                    Queue<PropiedadesDePalabras> auxCadenaRedux = new Queue<PropiedadesDePalabras>();
                    foreach (var item in CADENA)
                    {
                        auxCadenaRedux.Enqueue(item);
                        auxCadenasDespla.Enqueue(item);
                    }
                    if (!Error_Primer_Camino)//desplazar
                    {
                        SplitESE = SplitSinDivisor[0].Split('s');
                        var Estado_A_Desplazarse = int.Parse(SplitESE[1]);
                        LA_PILA.Push(Estado_A_Desplazarse);//meto a la pila el numero del estado a desplazar
                        SIMBOLO_PARSER.Push(Cadena_A_Evaluar.Cadena);//agrego a simbolo
                        auxCadenasDespla.Dequeue();//quito de entrada
                        //termine de hacer el proceso normal de desplazamineto, ahora tengo que ver si el siguiente me da error
                        AccionConLetra = string.Empty;

                        if (auxCadenasDespla.Peek().Nombre == "IDENTIFICADOR")
                        {
                            AccionConLetra = RegresarAccion(LA_PILA.Peek(), "id");
                        }
                        else if (auxCadenasDespla.Peek().Nombre == "NUMERO")
                        {
                            AccionConLetra = RegresarAccion(LA_PILA.Peek(), "intConstant");
                        }
                        else if (auxCadenasDespla.Peek().Nombre == "DECIMAL")
                        {
                            AccionConLetra = RegresarAccion(LA_PILA.Peek(), "doubleConstant");
                        }
                        else if (auxCadenasDespla.Peek().Nombre == "CADENA")
                        {
                            AccionConLetra = RegresarAccion(LA_PILA.Peek(), "stringConstant");
                        }
                        else if (auxCadenasDespla.Peek().Cadena == "true" || auxCadenasDespla.Peek().Cadena == "false")
                        {
                            AccionConLetra = RegresarAccion(LA_PILA.Peek(), "boolConstant");
                        }
                        else
                        {
                            AccionConLetra = RegresarAccion(LA_PILA.Peek(), auxCadenasDespla.Peek().Cadena);
                        }

                        if (AccionConLetra == "n")//como ya se encontre su accion, veo si da error si no, sigo normal
                        {
                            Error_Primer_Camino = true;
                            ERROR_PRIMER_CAMINO = true;
                            CADENA = auxCadenaRedux;
                        }
                        else
                        {
                            CADENA = auxCadenasDespla;
                        }
                    }
                    if (Error_Primer_Camino)//reducir
                    {
                        SplitR = SplitSinDivisor[1].Split('r');
                        LA_PILA.Pop();//quito el desplazar de antes, tengo que poner en los demas
                        SIMBOLO_PARSER.Pop();
                        RecursivoReducirEIrA(int.Parse(SplitR[1]), auxCadenaRedux.Peek());//le paso la cadena a reducir, tengo que ver si es error o no en el método
                    }
                    if (ERROR_PRIMER_CAMINO && ERROR_SEGUNDO_CAMINO)//Error de sintaxis, ya probe con ambos caminos
                    {
                        SINTAXIS_CORRECTA = false;
                        MENSAJE_RESULTANTE = "///////// ERROR DE SINTAXIS EN LA LINEA: " + Cadena_A_Evaluar.Linea +"     ///////////"+"\n"+ "Se encontró: "+Cadena_A_Evaluar.Cadena;
                        ERROR_PRIMER_CAMINO = false;
                        ERROR_SEGUNDO_CAMINO = false;
                        //recuperarse
                        while (LA_PILA.Count > 1)//limpio la pila, la dejo en 0
                        {
                            LA_PILA.Pop();
                            SIMBOLO_PARSER.Pop();
                        }
                        while (AccionConLetra == "n")// que pase de cadena y de accion hasta de reconozca algo con el estado 0
                        {
                            if (CADENA.Peek().Nombre == "IDENTIFICADOR")
                            {
                                AccionConLetra = RegresarAccion(LA_PILA.Peek(), "id");
                            }
                            else if (Cadena_A_Evaluar.Nombre == "NUMERO")
                            {
                                AccionConLetra = RegresarAccion(LA_PILA.Peek(), "intConstant");
                            }
                            else if (Cadena_A_Evaluar.Nombre == "DECIMAL")
                            {
                                AccionConLetra = RegresarAccion(LA_PILA.Peek(), "doubleConstant");
                            }
                            else if (Cadena_A_Evaluar.Nombre == "CADENA")
                            {
                                AccionConLetra = RegresarAccion(LA_PILA.Peek(), "stringConstant");
                            }
                            else if (CADENA.Peek().Cadena == "true" || CADENA.Peek().Cadena == "false")
                            {
                                AccionConLetra = RegresarAccion(LA_PILA.Peek(), "boolConstant");
                            }
                            else
                            {
                                AccionConLetra = RegresarAccion(LA_PILA.Peek(), CADENA.Peek().Cadena);///////////ALTO///////////////
                            }
                            SplitESE = AccionConLetra.Split('s');
                            if (SplitESE.Length > 1)
                            {
                                var Estado_A_Desplazarse = int.Parse(SplitESE[1]);
                                LA_PILA.Push(Estado_A_Desplazarse);//meto a la pila el numero del estado a desplazar
                                SIMBOLO_PARSER.Push(Cadena_A_Evaluar.Cadena);//agrego a simbolo
                                CADENA.Dequeue();//quito de entrada
                            }
                            else
                            {
                                CADENA.Dequeue();//quito de entrada
                                SIMBOLO_PARSER.Pop();
                                break;
                            }
                        }
                    }
                    //aqui termina la verificación de conflictos de la tabla de análisis, colocar también en el método recursivo
                }
                else if (SplitR.Length > 1)//quiere decir que es una reduccion lo que viene
                {

                    RecursivoReducirEIrA(int.Parse(SplitR[1]), Cadena_A_Evaluar);//como es primera iteracion, no le paso nada (hipotesis)

                }
                else if (SplitESE.Length > 1)//quiere decir que viene un desplazamiento
                {
                    var Estado_A_Desplazarse = int.Parse(SplitESE[1]);
                    LA_PILA.Push(Estado_A_Desplazarse);//meto a la pila el numero del estado a desplazar
                    SIMBOLO_PARSER.Push(Cadena_A_Evaluar.Cadena);//agrego a simbolo
                    CADENA.Dequeue();//quito de entrada
                    //var Devolver_Dic_De_No_Terminal = DICCIONARIO_DE_ESTADOS.First(x => x.Key ==  Estado_A_Desplazarse);
                }
                else if (AccionConLetra == "n")//quiere decir que fue error de sintaxis
                {
                    SINTAXIS_CORRECTA = false;
                    MENSAJE_RESULTANTE = "///////// ERROR DE SINTAXIS EN LA LINEA: " + Cadena_A_Evaluar.Linea + "     ///////////" + "\n" + "Se encontró: " + Cadena_A_Evaluar.Cadena;
                    while (LA_PILA.Count > 1)//limpio la pila, la dejo en 0
                    {
                        LA_PILA.Pop();
                        SIMBOLO_PARSER.Pop();
                    }
                    while (AccionConLetra == "n")// que pase de cadena y de accion hasta de reconozca algo con el estado 0
                    {
                        if (CADENA.Peek().Nombre == "IDENTIFICADOR")
                        {
                            AccionConLetra = RegresarAccion(LA_PILA.Peek(), "id");
                        }
                        else if (Cadena_A_Evaluar.Nombre == "NUMERO")
                        {
                            AccionConLetra = RegresarAccion(LA_PILA.Peek(), "intConstant");
                        }
                        else if (Cadena_A_Evaluar.Nombre == "DECIMAL")
                        {
                            AccionConLetra = RegresarAccion(LA_PILA.Peek(), "doubleConstant");
                        }
                        else if (Cadena_A_Evaluar.Nombre == "CADENA")
                        {
                            AccionConLetra = RegresarAccion(LA_PILA.Peek(), "stringConstant");
                        }
                        else if (CADENA.Peek().Cadena == "true" || CADENA.Peek().Cadena == "false")
                        {
                            AccionConLetra = RegresarAccion(LA_PILA.Peek(), "boolConstant");
                        }
                        else
                        {
                            AccionConLetra = RegresarAccion(LA_PILA.Peek(), CADENA.Peek().Cadena);
                        }
                        SplitESE = AccionConLetra.Split('s');
                        if (SplitESE.Length > 1)
                        {
                            var Estado_A_Desplazarse = int.Parse(SplitESE[1]);
                            LA_PILA.Push(Estado_A_Desplazarse);//meto a la pila el numero del estado a desplazar
                            SIMBOLO_PARSER.Push(Cadena_A_Evaluar.Cadena);//agrego a simbolo
                            CADENA.Dequeue();//quito de entrada
                        }
                        else
                        {
                            CADENA.Dequeue();//quito de entrada
                            SIMBOLO_PARSER.Pop();
                            break;
                        }
                    }
                }
                else
                {
                    SINTAXIS_CORRECTA = true;
                    MENSAJE_RESULTANTE = "/////////// COMPILACION TERMINADA, CODIGO SINTACTICAMENTE CORRECTO :) ///////";
                }
            }
        }

        public string Ejecutar_Analizador()
        {
            Tabla_De_Parser();
            if (SINTAXIS_CORRECTA)
            {
                return MENSAJE_RESULTANTE;
            }
            else
            {
                return MENSAJE_RESULTANTE;
            }
        }

    }
}
