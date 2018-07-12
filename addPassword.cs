using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VS_Proj
{
    public partial class addPassword : Form
    {
        public addPassword()
        {
            InitializeComponent();
            button1.Enabled = false;
            stateIndicatorComponent1.StateIndex = 1;
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            checkPW();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            checkPW();
        }

        public void checkPW()
        {
            if(textBox1.Text == textBox2.Text)
            {
                button1.Enabled = true;
                stateIndicatorComponent1.StateIndex = 3;
            }
            else
            {
                button1.Enabled = false;
                stateIndicatorComponent1.StateIndex = 1;
            }
        }

        private void checkBox1_CheckedChanged(object sender, EventArgs e)
        {
            if(checkBox1.CheckState.ToString() == "Checked")
            {
                textBox1.PasswordChar = '\0';
                textBox2.PasswordChar = '\0';
            }
            else
            {
                textBox1.PasswordChar = 'X';
                textBox2.PasswordChar = 'X';
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            globalVars.tmpPW = Verschlüsselung.Base64Encode(textBox2.Text);
            this.Close();
        }
    }
}
