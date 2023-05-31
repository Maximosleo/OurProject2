using System.Data.OleDb;
using System.Data;
using System;
using System.Data;
using System.Configuration;
using System.Linq;
using System.Web;
//using System.Web.Security;
//using System.Web.UI;
//using System.Web.UI.HtmlControls;
//using System.Web.UI.WebControls;
//using System.Web.UI.WebControls.WebParts;
using System.Xml.Linq;
using System.Data.OleDb;

namespace OurProject2.Pages
{

    public class MyAdoHelper
    {
        private readonly IWebHostEnvironment _env;

        public MyAdoHelper(IWebHostEnvironment env)
        {
            _env = env;
        }

        public static OleDbConnection ConnectToDb(string fileName)
        {
            // Get the base directory
            string baseDirectory = AppDomain.CurrentDomain.BaseDirectory;

            // Get the App_Data directory
            string appDataDirectory = Path.Combine(baseDirectory, "App_Data");

            // Combine to get the full path to the database file
            string path = Path.Combine(appDataDirectory, fileName);

            string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data source=" + path;
            OleDbConnection conn = new OleDbConnection(connString);
            return conn;
        }

        /// <summary>
        /// To Execute update / insert / delete queries
        ///  הפעולה מקבלת שם קובץ ומשפט לביצוע ומבצעת את הפעולה על המסד
        /// </summary>

        public void DoQuery(string fileName, string sql)//הפעולה מקבלת שם מסד נתונים ומחרוזת מחיקה/ הוספה/ עדכון
                                                               //ומבצעת את הפקודה על המסד הפיזי
        {

            OleDbConnection conn = ConnectToDb(fileName);
            conn.Open();
            OleDbCommand com = new OleDbCommand(sql, conn);
            com.ExecuteNonQuery();
            com.Dispose();
            conn.Close();

        }


        /// <summary>
        /// To Execute update / insert / delete queries
        ///  הפעולה מקבלת שם קובץ ומשפט לביצוע ומחזירה את מספר השורות שהושפעו מביצוע הפעולה
        /// </summary>
        public int RowsAffected(string fileName, string sql)//הפעולה מקבלת מסלול מסד נתונים ופקודת עדכון
                                                            //ומבצעת את הפקודה על המסד הפיזי
        {

            OleDbConnection conn = ConnectToDb(fileName);
            conn.Open();
            OleDbCommand com = new OleDbCommand(sql, conn);
            int rowsA = com.ExecuteNonQuery();
            conn.Close();
            return rowsA;
        }

        /// <summary>
        /// הפעולה מקבלת שם קובץ ומשפט לחיפוש ערך - מחזירה אמת אם הערך נמצא ושקר אחרת
        /// </summary>
        public bool IsExist(string fileName, string sql)//הפעולה מקבלת שם קובץ ומשפט בחירת נתון ומחזירה אמת אם הנתונים קיימים ושקר אחרת
        {

            OleDbConnection conn = ConnectToDb(fileName);
            conn.Open();
            OleDbCommand com = new OleDbCommand(sql, conn);
            OleDbDataReader data = com.ExecuteReader();
            bool found;
            found = (bool)data.Read();// אם יש נתונים לקריאה יושם אמת אחרת שקר - הערך קיים במסד הנתונים
            conn.Close();
            return found;

        }
        //רועי

        public DataTable ExecuteDataTable(string fileName, string sql)
        {
            OleDbConnection conn = ConnectToDb(fileName);
            conn.Open();
            OleDbDataAdapter tableAdapter = new OleDbDataAdapter(sql, conn);
            DataTable dt = new DataTable();
            tableAdapter.Fill(dt);
            return dt;
        }


        public void ExecuteNonQuery(string fileName, string sql)
        {
            OleDbConnection conn = ConnectToDb(fileName);
            conn.Open();
            OleDbCommand command = new OleDbCommand(sql, conn);
            command.ExecuteNonQuery();
            conn.Close();
        }

        public string printDataTable(string fileName, string sql)//הפעולה מקבלת שם קובץ ומשפט בחירת נתון ומחזירה אמת אם הנתונים קיימים ושקר אחרת
        {


            DataTable dt = ExecuteDataTable(fileName, sql);

            string printStr = "<table border='1'>";

            foreach (DataRow row in dt.Rows)
            {
                printStr += "<tr>";
                foreach (object myItemArray in row.ItemArray)
                {

                    printStr += "<td>" + myItemArray.ToString() + "</td>";
                }
                printStr += "</tr>";
            }
            printStr += "</table>";

            return printStr;
        }

    }
}