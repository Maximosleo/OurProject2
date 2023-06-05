using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using System.Data.OleDb;
using System.Reflection;
using System.IO;
using System.Data;
using System.Linq.Expressions;
using System.Text;

namespace OurProject2.Pages
{
    public class LogInModel : PageModel
    {
        private readonly IMemoryCache _cache;
       

        public LogInModel(IMemoryCache cache)
        {
            _cache = cache;
        }

        // 2 server called by client
        public IActionResult OnGet(string email, string password)
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                bool check = MyDAO2.GetInstance().CheckUser(email, password);


                //    ReadData(email, password);

                if(check == true) {
                    // return Content("signed in sucessfull");
                  
                    bool isAdmin = MyDAO.GetInstance(_cache).CheckAdmin(email);

                    string session = GenerateRandomString(8); // Generate a random string for the session key

                    _cache.Set(session, email);

                    var success = true;
                    var result = new { success, isAdmin,session };
                    var jsonResult = new JsonResult(result);
                    return jsonResult;
                }
                else
                {
                    var success = false;
                    var result = new { success };
                    var jsonResult = new JsonResult(result);
                    return jsonResult;
                }
            }
            else
            {
                return Page();
            }


        }   

/*        private void ReadData(string email, string password)
        {

            DataTableData dataTableData = MyDAO2.GetInstance().GetTable();

            foreach (var rowData in dataTableData.Rows)
            {
                Console.WriteLine($"ID: {rowData.ID},Name: {rowData.Name} LastName: {rowData.lastname}, Enail: {rowData.email}, Password: {rowData.password}, Gender: {rowData.gender}, Age: {rowData.age}, user/admin?: {rowData.isAdmin}");
            }
        }*/

        private string GenerateRandomString(int length)
        {
            var _random = new Random();
            const string chars = "ABCDEFGHIJKLMNOPQRSTUVWXYZabcdefghijklmnopqrstuvwxyz0123456789";
            StringBuilder builder = new StringBuilder();

            for (int i = 0; i < length; i++)
            {
                int index = _random.Next(chars.Length);
                builder.Append(chars[index]);
            }

            return builder.ToString();
        }
    }
    
}
