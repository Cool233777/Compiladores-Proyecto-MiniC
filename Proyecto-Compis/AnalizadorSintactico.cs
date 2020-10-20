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

        public void Crear_Tabla()
        {
            var Simbolos = new string[89] { ";", "id", "const", "int", "double", "bool", "string","[]","(",")","void", ",", "class", "{","}",":","interface","if","else","while","for","return","break","Console", ".","WriteLine","=","==","&&","<","<=","+","*","%","-","!","this","New","intConstant","doubleConstant","boolConstant","stringConstant","null","$","S'","Program","Decl","VariableDecl","Variable","ConstDecl","ConstType","Type","Type_P","Type_R","FunctionDecl","Formals","Formals_P","ClassDecl","ClassDecl_P","ClassDecl_R","ClassDecl_O","ClassDecl_Q","Field","InterfaceDecl","InterfaceDecl_P","Prototype","StmtBlock","StmtBlock_P","StmtBlock_R", "StmtBlock_O","Stmt","Stmt_P","IfStmt","IfStmt_P","WhileStmt","ForStmt","ReturnStmt","BreakStmt","PrintStmt","PrintStmt_P","Expr","ExprOr","ExprOrP","ExprAnd","ExprAndP","ExprEquals","ExprEqualsP","ExprComp","ExprCompP" };
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

        public void Tabla_De_Parser()
        {

        }

    }
}
