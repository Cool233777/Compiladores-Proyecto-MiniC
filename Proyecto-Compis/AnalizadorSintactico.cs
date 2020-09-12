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
        //proyectin
        public Regex Regex;
        public bool PrimeraVez = true;
        public string LookAhead;
        //public List<PropiedadesDePalabras> Tokens;
        public int cont = 0;
        public List<string> ListaErrores = new List<string>();
        Queue<PropiedadesDePalabras> Tokens = new Queue<PropiedadesDePalabras>();
        public AnalizadorSintactico(AnalizadorLex Lex, List<PropiedadesDePalabras> Tokns)
        {
            Regex = Lex.REGEX;
            foreach (var item in Tokns)
            {
                Tokens.Enqueue(item);
            }
        }

        public void Programa()
        {
            while (Tokens.Count != 0 && ((Tokens.Peek().Cadena == "void") || (Tokens.Peek().Nombre == "RESERVADO" && (Tokens.Peek().Cadena == "int" || Tokens.Peek().Cadena == "bool" || Tokens.Peek().Cadena == "double")) & Tokens.Count > 1))
            {
                DeclMas();//una o más
            }
        }

        public void DeclMas()
        {
            // variable decl
            var pos = Tokens.Count();
            var elemento = Tokens.ElementAt(2);
            if (elemento.Cadena == ";")//dos mas
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
            MatchToken(Tokens.Peek().Cadena);//espero ;
        }

        public void Generar_Variable()
        {
            Generar_Tipo_Var();
            //espero identificador
            if (Tokens.Peek().Nombre == "IDENTIFICADOR")
            {
                Tokens.Dequeue();
                LookAhead = Tokens.Peek().Cadena;
            }
            else
            {
                //error de sintaxis
                var pos = Tokens.Count();
                var elementoER = Tokens.ElementAt(pos + 1);
                ListaErrores.Add("/////////ERROR DE SINTAXIS, Se esperaba: Nombre de identificador. Ingresó: " + elementoER.Cadena + "   ///////////////");
                while (Tokens.Peek().Cadena != ";")
                {
                    Tokens.Dequeue();
                }
                LookAhead = Tokens.Peek().Cadena;
            }
        }

        public void Generar_Tipo_Var()
        {
            var entrada = Tokens.Peek();
            var pos = Tokens.Count();
            var elemento = Tokens.ElementAt(pos + 1);
            if (entrada.Nombre == "RESERVADO" && elemento.Cadena == "IDENTIFICADOR")
            {
                if (entrada.Cadena == "bool")
                {
                    Tokens.Dequeue();
                    LookAhead = Tokens.Peek().Cadena;
                }
                else if (entrada.Cadena == "int")
                {
                    Tokens.Dequeue();
                    LookAhead = Tokens.Peek().Cadena;
                }
                else if (entrada.Cadena == "double")
                {
                    Tokens.Dequeue();
                    LookAhead = Tokens.Peek().Cadena;
                }
                else if (entrada.Cadena == "string")
                {
                    Tokens.Dequeue();
                    LookAhead = Tokens.Peek().Cadena;
                }
            }
            else if (entrada.Nombre == "IDENTIFICADOR")
            {
                Tokens.Dequeue();
                LookAhead = Tokens.Peek().Cadena;
            }
            else
            {
                Tokens.Dequeue();
                LookAhead = Tokens.Peek().Cadena;
                MatchToken(Tokens.Peek().Cadena); // [
                MatchToken(Tokens.Peek().Cadena); // ]
                //LookAhead = Tokens[cont+1].Cadena;
                LookAhead = Tokens.Peek().Cadena;
                if (Tokens.Peek().Nombre == "IDENTIFICADOR")
                {
                    Tokens.Dequeue();
                    MatchToken(Tokens.Peek().Cadena);// ;
                }
                else
                {
                    //error sintaxis
                    var elementoER = Tokens.ElementAt(pos + 1);
                    ListaErrores.Add("/////////ERROR DE SINTAXIS, Se esperaba: Nombre de identificador. Ingresó: " + elementoER.Cadena + "   ///////////////");
                    while (Tokens.Peek().Cadena != ";")
                    {
                        Tokens.Dequeue();
                    }
                    LookAhead = Tokens.Peek().Cadena;
                }

            }
        }

        public void FunctionDecl()
        {
            var pos = Tokens.Count();
            var elemento = Tokens.ElementAt(1);
            var elemento2 = Tokens.ElementAt(2);
            if ((Tokens.Peek().Nombre == "RESERVADO" && (Tokens.Peek().Cadena == "int" || Tokens.Peek().Cadena == "bool" || Tokens.Peek().Cadena == "double")) || (Tokens.Peek().Nombre == "IDENTIFICADOR" && elemento.Nombre == "IDENTIFICADOR") || (elemento.Cadena == "[" && elemento2.Cadena == "]"))
            {
                Generar_Tipo_Var();
                MatchToken(Tokens.Peek().Cadena);// (
                Formals();
                MatchToken(Tokens.Peek().Cadena);// )
                MatchToken(Tokens.Peek().Cadena); // {
                // puede venir o no varios stmt
                while (Tokens.Peek().Cadena == "if" || Tokens.Peek().Cadena == "else" || Tokens.Peek().Cadena == "return" || Tokens.Peek().Cadena != "}")
                {
                    Parse_Statement();
                }
                MatchToken(Tokens.Peek().Cadena);//}
            }
            else if (Tokens.Peek().Cadena == "void")
            {
                MatchToken(Tokens.Peek().Cadena);// void
                //espero identificador
                if (Tokens.Peek().Nombre == "IDENTIFICADOR")
                {
                    Tokens.Dequeue();
                    LookAhead = Tokens.Peek().Cadena;
                }
                else
                {
                    //error de sintaxis
                    var elementoER = Tokens.ElementAt(pos + 1);
                    ListaErrores.Add("/////////ERROR DE SINTAXIS, Se esperaba: Nombre de identificador. Ingresó: " + elementoER.Cadena + "   ///////////////");
                    while (Tokens.Peek().Cadena != "{")
                    {
                        Tokens.Dequeue();
                    }
                    LookAhead = Tokens.Peek().Cadena;
                }
                MatchToken(Tokens.Peek().Cadena);// (
                Formals();
                MatchToken(Tokens.Peek().Cadena);// )
                MatchToken(Tokens.Peek().Cadena); //{
                // puede venir o no varios stmt
                while (Tokens.Count != 0 && (Tokens.Peek().Cadena == "if" || Tokens.Peek().Cadena == "else" || Tokens.Peek().Cadena == "return" || Tokens.Peek().Cadena != "}"))
                {
                    Parse_Statement();
                }
                if (Tokens.Count != 0)
                {
                    MatchToken(Tokens.Peek().Cadena);//}
                }
                else
                {
                    var h = 0;
                }
            }
        }

        public void Formals()
        {
            var pos = Tokens.Count();
            var elemento = Tokens.ElementAt(pos - pos + 1);
            while (Tokens.Peek().Cadena != ")")
            {
                if (Tokens.Peek().Cadena != ")")
                {
                    //una o más variables o Eps
                    Generar_Variable();
                    MatchToken(Tokens.Peek().Cadena);//espero ,
                }
                else if (Tokens.Peek().Cadena == ")")
                {
                    var poss = Tokens.Count();
                    var elementos = Tokens.ElementAt(poss + 1);
                    Tokens.Dequeue();
                    LookAhead = elementos.Cadena;
                }
                else
                {
                    var poss = Tokens.Count();
                    var elementos = Tokens.ElementAt(poss + 1);
                    ListaErrores.Add("/////////ERROR DE SINTAXIS, Se esperaba: TYPE y nombre de identificador. Ingresó: " + elementos.Cadena + "   ///////////////");
                    while (elementos.Cadena != ";" || elementos.Cadena != "{")
                    {
                        Tokens.Dequeue();
                    }
                    LookAhead = elementos.Cadena;
                    //error de sintaxis
                }
            }
        }
        public void MatchToken(string Expected)
        {
            if (PrimeraVez)
            {
                LookAhead = Regex.Match(Expected).Value;
                PrimeraVez = false;
            }
            //aqui hace match el token y creo que se selecciona el lookahead aqui Match match = REGEX.Match(Texto_A_Compilar);
            if (LookAhead == Expected)
            {
                var poss = Tokens.Count();
                if (Tokens.Count > 1)
                {
                    LookAhead = Tokens.ElementAt(1).Cadena;
                    Tokens.Dequeue();
                }
                else
                {
                    LookAhead = Tokens.ElementAt(0).Cadena;
                    Tokens.Dequeue();// termino de compilar
                }
            }
            else
            {
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                ListaErrores.Add("/////////ERROR DE SINTAXIS, no se encontro el token ingresado. Ingresó: " + elementos.Cadena + "   ///////////////");
                while (elementos.Cadena != ";" && elementos.Cadena != "{" && elementos.Cadena != "}")
                {
                    Tokens.Dequeue();
                }
                Tokens.Dequeue();
                LookAhead = Tokens.Peek().Cadena;
            }
        }

        public void IF_Statement()
        {
            while (Tokens.Count > 0)
            {
                MatchToken(Tokens.Peek().Cadena);//MatchToken("if");
                MatchToken(Tokens.Peek().Cadena);// (
                Parse_Expression();
                MatchToken(Tokens.Peek().Cadena);// )
                MatchToken(Tokens.Peek().Cadena); // {
                while (Tokens.Peek().Cadena != "}" && Tokens.Peek().Cadena != "{")
                {
                    Parse_Statement();
                }
                // poner while token[cont].cadena != "}"
                MatchToken(Tokens.Peek().Cadena);// }
                MatchToken(Tokens.Peek().Cadena);//MatchToken("else");
                MatchToken(Tokens.Peek().Cadena); // {
                ELSE_Parse_Statement();
                MatchToken(Tokens.Peek().Cadena);// }
                break;
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
                MatchToken(Tokens.Peek().Cadena);//;
            }

        }

        public void RETURN_Statement()
        {
            MatchToken(Tokens.Peek().Cadena);// return
            MatchToken(Tokens.Peek().Cadena);//(
            Parse_Expression();
            MatchToken(Tokens.Peek().Cadena);// )
            MatchToken(Tokens.Peek().Cadena);//;
        }

        //public void Parse_Expression_IF()
        //{
        //    //ver todo lo que esta adentro de la expresion
        //    //var contador = cont;
        //    LookAhead = Tokens[cont + 1].Cadena;
        //    while (Tokens[cont].Cadena != ")" || LookAhead != ")")
        //    {
        //        if ((Tokens[cont].Cadena == ")" && LookAhead == "{") || Tokens[cont].Cadena == "}")
        //        {
        //            break;
        //        }
        //        Expr.Enqueue(Tokens[cont]);
        //        cont++;
        //        //contador++;
        //        LookAhead = Tokens[cont + 1].Cadena;
        //    }
        //    LookAhead = Tokens[cont].Cadena;
        //    var Cadena_A_Comparar = 1;
        //    while (Expr.Count > 0)
        //    {
        //        switch (Cadena_A_Comparar)// el switch aqui ya no se va a usar
        //        {
        //            //case 1:
        //            //if ((Expr.Peek().Nombre == "IDENTIFICADOR") || (Parse_Expression()+"." + "IDENTIFICADOR" == "R"))
        //            // {

        //            //break;
        //            case 1:
        //                //Parse_Constant();
        //                Cadena_A_Comparar = 2;
        //                break;
        //            case 2:
        //                //Parse_LValue();
        //                Cadena_A_Comparar = 3;
        //                break;
        //            case 3:
        //                //if (Expr.Peek().Nombre == "RESERVADO" && Expr.Peek().Cadena == "this")
        //                //{
        //                //    // solo le puede seguir .ident
        //                //}
        //                Cadena_A_Comparar = 4;
        //                break;
        //            case 4:
        //                //Parse_Expression_Parent();
        //                //if ((Expr.Peek().Nombre == "IDENTIFICADOR" && ) || Expresion == ">")//Expresion == "+" || Expresion == "-" || Expresion == "*" || Expresion == "/" || Expresion == "%" || 
        //                //{
        //                //    Parse_Expression();
        //                //}
        //                //Parse_Expression();
        //                Cadena_A_Comparar = 5;
        //                break;
        //            case 5:
        //                //var lookahead2 = "";
        //                var expresion_Izquierda = Expr.Dequeue();
        //                //if (Expr.Peek().Cadena == "<" || Expr.Peek().Cadena == "="|| Expr.Peek().Cadena == ">" || Expr.Peek().Cadena == "|")
        //                //{

        //                //}
        //                if (expresion_Izquierda.Nombre == "IDENTIFICADOR")
        //                {
        //                    //if (Expr.Peek().Nombre == "OPERADOR")
        //                    //{
        //                    //    Expr.Dequeue();
        //                    //}
        //                    if (Expr.Peek().Cadena == "<" || Expr.Peek().Cadena == "=" || Expr.Peek().Cadena == ">" || Expr.Peek().Cadena == "||")
        //                    {
        //                        var comparador = Expr.Dequeue();
        //                        if (Expr.Peek().Nombre != "IDENTIFICADOR")
        //                        {
        //                            //error de sintaxis

        //                        }
        //                        else
        //                        {
        //                            Expr.Dequeue();
        //                            //si se esta comparando con lo mismo
        //                        }
        //                    }
        //                    //Error de sintaxis
        //                }
        //                else if (expresion_Izquierda.Nombre == "NUMERO" || expresion_Izquierda.Nombre == "DECIMAL" || expresion_Izquierda.Nombre == "HEXADECIMAL")
        //                {
        //                    //if (Expr.Peek().Nombre == "OPERADOR")
        //                    //{
        //                    //    Expr.Dequeue();
        //                    //}
        //                    if (Expr.Peek().Cadena == "<" || Expr.Peek().Cadena == "=" || Expr.Peek().Cadena == ">" || Expr.Peek().Cadena == "|" || Expr.Peek().Cadena == "+" || Expr.Peek().Cadena == "-" || Expr.Peek().Cadena == "*" || Expr.Peek().Cadena == "/" || Expr.Peek().Cadena == "&")
        //                    {
        //                        var comparador = Expr.Dequeue();
        //                        //if (Expr.Peek().Nombre != expresion_Izquierda.Nombre)
        //                        //{
        //                        //    //error de sintaxis

        //                        //}
        //                        if (Expr.Peek().Nombre != "NUMERO" || Expr.Peek().Nombre != "DECIMAL" || Expr.Peek().Nombre != "HEXADECIMAL")
        //                        {
        //                            //error de sintaxis
        //                        }
        //                        else
        //                        {
        //                            //si se esta comparando con lo mismo
        //                        }
        //                    }
        //                    // ERROR DE SINTAXIS
        //                }
        //                //Parse_Expression();
        //                break;
        //            default:
        //                break;

        //        }
        //    }
        //}

        public void Parse_Expression()
        {
            var pos = Tokens.Count();
            var Expresion = Tokens.ElementAt(0);
            if (Expresion.Nombre == "NUMERO" || Expresion.Nombre == "DECIMAL" || Expresion.Nombre == "HEXADECIMAL" || Expresion.Nombre == "IDENTIFICADOR" || (Expresion.Nombre == "RESERVADA" && Expresion.Cadena == "bool"))
            {
                Parse_Constant();
                Parse_Expression();
            }
            else if (Expresion.Cadena == "(")
            {
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                Tokens.Dequeue();
                LookAhead = elementos.Cadena;
                Parse_Expression();
                Tokens.Dequeue();
                LookAhead = elementos.Cadena;
            }
            else// T
            {
                Parse_Expression_T();
            }
        }

        public void Parse_Expression_T()
        {
            var pos = Tokens.Count();
            var Expresion = Tokens.ElementAt(0);
            if (Expresion.Cadena == "&&")
            {
                Tokens.Dequeue();
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                LookAhead = elementos.Cadena;
                Parse_Expression_F();
            }
            else
            {
                Parse_Expression_F();
            }
        }

        public void Parse_Expression_F()
        {
            var pos = Tokens.Count();
            var Expresion = Tokens.ElementAt(0);
            if (Expresion.Cadena == "==")
            {
                Tokens.Dequeue();
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                LookAhead = elementos.Cadena;
                Parse_Expression_K();
            }
            else if (Expresion.Cadena == "!=")
            {
                Tokens.Dequeue();
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                LookAhead = elementos.Cadena;
                Parse_Expression_K();
            }
            else if (Expresion.Cadena == "!")
            {
                Tokens.Dequeue();
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                LookAhead = elementos.Cadena;
                Parse_Expression_F();
            }
            else
            {
                Parse_Expression_K();
            }
        }

        public void Parse_Expression_K()
        {
            var pos = Tokens.Count();
            var Expresion = Tokens.ElementAt(0);
            if (Expresion.Cadena == "<")
            {
                Tokens.Dequeue();
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                LookAhead = elementos.Cadena;
                Parse_Expression_M();
            }
            else if (Expresion.Cadena == "<=")
            {
                Tokens.Dequeue();
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                LookAhead = elementos.Cadena;
                Parse_Expression_M();
            }
            else if (Expresion.Cadena == ">")
            {
                Tokens.Dequeue();
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                LookAhead = elementos.Cadena;
                Parse_Expression_M();
            }
            else if (Expresion.Cadena == ">=")
            {
                Tokens.Dequeue();
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                LookAhead = elementos.Cadena;
                Parse_Expression_M();
            }
            else
            {
                Parse_Expression_M();
            }
        }

        public void Parse_Expression_M()
        {
            var pos = Tokens.Count();
            var Expresion = Tokens.ElementAt(0);
            if (Expresion.Cadena == "+")
            {
                Tokens.Dequeue();
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                LookAhead = elementos.Cadena;
                Parse_Expression_L();
            }
            else if (Expresion.Cadena == "-")
            {
                Tokens.Dequeue();
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                LookAhead = elementos.Cadena;
                Parse_Expression_L();
            }
            else
            {
                Parse_Expression_L();
            }

        }

        public void Parse_Expression_L()
        {
            var pos = Tokens.Count();
            var Expresion = Tokens.ElementAt(0);
            if (Expresion.Cadena == "*")
            {
                Tokens.Dequeue();
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                LookAhead = elementos.Cadena;
                Parse_Expression_U();
            }
            else if (Expresion.Cadena == "/")
            {
                Tokens.Dequeue();
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                LookAhead = elementos.Cadena;
                Parse_Expression_U();
            }
            else if (Expresion.Cadena == "%")
            {
                Tokens.Dequeue();
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                LookAhead = elementos.Cadena;
                Parse_Expression_U();
            }
            else
            {
                Parse_Expression_U();
            }
        }

        public void Parse_Expression_U()
        {
            Parse_Constant();

        }

        public void Parse_Constant()
        {
            if (Tokens.Peek().Nombre == "NUMERO")//adentro if
            {
                Tokens.Dequeue();
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                LookAhead = elementos.Cadena;
            }
            else if (Tokens.Peek().Nombre == "DECIMAL")//adentro if
            {
                Tokens.Dequeue();
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                LookAhead = elementos.Cadena;
            }
            else if (Tokens.Peek().Nombre == "RESERVADO" && Tokens.Peek().Cadena == "bool")//creacion variable
            {
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(poss + 1);
                if (elementos.Nombre == "IDENTIFICADOR")
                {
                    Tokens.Dequeue();
                    LookAhead = elementos.Cadena;
                }
                else
                {
                    var posR = Tokens.Count();
                    var elementoER = Tokens.ElementAt(posR + 1);
                    ListaErrores.Add("/////////ERROR DE SINTAXIS, Se esperaba: Nombre de identificador. Ingresó: " + elementoER.Cadena + "   ///////////////");
                    while (Tokens.Peek().Cadena != ";")
                    {
                        Tokens.Dequeue();
                    }
                    LookAhead = elementos.Cadena;
                    //error de sintaxis
                }
            }
            else if (Tokens.Peek().Nombre == "RESERVADO" && Tokens.Peek().Cadena == "int")//creacion variable
            {
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                LookAhead = elementos.Cadena;
                if (elementos.Nombre == "IDENTIFICADOR")
                {
                    Tokens.Dequeue();
                    LookAhead = elementos.Cadena;
                }
                else
                {
                    ListaErrores.Add("/////////ERROR DE SINTAXIS, Se esperaba: Nombre de identificador. Ingresó: " + elementos.Cadena + "   ///////////////");
                    while (elementos.Cadena != ";")
                    {
                        Tokens.Dequeue();
                    }
                    LookAhead = elementos.Cadena;
                    //error de sintaxis
                }
            }
            else if (Tokens.Peek().Nombre == "RESERVADO" && Tokens.Peek().Cadena == "double")//creacion variable
            {
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                if (elementos.Nombre == "IDENTIFICADOR")
                {
                    Tokens.Dequeue();
                    LookAhead = elementos.Cadena;
                }
                else
                {
                    ListaErrores.Add("/////////ERROR DE SINTAXIS, Se esperaba: Nombre de identificador. Ingresó: " + elementos.Cadena + "   ///////////////");
                    while (Tokens.Peek().Cadena != ";")
                    {
                        LookAhead = Tokens.Peek().Cadena;
                        var aux = Tokens.ElementAt(1);
                        if (aux.Cadena == ";")
                        {
                            break;
                        }
                        Tokens.Dequeue();
                    }
                    //error de sintaxis
                }
            }
            else if (Tokens.Peek().Nombre == "RESERVADO" && Tokens.Peek().Cadena == "string")//creacion variable
            {
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                if (elementos.Nombre == "IDENTIFICADOR")
                {
                    Tokens.Dequeue();
                    LookAhead = elementos.Cadena;
                }
                else
                {
                    ListaErrores.Add("/////////ERROR DE SINTAXIS, Se esperaba: Nombre de identificador. Ingresó: " + elementos.Cadena + "   ///////////////");
                    while (elementos.Cadena != ";")
                    {
                        Tokens.Dequeue();
                    }
                    LookAhead = elementos.Cadena;
                    //error de sintaxis
                }
            }
            else if (Tokens.Peek().Nombre == "RESERVADO" && Tokens.Peek().Cadena == "null")//creacion variable
            {
                Tokens.Dequeue();
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                LookAhead = elementos.Cadena;
            }
            else if (Tokens.Peek().Nombre == "IDENTIFICADOR")//adentro if
            {
                Tokens.Dequeue();
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                LookAhead = elementos.Cadena;

                if (Tokens.Peek().Cadena == ";")
                {
                    Tokens.Dequeue();
                    LookAhead = Tokens.ElementAt(1).Cadena;
                }
            }
            else if (Tokens.Peek().Cadena == "if" || Tokens.Peek().Cadena == "else" || Tokens.Peek().Cadena == "return" || Tokens.Peek().Cadena == "}" || Tokens.Peek().Cadena == "}")
            {
                // no haga nada
                var h = 0;
            }
            else
            {
                var poss = Tokens.Count();
                var elementos = Tokens.ElementAt(1);
                //error de sintaxis
                ListaErrores.Add("/////////ERROR DE SINTAXIS, Se esperaba: Tipo de constante. Ingresó: " + elementos.Cadena + "   ///////////////");
                while (elementos.Cadena != ";")
                {
                    Tokens.Dequeue();
                }
                LookAhead = elementos.Cadena;
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
                Tokens.Dequeue();
                LookAhead = Tokens.Peek().Cadena;
                MatchToken(Tokens.Peek().Cadena);//;
            }
        }
    }
}
