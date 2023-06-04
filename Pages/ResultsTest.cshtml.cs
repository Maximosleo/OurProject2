using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

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
    }
}
