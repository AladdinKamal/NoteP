using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using System.IO;

namespace Text_Editor
{
    public partial class Form1 : Form
    {
        String path = "";
        public Form1()
        {
            InitializeComponent();
        }

        private void exitToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Application.Exit();
        }

        private void openToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult openResult = openFileDialog1.ShowDialog();

            if (openResult == DialogResult.OK)
            {
                path = openFileDialog1.FileName;

                try
                {
                    StreamReader file = new StreamReader(path);
                    String lines = file.ReadToEnd();
                    file.Close();
                    textBox1.Text = lines;
                }
                catch (IOException error)
                {
                    MessageBox.Show("Error opening file : " + error.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }
    }
}
