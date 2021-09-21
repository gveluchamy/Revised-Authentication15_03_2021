
using EmployeeDetails.Models;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;


namespace EmployeeDetails.Controllers
{
    public class AccountController : Controller  
    {
        
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;

        public readonly ApplicationDpclass _dpclass;
        public AccountController(UserManager<ApplicationUser> userManager, SignInManager<ApplicationUser> signInManager, ApplicationDpclass dpclass)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _dpclass = dpclass;
          
         
        }
        [HttpGet]
        public IActionResult Register()
        {
            return View();
        }
        //public IActionResult Frontpage()
        //{
        //    return View();

        //}

        //public IActionResult SignIn()
        //{
        //    var props = new AuthenticationProperties();
        //    props.RedirectUri = "/Emp/SignInSuccess";

        //    return Challenge(props);
        //}

        //public IActionResult SignInSuccess()
        //{
        //    return RedirectToAction("Index");

        //}
        //[ValidateAntiForgeryToken]
        //public async Task<IActionResult> SignOut()
        //{

        //    await _signInManager.SignOutAsync();

        //    return RedirectToAction("SignIn");
        //}


        [HttpPost]
        public async Task<IActionResult> Register(RegisterUserModel model) 
        {


            if (ModelState.IsValid)
            {

                var user = new ApplicationUser   //it is used for  Copy data from RegisterUserModel to ApplicationUser
                {
                    UserName = model.Email,
                    Email = model.Email,
                    FirstName = model.FirstName,
                    lastname = model.lastname,
                    Gender = model.Gender,
                    DateofBirth = model.DateofBirth,
                    City=model.City

                };

                var result = await _userManager.CreateAsync(user, model.Password);   //it is userd for  Store user data in AspNetUsers database table

                if (result.Succeeded)
                {
                    await _signInManager.SignInAsync(user, isPersistent: false);
                    return RedirectToAction("Login", "Account");
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }

            return View(model);
        }



        [HttpPost]
       
        //[IgnoreAntiforgeryToken]
        public async Task<IActionResult> Logout()
        {

            //var result = _dpclass.Users.Where(x => x.Id == id).FirstOrDefault();
            //if(result !=null)
            //{
            //    _dpclass.Users.Remove(result);
            //    _dpclass.SaveChanges();

            //}

            foreach (var cookie in HttpContext.Request.Cookies)
            {
                Response.Cookies.Delete(cookie.Key);
            }

            await _signInManager.SignOutAsync();

            //HttpContext.Session.Clear();
            //await _signInManager.SignOutAsync();

            return RedirectToAction("index", "Data");

            //var user = await _userManager.FindByIdAsync(userId);
            //if (user != null)
            //{
            //    IdentityResult result = await _userManager.DeleteAsync(user);
            //    if (result.Succeeded)
            //        return RedirectToAction("Index");
            //    else
            //    {
            //        return View(result);
            //    }

            //}
            //else
            //{
            //    ModelState.AddModelError("", "User Not Found");
            //    return View("Index", _userManager.Users); 

            //}

        }

        //[HttpPost]
        //public async Task<IActionResult> Logout(string Id)
        //{
        //    var del = _dpclass.UserLogins.Where(x => x.UserId == Id);

        //    if (del.Any())
        //    {
        //        _dpclass.UserLogins.DeleteOnSubmit(del);
        //        db.DeleteAllOnSubmit(students);

        //        db.SubmitChanges();
        //    }

        //    return true;
        //    var user = await _userManager.FindByEmailAsync(email);
        //    await _userManager.DeleteAsync(user);


        //    //var user = await _userManager.FindByIdAsync(loginUserModel.UserId);
        //    //var logins = user.

        //    //var dels = _dpclass.Users.Where(x => x.Id == UserId);
        //    ////var dels = _dpclass.Employees.Where(x => x.EmpID == id).FirstOrDefault();
        //    //if (dels == null)
        //    //{
        //    //    return RedirectToAction("index", "Data");
        //    //}
        //    //_dpclass.Users.RemoveRange(dels);


        //    //_dpclass.SaveChanges();

        //    //await _signInManager.SignOutAsync();

        //    return RedirectToAction("index", "Data");

        //}
        //[HttpPost]
        //public async Task<IActionResult> Logouts(string UserId)
        //{

        //    var user = await _userManager.FindByIdAsync
        //    if (user != null)
        //    {
        //        var result = await _userManager.DeleteAsync(user);

        //        if (result.Succeeded)
        //            return RedirectToAction("ListOfRoles");

        //        foreach (var error in result.Errors)
        //        {
        //            ModelState.AddModelError("", error.Description);
        //        }
        //    }
        //    else
        //    {
        //        return View("NotFound");
        //    }

        //    return View("NotFound");
        //}

       
        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> Login(string returnUrl)
        {
            LoginUserModel loginUserModel = new LoginUserModel()
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()

            };
            return View(loginUserModel);
        }

