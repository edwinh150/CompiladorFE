using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Irony.Parsing;
namespace BLL
{
    public class Gramatica : Grammar
    {
        public Gramatica() : base(caseSensitive: false)
        {
            #region ER
            RegexBasedTerminal numero = new RegexBasedTerminal("numero", "[0-9]+");
            #endregion

            #region Terminales
            var mas = ToTerm("+");
            var menos = ToTerm("-");
            var div = ToTerm("/");
            var mul = ToTerm("*");
            var pa = ToTerm("(");
            var pc = ToTerm(")");
            #endregion

            #region no terminales
            NonTerminal S0 = new NonTerminal("S0"),
            S1 = new NonTerminal("S1"),
            S2 = new NonTerminal("S2"),
            S3 = new NonTerminal("S3"),
            S4 = new NonTerminal("S4"),
            S5 = new NonTerminal("S5");
            #endregion

            #region producciones

            //Gramatica ambigua menos nodos creados
            S0.Rule = S1;

            S1.Rule = S1 + div + S1
                 | S1 + mul + S1
                 | S1 + mas + S1
                 | S1 + menos + S1
                 | pa + S1 + pc
                 | numero
                 | SyntaxError;


            #endregion

            #region de configuraciones
            this.Root = S0;
            RegisterOperators(2, Associativity.Left, "*", "/");
            RegisterOperators(1, Associativity.Left, "+", "-");
            #endregion
        }
    }
}
