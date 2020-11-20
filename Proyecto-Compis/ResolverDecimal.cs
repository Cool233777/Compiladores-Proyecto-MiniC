using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Proyecto_Compis
{
   public class ResolverDecimal
    {
        public string CambiarPostfijo(string infijo)
        {
            int tamanio = infijo.Length;
            Stack<char> pila = new Stack<char>();
            StringBuilder postfijo = new StringBuilder();
            for (int i = 0; i < tamanio; i++)
            {
                if ((infijo[i] >= '0') && (infijo[i] <= '9') || infijo[i] =='.')
                {
                    postfijo.Append(infijo[i]);
                }
                else if (infijo[i] == '(')
                {
                    pila.Push(infijo[i]);
                }
                else if ((infijo[i] == '*') || (infijo[i] == '+') || (infijo[i] == '-') || (infijo[i] == '/'))
                {
                    while ((pila.Count > 0) && (pila.Peek() != '('))
                    {
                        if (precedenciadeoperadores(pila.Peek(), infijo[i]))
                        {
                            postfijo.Append(pila.Pop());
                        }
                        else
                        {
                            break;
                        }
                    }
                    pila.Push(infijo[i]);
                }
                else if (infijo[i] == ')')
                {
                    while ((pila.Count > 0) && (pila.Peek() != '('))
                    {
                        postfijo.Append(pila.Pop());
                    }
                    if (pila.Count > 0)
                        pila.Pop(); //quita el parentesis izquierdo de la pila
                }
                else
                {
                    return "Error";
                }
            }
            while (pila.Count > 0)
            {
                postfijo.Append(pila.Pop());
            }
            return postfijo.ToString();
        }
        public bool precedenciadeoperadores(char top, char p_2)
        {
            if (top == '+' && p_2 == '*') // + tiene menor precedencia que *
                return false;
            if (top == '*' && p_2 == '-') // * tiene mayor precedencia que -
                return true;
            if (top == '+' && p_2 == '-') // + tiene la misma precedencia que +
                return true;
            return true;
        }
        public int evaluarResultado(string posfija)
        {
            Stack<int> pilaResultado = new Stack<int>();
            int tama = posfija.Length;
            for (int i = 0; i < tama; i++)
            {
                if ((posfija[i] == '*') || (posfija[i] == '+') || (posfija[i] == '-') || (posfija[i] == ' '))
                {
                    int resz = operador(pilaResultado.Pop(), pilaResultado.Pop(), posfija[i]);
                    pilaResultado.Push(resz);
                }
                else if ((posfija[i] >= '0') || (posfija[i] <= '9'))
                {
                    pilaResultado.Push((int)(posfija[i] - '0'));
                }
            }
            return pilaResultado.Pop();
        }
        public int operador(int p, int p_2, char p_3)
        {
            switch (p_3)
            {
                case '+':
                    return p_2 + p;
                case '-':
                    return p_2 - p;
                case '*':
                    return p_2 * p;
                case '/':
                    return p_2 / p;
                default:
                    return -1;
            }
        }
    }
}