        [HttpPost]
        public async Task<IActionResult> Login(LoginUserModel model)
        {
            if (ModelState.IsValid)
            {
                var result = await _signInManager.PasswordSignInAsync(model.Email, model.Password, model.RememberMe, lockoutOnFailure: true);

                if (result.Succeeded)
                {
                    return RedirectToAction("index", "Data");
                }

                ModelState.AddModelError(string.Empty, "Invalid Login Attempt");

                if(result.IsLockedOut)
                {
                    return View("Accountlocked");
                }
            }
            

            return View(model);
        }

        [HttpPost]
        [AllowAnonymous]

        public IActionResult ExternalLogin(string provider, string returnUrl)
        {
            var redirectUrl = Url.Action("ExternalLoginCallback", "Account",new { ReturnUrl = returnUrl });
            var properties = _signInManager.ConfigureExternalAuthenticationProperties(provider, redirectUrl);
            return new ChallengeResult(provider, properties);
        }


        [AllowAnonymous]
        public async Task<IActionResult> ExternalLoginCallback(string returnUrl = null, string remoteError = null)

        {
            returnUrl = returnUrl ?? Url.Content("~/");

            LoginUserModel loginUserModel = new LoginUserModel
            {
                ReturnUrl = returnUrl,
                ExternalLogins = (await _signInManager.GetExternalAuthenticationSchemesAsync()).ToList()
            };

            if (remoteError != null)
            {
                ModelState.AddModelError(string.Empty, $"Error from external provider: {remoteError}");

                return View("Login", loginUserModel);
            }
            var info = await _signInManager.GetExternalLoginInfoAsync();    // Get the login information about  user from  external login provider key
            if (info == null)
            {
                ModelState.AddModelError(string.Empty, "Error loading external login information.");

                return View("Login", loginUserModel);
            }

            var signInResult = await _signInManager.ExternalLoginSignInAsync(info.LoginProvider, info.ProviderKey, isPersistent: false, bypassTwoFactor: false); // If user already has a login (if there is a record in AspNetUserLoginstable) then sign-in the user with this external login provider

            if (signInResult.Succeeded)
            {
                return LocalRedirect(returnUrl);
            }
            // If there is no record in AspNetUserLogins table, the user may not have a local account
            else
            {
                // Get the email claim value
                var email = info.Principal.FindFirstValue(ClaimTypes.Email);

                if (email != null)
                {
                    // Create a new user without password if we do not have a user already
                    var user = await _userManager.FindByEmailAsync(email);

                    if (user == null)
                    {
                        user = new ApplicationUser
                        {
                            UserName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Email = info.Principal.FindFirstValue(ClaimTypes.Email),
                            City = info.Principal.FindFirstValue(ClaimTypes.Email),
                            FirstName = info.Principal.FindFirstValue(ClaimTypes.Email),
                            lastname = info.Principal.FindFirstValue(ClaimTypes.Email),
                            Gender = info.Principal.FindFirstValue(ClaimTypes.NameIdentifier),

                        };

                        await _userManager.CreateAsync(user);
                    }

                    // Add a login (i.e insert a row for the user in AspNetUserLogins table)
                    await _userManager.AddLoginAsync(user, info);
                    await _signInManager.SignInAsync(user, isPersistent: false);

                    return LocalRedirect(returnUrl);
                }


                // If we cannot find the user email we cannot continue
                ViewBag.ErrorTitle = $"Email claim not received from: {info.LoginProvider}";
                ViewBag.ErrorMessage = "Please contact support on vpganapathy2@gmail.com";

                return View("Error");
            }
        }




    }
}
