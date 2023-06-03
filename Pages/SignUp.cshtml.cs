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

                // TODO 
                //  check if email is exist already in DB

                MyDAO.GetInstance(_cache11).SaveToDB(firstname, lastname, email, password, gender, age, identification, isAdmin);

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





    }
}
