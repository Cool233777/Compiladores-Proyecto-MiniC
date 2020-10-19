using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compis
{
    class AnalizadorSintactico
    {
        public static Dictionary<string, string[]> DiCCIONARIO_DE_SIMBOLOS = new Dictionary<string, string[]>();
        public static Dictionary<int, Dictionary<string, string[]>> DICCIONARIO_DE_ESTADOS = new Dictionary<int, Dictionary<string, string[]>>();

        public static void Agregar_Estados()
        {
            Dictionary<string, string[]> Dic_simbolos0 = new Dictionary<string, string[]>();
            
            //ESTADO 0 
            Dic_simbolos0.Add(";", new string[2] {"Error", ""});
            Dic_simbolos0.Add("id", new string[2] { "D19", "" });
            Dic_simbolos0.Add("const", new string[2] { "D11", "" });
            
            DICCIONARIO_DE_ESTADOS.Add(0, Dic_simbolos0);
            //terminamos con el estado 0
            Dictionary<string, string[]> Dic_simbolos1 = new Dictionary<string, string[]>();
            Dic_simbolos1.Add(";", new string[2] { "ERROR", "" });
            Dic_simbolos1.Add("id", new string[2] { "D19", "" });
            Dic_simbolos1.Add("const", new string[2] { "D11", "" });
            //terminamos con el estado 0
            DICCIONARIO_DE_ESTADOS.Add(1, Dic_simbolos1);



        }

    }
}
