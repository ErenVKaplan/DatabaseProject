using AutoMapper;
using DatabaseProject.Entities;
using DatabaseProject.Models;
using DatabaseProject.Services;
using DatabaseProject.Services.Concrete;
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
            return View();
        }
    }
}
