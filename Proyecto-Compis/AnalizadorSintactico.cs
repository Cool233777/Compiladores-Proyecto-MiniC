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
        
        public AnalizadorSintactico(AnalizadorLex Lex, List<PropiedadesDePalabras>Tokns)
        {
            Regex = Lex.REGEX;
            Tokens = Tokns;
        }
        public void IF_Statement()
        {
            while(cont<Tokens.Count)
            {
                MatchToken(Tokens[cont].Cadena, Tokens[cont]); //MatchToken("if")
                MatchToken(Tokens[cont].Cadena, Tokens[cont]);
                Parse_Expression();
                MatchToken(Tokens[cont].Cadena, Tokens[cont]);
                Parse_Expression();
                MatchToken(Tokens[cont].Cadena, Tokens[cont]);
                MatchToken(Tokens[cont].Cadena, Tokens[cont]);//MatchToken("else")
                Parse_Statement();
                MatchToken(Tokens[cont].Cadena, Tokens[cont]);


            }
        }

        public void RETURN_Statement(string T_Ex)
        {
          
        }

        public void MatchToken(string Expected,PropiedadesDePalabras tk)
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
                var aux = Tokens.FindIndex(x => x.Cadena == tk.Cadena);
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

        }

        public void Parse_Statement()
        {

        }
    }
}
