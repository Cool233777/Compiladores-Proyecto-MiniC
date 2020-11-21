using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compis
{
   public class ResolverDecimal
    {
        public List<string> CambiarPostfijo(List<PropiedadesDePalabras> TokensOperando)
        {
            //var vec = infijo.Split(' ');
            //int tamanio = infijo.Length;
            Stack<string> pila = new Stack<string>();
            List<string> postfijo = new List<string>();
            for (int i = 0; i < TokensOperando.Count; i++)
            {
                if (TokensOperando[i].Nombre == "DECIMAL" || TokensOperando[i].Nombre == "NUMERO")
                {
                    postfijo.Add(TokensOperando[i].Cadena);
                }
                else if (TokensOperando[i].Cadena == "(")
                {
                    pila.Push(TokensOperando[i].Cadena);
                }
                else if ((TokensOperando[i].Cadena == "*") || (TokensOperando[i].Cadena == "+") || (TokensOperando[i].Cadena == "-") || (TokensOperando[i].Cadena == "/"))
                {
                    while ((pila.Count > 0) && (pila.Peek() != "("))
                    {
                        if (precedenciadeoperadores(pila.Peek(), TokensOperando[i].Cadena))
                        {
                            postfijo.Add(pila.Pop());
                        }
                        else
                        {
                            break;
                        }
                    }
                    pila.Push(TokensOperando[i].Cadena);
                }
                else if (TokensOperando[i].Cadena == ")")
                {
                    while ((pila.Count > 0) && (pila.Peek() != "("))
                    {
                        postfijo.Add(pila.Pop());
                    }
                    if (pila.Count > 0)
                        pila.Pop(); //quita el parentesis izquierdo de la pila
                }
                else
                {
                    while (postfijo.Count > 0)
                    {
                        postfijo.RemoveAt(0);
                    }
                    postfijo.Add("Error");
                    return postfijo;
                }
            }
            while (pila.Count > 0)
            {
                postfijo.Add(pila.Pop());
            }
            return postfijo;
        }
        public bool precedenciadeoperadores(string top, string p_2)
        {
            if (top == "+" && p_2 == "*") // + tiene menor precedencia que *
                return false;
            if (top == "*" && p_2 == "-") // * tiene mayor precedencia que -
                return true;
            if (top == "+" && p_2 == "-") // + tiene la misma precedencia que +
                return true;
            return true;
        }
        public double evaluarResultado(List<string> posfija)
        {
            Stack<double> pilaResultado = new Stack<double>();
            //var tamanio = posfija
            //int tama = posfija.Length;
            for (int i = 0; i < posfija.Count; i++)
            {
                if ((posfija[i] == "*") || (posfija[i] == "+") || (posfija[i] == "-") || (posfija[i] == "/"))
                {
                    double resz = operador(pilaResultado.Pop(), pilaResultado.Pop(), posfija[i]);
                    pilaResultado.Push(resz);
                }
                else //if ((posfija.Peek() >= 0) || (posfija.Peek() <= '9'))
                {
                    pilaResultado.Push((Convert.ToDouble(posfija[i]) - 0));
                }
            }
            return Convert.ToDouble(pilaResultado.Pop());
        }
        public double operador(double p, double p_2, string p_3)
        {
            switch (p_3)
            {
                case "+":
                    return p_2 + p;
                case "-":
                    return p_2 - p;
                case "*":
                    return p_2 * p;
                case "/":
                    return p_2 / p;
                default:
                    return -1;
            }
        }
    }
}
