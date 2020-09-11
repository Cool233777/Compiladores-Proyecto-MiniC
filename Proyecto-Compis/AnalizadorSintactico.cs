using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Text.RegularExpressions;
namespace Proyecto_Compis
{
    class AnalizadorSintactico
    {
        public Regex Regex;
        public bool PrimeraVez = true;
        public string LookAhead;
        public List<PropiedadesDePalabras> Tokens;
        public int cont = 0;
        Queue<PropiedadesDePalabras> Expr = new Queue<PropiedadesDePalabras>();
        public AnalizadorSintactico(AnalizadorLex Lex, List<PropiedadesDePalabras> Tokns)
        {
            Regex = Lex.REGEX;
            Tokens = Tokns;
        }

        public void Programa()
        {
            DeclMas();
        }

        public void DeclMas()
        {
            // variable decl
            if (true)
            {
                VariableDecl();
            }
            else 
            {
                FunctionDecl();
            }
            //function decl
        }

        public void VariableDecl()
        {
            Generar_Variable();
            MatchToken(Tokens[cont].Cadena);//espero ;
        }

        public void Generar_Variable()
        {
            Generar_Tipo_Var();
            //espero identificador
        }

        public void Generar_Tipo_Var()
        {
            var entrada = Tokens[cont];
            if (entrada.Nombre == "RESERVADA")
            {
                if (entrada.Cadena == "bool")
                {
                    cont++;
                    LookAhead = Tokens[cont].Cadena;
                }
                else if (entrada.Cadena == "int")
                {
                    cont++;
                    LookAhead = Tokens[cont].Cadena;
                }
                else if (entrada.Cadena == "double")
                {
                    cont++;
                    LookAhead = Tokens[cont].Cadena;
                }
                else if (entrada.Cadena == "string")
                {
                    cont++;
                    LookAhead = Tokens[cont].Cadena;
                }
            }
            else if (entrada.Nombre== "IDENTIFICADOR")
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
            }
            else
            {
                Generar_Tipo_Var();
                MatchToken(Tokens[cont].Cadena);// [
                MatchToken(Tokens[cont].Cadena);// ]
            }
        }

        public void FunctionDecl()
        {
            Generar_Tipo_Var();
            //espero identificador
            MatchToken(Tokens[cont].Cadena);// (
            //Formals();
            MatchToken(Tokens[cont].Cadena);// )
            // puede venir o no un stmt

        }
        public void MatchToken(string Expected)
        {
            if (PrimeraVez)
            {
                LookAhead = Regex.Match(Expected).Value;
                PrimeraVez = false;
            }
            //aqui hace match el token y creo que se selecciona el lookahead aqui Match match = REGEX.Match(Texto_A_Compilar);
            //Match match1 = Regex.Match(Expected);
            if (LookAhead == Expected)
            {
                //var aux = Tokens.FindIndex(x => x.Cadena == Expected);//cambiar/arrelgar para sacar los tokens aprobados
                LookAhead = Tokens[cont + 1].Cadena;
                cont++;
            }
            else
            {
                //ERROR DE SINTAXIS
            }
        }

        public void IF_Statement()
        {
            while (cont < Tokens.Count)
            {
                MatchToken(Tokens[cont].Cadena);//MatchToken("if");
                MatchToken(Tokens[cont].Cadena);// (
                Parse_Expression();
                MatchToken(Tokens[cont].Cadena);// )
                MatchToken(Tokens[cont].Cadena); // {
                Parse_Statement();// poner while token[cont].cadena != "}"
                MatchToken(Tokens[cont].Cadena);// }
                MatchToken(Tokens[cont].Cadena);//MatchToken("else");
                MatchToken(Tokens[cont].Cadena); // {
                ELSE_Parse_Statement();
                MatchToken(Tokens[cont].Cadena);// }
            }
        }  

