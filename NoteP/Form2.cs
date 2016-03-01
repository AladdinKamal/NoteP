using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace NoteP
{
    public partial class Form2 : Form
    {
        public static int lineNumber;
        public Form2()
        {
            InitializeComponent();
            Form1 x1 = new Form1();
        }

        private void button2_Click(object sender, EventArgs e)
        {
            this.Hide();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            Form1 x = new Form1();
            int i = 0;
            foreach (String s in x.getText().Trim().Split('\n'))
            {
                i++;
            }
            if (textBox1.Text.Trim() != "")
            {
                if (Convert.ToInt32(textBox1.Text) > i)
                    MessageBox.Show("Please choose a line from 1 to " + i + ".", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                else if (Convert.ToInt32(textBox1.Text) == 0)
                {
                    MessageBox.Show("Please choose a line from 1 to " + i + ".", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
                }
                else
                {
                    lineNumber = Convert.ToInt32(textBox1.Text);
                    this.Hide();
                    x.goToLine();
                }
            }
            else
            {
                MessageBox.Show("Please choose a line from 1 to " + i + ".", "Error!", MessageBoxButtons.OK, MessageBoxIcon.Error);
            }
        }

        private void textBox1_KeyPress(object sender, KeyPressEventArgs e)
        {
            if (!char.IsControl(e.KeyChar) && !char.IsDigit(e.KeyChar))
            {
                e.Handled = true;
            }
        }

        public int getlineNumber()
        {
            return lineNumber;
        }
    }
}
