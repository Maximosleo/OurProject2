using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using System.Data.OleDb;
using System.Reflection;
using System.IO;

namespace OurProject2.Pages
{
    public class LogInModel : PageModel
    {
        private readonly IMemoryCache _cache;
        private readonly MyAdoHelper _myAdoHelper;

        public LogInModel(IMemoryCache cache, MyAdoHelper myAdoHelper)
        {
            _cache = cache;
        }
        public IActionResult OnGet(string email, string password)
        {
            
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {


   //             UpdateAccess(email, password);

                Console.WriteLine("email : " + email);
                Console.WriteLine("password : " + password);

                return Content("pass the zaza");
            }
            else
            {
                return Page();
            }
        }

        private void UpdateAccess(string email, string password)
        {
            //// Path to your access database file. Update if your database is located somewhere else.
            //string databasePath = Path.Combine(Directory.GetCurrentDirectory(), "Database1.mdb");
            //string connectionString = $@"Provider=Microsoft.ACE.OLEDB.12.0;Data Source={databasePath}";


            //using (OleDbConnection conn = new OleDbConnection(connectionString))
            //{
            //    conn.Open();

            //    using (OleDbCommand cmd = new OleDbCommand("INSERT INTO table0 (Email, Password) VALUES (11, 22)", conn))
            //    {
            //        cmd.Parameters.AddWithValue("@Email", email);
            //        cmd.Parameters.AddWithValue("@Password", password);
            //        cmd.ExecuteNonQuery();
            //    }
            //}


            //string fileName = "Database1.mdb";   //שם הקובץ
            //string path = HttpContext.Current.Server.MapPath("App_Data/");//מיקום מסד בפורוייקט
            //path += fileName;
            //string connString = "Provider=Microsoft.Jet.OLEDB.4.0;Data source=" + path;//נתוני ההתחברות הכוללים מיקום וסוג המסד
            //OleDbConnection conn = new OleDbConnection(connString);

            //string tableName = "users"; //שם הטבלה

            //   MyAdoHelper conn = new MyAdoHelper();
           // OleDbConnection oleDbConnection = _myAdoHelper.ConnectToDb("tabel0");

            //     conn.ConnectToDb("tabel0");
        }
}
}
