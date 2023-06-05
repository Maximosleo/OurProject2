using Microsoft.Extensions.Caching.Memory;
using static System.Formats.Asn1.AsnWriter;

using System.Data.SQLite;


namespace OurProject2.Pages
{
    public class MyDAO
    {
        private static MyDAO instance = null;
        private readonly IMemoryCache _cache11;

        // Make constructor private so it can't be called from outside this class
        private MyDAO(IMemoryCache _cache)
        {
            _cache11 = _cache;

        }

        // Provide a global point of access to the instance
        public static MyDAO GetInstance(IMemoryCache _cache)
        {
            if (instance == null)
            {
                instance = new MyDAO(_cache);
            }
            return instance;
        }

        //save the data
        public void SaveToDB(string firstname, string lastname, string email, string password, string gender, string age, string identification, bool isAdmin, int score)
        {
            var tableJson = _cache11.GetOrCreate("DB", entry => "");
            GlobalDataTable globalDataTable = GlobalDataTable.DeserializeFromJson(tableJson);

            DataTableData dataTableData = globalDataTable.DataTableData;
            dataTableData.Rows.Add(new RowData { ID = identification, Name = firstname, lastname = lastname, email = email, password = password, gender = gender, age = age, isAdmin = isAdmin , score = score});

            var tJson = globalDataTable.SerializeToJson();
            _cache11.Set("DB", tJson);
        }
        //return/give data of a table,for acheck
        public DataTableData GetTable()
        {
            var tableJson = _cache11.GetOrCreate("DB", entry => "");
            GlobalDataTable globalDataTable = GlobalDataTable.DeserializeFromJson(tableJson);

            DataTableData dataTableData = globalDataTable.DataTableData;

            return dataTableData;
        }

        public bool CheckUser(string email11, string password)
        {
            DataTableData dataTableData = GetTable();

            for (var i = 0; i < dataTableData.Rows.Count; i++)
            {
                var rowData = dataTableData.Rows[i];
                if (rowData.email.Equals(email11) && rowData.password.Equals(password))
                {
                    return true;
                }
            }

            return false;
        }


        public  bool CheckAdmin(string email)
        {
            DataTableData dataTableData = GetTable();

            for (var i = 0; i < dataTableData.Rows.Count; i++)
            {
                var rowData = dataTableData.Rows[i];
                if (rowData.email.Equals(email))
                {
                    if (rowData.isAdmin == true)
                    {
                        return true;
                    }
                    else
                    {
                        return false;
                    }

                }
            }
            return false;
        }

        public void UpdateScore(string email, string score)
        {
            var tableJson = _cache11.GetOrCreate("DB", entry => "");
            GlobalDataTable globalDataTable = GlobalDataTable.DeserializeFromJson(tableJson);

            DataTableData dataTableData = globalDataTable.DataTableData;

            for (var i = 0; i < dataTableData.Rows.Count; i++)
            {
                var rowData = dataTableData.Rows[i];
                if (rowData.email.Equals(email))
                {
                    // turn string to int
                    int scoreInt = int.Parse(score);
                    rowData.score = scoreInt;
                }
            }

            var tJson = globalDataTable.SerializeToJson();
            _cache11.Set("DB", tJson);
        }

        public void PrintData()
        {
            DataTableData dataTableData = GetTable();

            for (var i = 0; i < dataTableData.Rows.Count; i++)
            {
                var rowData = dataTableData.Rows[i];
                Console.WriteLine(rowData.score);
                Console.WriteLine(rowData.email);
            }
        }

        public void CreateTable()
        {
            string cs = "Data Source=MyDatabase.db";

            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con);

            //firstname, lastname, email, password, gender, age, identification, isAdmin, -99

            // Create a new table
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS MyTable (Id INTEGER PRIMARY KEY, First_name TEXT,Last_name TEXT,Email TEXT, Password TEXT,Gender TEXT, Age INTEGER,Identification TEXT, Score INTEGER ,Living_Area TEXT, Company_Car TEXT )";
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public void DoSQL()
        {
            string cs = "Data Source=MyDatabase.db";

            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con);

            // Create a new table
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS MyTable (Id INTEGER PRIMARY KEY, Name TEXT)";
            cmd.ExecuteNonQuery();

            // Insert a row into the table
            cmd.CommandText = @"INSERT INTO MyTable (Name) VALUES ('Sample Name')";
            cmd.ExecuteNonQuery();

            // Select all rows from the table
            cmd.CommandText = @"SELECT * FROM MyTable";

            using SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine($"{rdr.GetInt32(0)}: {rdr.GetString(1)}");
            }

            con.Close();
        }
    }
}
