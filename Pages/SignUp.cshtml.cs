using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using System.Data;
using System.Diagnostics.Metrics;
using System.Text.Json;

namespace OurProject2.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly IMemoryCache _cache;

        public SignUpModel(IMemoryCache cache)
        {
            _cache = cache;
        }
        public IActionResult OnGet(string firstname, string lastname, string email, string password, string gender, string age,string identification, string admin_user)
        {


            // Set response
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                /*              var counter = _cache.GetOrCreate("Counter", entry => 0);
                              counter++;
                              _cache.Set("Counter", counter);

                              Random random = new Random();
                              int session = random.Next(1, 10001); // The upper bound is exclusive, so we use 10001 to include 10000
                              _cache.Set(counter + "", session + "");*/

                // Log inputs
                //Console.WriteLine("First name : " + firstname);
                //Console.WriteLine("Last Name : " + lastname);
                //Console.WriteLine("email : " + email);
                //Console.WriteLine("password : " + password);
                //Console.WriteLine("gender : " + gender);
                //Console.WriteLine("age : " + age);

                bool isAdmin;
                if (admin_user == "admin")
                {
                    isAdmin = true;
                }
                else
                {
                    isAdmin = false;
                }
               

                SaveToDB(firstname, lastname, email, password, gender, age, identification, isAdmin);

                    // Create a new anonymous object for your JSON response
                var jsonResponse = new { isAdmin1 = isAdmin, message = "Operation successful" };

                var obj = new { isAdmin = true, message = "Operation successful" };
                string json = JsonSerializer.Serialize(obj);

             //         return new JsonResult(json);


                return Content(jsonResponse.ToString());
            }
            else
            {
                return Page();
            }
        }

        private void SaveToDB(string firstname, string lastname, string email, string password, string gender, string age, string identification, bool isAdmin)
        {
            var tableJson = _cache.GetOrCreate("DB", entry => "");
            GlobalDataTable globalDataTable = GlobalDataTable.DeserializeFromJson(tableJson);

            DataTableData dataTableData = globalDataTable.DataTableData;
            dataTableData.Rows.Add(new RowData { ID = identification, Name = firstname, lastname = lastname, email = email, password = password, gender = gender, age = age, isAdmin = isAdmin });

            var tJson = globalDataTable.SerializeToJson();
            _cache.Set("DB", tJson);
        }
    }
}
