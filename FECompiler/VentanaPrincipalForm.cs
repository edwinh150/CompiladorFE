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
        }

    }
}
