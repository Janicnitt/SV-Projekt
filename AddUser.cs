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
    public partial class AddUser : Form
    {
        public AddUser()
        {
            InitializeComponent();

            button1.Enabled = false;

            foreach (List<string> items in globalVars.permDef)
            {
                checkedListBoxControl1.Items.Add(items[0]);
            }

            string cmd = "SELECT Mitglieder.Vorname, Mitglieder.Nachname FROM Mitglieder;";

            globalVars.memberList = DBConnection.getUser(cmd);

            foreach (List<string> tmp in globalVars.memberList)
            {
                try
                {
                    comboBox1.Items.Add(tmp[0] + " " + tmp[1]);
                }
                catch (Exception ex)
                {
                    MessageBox.Show(ex.Message);
                }
            }
        }

        public void checkFilledStatus()
        {
            if(comboBox1.Text == "" || (checkedListBoxControl1.CheckedItemsCount == 0))
            {
                button1.Enabled = false;
            }
            else
            {
                button1.Enabled = true;
            }
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!(checkedListBoxControl1.CheckedItemsCount == 0))
            {
                string userData = "";

                foreach (List<string> lstR in globalVars.memberList)
                {
                    if (comboBox1.Text.Contains(lstR[0]))
                    {
                        userData = lstR[0] + ":" + lstR[1];
                    }
                }

                List<object> tmp = checkedListBoxControl1.Items.GetCheckedValues();
                string tmpV = "";

                foreach (string item in tmp)
                {
                    foreach (List<string> lstV in globalVars.permDef)
                    {
                        if (lstV.Contains(item))
                        {
                            if (tmpV.Length == 0)
                            {
                                tmpV += ";" + lstV[1];
                            }
                            else
                            {
                                tmpV += "," + lstV[1];
                            }
                        }
                    }
                }

                userData += tmpV;

                this.Visible = false;
                this.Enabled = false;

                addPassword frm = new addPassword();
                frm.ShowDialog();

                if (globalVars.tmpPW == "")
                {
                    MessageBox.Show("Bitte Passwort eingeben!");
                }
                else
                {

                    userData += ";" + globalVars.tmpPW;

                    globalVars.tmpPW = "";

                    List<string> lst = new List<string>();

                    string[] split = userData.Split(';');

                    foreach (string str in split)
                    {
                        lst.Add(str);
                    }

                    DBConnection.insertUser(lst);
                }
                this.Close();
            }
            else
            {
                MessageBox.Show("Bitte mindestens eine Berechtigung auswählen!");
            }
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            checkFilledStatus();
        }

        private void checkedListBoxControl1_ItemCheck(object sender, DevExpress.XtraEditors.Controls.ItemCheckEventArgs e)
        {
            checkFilledStatus();
        }
    }
}
