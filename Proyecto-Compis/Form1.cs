﻿using System;
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

        public Form1()
        {
            InitializeComponent();
        }

        public List<string> Lista_Reservadas = new List<string>() { { "void" }, { "public" }, { "int" }, { "double" }, { "bool" }, { "string" }, { "const" }, { "if" }, { "null" }, { "if" }, { "else" }, { "return" }, { "New" }, { "Console" }, { "WriteLine" }, { "for" }, { "while" }, { "break" }, { "class" }, { "interface" }, { "foreach" }, { "NewArray" }, { "class" }, { "this" }, };
        public AnalizadorLex Analizador_Lexico = new AnalizadorLex();//
        public List<PropiedadesDePalabras> Lista_Tokens = new List<PropiedadesDePalabras>();//
        public string Texto_A_Compilar;

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
            Analizador_Lexico.NuevaReglaDeTokens(@"\s+", "ESPACIO", true);
            Analizador_Lexico.NuevaReglaDeTokens(@"\b[_a-zA-Z](\w){0,24}\b", "IDENTIFICADOR");
            Analizador_Lexico.NuevaReglaDeTokens("\".*?\"", "CADENA");
            Analizador_Lexico.NuevaReglaDeTokens(@"'\\.'|'[^\\]'", "CARACTER");
            Analizador_Lexico.NuevaReglaDeTokens("//[^\r\n]*", "COMENTARIO_SIMPLE");
            Analizador_Lexico.NuevaReglaDeTokens("/[*].*?[*]/", "COMENTARIO_MULTILINEA");
            Analizador_Lexico.NuevaReglaDeTokens(@"-*[0-9]+\.[0-9]*([eE][+-]?)?[0-9]+", "DECIMAL");
            Analizador_Lexico.NuevaReglaDeTokens(@"([0][x|X])([0-9]|[a-f]|[A-F])*", "HEXADECIMAL");
            Analizador_Lexico.NuevaReglaDeTokens(@"-*[0-9]+", "NUMERO");
            Analizador_Lexico.NuevaReglaDeTokens(@"[\(\)\{\};,:]", "DELIMITADOR");
            Analizador_Lexico.NuevaReglaDeTokens(@"[\.=\+\-/*%]", "OPERADOR");
            Analizador_Lexico.NuevaReglaDeTokens(@">|<|==|>=|<=|!", "COMPARADOR");

            Analizador_Lexico.Debuggear(RegexOptions.Compiled | RegexOptions.Singleline | RegexOptions.ExplicitCapture);
        }

        public void AnalizarCodigo()
        {
            foreach (var token in Analizador_Lexico.Tokens(Texto_A_Compilar))
            {
                if (token.Nombre != "COMENTARIO_SIMPLE")
                {
                    if (token.Nombre != "COMENTARIO_MULTILINEA")
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
            }
        }

        private void button2_Click(object sender, EventArgs e)
        {
            AnalizarCodigo();
            AnalizadorSintactico sintax = new AnalizadorSintactico(Lista_Tokens);
            var Resultado_De_Sintaxis = sintax.Ejecutar_Analizador();
            AnalizadorSemántico Semantico = new AnalizadorSemántico(Lista_Tokens);
             Semantico.DefinirAmbitos();
            var lista = Semantico.RegresarAmbitos();
            using (var Archivo_Salida = new FileStream(Path.GetFullPath("Archivo de salida.out"), FileMode.Create))
            {
                using (var escritor = new StreamWriter(Archivo_Salida))
                {
                    escritor.Write(Resultado_De_Sintaxis);
                }
            }
        }
    }
}
