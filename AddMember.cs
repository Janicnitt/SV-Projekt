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
    public partial class AddMember : Form
    {
        public AddMember()
        {
            InitializeComponent();

            button1.Enabled = false;

            foreach (string item in globalVars.classes)
            {
                comboBox1.Items.Add(item);
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            List<string> lst = new List<string>();

            lst.Add(textBox1.Text);
            lst.Add(textBox2.Text);
            lst.Add(textBox3.Text);
            lst.Add(textBox4.Text);
            lst.Add(textBox5.Text);

            string cB1;

            if(checkBox1.CheckState.ToString() == "Checked")
            {
                cB1 = "True";
            }
            else
            {
                cB1 = "False";
            }

            lst.Add(cB1);
            lst.Add(textBox6.Text);
            lst.Add(comboBox1.Text);

            string cB2;

            if (checkBox2.CheckState.ToString() == "Checked")
            {
                cB2 = "True";
            }
            else
            {
                cB2 = "False";
            }

            lst.Add(cB2);

            string cB3;

            if (checkBox3.CheckState.ToString() == "Checked")
            {
                cB3 = "True";
            }
            else
            {
                cB3 = "False";
            }

            lst.Add(cB3);
            
            lst.Add(dateTimePicker1.Value.Date.ToString());
            
            DBConnection.insertMember(lst);
        }

        public void checkFillStatus()
        {
            if (textBox1.Text == "" || textBox2.Text == "" || textBox3.Text == "" || textBox4.Text == "" || textBox5.Text == "" || textBox6.Text == "" || comboBox1.Text == "")
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }

        private void textBox1_TextChanged(object sender, EventArgs e)
        {
            checkFillStatus();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            checkFillStatus();
        }

        private void textBox3_TextChanged(object sender, EventArgs e)
        {
            checkFillStatus();
        }

        private void textBox4_TextChanged(object sender, EventArgs e)
        {
            checkFillStatus();
        }

        private void textBox5_TextChanged(object sender, EventArgs e)
        {
            checkFillStatus();
        }

        private void textBox6_TextChanged(object sender, EventArgs e)
        {
            checkFillStatus();
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkFillStatus();
        }
    }
}
