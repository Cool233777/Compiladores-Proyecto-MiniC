using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compis
{
    class AnalizadoeSintactico
    {
        public void IF_Statement(string T_Ex)
        {
            MatchToken("if");
            Parse_Expression();
            Parse_Statement();
            MatchToken("else");
            Parse_Statement();
        }

        public void RETURN_Statement(string T_Ex)
        {
            MatchToken("return");
            Parse_Expression();
            MatchToken(";");
        }

        public void MatchToken(string Expected)
        {
            //aqui hace match el token y creo que se selecciona el lookahead aqui
        }

        public void Parse_Expression()
        {

        }

        public void Parse_Statement()
        {

        }
    }
}
