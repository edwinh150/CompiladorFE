using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Collections.Generic;

namespace FECompiler
{
    public partial class MainForm : Form
    {
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        public static extern bool AllocConsole();
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        public static extern bool FreeConsole();

        //Funciones a utilizar
        const string Multiplicar = "#mul";
        const string Dividir = "#div";
        const string Sumar = "#sum";
        const string Restar = "#res";
        const string Porcentaje = "#por";
        const string RaizCuadrada = "#rac";
        const string Concatenar = "#con";
        const string Imprimir = "#imp";
        string Codigo;
        List<string> Var = new List<string>();
        List<string> Val = new List<string>();

        public MainForm()
        {
            InitializeComponent();
        }

        public bool Variables()
        {
            bool retorno = false;
            int menor = 0;
            int mayor = 0;
            int indice = 0;
            string aux, aux2; ;
            string variables;
            menor = Codigo.IndexOf('<');
            mayor = Codigo.IndexOf('>');
            if (menor != -1 || mayor != -1)
            {
                variables = Codigo.Substring(menor + 1, mayor - 1);

                //agregando variables
                do
                {
                    indice = variables.IndexOf(";");
                    aux = variables.Substring(0, indice);
                    variables = variables.Substring(indice);
                    menor = aux.IndexOf('=');
                    mayor = aux.Length;
                    aux2 = aux.Substring(menor + 1);
                    menor -= 4;
                    aux = aux.Substring(4, menor);
                    Var.Add(aux);
                    Val.Add(aux2);
                    retorno = true;
                } while (variables.Length != variables.Length);
            }
            return retorno;
        }

        private void abrirToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainopenFileDialog.ShowDialog();
            StreamReader file = new StreamReader(MainopenFileDialog.FileName);
            CompiladorRichTextBox.Text = file.ReadLine();            
        }

