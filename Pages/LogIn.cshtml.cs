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
                bool check = CheckUser(email, password);

                    ReadData(email, password);

                if(check == true) {
                    return Content("signed in sucessfull");
                }
                else
                {
                    return Content("signed in faild");
                }
            }
            else
            {
                return Page();
            }
        }

        private bool CheckUser(string email, string password)
        {
            var tableJson = _cache.GetOrCreate("DB", entry => "");
            GlobalDataTable globalDataTable = GlobalDataTable.DeserializeFromJson(tableJson);

            DataTableData dataTableData = globalDataTable.DataTableData;
            for (var i = 0; i<  dataTableData.Rows.Count; i++)
            {
                var rowData = dataTableData.Rows[i];
                if (rowData.email == email && rowData.password == password)
                {
                    return true;
                }
            }

            return false;
        }

        private void ReadData(string email, string password)
        {

            var tableJson = _cache.GetOrCreate("DB", entry => "");
            GlobalDataTable globalDataTable = GlobalDataTable.DeserializeFromJson(tableJson);

            DataTableData dataTableData = globalDataTable.DataTableData;
            foreach (var rowData in dataTableData.Rows)
            {
                Console.WriteLine($"ID: {rowData.ID},Name: {rowData.Name} LastName: {rowData.lastname}, Enail: {rowData.email}, Password: {rowData.password}, Gender: {rowData.gender}, Age: {rowData.age}, user/admin?: {rowData.isAdmin}");
            }
        }
    }
    
}
