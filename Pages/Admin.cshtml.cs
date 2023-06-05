using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OurProject2.Pages
{
    public class AdminModel : PageModel
    {
        public IActionResult OnGet(string email, string buttonNumber, string session)
        {
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
                if (true)
                {
                    var success = true;
                    var result = new { success, session };
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
    }
}
