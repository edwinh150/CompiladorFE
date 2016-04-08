using System;
using BLL;
using Irony.Parsing;
using System.Windows.Forms;
using System.IO;
using System.Diagnostics;

namespace FECompiler
{
    class Sintactico
    {

        public Boolean EsValido(String Cadena)
        {
            #region parser
            LanguageData lenguaje = new LanguageData(new Gramatica());
            Parser Par = new Parser(lenguaje);
            ParseTree raiz = Par.Parse(Cadena);
            #endregion

            #region validacion
            if (raiz.Root == null)
            {
                #region reportes de errores
                raiz = Par.Context.CurrentParseTree;
                Par.RecoverFromError();

                if (raiz.ParserMessages.Count == 0) MessageBox.Show("No error");
                foreach (var err in raiz.ParserMessages)
                {
                    MessageBox.Show("Linea: " + err.Location.Line.ToString() + " Columna: " + err.Location.Column.ToString() + " Mensaje: " + err.Message + "\nInforme completo: " + err.ToString());
                }
                #endregion

                return false;
            }
            else
            {
                String cuerpo2 = "";
                Recorrido(raiz.Root, ref cuerpo2, 0);
                archivo(cuerpo2);
                Recorrido sa = new Recorrido();
                Ejecutar(sa.salida(raiz.Root));
                return true;
            }
            #endregion

        }

        private void archivo(String cadena)
        {
            try
            {
                String contenido = "digraph G {node[shape=box, style=filled, color=Gray95]; edge[color=blue];rankdir=UD \n" + cadena + "   \n}";
                FileStream buffer = new FileStream("arbol.dot", FileMode.Create, FileAccess.Write);
                StreamWriter escribir = new StreamWriter(buffer);
                escribir.WriteLine(contenido);
                escribir.Close();
                
                var comando = string.Format("dot -Tjpg arbol.dot -o arbol.jpg");
                var ejecutar = new System.Diagnostics.ProcessStartInfo("cmd", "/C" + comando);
                var proc = new System.Diagnostics.Process();
                proc.StartInfo = ejecutar;
                proc.Start();
                proc.WaitForExit();
            }
            catch (Exception x)
            {
                MessageBox.Show("Error: " + x);
            }
        }

        int aux = 1;
        int incremento()
        {
            return aux++;
        }

        void Recorrido(ParseTreeNode raiz, ref String cuerpo, int id)
        {
            int var;
            foreach (ParseTreeNode hijos in raiz.ChildNodes)
            {
                var = incremento();
                cuerpo += "\"" + id.ToString() + "_" + raiz.ToString() + "\"->\"" + var.ToString() + "_" + hijos.ToString() + "\"";
                Recorrido(hijos, ref cuerpo, var);
            }

        }

        public void Ejecutar(string resultado)
        {
            string fileName = "imprimir.bat";
            FileStream stream = new FileStream(fileName, FileMode.Create, FileAccess.Write);
            StreamWriter writer = new StreamWriter(stream);
            writer.WriteLine("@echo off \ntitle FECompiler \necho El Resultado es: " + resultado + " \n\n\npause");
            writer.Close();
            Process.Start("imprimir.bat");
        }
    }
}
