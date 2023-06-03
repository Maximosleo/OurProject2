using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using System.Data.OleDb;
using System.Reflection;
using System.IO;
using System.Data;
using System.Linq.Expressions;

namespace OurProject2.Pages
{
    public class LogInModel : PageModel
    {
        private readonly IMemoryCache _cache;
       

        public LogInModel(IMemoryCache cache)
        {
            _cache = cache;
        }
        public IActionResult OnGet(string email, string password)
        {

            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                bool check = CheckUser(email, password);

                //    ReadData(email, password);

                if(check == true) {
                    // return Content("signed in sucessfull");
                    bool isAdmin = CheckAdmin(email);

                    var success = true;
                    var result = new { success, isAdmin };
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

        private bool CheckUser(string email11, string password)
        {
            var tableJson = _cache.GetOrCreate("DB", entry => "");
            GlobalDataTable globalDataTable = GlobalDataTable.DeserializeFromJson(tableJson);

            DataTableData dataTableData = globalDataTable.DataTableData;
            for (var i = 0; i<  dataTableData.Rows.Count; i++)
            {
                var rowData = dataTableData.Rows[i];
                if (rowData.email.Equals(email11) && rowData.password.Equals(password))
                {
                    return true;
                }
            }

            return false;
        }

        private bool CheckAdmin(string email)
        {
            var tableJson = _cache.GetOrCreate("DB", entry => "");
            GlobalDataTable globalDataTable = GlobalDataTable.DeserializeFromJson(tableJson);

            DataTableData dataTableData = globalDataTable.DataTableData;
            for (var i = 0; i < dataTableData.Rows.Count; i++)
            {
                var rowData = dataTableData.Rows[i];
                if (rowData.email.Equals(email))
                {
                    if(rowData.isAdmin == true)
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
