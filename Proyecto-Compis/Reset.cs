using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compis
{
   public class Reset
    {
        AnalizadorLex lexer;
        public void reset()
        {
            lexer.REGEX = null;
            lexer.PATTERN = null;
            lexer.DEBUG = true;
            lexer.TNOMBRES.Clear();
            lexer.NUMEROS = null;
        }
        
    }
}