        public void Parse_Expression_IF() 
        { 
            //ver todo lo que esta adentro de la expresion
            //var contador = cont;
            LookAhead = Tokens[cont + 1].Cadena;
            while (Tokens[cont].Cadena != ")" || LookAhead != ")")
            {
                if ((Tokens[cont].Cadena == ")" && LookAhead == "{") || Tokens[cont].Cadena == "}")
                {
                    break;
                }
                Expr.Enqueue(Tokens[cont]);
                cont++;
                //contador++;
                LookAhead = Tokens[cont + 1].Cadena;
            }
            LookAhead = Tokens[cont].Cadena;
            var Cadena_A_Comparar = 1;
            while (Expr.Count > 0)
            {
                switch (Cadena_A_Comparar)// el switch aqui ya no se va a usar
                {
                    //case 1:
                    //if ((Expr.Peek().Nombre == "IDENTIFICADOR") || (Parse_Expression()+"." + "IDENTIFICADOR" == "R"))
                    // {

                    //break;
                    case 1:
                        //Parse_Constant();
                        Cadena_A_Comparar = 2;
                        break;
                    case 2:
                        //Parse_LValue();
                        Cadena_A_Comparar = 3;
                        break;
                    case 3:
                        //if (Expr.Peek().Nombre == "RESERVADO" && Expr.Peek().Cadena == "this")
                        //{
                        //    // solo le puede seguir .ident
                        //}
                        Cadena_A_Comparar = 4;
                        break;
                    case 4:
                        //Parse_Expression_Parent();
                        //if ((Expr.Peek().Nombre == "IDENTIFICADOR" && ) || Expresion == ">")//Expresion == "+" || Expresion == "-" || Expresion == "*" || Expresion == "/" || Expresion == "%" || 
                        //{
                        //    Parse_Expression();
                        //}
                        //Parse_Expression();
                        Cadena_A_Comparar = 5;
                        break;
                    case 5:
                        //var lookahead2 = "";
                        var expresion_Izquierda = Expr.Dequeue();
                        //if (Expr.Peek().Cadena == "<" || Expr.Peek().Cadena == "="|| Expr.Peek().Cadena == ">" || Expr.Peek().Cadena == "|")
                        //{

                        //}
                        if (expresion_Izquierda.Nombre == "IDENTIFICADOR")
                        {
                            //if (Expr.Peek().Nombre == "OPERADOR")
                            //{
                            //    Expr.Dequeue();
                            //}
                            if (Expr.Peek().Cadena == "<" || Expr.Peek().Cadena == "=" || Expr.Peek().Cadena == ">" || Expr.Peek().Cadena == "||")
                            {
                                var comparador = Expr.Dequeue();
                                if (Expr.Peek().Nombre != "IDENTIFICADOR")
                                {
                                    //error de sintaxis

                                }
                                else
                                {
                                    Expr.Dequeue();
                                    //si se esta comparando con lo mismo
                                }
                            }
                            //Error de sintaxis
                        }
                        else if (expresion_Izquierda.Nombre == "NUMERO" || expresion_Izquierda.Nombre == "DECIMAL" || expresion_Izquierda.Nombre == "HEXADECIMAL")
                        {
                            //if (Expr.Peek().Nombre == "OPERADOR")
                            //{
                            //    Expr.Dequeue();
                            //}
                            if (Expr.Peek().Cadena == "<" || Expr.Peek().Cadena == "=" || Expr.Peek().Cadena == ">" || Expr.Peek().Cadena == "|" || Expr.Peek().Cadena == "+" || Expr.Peek().Cadena == "-" || Expr.Peek().Cadena == "*" || Expr.Peek().Cadena == "/" || Expr.Peek().Cadena == "&")
                            {
                                var comparador = Expr.Dequeue();
                                //if (Expr.Peek().Nombre != expresion_Izquierda.Nombre)
                                //{
                                //    //error de sintaxis

                                //}
                                if (Expr.Peek().Nombre != "NUMERO" || Expr.Peek().Nombre != "DECIMAL" || Expr.Peek().Nombre != "HEXADECIMAL")
                                {
                                    //error de sintaxis
                                }
                                else
                                {
                                    //si se esta comparando con lo mismo
                                }
                            }
                            // ERROR DE SINTAXIS
                        }
                        //Parse_Expression();
                        break;
                    default:
                        break;

                }
            }
        }

