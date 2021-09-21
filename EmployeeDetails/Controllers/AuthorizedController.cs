using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.MicrosoftAccount;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EmployeeDetails.Controllers
{
    public class AuthorizedController : Controller
    {
        public IActionResult Login()
        {
            return View();
        }


        //public async void LoginWithMicrosoft()
        //{

        //    await HttpContext.ChallengeAsync(MicrosoftAccountDefaults.AuthenticationScheme,
        //       new AuthenticationProperties() { RedirectUri = "/Account/Index" });



        //    // this is for OAth Specilation , IT's don't show Who is user 
        //    //await HttpContext.ChallengeAsync("Microsoft-AccessToken",
        //    //   new AuthenticationProperties() { RedirectUri = "/Account/Index" });
        //}

        //public ActionResult Logout()
        //{
        //    HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
        //    return RedirectToAction("Login", "Authorized");
        //}
    }
}
