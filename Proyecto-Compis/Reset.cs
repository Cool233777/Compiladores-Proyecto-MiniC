using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compis
{
    class Reset
    {
        AnalizadorLex lexer;
        public void reset()
        {
            lexer.gex = null;
            lexer.pattern = null;
            lexer.compilar = true;
            lexer.TNombres.Clear();
            lexer.numeros = null;
        }
        
    }
}
