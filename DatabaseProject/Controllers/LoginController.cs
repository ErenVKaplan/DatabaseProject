using DatabaseProject.Entities;
using DatabaseProject.Models;
using DatabaseProject.Services;
using DatabaseProject.Services.Concrete;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DatabaseProject.Controllers
{
    public class LoginController : Controller
    { private readonly LoginService _loginService;
        public LoginController(LoginService loginService)
        {
            _loginService = loginService;
        }
        #region Login
        [HttpGet]
        public IActionResult Login()
        {
            ClaimsPrincipal claimUser = HttpContext.User;
            if (claimUser.Identity.IsAuthenticated)
            {
                if (claimUser.IsInRole("User"))
                    return RedirectToAction("Dashboard", "UserDashboard");

                else if (claimUser.IsInRole("Admin"))
                    return RedirectToAction("Dashboard", "AdminDashboard");
            }
            return View();
        }
        [HttpPost]
        public async Task<IActionResult> Login(LoginViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Giriş ekranında boş yer bırakmayınız");
                return View(model);
            }


            ServiceResponse<User> response = _loginService.Login(model);


            if (response.IsError)
            {
                foreach (var item in response.Errors)
                {
                    ModelState.AddModelError(string.Empty, item);
                }
                return View();
            }



            // kullanici nesnesi uzerinden buraya Claim atayabiliriz.
            //
            User user = response.Data;
            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.NameIdentifier, user.Email),
                new Claim(ClaimTypes.Role, user.Role),
                new Claim(ClaimTypes.Name, user.Name),

                new Claim("KullaniciId", user.Id.ToString()),
                new Claim("Isim", user.Name),

            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
                IsPersistent = model.OturumuAcikTut
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity), properties
            );

            if (user.Role.ToLower() == "user")
                return RedirectToAction("Dashboard", "UserDashboard");

            else
                return RedirectToAction("Dashboard", "AdminDashboard");

        }
        #endregion





        #region Register
        [HttpGet]
        public IActionResult Register()
        {
            ViewBag.Gender = new List<string>() { "Erkek", "Kadin" };
            var model = new RegisterViewModel();
            return View(model);
        }
       
        [HttpPost]
        public IActionResult Register(RegisterViewModel model)
        {   
            
            if(!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Form alanlarını eksiksiz doldurunuz.");
                return View(model);
            }

            // model valid ise kayıt etmeye çalış.
            ServiceResponse<bool> response = _loginService.Register(model);
            if (!response.IsError) 
            {
                TempData["KayitDurumu"] = "Kullanıcı başarıyla kaydedildi.";
                return RedirectToAction("Login", "Login");
            }
            foreach (var item in response.Errors)
            {
                ModelState.AddModelError(string.Empty, item);
            }
            return View(model);
        }
        #endregion


        [HttpGet]
        public IActionResult LandingPage()
        {
            return View();
        }
    }

}
    


