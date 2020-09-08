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
        Stack<PropiedadesDePalabras> Expr = new Stack<PropiedadesDePalabras>();
        public AnalizadorSintactico(AnalizadorLex Lex, List<PropiedadesDePalabras>Tokns)
        {
            Regex = Lex.REGEX;
            Tokens = Tokns;
        }
        public void IF_Statement()
        {
            while(cont<Tokens.Count)
            {
                MatchToken(Tokens[cont].Cadena);//MatchToken("if");
                MatchToken(Tokens[cont].Cadena);// (
                Parse_Expression();
                MatchToken(Tokens[cont].Cadena);// )
                MatchToken(Tokens[cont].Cadena); // {
                Parse_Statement();
                MatchToken(Tokens[cont].Cadena);// }
                MatchToken(Tokens[cont].Cadena);//MatchToken("else");
                MatchToken(Tokens[cont].Cadena); // {
                ELSE_Parse_Statement();
                MatchToken(Tokens[cont].Cadena);// }
                //Parse_Statement(); // o es if o es {
                //MatchToken(Tokens[cont].Cadena);// (
                //Parse_Expression();
                //MatchToken(Tokens[cont].Cadena);// )
                //MatchToken(Tokens[cont].Cadena);// {
                //Parse_Statement();
                //MatchToken(Tokens[cont].Cadena); // }
            }
        }

        public void RETURN_Statement()
        {
          
        }

        public void MatchToken(string Expected)
        {
            if(PrimeraVez)
            {
                LookAhead = Regex.Match(Expected).Value;
                PrimeraVez = false;
            }
            //aqui hace match el token y creo que se selecciona el lookahead aqui Match match = REGEX.Match(Texto_A_Compilar);
            //Match match1 = Regex.Match(Expected);
            if (LookAhead == Expected)
            {
                var aux = Tokens.FindIndex(x => x.Cadena == Expected);//cambiar/arrelgar para sacar los tokens aprobados
                LookAhead = Tokens[aux + 1].Cadena;
                cont++;
            }
            else
            {
                //ERROR DE SINTAXIS
            }
        }

        public void Parse_Expression()
        {
            //ver todo lo que esta adentro de la expresion

            var contador = cont;
            while (Tokens[contador].Cadena!=")")
            {
                Expr.Push(Tokens[contador]);
                contador++;
            }
            var Cadena_A_Comparar = 1;
            switch (Cadena_A_Comparar)
            {
                //case 1:
                //if ((Expr.Peek().Nombre == "IDENTIFICADOR") || (Parse_Expression()+"." + "IDENTIFICADOR" == "R"))
                // {

                //break;
                case 1:
                    Parse_Constant();
                    Cadena_A_Comparar = 2;
                    break;
                case 2:
                    Parse_LValue();
                    break;
                default:
                    break;
 
            }
        }
        public void Parse_LValue()
        {
            //dudas
        }
        public void Parse_Constant()
        {
            if(Tokens[cont].Nombre == "NUMERO")
            {

            }
            else if 
                (Tokens[cont].Nombre=="DECIMAL")
            {

            }
            else if (Tokens[cont].Nombre=="RESERVADO" && Tokens[cont].Cadena == "bool")
            {

            }
            else if (Tokens[cont].Nombre == "RESERVADO" && Tokens[cont].Cadena == "string")
            {

            }
            else if (Tokens[cont].Nombre == "RESERVADO" && Tokens[cont].Cadena == "null")
            {

            }
            else
            {
                //error de sintaxis, porque estos son los parámetros que recibe el if obligatorio 
            }
        }
        public void Parse_Statement()
        {
            if (LookAhead == "if")
            {
                IF_Statement();
            }
            else if (LookAhead == "return")
            {
                RETURN_Statement();
            }
            Parse_Expression();
        }

        public void ELSE_Parse_Statement()
        {
            if (LookAhead == "if")
            {
                IF_Statement();
            }
            Parse_Expression();
            //if (LoockAhead == "if")
            //{
            //    MatchToken(LoockAhead);
            //    cont++;
            //    if (LoockAhead == "(")
            //    {
            //        MatchToken(LoockAhead);
            //        cont++;
            //    }
            //}
            //else if (LoockAhead == "{")
            //{
            //    MatchToken(LoockAhead);
            //    cont++;
            //}
            //else
            //{
            //    //error de sintaxis
            //}
        }
    }
}
