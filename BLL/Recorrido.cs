using Irony.Parsing;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL
{
    public class Recorrido
    {
        public string salida(ParseTreeNode raiz) // esto es quivalente a decir S0->S1{print(S0.resultado)}
        {
            return MiRecorrido(raiz.ChildNodes[0]).ToString();
        }

        private int MiRecorrido(ParseTreeNode raiz)//recorrido del arbol de forma recursiva 
        {
            if (raiz.ChildNodes.Count == 3)// se sabe q si la raiz tiene 3 nodos va a ser una expresion por ejemplo 78*8
            {
                if (raiz.ChildNodes[1].Term.Name.ToString() == "*") //tomar en cuenta que es mas optimo hacerlo con un switch
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
                else // es '('numero')'
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
