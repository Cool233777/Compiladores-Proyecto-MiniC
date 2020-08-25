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
             
        public Dictionary<int, string> DicReservadas = new Dictionary<int, string>() { {1,  "void" }, {2, "public" }, { 3, "int" }, { 4, "double" }, { 5, "bool" }, { 6, "string" }, { 7, "const" }, { 8, "if" }, { 9, "null" }, { 10, "if" }, { 11, "else" }, { 12, "return" }, { 13, "New" }, { 14, "Console" }, { 15, "WriteLine" }, { 16, "for" }, { 17, "while" }, { 18, "break" }, { 19, "class" }, { 20, "interface" }, { 21, "foreach" }, { 22, "NewArray" }, { 23, "class" }, { 24, "this" }, };
        public AnalizadorLex AnalizadorLexico = new AnalizadorLex();
        public Form1()
        {
            InitializeComponent();
        }

        public string Analizador;

        private void button1_Click(object sender, EventArgs e)
        {
            if (openFileDialog1.ShowDialog() == DialogResult.OK)
            {
                var archivo = new FileStream(Path.GetFullPath(openFileDialog1.FileName), FileMode.OpenOrCreate);
                var lector = new StreamReader(archivo);
                var listaArchivoOriginal = new List<string>();
                while (!lector.EndOfStream)
                {
                    listaArchivoOriginal.Add(lector.ReadLine());
                }
                label2.Text = archivo.Name;
                archivo.Close(); lector.Close();
            }
            
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            //Introduccir reglas para reconcoer los tokens
            AnalizadorLexico.NuevaReglaDeTokens(@"\s+", "ESPACIO", true);
            AnalizadorLexico.NuevaReglaDeTokens(@"\b[_a-zA-Z][\w]*\b", "IDENTIFICADOR");
            AnalizadorLexico.NuevaReglaDeTokens("\".*?\"", "CADENA");
            AnalizadorLexico.NuevaReglaDeTokens(@"'\\.'|'[^\\]'", "CARACTER");
            AnalizadorLexico.NuevaReglaDeTokens("//[^\r\n]*", "COMENTARIO1");
            AnalizadorLexico.NuevaReglaDeTokens("/[*].*?[*]/", "COMENTARIO2");
            AnalizadorLexico.NuevaReglaDeTokens(@"\d*\.?\d+", "NUMERO");
            AnalizadorLexico.NuevaReglaDeTokens(@"[\(\)\{\}\[\];,]", "DELIMITADOR");
            AnalizadorLexico.NuevaReglaDeTokens(@"[\.=\+\-/*%]", "OPERADOR");
            AnalizadorLexico.NuevaReglaDeTokens(@">|<|==|>=|<=|!", "COMPARADOR");
        }
    }
}
