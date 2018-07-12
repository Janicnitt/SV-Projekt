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
    public partial class Form1 : Form
    {
        public Form1()
        {
            Console.WriteLine(System.IO.Directory.GetCurrentDirectory());

            Login frm = new Login();
            frm.ShowDialog();

            string[] tmpR = "Kasse (Käufe einlesen),P_Kasse_add;Kasse (Log lesen),P_Kasse_log;Mitglieder Liste (Mitglieder anzeigen),P_MListe_show;Mitglieder Liste (Mitglied hinzufügen),P_MListe_add;Admin (Vollzugriff),*".Split(';');

            foreach (string item in tmpR)
            {
                string[] tmpF = item.Split(',');

                List<string> lst = new List<string>();
                
                foreach (string itemF in tmpF)
                {
                    lst.Add(itemF);
                }

                globalVars.permDef.Add(lst);
            }

            InitializeComponent();

            initGrid();
        }

        public void initGrid()
        {
            DataTable dt = new DataTable();

            dt.Columns.Add("Vorname", Type.GetType("System.String"));
            dt.Columns.Add("Nachname", Type.GetType("System.String"));
            dt.Columns.Add("Adresse", Type.GetType("System.String"));
            dt.Columns.Add("PLZ", Type.GetType("System.String"));
            dt.Columns.Add("Stadt", Type.GetType("System.String"));
            dt.Columns.Add("Aktiv / Passiv", Type.GetType("System.String"));
            dt.Columns.Add("E-Mail", Type.GetType("System.String"));
            dt.Columns.Add("Klasse", Type.GetType("System.String"));
            dt.Columns.Add("WBK", Type.GetType("System.String"));
            dt.Columns.Add("MES", Type.GetType("System.String"));
            dt.Columns.Add("Beitritt", Type.GetType("System.String"));

            gridControl1.DataSource = dt;


            DataTable dtF = new DataTable();

            dtF.Columns.Add("Benutzername", Type.GetType("System.String"));
            dtF.Columns.Add("Rechte", Type.GetType("System.String"));

            gridControl2.DataSource = dtF;
        }

        private void ribbonControl1_SelectedPageChanged(object sender, EventArgs e)
        {
            if (ribbonControl1.SelectedPage.ToString() == "Mitglieder")
            {
                TabPage t = tabControl1.TabPages[1];
                tabControl1.SelectedTab = t;
                loadMember();
            }
            else if(ribbonControl1.SelectedPage.ToString() == "Admin")
            {
                TabPage t = tabControl1.TabPages[2];
                tabControl1.SelectedTab = t;
                loadUser();
            }
            else
            {
                TabPage t = tabControl1.TabPages[0];
                tabControl1.SelectedTab = t;
            }
        }

        public void loadMember()
        {
            for (int i = 0; i < gridView1.RowCount;)
            {
                gridView1.DeleteRow(i);
            }

            string cmd = "SELECT [Vorname], [Nachname], [Adresse], [PLZ], [Stadt], [AP], [EMAIL], [Klasse], [WBK], [MES], [Beitritt] FROM Mitglieder;";

            List<List<string>> lst = DBConnection.getUser(cmd);

            try
            {
                foreach (List<string> tmp in lst)
                {
                    gridView1.AddNewRow();
                    gridView1.SetRowCellValue(gridView1.GetRowHandle(gridView1.DataRowCount), gridView1.Columns[0], tmp[0]);
                    gridView1.SetRowCellValue(gridView1.GetRowHandle(gridView1.DataRowCount), gridView1.Columns[1], tmp[1]);
                    gridView1.SetRowCellValue(gridView1.GetRowHandle(gridView1.DataRowCount), gridView1.Columns[2], tmp[2]);
                    gridView1.SetRowCellValue(gridView1.GetRowHandle(gridView1.DataRowCount), gridView1.Columns[3], tmp[3]);
                    gridView1.SetRowCellValue(gridView1.GetRowHandle(gridView1.DataRowCount), gridView1.Columns[4], tmp[4]);

                    string AP;

                    if (tmp[5] == "True")
                    {
                        AP = "Aktiv";
                    }
                    else
                    {
                        AP = "Passiv";
                    }

                    gridView1.SetRowCellValue(gridView1.GetRowHandle(gridView1.DataRowCount), gridView1.Columns[5], AP);
                    gridView1.SetRowCellValue(gridView1.GetRowHandle(gridView1.DataRowCount), gridView1.Columns[6], tmp[6]);
                    gridView1.SetRowCellValue(gridView1.GetRowHandle(gridView1.DataRowCount), gridView1.Columns[7], tmp[7]);

                    string WBK;

                    if(tmp[8] == "True")
                    {
                        WBK = "Vorhanden";
                    }
                    else
                    {
                        WBK = "Nicht Vorhanden";
                    }

                    gridView1.SetRowCellValue(gridView1.GetRowHandle(gridView1.DataRowCount), gridView1.Columns[8], WBK);

                    string MES;

                    if(tmp[9] == "True")
                    {
                        MES = "Vorhanden";
                    }
                    else
                    {
                        MES = "Nicht Vorhanden";
                    }
                    
                    gridView1.SetRowCellValue(gridView1.GetRowHandle(gridView1.DataRowCount), gridView1.Columns[9], MES);
                    gridView1.SetRowCellValue(gridView1.GetRowHandle(gridView1.DataRowCount), gridView1.Columns[10], tmp[10]);
                    gridView1.UpdateCurrentRow();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        public void loadUser()
        {
            for (int i = 0; i < gridView2.RowCount;)
            {
                gridView2.DeleteRow(i);
            }

            string cmd = "SELECT Benutzername, Berechtigungen FROM Benutzer;";

            List<List<string>> lst = DBConnection.getUser(cmd);

            try
            {
                foreach (List<string> tmp in lst)
                {
                    gridView2.AddNewRow();
                    gridView2.SetRowCellValue(gridView2.GetRowHandle(gridView2.DataRowCount), gridView2.Columns[0], tmp[0]);
                    gridView2.SetRowCellValue(gridView2.GetRowHandle(gridView2.DataRowCount), gridView2.Columns[1], tmp[1]);
                    gridView2.UpdateCurrentRow();
                }
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
            }
        }

        private void barButtonItem1_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddMember frm = new AddMember();
            frm.ShowDialog();
            loadMember();
        }

        private void barButtonItem2_ItemClick(object sender, DevExpress.XtraBars.ItemClickEventArgs e)
        {
            AddUser frm = new AddUser();
            frm.ShowDialog();
            loadUser();
        }
    }
}