        public void Parse_Expression()
        {
            var Expresion = Tokens[cont];
            if (Expresion.Nombre == "NUMERO" || Expresion.Nombre == "DECIMAL" || Expresion.Nombre == "HEXADECIMAL" || Expresion.Nombre == "IDENTIFICADOR" || (Expresion.Nombre == "RESERVADA" && Expresion.Cadena == "bool"))
            {
                Parse_Constant();
                Parse_Expression();
            }
            else if (Expresion.Cadena == "(")
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
                Parse_Expression();
                cont++;
                LookAhead = Tokens[cont].Cadena;
            }
            else// T
            {
                Parse_Expression_T();
            }
        }
        public void Parse_Expression_T()
        {
            var Expresion = Tokens[cont];
            if (Expresion.Cadena == "&&")
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
                Parse_Expression_F();
            }
            else
            {
                Parse_Expression_F();
            }
        }

        public void Parse_Expression_F()
        {
            var Expresion = Tokens[cont];
            if (Expresion.Cadena == "==")
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
                Parse_Expression_K();
            }
            else if (Expresion.Cadena == "!=")
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
                Parse_Expression_K();
            }
            else if (Expresion.Cadena == "!")
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
                Parse_Expression_F();
            }
            else
            {
                Parse_Expression_K();
            }
        }

        public void Parse_Expression_K()
        {
            var Expresion = Tokens[cont];
            if (Expresion.Cadena == "<")
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
                Parse_Expression_M();
            }
            else if (Expresion.Cadena == "<=")
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
                Parse_Expression_M();
            }
            else if (Expresion.Cadena == ">")
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
                Parse_Expression_M();
            }
            else if (Expresion.Cadena == ">=")
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
                Parse_Expression_M();
            }
            else
            {
                Parse_Expression_M();
            }
        }
        public void Parse_Expression_M()
        {
            var Expresion = Tokens[cont];
            if (Expresion.Cadena == "+")
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
                Parse_Expression_L();
            }
            else if (Expresion.Cadena == "-")
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
                Parse_Expression_L();
            }
            else
            {
                Parse_Expression_L();
            }

        }

        public void Parse_Expression_L()
        {
            var Expresion = Tokens[cont];
            if (Expresion.Cadena == "*")
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
                Parse_Expression_U();
            }
            else if (Expresion.Cadena == "/")
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
                Parse_Expression_U();
            }
            else if (Expresion.Cadena == "%")
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
                Parse_Expression_U();
            }
            else
            {
                Parse_Expression_U();
            }
        }
        public void Parse_Expression_U()
        {
            var Expresion = Tokens[cont];
            Parse_Constant();

        }

        public void Parse_Constant()
        {
            if (Tokens[cont].Nombre == "NUMERO")//adentro if
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
            }
            else if (Tokens[cont].Nombre == "DECIMAL")//adentro if
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
            }
            else if (Tokens[cont].Nombre == "RESERVADO" && Expr.Peek().Cadena == "bool")//creacion variable
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
            }
            else if (Tokens[cont].Nombre == "RESERVADO" && Expr.Peek().Cadena == "string")//creacion variable
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
            }
            else if (Tokens[cont].Nombre == "RESERVADO" && Expr.Peek().Cadena == "null")//creacion variable
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
            }
            else if (Tokens[cont].Nombre == "IDENTIFICADOR")//adentro if
            {
                cont++;
                LookAhead = Tokens[cont].Cadena;
                //Expr.Dequeue();
                //ver que sea del mismo tipo al que se va a comparar
            }
            else
            {
                //error de sintaxis
            } 
        }
        public void Parse_Statement()
        {
            //Expresion = Tokens[cont + 1].Cadena;
            if (LookAhead == "if")
            {
                IF_Statement();
            }
            else if (LookAhead == "return")
            {
                RETURN_Statement();
            }
            else if (LookAhead == "else")
            {
                ELSE_Parse_Statement();
            }
            else
            {
                Parse_Expression();
                MatchToken(Tokens[cont].Cadena);//;
            }
        }

        public void ELSE_Parse_Statement()
        {
            if (LookAhead == "if")
            {
                IF_Statement();
            }
            else if (LookAhead == "return")
            {
                RETURN_Statement();
            }
            else if (LookAhead == "else")
            {
                ELSE_Parse_Statement();
            }
            else
            {
                Parse_Expression();
                MatchToken(Tokens[cont].Cadena);//;
            }

        }

        public void RETURN_Statement()
        {
            MatchToken(Tokens[cont].Cadena);// return
            MatchToken(Tokens[cont].Cadena);//(
            Parse_Expression();
            MatchToken(Tokens[cont].Cadena);// )
            MatchToken(Tokens[cont].Cadena);//;
        }
    }
}
