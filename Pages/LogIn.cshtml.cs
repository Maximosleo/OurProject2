using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using System.Data.OleDb;
using System.Reflection;
using System.IO;
using System.Data;

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


                UpdateAccess(email, password);

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


            DataTable dataTable = GlobalDataTable.Instance.DataTable;

            foreach (DataRow row in dataTable.Rows)
            {
                Console.WriteLine($"ID: {row["ID"]}, Name: {row["Name"]}");
            }
        }
}
}
