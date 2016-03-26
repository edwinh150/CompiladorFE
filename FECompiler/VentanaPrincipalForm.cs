using System;
using System.Windows.Forms;
using System.IO;
using System.Threading;
using System.Collections.Generic;
using System.Diagnostics;

namespace FECompiler
{
    public partial class MainForm : Form
    {
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        public static extern bool AllocConsole();
        [System.Runtime.InteropServices.DllImport("Kernel32")]
        public static extern bool FreeConsole();

        public MainForm()
        {
            InitializeComponent();
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
            Sintactico sint = new Sintactico();
            if (sint.EsValido(this.CompiladorRichTextBox.Text))
            {
                MessageBox.Show("Analisis terminado");
            }
            else
            {
                MessageBox.Show("Error :(");
            }
            //Process programa = new Process();
            //ProcessStartInfo info = new ProcessStartInfo("cmd", "/c " + "ipconfig");
            //info.WindowStyle = ProcessWindowStyle.Minimized; //Iniciamos la aplicación minimizada
            //info.Arguments = "ipconfig";
            //programa = Process.Start(info);
            //Lanzamos nuestra aplicación utilizando nuestro objeto de tipo ProcessStartInfo
            // Compilar();
            //Indicamos que deseamos inicializar el proceso cmd.exe junto a un comando de arranque. 
            //(/C, le indicamos al proceso cmd que deseamos que cuando termine la tarea asignada se cierre el proceso).
            //Para mas informacion consulte la ayuda de la consola con cmd.exe /? 
            //System.Diagnostics.ProcessStartInfo procStartInfo = new System.Diagnostics.ProcessStartInfo("cmd", "/c " + "ipconfig");
            // Indicamos que la salida del proceso se redireccione en un Stream
            //procStartInfo.RedirectStandardOutput = true;
            //procStartInfo.UseShellExecute = false;
            ////Indica que el proceso no despliegue una pantalla negra (El proceso se ejecuta en background)
            ////procStartInfo.CreateNoWindow = false;
            ////Inicializa el proceso
            //System.Diagnostics.Process proc = new System.Diagnostics.Process();
            //proc.StartInfo = procStartInfo;
            //proc.Start();
            ////Consigue la salida de la Consola(Stream) y devuelve una cadena de texto
            //string result = proc.StandardOutput.ReadToEnd();
            ////Muestra en pantalla la salida del Comando
            //Console.WriteLine(result);
        }
    }
}
