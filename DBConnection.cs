using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace VS_Proj
{
    class DBConnection
    {
        public static List<List<string>> getUser(string cmd)
        {
            List<List<string>> lst = new List<List<string>>();
            
            try
            {
                OleDbCommand dbCommand = new OleDbCommand(cmd, globalVars.dbConnection);
                globalVars.dbConnection.Open();
                OleDbDataReader dr = dbCommand.ExecuteReader();

                while (dr.Read())
                {
                    List<string> tmp = new List<string>();

                    for (int i = 0; i < dr.FieldCount; i++)
                    {
                        tmp.Add(dr[i].ToString());
                    }

                    lst.Add(tmp);
                }
                dr.Close();
                return lst;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                List<List<string>> lstV = new List<List<string>>();
                return lstV;
            }
            finally
            {
                globalVars.dbConnection.Close();
            }
        }

        public static void insertMember(List<string> lst)
        {
            string cmd = "INSERT INTO Mitglieder (Vorname, Nachname, Adresse, PLZ, Stadt, AP, EMAIL, Klasse, WBK, MES, Beitritt) VALUES (@Vorname, @Nachname, @Adresse, @PLZ, @Stadt, @AP, @EMAIL, @Klasse, @WBK, @MES, @Beitritt);";

            try
            {
                OleDbCommand dbCommand = new OleDbCommand(cmd, globalVars.dbConnection);

                dbCommand.Parameters.Add("@Vorname", OleDbType.VarChar).Value = lst[0];
                dbCommand.Parameters.Add("@Nachname", OleDbType.VarChar).Value = lst[1];
                dbCommand.Parameters.Add("@Adresse", OleDbType.VarChar).Value = lst[2];
                dbCommand.Parameters.Add("@PLZ", OleDbType.VarChar).Value = lst[3];
                dbCommand.Parameters.Add("@Stadt", OleDbType.VarChar).Value = lst[4];
                dbCommand.Parameters.Add("@AP", OleDbType.VarChar).Value = lst[5];
                dbCommand.Parameters.Add("@EMAIL", OleDbType.VarChar).Value = lst[6];
                dbCommand.Parameters.Add("@Klasse", OleDbType.VarChar).Value = lst[7];
                dbCommand.Parameters.Add("@WBK", OleDbType.VarChar).Value = lst[8];
                dbCommand.Parameters.Add("@MES", OleDbType.VarChar).Value = lst[9];
                dbCommand.Parameters.Add("@Beitritt", OleDbType.VarChar).Value = lst[10];

                globalVars.dbConnection.Open();
                dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mitglied konnte nicht hinzugefügt werden!");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                globalVars.dbConnection.Close();
            }
        }

        public static void insertUser(List<string> lst)
        {
            string cmd = "INSERT INTO Benutzer (ID, Benutzername, Passwort, Berechtigungen) VALUES (@ID, @Benutzername, @Passwort, @Berechtigungen);";

            string id = getMemberID(lst[0]);

            string userName = lst[0].Replace(':', ' ');

            string pw = lst[2];

            string permissions = lst[1];

            try
            {
                OleDbCommand dbCommand = new OleDbCommand(cmd, globalVars.dbConnection);

                dbCommand.Parameters.Add("@ID", OleDbType.VarChar).Value = id;
                dbCommand.Parameters.Add("@Benutzername", OleDbType.VarChar).Value = userName;
                dbCommand.Parameters.Add("@Passwort", OleDbType.VarChar).Value = pw;
                dbCommand.Parameters.Add("@Berechtigungen", OleDbType.VarChar).Value = permissions;

                globalVars.dbConnection.Open();
                dbCommand.ExecuteNonQuery();
            }
            catch (Exception ex)
            {
                MessageBox.Show("Mitglied konnte nicht hinzugefügt werden!");
                MessageBox.Show(ex.Message);
            }
            finally
            {
                globalVars.dbConnection.Close();
            }
        }

        public static string getMemberID(string memberName)
        {

            string cmd = "SELECT Mitglieder.ID FROM Mitglieder WHERE (((Mitglieder.Vorname)=@Vorname) AND ((Mitglieder.Nachname)=@Nachname));";

            string[] tmp = memberName.Split(':');

            try
            {
                OleDbCommand dbCommand = new OleDbCommand(cmd, globalVars.dbConnection);

                dbCommand.Parameters.Add("@Vorname", OleDbType.VarChar).Value = tmp[0];
                dbCommand.Parameters.Add("@Nachname", OleDbType.VarChar).Value = tmp[1];

                globalVars.dbConnection.Open();
                OleDbDataReader dr = dbCommand.ExecuteReader();

                string tmpV = "";
                
                while (dr.Read())
                {
                    tmpV = dr[0].ToString();
                }
                dr.Close();
                return tmpV;
            }
            catch (Exception ex)
            {
                MessageBox.Show(ex.Message);
                return "Fehler!";
            }
            finally
            {
                globalVars.dbConnection.Close();
            }
        }
    }
}
