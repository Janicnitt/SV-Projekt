using System;
using System.Collections.Generic;
using System.Data.OleDb;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace VS_Proj
{
    class globalVars
    {
        public static List<string> permissions = new List<string>();
        public static OleDbConnection dbConnection = new OleDbConnection(@"Provider=Microsoft.Jet.OLEDB.4.0;Data Source=" + System.IO.Directory.GetCurrentDirectory() + "\\svDB.mdb;Persist Security Info=False");
        public static List<string> classes = new List<string>();
        public static List<List<string>> permDef = new List<List<string>>();
        public static List<List<string>> memberList = new List<List<string>>();
        public static string tmpPW = "";
    }
}
