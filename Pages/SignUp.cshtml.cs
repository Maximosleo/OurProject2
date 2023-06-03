using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace OurProject2.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly IMemoryCache _cache11;

        // constructor
        public SignUpModel(IMemoryCache cache)
        {
            _cache11 = cache;
        }
        public IActionResult OnGet(string firstname, string lastname, string email,
            string password, string gender, string age,string identification, string admin_user)
        {
            // Set response
            // if this code is called from a button click in the client
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
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

                var success = true;
                var result = new { success, isAdmin};
                var jsonResult = new JsonResult(result);
                return jsonResult;
            }
            // if this code is called when the client page is starting
            else
            {
                return Page();
            }
        }




        private void SaveToDB(string firstname, string lastname, string email, string password, string gender, string age, string identification, bool isAdmin)
        {
            var tableJson = _cache11.GetOrCreate("DB", entry => "");
            GlobalDataTable globalDataTable = GlobalDataTable.DeserializeFromJson(tableJson);

            DataTableData dataTableData = globalDataTable.DataTableData;
            dataTableData.Rows.Add(new RowData { ID = identification, Name = firstname, lastname = lastname, email = email, password = password, gender = gender, age = age, isAdmin = isAdmin });

            var tJson = globalDataTable.SerializeToJson();
            _cache11.Set("DB", tJson);
        }
    }
}