        private void guardarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MainsaveFileDialog.FileName = "New File.fe";
            var sf = MainsaveFileDialog.ShowDialog();
            if (sf == DialogResult.OK)
            {
                using (var savefile = new StreamWriter(MainsaveFileDialog.FileName))
                {
                    savefile.WriteLine(CompiladorRichTextBox);
                }                    
            }
        }

        private void cerrarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Environment.Exit(0);
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var cl = MaincolorDialog.ShowDialog();
            if (cl == DialogResult.OK)
            {
                CompiladorRichTextBox.ForeColor = MaincolorDialog.Color;
            }
        }

        private void formatoToolStripMenuItem_Click(object sender, EventArgs e)
        {
            var fm = MainfontDialog.ShowDialog();
            if (fm == DialogResult.OK)
            {
                CompiladorRichTextBox.Font = MainfontDialog.Font;
            }
        }

        public void Ejecutar(string resultado)
        {
            Thread.Sleep(2000);
            AllocConsole();
            Console.Title = "FECompiler 0.1.3";
            Console.WriteLine("El resultado es: "+resultado);
            Console.Write("\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n\n");
            Console.Write("------------------------------------------------------------\n");
            Console.Write("por favor precione una tecle para continuar...");
            Console.ReadKey();
            FreeConsole();
        }

        private void strarToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Compilar();
        }

        void Compilar()
        {
            bool varia = false;
            Codigo = CompiladorRichTextBox.Text;
            int menor;
            int mayor;
            menor = Codigo.IndexOf('<');
            mayor = Codigo.IndexOf('>');
            if (menor + 1 != mayor)
                varia = Variables();

            Codigo = Codigo.Substring(Codigo.IndexOf("#"));
            string Funcion = Codigo.Substring(0, 4);
            string Ejecucion = Codigo.Substring(4);

            if (Funcion == Sumar)
            {
                int Ap = Ejecucion.IndexOf("(");
                int CP = Ejecucion.IndexOf(")");
                int C = Ejecucion.IndexOf(",");
                double Num, Num1;
                int num1 = C;
                int num2 = CP - C;
                if (varia == true)
                {

                    Ejecucion = Ejecucion.Replace(Var[0].ToString(), Val[0].ToString());
                    Var = new List<string>();
                    Val = new List<string>();
                    Ap = Ejecucion.IndexOf("(");
                    CP = Ejecucion.IndexOf(")");
                    C = Ejecucion.IndexOf(",");
                    num1 = C;
                    num2 = CP - C;
                    double.TryParse((Ejecucion.Substring(1, (num1 - 1))), out Num);
                    double.TryParse((Ejecucion.Substring((C + 1), (num2 - 1))), out Num1);
                    Ejecutar((Num + Num1).ToString());
                    //CompiladorRichTextBox.Text += (Num + Num1).ToString();
                }
                else
                {
                    double.TryParse((Ejecucion.Substring(1, (num1 - 1))), out Num);
                    double.TryParse((Ejecucion.Substring((C + 1), (num2 - 1))), out Num1);
                    Ejecutar((Num + Num1).ToString());
                    //CompiladorRichTextBox.Text += (Num + Num1).ToString();
                }
            }
            else if (Funcion == Restar)
            {
                int Ap = Ejecucion.IndexOf("(");
                int CP = Ejecucion.IndexOf(")");
                int C = Ejecucion.IndexOf(",");
                double Num, Num1;
                int num1 = C;
                int num2 = CP - C;
                if (varia == true)
                {

                    Ejecucion = Ejecucion.Replace(Var[0].ToString(), Val[0].ToString());
                    Var = new List<string>();
                    Val = new List<string>();
                    Ap = Ejecucion.IndexOf("(");
                    CP = Ejecucion.IndexOf(")");
                    C = Ejecucion.IndexOf(",");
                    num1 = C;
                    num2 = CP - C;
                    double.TryParse((Ejecucion.Substring(1, (num1 - 1))), out Num);
                    double.TryParse((Ejecucion.Substring((C + 1), (num2 - 1))), out Num1);
                    Ejecutar((Num - Num1).ToString());
                    //CompiladorRichTextBox.Text += (Num - Num1).ToString();
                }
                else
                {
                    double.TryParse((Ejecucion.Substring(1, (num1 - 1))), out Num);
                    double.TryParse((Ejecucion.Substring((C + 1), (num2 - 1))), out Num1);
                    Ejecutar((Num - Num1).ToString());
                    //CompiladorRichTextBox.Text += (Num - Num1).ToString();
                }
            }
            else if (Funcion == Multiplicar)
            {
                int Ap = Ejecucion.IndexOf("(");
                int CP = Ejecucion.IndexOf(")");
                int C = Ejecucion.IndexOf(",");
                double Num, Num1;
                int num1 = C;
                int num2 = CP - C;
                if (varia == true)
                {

                    Ejecucion = Ejecucion.Replace(Var[0].ToString(), Val[0].ToString());
                    Var = new List<string>();
                    Val = new List<string>();
                    Ap = Ejecucion.IndexOf("(");
                    CP = Ejecucion.IndexOf(")");
                    C = Ejecucion.IndexOf(",");
                    num1 = C;
                    num2 = CP - C;
                    double.TryParse((Ejecucion.Substring(1, (num1 - 1))), out Num);
                    double.TryParse((Ejecucion.Substring((C + 1), (num2 - 1))), out Num1);
                    Ejecutar((Num * Num1).ToString());
                    //CompiladorRichTextBox.Text += (Num * Num1).ToString();
                }
                else
                {
                    double.TryParse((Ejecucion.Substring(1, (num1 - 1))), out Num);
                    double.TryParse((Ejecucion.Substring((C + 1), (num2 - 1))), out Num1);
                    Ejecutar((Num * Num1).ToString());
                    //CompiladorRichTextBox.Text += (Num * Num1).ToString();
                }
            }
            else if (Funcion == Dividir)
            {
                int Ap = Ejecucion.IndexOf("(");
                int CP = Ejecucion.IndexOf(")");
                int C = Ejecucion.IndexOf(",");
                double Num, Num1;
                int num1 = C;
                int num2 = CP - C;
                if (varia == true)
                {

                    Ejecucion = Ejecucion.Replace(Var[0].ToString(), Val[0].ToString());
                    Var = new List<string>();
                    Val = new List<string>();
                    Ap = Ejecucion.IndexOf("(");
                    CP = Ejecucion.IndexOf(")");
                    C = Ejecucion.IndexOf(",");
                    num1 = C;
                    num2 = CP - C;
                    double.TryParse((Ejecucion.Substring(1, (num1 - 1))), out Num);
                    double.TryParse((Ejecucion.Substring((C + 1), (num2 - 1))), out Num1);
                    Ejecutar((Num / Num1).ToString());
                    //CompiladorRichTextBox.Text += (Num / Num1).ToString();
                }
                else
                {
                    double.TryParse((Ejecucion.Substring(1, (num1 - 1))), out Num);
                    double.TryParse((Ejecucion.Substring((C + 1), (num2 - 1))), out Num1);
                    Ejecutar((Num / Num1).ToString());
                    //CompiladorRichTextBox.Text += (Num / Num1).ToString();
                }
            }
            else if (Funcion == Porcentaje)
            {
                int Ap = Ejecucion.IndexOf("(");
                int CP = Ejecucion.IndexOf(")");
                int C = Ejecucion.IndexOf(",");
                double Num;
                int num1 = C;
                int num2 = CP - C;
                double.TryParse((Ejecucion.Substring(1, (num1 - 1))), out Num);
                int.TryParse((Ejecucion.Substring((C + 1), (num2 - 1))), out num2);
                Ejecutar(((Num * num2) / 100).ToString());
                //CompiladorRichTextBox.Text += ((Num * num2) / 100).ToString();
            }
            else if (Funcion == RaizCuadrada)
            {
                int Ap = Ejecucion.IndexOf("(");
                int CP = Ejecucion.IndexOf(")");
                double Num;
                int num1 = CP;
                double.TryParse((Ejecucion.Substring(1, (num1 - 1))), out Num);
                Ejecutar((Math.Sqrt(Num)).ToString());
                //CompiladorRichTextBox.Text += (Math.Sqrt(Num)).ToString();
            }
            else if (Funcion == Concatenar)
            {
                int Ap = Ejecucion.IndexOf("(");
                int CP = Ejecucion.IndexOf(")");
                int C = Ejecucion.IndexOf(",");
                int num1 = C;
                int num2 = CP - C;
                string s1 = Ejecucion.Substring(1, (num1 - 1));
                string s2 = Ejecucion.Substring((C + 1), (num2 - 1));
                Ejecutar(string.Concat(s1, s2));
                //CompiladorRichTextBox.Text += string.Concat(s1, s2);
            }
            else if (Funcion == Imprimir)
            {
                int Ap = Ejecucion.IndexOf("(");
                int CP = Ejecucion.IndexOf(")");
                Ejecutar(Ejecucion.Substring(1, (CP - 1)));
                //CompiladorRichTextBox.Text += Ejecucion.Substring(1, (CP - 1));
            }
            else
            {
                MessageBox.Show("El formato no es Correcto!! \n Verifique el codigo.");
            }

        }
    }
}
