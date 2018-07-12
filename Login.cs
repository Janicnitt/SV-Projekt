using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Data.OleDb;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VS_Proj
{
    public partial class Login : Form
    {
        private bool violation = true;
        public Login()
        {
            InitializeComponent();
            globalVars.permissions.Clear();
            globalVars.classes.Add("Schüler");
            globalVars.classes.Add("Schütze");

            button1.Enabled = false;

            string cmd = "SELECT Benutzer.Benutzername FROM Benutzer;";

            List<List<string>> lst = DBConnection.getUser(cmd);

            foreach (List<string> tmp in lst)
            {
                comboBox1.Items.Add(tmp[0]);
            }

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.Text == "admin" && textBox2.Text == "hjkl")
            {
                globalVars.permissions.Add("*");
                violation = false;
                this.Close();
            }
            else
            {

                string cmd = String.Format("SELECT Benutzer.ID, Benutzer.Benutzername, Benutzer.Passwort, Benutzer.Berechtigungen FROM Benutzer WHERE(((Benutzer.Benutzername) = '{0}'));", comboBox1.Text);

                List<List<string>> lst = DBConnection.getUser(cmd);

                string pw = Verschlüsselung.Base64Decode(lst[0][2]);

                try
                {
                    if (pw == textBox2.Text)
                    {
                        listPermissions(lst);
                    }
                    else
                    {
                        MessageBox.Show("Passwort falsch!");
                        textBox2.Text = "";
                    }
                }
                catch (Exception ex)
                {
                    MessageBox.Show("Benutzer existiert nicht!");
                    comboBox1.Text = "";
                    textBox2.Text = "";
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void listPermissions(List<List<string>> lst)
        {
            string[] tmp = lst[0][3].Split(';');
            foreach(string perm in tmp)
            {
                globalVars.permissions.Add(perm);
            }
            violation = false;
            this.Close();
        }

        private void Login_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (violation)
            {
                Application.Exit();
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkFillStatus();
        }

        private void textBox2_TextChanged(object sender, EventArgs e)
        {
            checkFillStatus();
        }

        public void checkFillStatus()
        {
            if (textBox2.Text == "" || comboBox1.Text == "")
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }
    }
}
