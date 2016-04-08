using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class Recorrido
    {
        public string salida(ParseTreeNode raiz)
        {
            return MiRecorrido(raiz.ChildNodes[0]).ToString();
        }

        private int MiRecorrido(ParseTreeNode raiz)
        {
            if (raiz.ChildNodes.Count == 3)
            {
                if (raiz.ChildNodes[1].Term.Name.ToString() == "*")
                {
                    return MiRecorrido(raiz.ChildNodes[0]) * MiRecorrido(raiz.ChildNodes[2]);
                }
                else if (raiz.ChildNodes[1].Term.Name.ToString() == "+")
                {
                    return MiRecorrido(raiz.ChildNodes[0]) + MiRecorrido(raiz.ChildNodes[2]);
                }
                else if (raiz.ChildNodes[1].Term.Name.ToString() == "-")
                {
                    return MiRecorrido(raiz.ChildNodes[0]) - MiRecorrido(raiz.ChildNodes[2]);
                }
                else if (raiz.ChildNodes[1].Term.Name.ToString() == "/")
                {
                    return MiRecorrido(raiz.ChildNodes[0]) / MiRecorrido(raiz.ChildNodes[2]);
                }
                else
                {
                    return MiRecorrido(raiz.ChildNodes[1]);
                }

            }
            else
            {
                return Convert.ToInt32(raiz.ChildNodes[0].Token.Text);
            }

        }
    }
}
