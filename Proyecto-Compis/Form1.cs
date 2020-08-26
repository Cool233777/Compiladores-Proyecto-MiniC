using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.Text.RegularExpressions;

namespace Proyecto_Compis
{
    public partial class Form1 : Form
    {

        public List<string> Lista_Reservadas = new List<string>() { { "void" }, { "public" }, { "int" }, { "double" }, { "bool" }, { "string" }, { "const" }, { "if" }, { "null" }, { "if" }, { "else" }, { "return" }, { "New" }, { "Console" }, { "WriteLine" }, { "for" }, { "while" }, { "break" }, { "class" }, { "interface" }, { "foreach" }, { "NewArray" }, { "class" }, { "this" }, };
        public AnalizadorLex Analizador_Lexico = new AnalizadorLex();
        public List<PropiedadesDePalabras> Lista_Tokens = new List<PropiedadesDePalabras>();
        public string Texto_A_Compilar;

        public Form1()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var archivo = new FileStream(Path.GetFullPath(openFileDialog1.FileName), FileMode.OpenOrCreate);
                var lector = new StreamReader(archivo);
                Texto_A_Compilar = lector.ReadToEnd();
                label2.Text = archivo.Name;
                archivo.Close(); lector.Close();
            }

        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Introduccir reglas para reconcoer los tokens
            
            Analizador_Lexico.NuevaReglaDeTokens(@"\d*\.?\d+", "NUMERO");          
            Analizador_Lexico.NuevaReglaDeTokens(@"[\.=\+\-/*%]", "OPERADOR");
            Analizador_Lexico.NuevaReglaDeTokens(@"\s+", "ESPACIO", true);
            Analizador_Lexico.NuevaReglaDeTokens("//[^\r\n]*", "COMENTARIO1");
            Analizador_Lexico.NuevaReglaDeTokens("/[*].*?[*]/", "COMENTARIO2");
            Analizador_Lexico.NuevaReglaDeTokens(@"\b[_a-zA-Z][\w]*\b{32}", "IDENTIFICADOR");
            Analizador_Lexico.NuevaReglaDeTokens("\".*?\"", "CADENA");
            Analizador_Lexico.NuevaReglaDeTokens(@"[\(\)\{\}\[\];,]", "DELIMITADOR");
            Analizador_Lexico.NuevaReglaDeTokens(@"'\\.'|'[^\\]'", "CARACTER");
            Analizador_Lexico.NuevaReglaDeTokens(@">|<|==|>=|<=|!", "COMPARADOR");
           // Analizador_Lexico.NuevaReglaDeTokens(@"/(0x)?[0-9a-f]+/", "HEXADECIMAL");
            //Analizador_Lexico.NuevaReglaDeTokens(@"^[0-9]([.,][0-9]{1,3})?$", "DOUBLE");
            
            
            Analizador_Lexico.Debuggear(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture);
        }

        public void AnalizarCodigo()
        {

            foreach (var token in Analizador_Lexico.Tokens(Texto_A_Compilar))
            {

                if (token.Nombre == "IDENTIFICADOR")
                {
                    if (Lista_Reservadas.Contains(token.Cadena))
                    {
                        token.Nombre = "RESERVADO";
                    }
                }
                Lista_Tokens.Add(token);
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AnalizarCodigo();
            using (var Archivo_Salida = new FileStream(Path.GetFullPath("Archivo de salida.out"), FileMode.Create))
            {
                using (var escritor = new StreamWriter(Archivo_Salida))
                {
                    foreach (var item in Lista_Tokens)
                    {
                        if (item.Nombre == "ERROR")
                        {
                            escritor.WriteLine("// Error en la Linea: " + item.Linea + ". // Caracter no reconocido: " + item.Cadena);
                        }
                        else
                        {
                            escritor.WriteLine(item.Cadena + "      Linea: " + item.Linea + ", Columna: " + item.Columna + ", es:     " + item.Nombre);
                        }
                    }
                }
            }
        }
    }
}
