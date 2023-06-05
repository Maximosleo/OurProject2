using Microsoft.AspNetCore.DataProtection.KeyManagement;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Caching.Memory;
using System.Text;
using System;
using static System.Formats.Asn1.AsnWriter;

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
            string password, string gender, string age,string identification, string admin_user, String livingArea, String[] companyCars)
        {
            // Set response
            // if this code is called from a button click in the client
            if (Request.Headers["X-Requested-With"] == "XMLHttpRequest")
            {
        //        MyDAO2.GetInstance().DeleteTable(); /////////////////////***************************************************************

                MyDAO2.GetInstance().CreateTable();


           //     SaveToDB(string firstname, string lastname, string email, string password, string gender,
           //string age, string identification, bool isAdmin, int score, String companyCar, String livingArea)



                int isAdmin;
                if (admin_user == "admin")
                {
                    isAdmin = 1;
                }
                else
                {
                    isAdmin = 0;
                }

                // TODO 
                //  check if email is exist already in DB

                MyDAO2.GetInstance().SaveToDB(firstname, lastname, email, password, gender, age, identification, isAdmin, -99, "some company car", "some living area");
                MyDAO2.GetInstance().PrintData();

                string session = GenerateRandomString(8); // Generate a random string for the session key

                //  var session = _cache11.GetOrCreate(session, entry => "");
                _cache11.Set(session, email);


                var success = true;
                var result = new { success, isAdmin, session };
                var jsonResult = new JsonResult(result);
                return jsonResult;
            }
            // if this code is called when the client page is starting
            else
            {
                return Page();
            }
        }

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
