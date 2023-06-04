using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

using System.Data.SQLite;


namespace OurProject2.Pages
{

    public class ResultsTestModel : PageModel
    {
        private readonly IMemoryCache _cache;

        public ResultsTestModel(IMemoryCache cashe)
        {
            _cache = cashe;
        }

        public IActionResult OnGet(string session, string score)
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                DoSQL();

                // get email is related to this session
                var email = _cache.Get<string>(session);

                MyDAO.GetInstance(_cache).PrintData();
                MyDAO.GetInstance(_cache).UpdateScore(email, score);

                MyDAO.GetInstance(_cache).PrintData();

                var success = true;
                var result = new { success, session };
                var jsonResult = new JsonResult(result);
                return jsonResult;
            }
            else
            {
                return Page();
            }
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
