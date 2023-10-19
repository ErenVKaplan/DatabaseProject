using AutoMapper;
using DatabaseProject.Entities;
using DatabaseProject.Models;
using DatabaseProject.Services;
using DatabaseProject.Services.Concrete;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace DatabaseProject.Controllers
{
    [Authorize(Roles ="User")]
    public class ProfileController : Controller
    {
        private readonly KullaniciService _kullaniciService;
        private readonly IMapper _mapper;
        public ProfileController(KullaniciService kullaniciService, IMapper mapper)
        {
            _kullaniciService = kullaniciService;
            _mapper = mapper;
        }


        [HttpGet]
        public IActionResult Profile() 
        {
            ViewData["Title"] = "Profil";
            ServiceResponse<User> response = _kullaniciService.GetKullaniciByEmail(User.FindFirstValue(ClaimTypes.NameIdentifier));
            if (!response.IsError)
            {
                User user = response.Data;
                return View(_mapper.Map<ProfileUpdateViewModel>(user));
            }
            return RedirectToAction("Dashboard", "UserDashboard");
        }
        [HttpPost]
        public IActionResult Profile(ProfileUpdateViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                ViewData["ValidateMessage"] = "Bilgilerinizi Lütfen Düzgün Giriniz.";
                return View(model);
            }
            ServiceResponse<User> response = _kullaniciService.UpdateUser(_mapper.Map<User>(model));

            if (!response.IsError)
            {
                ViewData["SuccessMessage"] = "Kullanıcı başarıyla güncellendi.";
                return RedirectToAction("Profile", "Profile");
            }

            foreach (var item in response.Errors)
            {
                ModelState.AddModelError("", item);
            }
            return View(model);
        }
        [HttpPost]
        public IActionResult DeleteUser(int id) 
        {
            _kullaniciService.DeleteUserById(id);
             HttpContext.SignOutAsync();
            return RedirectToAction("Login", "Login");
        }
    }
}
