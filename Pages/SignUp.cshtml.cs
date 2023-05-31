using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;

namespace OurProject2.Pages
{
    public class SignUpModel : PageModel
    {
        private readonly IMemoryCache _cache;

        public SignUpModel(IMemoryCache cache)
        {
            _cache = cache;
        }
        public IActionResult OnGet(string firstname, string lastname, string email, string password, string gender, string age)
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
                Console.WriteLine("First name : " + firstname);
                Console.WriteLine("Last Name : " + lastname);
                Console.WriteLine("email : " + email);
                Console.WriteLine("password : " + password);
                Console.WriteLine("gender : " + gender);
                Console.WriteLine("age : " + age);

                return Content("pass the zaza");
            }
            else
            {
                return Page();
            }
        }
    }
}
