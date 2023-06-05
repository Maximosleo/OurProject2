using Microsoft.Extensions.Caching.Memory;
using static System.Formats.Asn1.AsnWriter;

using System.Data.SQLite;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using System.Drawing;
using Microsoft.AspNetCore.Mvc.RazorPages;


namespace OurProject2.Pages
{
    public class MyDAO2
    {
        private static MyDAO2 instance = null;
        private readonly IMemoryCache _cache11;

        // Make constructor private so it can't be called from outside this class
        private MyDAO2()
        {
            
            CreateTable();
        }

        // Provide a global point of access to the instance
        public static MyDAO2 GetInstance()
        {
            if (instance == null)
            {
                instance = new MyDAO2();
            }
            return instance;
        }

        //save the data
        public void SaveToDB(string firstname, string lastname, string email, string password, string gender,
            string age, string identification, int isAdmin, int score, String companyCar, String livingArea)
        {
            string cs = "Data Source=MyDatabase.db";
            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con);



     /*       cmd.CommandText = @"CREATE TABLE IF NOT EXISTS MyTable (Id INTEGER PRIMARY KEY,First_name TEXT,Last_name TEXT,Email TEXT,
                    Password TEXT,Gender TEXT, Age INTEGER,Identification TEXT, Score INTEGER ,Living_Area TEXT, Company_Car TEXT, IsAdmin INTEGER)";
*/

            cmd.CommandText = $@"INSERT INTO MyTable (First_name, Last_name, Email, Password, Gender, Age, Identification, Score, Living_Area, Company_Car, IsAdmin) 
                VALUES (@First_name, @Last_name, @Email, @Password, @Gender, @Age, @Identification, @Score, @Living_Area, @Company_Car, @IsAdmin)";

            // Add parameters
            cmd.Parameters.AddWithValue("@First_name", firstname);
            cmd.Parameters.AddWithValue("@Last_name", lastname);
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);
            cmd.Parameters.AddWithValue("@Gender", gender);
            cmd.Parameters.AddWithValue("@Age", age);
            cmd.Parameters.AddWithValue("@Identification", identification);
            cmd.Parameters.AddWithValue("@Living_Area", livingArea);
            cmd.Parameters.AddWithValue("@Company_Car", companyCar);
            cmd.Parameters.AddWithValue("@IsAdmin", isAdmin);
            cmd.Parameters.AddWithValue("@Score", score);


            cmd.ExecuteNonQuery();

