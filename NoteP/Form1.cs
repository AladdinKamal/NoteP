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

namespace NoteP
{

    public partial class Form1 : Form
    {
        public static String text;
        String path = "";
        public static Form2 x = new Form2();
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
                    saveToolStripMenuItem.Enabled = true;
                }
                catch (IOException error)
                {
                    MessageBox.Show("Error opening file : " + error.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void newToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Clear();
            path = "";
            saveToolStripMenuItem.Enabled = false;
        }

        private void saveAsToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult saveResult = saveFileDialog1.ShowDialog();

            if (saveResult == DialogResult.OK)
            {
                path = saveFileDialog1.FileName;
                try
                {
                    StreamWriter file = new StreamWriter(path);
                    file.Write(textBox1.Text);
                    file.Close();
                    saveToolStripMenuItem.Enabled = true;
                }
                catch (IOException error)
                {
                    MessageBox.Show("Error saving file : " + error.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void saveToolStripMenuItem_Click(object sender, EventArgs e)
        {
            try
            {
                StreamWriter file = new StreamWriter(path);
                file.Write(textBox1.Text);
                file.Close();
                saveToolStripMenuItem.Enabled = true;
            }
            catch (IOException error)
            {
                MessageBox.Show("Error saving file : " + error.Message, "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void cutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.SelectedText);
            SendKeys.Send("{DELETE}");
        }

        private void copyToolStripMenuItem_Click(object sender, EventArgs e)
        {
            Clipboard.SetText(textBox1.SelectedText);
        }

        private void pasteToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.Paste(Clipboard.GetText());
            if (Clipboard.GetText() != "")
                undoCtrlZToolStripMenuItem.Enabled = true;
        }

        private void textWrapToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (textWrapToolStripMenuItem.Checked == true)
            {
                textBox1.WordWrap = true;
            }
            else
            {
                textBox1.WordWrap = false;
            }
        }

        private void fontToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult fontResult = fontDialog1.ShowDialog();

            if (fontResult == DialogResult.OK)
            {
                textBox1.Font = fontDialog1.Font;
            }
        }

        private void colorToolStripMenuItem_Click(object sender, EventArgs e)
        {
            DialogResult colorResult = colorDialog1.ShowDialog();
            if (colorResult == DialogResult.OK)
            {
                textBox1.ForeColor = colorDialog1.Color;
            }
        }

        private void aboutToolStripMenuItem_Click(object sender, EventArgs e)
        {
            MessageBox.Show("NoteP V1.0", "About", MessageBoxButtons.OK, MessageBoxIcon.Information);
        }

        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (path == "")
            {
                if (textBox1.Text != "")
                {
                    DialogResult result = MessageBox.Show("Do you want to save before exit ?", "Exit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                    if (result == DialogResult.Yes)
                    {
                        saveAsToolStripMenuItem_Click(0, e);
                        Application.Exit();
                    }
                    else if (result == DialogResult.No)
                    {

                    }
                    else
                    {
                        e.Cancel = true;
                    }
                }
                else
                {
                    Application.Exit();
                }
            }
            else
            {
                try
                {
                    StreamReader file = new StreamReader(path);
                    String lines = file.ReadToEnd();
                    file.Close();
                    if (textBox1.Text == lines)
                    {
                        Application.Exit();
                    }
                    else
                    {
                        DialogResult result = MessageBox.Show("Do you want to save before exit ?", "Exit", MessageBoxButtons.YesNoCancel, MessageBoxIcon.Question);
                        if (result == DialogResult.Yes)
                        {
                            saveToolStripMenuItem_Click(0, e);

                            Application.Exit();
                        }
                        else if (result == DialogResult.No)
                        {

                        }
                        else
                        {
                            e.Cancel = true;
                        }
                    }
                }
                catch (IOException error)
                {
                    MessageBox.Show("Error checking file : ", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
            }
        }

        private void textBox1_KeyDown(object sender, KeyEventArgs e)
        {
            undoCtrlZToolStripMenuItem.Enabled = true;


            if (e.KeyValue.Equals('A') && e.Control)
            {
                if (textBox1.Text != "")
                    selectAllToolStripMenuItem_Click(0, e);
            }
            else if (e.KeyValue.Equals('N') && e.Control)
            {
                newToolStripMenuItem_Click(0, e);
            }
            else if (e.KeyValue.Equals('O') && e.Control)
            {
                openToolStripMenuItem_Click(0, e);
            }
            else if (e.KeyValue.Equals('S') && e.Control && path != "")
            {
                saveToolStripMenuItem_Click(0, e);
            }
            else if (e.KeyValue.Equals('S') && e.Control && e.Shift)
            {
                saveAsToolStripMenuItem_Click(0, e);
            }
            else if (e.KeyCode == Keys.F5)
            {
                timeDateToolStripMenuItem_Click(0, e);
            }
            else if (e.KeyCode == Keys.F3)
            {
                if (textBox1.Text != "")
                    findAToolStripMenuItem_Click(0, e);
            }
            else if (e.KeyValue.Equals('P') && e.Control)
            {
                printToolStripMenuItem_Click(0, e);
            }
            else if (e.KeyValue.Equals('F') && e.Control)
            {
                if (textBox1.Text != "")
                    findToolStripMenuItem_Click(0, e);
            }
            /*
            else if (e.KeyValue.Equals('G') && e.Control)
            {
                goToToolStripMenuItem_Click(0, e);
            }
            */
            else if (e.KeyValue.Equals('H') && e.Control)
            {
                replaceToolStripMenuItem_Click(0, e);
            }
        }
        private void timer3_Tick(object sender, EventArgs e)
        {
            if (textBox1.Text == "")
            {
                findToolStripMenuItem.Enabled = false;
                findAToolStripMenuItem.Enabled = false;
                selectAllToolStripMenuItem.Enabled = false;
            }
            else
            {
                findToolStripMenuItem.Enabled = true;
                findAToolStripMenuItem.Enabled = true;
                selectAllToolStripMenuItem.Enabled = true;
            }
            if (textBox1.SelectedText != "")
            {
                cutToolStripMenuItem.Enabled = true;
                copyToolStripMenuItem.Enabled = true;
                deleteDelToolStripMenuItem.Enabled = true;
            }
            else
            {
                cutToolStripMenuItem.Enabled = false;
                copyToolStripMenuItem.Enabled = false;
                deleteDelToolStripMenuItem.Enabled = false;
            }
        }

        private void helpToolStripMenuItem1_Click(object sender, EventArgs e)
        {
            System.Diagnostics.Process.Start("http://github.com/AladdinKamal/");
        }

        private void deleteDelToolStripMenuItem_Click(object sender, EventArgs e)
        {
            SendKeys.Send("{DELETE}");
        }

        private void timeDateToolStripMenuItem_Click(object sender, EventArgs e)
        {
            undoCtrlZToolStripMenuItem.Enabled = true;
            textBox1.Paste(DateTime.Now.ToString() + " ");
        }

        private void selectAllToolStripMenuItem_Click(object sender, EventArgs e)
        {
            textBox1.SelectAll();
        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            if (sToolStripMenuItem.Checked == true)
            {
                int line = 1 + textBox1.GetLineFromCharIndex(textBox1.GetFirstCharIndexOfCurrentLine());
                int column = 1 + textBox1.SelectionStart - textBox1.GetFirstCharIndexOfCurrentLine();
                label1.Text = "line: " + line.ToString() + ", column: " + column.ToString();
                int word = 0;
                if (textBox1.Text.Trim() != "")
                {
                    foreach (String s in textBox1.Text.Split(' '))
                    {
                        if (s.Trim() != "")
                            word++;
                    }
                }
                int character = 0;
                foreach (char c in textBox1.Text)
                {
                    character++;
                }
                label3.Text = "Words: " + word.ToString() + ", Characters: " + character.ToString();
            }
        }

        private void timer2_Tick(object sender, EventArgs e)
        {
            if (dateTimeToolStripMenuItem.Checked == true)
                label2.Text = DateTime.Now.ToString();
        }

        private void goToToolStripMenuItem_Click(object sender, EventArgs e)
        {
            text = textBox1.Text;
            x.Show();

        }

        public String getText()
        {
            return text;
        }

        public void goToLine()
        {
            int lineNumber = x.getlineNumber();
            textBox1.SelectAll();
            textBox1.SelectionStart = lineNumber;
            textBox1.SelectionLength = 0;
            x.Hide();
        }

        private void sToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (sToolStripMenuItem.Checked == true)
            {
                label1.Show();
                label3.Show();
            }
            else
            {
                label1.Hide();
                label3.Hide();
            }
        }

        private void dateTimeToolStripMenuItem_Click(object sender, EventArgs e)
        {
            if (dateTimeToolStripMenuItem.Checked == true)
                label2.Show();
            else
                label2.Hide();
        }

        private void findAToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void printToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void findToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void replaceToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void pageSetupToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }

        private void undoCtrlZToolStripMenuItem_Click(object sender, EventArgs e)
        {

        }
    }
}