            con.Close();
        }


        public bool CheckUser(string email, string password)
        {
            string cs = "Data Source=MyDatabase.db";
            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = @"SELECT Email, Password FROM MyTable WHERE Email = @Email AND Password = @Password";
            cmd.Parameters.AddWithValue("@Email", email);
            cmd.Parameters.AddWithValue("@Password", password);

            using SQLiteDataReader rdr = cmd.ExecuteReader();

            bool exists = rdr.Read(); // Returns true if there is at least one row in the result set

            con.Close();

            return exists;
        }


        public bool CheckAdmin(string email)
        {
            string cs = "Data Source=MyDatabase.db";
            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = @"SELECT IsAdmin FROM MyTable WHERE Email = @Email";
            cmd.Parameters.AddWithValue("@Email", email);

            using SQLiteDataReader rdr = cmd.ExecuteReader();

            bool isAdmin = false;

            if (rdr.Read())
            {
                isAdmin = rdr.GetBoolean(0);
            }

            con.Close();

            return isAdmin;
        }

        public void UpdateScore(string email, string score)
        {
            string cs = "Data Source=MyDatabase.db";
            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = @"UPDATE MyTable SET Score = @score WHERE Email = @email";

            cmd.Parameters.AddWithValue("@score", score);
            cmd.Parameters.AddWithValue("@email", email);

            cmd.ExecuteNonQuery();

            con.Close();
        }

        public void PrintData()
        {
            string cs = "Data Source=MyDatabase.db";
            using var con = new SQLiteConnection(cs);
            con.Open();
            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = @"SELECT * FROM MyTable";

            using SQLiteDataReader rdr = cmd.ExecuteReader();

/*            while (rdr.Read())
            {
                Console.WriteLine($"{rdr.GetInt32(0)}: {rdr.GetString(1)}:{rdr.GetString(2)}:" +
                    $"{rdr.GetString(3)}:{rdr.GetString(4)}:{rdr.GetString(5)}:{rdr.GetInt32(6)}:" +
                    $"{rdr.GetString(7)}::{rdr.GetInt32(8)}::{rdr.GetString(9)}:");
            }*/

            while (rdr.Read())
            {
                for (int i = 0; i < rdr.FieldCount; i++) // rdr.FieldCount gives you the total number of columns.
                {
                    try
                    {
                        string fieldType = rdr.GetFieldType(i).Name; // Get the type of the field.

                        // Check the type of the field and print accordingly.
                        if (fieldType == "Int32")
                        {
                            Console.Write($"{rdr.GetInt32(i)}: ");
                        }
                        else if (fieldType == "Int64")
                        {
                            Console.Write($"{rdr.GetInt32(i)}: ");
                        }
                        else if (fieldType == "String")
                        {
                            Console.Write($"{rdr.GetString(i)}: ");
                        }
                        // Add more conditions here for other field types.
                    }
                    catch (InvalidCastException e)
                    {
                        Console.WriteLine($"Failed to read value at index {i}: {e.Message}");
                    }
                }
                Console.WriteLine(); // Newline for each row.
            }

            con.Close();
        }

        public void CreateTable()
        {
            string cs = "Data Source=MyDatabase.db";
            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con);

            //firstname, lastname, email, password, gender, age, identification, isAdmin, -99

            // Create a new table
            cmd.CommandText = @"CREATE TABLE IF NOT EXISTS MyTable (Id INTEGER PRIMARY KEY,First_name TEXT,Last_name TEXT,Email TEXT,
                    Password TEXT,Gender TEXT, Age INTEGER,Identification TEXT, Score INTEGER ,Living_Area TEXT, Company_Car TEXT, IsAdmin INTEGER)";
            cmd.ExecuteNonQuery();

            con.Close();
        }

        public void PrintTable()
        {
            string cs = "Data Source=MyDatabase.db";
            using var con = new SQLiteConnection(cs);
            con.Open();
            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = @"SELECT * FROM MyTable";

            using SQLiteDataReader rdr = cmd.ExecuteReader();

            while (rdr.Read())
            {
                Console.WriteLine($"{rdr.GetInt32(0)}: {rdr.GetString(1)}");
            }

            con.Close();
        }

        public void DeleteTable()
        {
            string cs = "Data Source=MyDatabase.db";
            using var con = new SQLiteConnection(cs);
            con.Open();

            using var cmd = new SQLiteCommand(con);

            cmd.CommandText = $@"DROP TABLE IF EXISTS MyTable";
            cmd.ExecuteNonQuery();

            con.Close();
        }

        //public void DoSQL()
        //{
        //    string cs = "Data Source=MyDatabase.db";

        //    using var con = new SQLiteConnection(cs);
        //    con.Open();

        //    using var cmd = new SQLiteCommand(con);

        //    // Create a new table
        //    cmd.CommandText = @"CREATE TABLE IF NOT EXISTS MyTable (Id INTEGER PRIMARY KEY, Name TEXT)";
        //    cmd.ExecuteNonQuery();

        //    // Insert a row into the table
        //    cmd.CommandText = @"INSERT INTO MyTable (Name) VALUES ('Sample Name')";
        //    cmd.ExecuteNonQuery();

        //    // Select all rows from the table
        //    cmd.CommandText = @"SELECT * FROM MyTable";

        //    using SQLiteDataReader rdr = cmd.ExecuteReader();

        //    while (rdr.Read())
        //    {
        //        Console.WriteLine($"{rdr.GetInt32(0)}: {rdr.GetString(1)}");
        //    }

        //    con.Close();
        //}
    }
}
