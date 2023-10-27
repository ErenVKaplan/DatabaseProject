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
        private readonly BankService _bankService;
        private readonly AdressService _adressService;
        public ProfileController(KullaniciService kullaniciService, IMapper mapper, BankService bankService, AdressService adressService)
        {
            _kullaniciService = kullaniciService;
            _mapper = mapper;
            _bankService = bankService;
            _adressService = adressService;
        }

        #region Profile
        [HttpGet]
        public IActionResult Profile() 
        {
            

           

            ViewData["Title"] = "Profil";
            ServiceResponse<User> response = _kullaniciService.GetKullaniciByEmail(User.FindFirstValue(ClaimTypes.NameIdentifier));
            
            
            if (!response.IsError)
            {
                User user = response.Data;
                var map = _mapper.Map<ProfileUpdateViewModel>(user);
                map.Genders = new List<string>() { "Erkek", "Kadin" };
                return View(map);
            }


            return RedirectToAction("Dashboard", "UserDashboard");
        }


        [HttpPost]
        public IActionResult Profile(ProfileUpdateViewModel model) 
        {
            if (!ModelState.IsValid)
            {
                ViewData["ValidateMessage"] = "Bilgilerinizi Lütfen Düzgün Giriniz.";
                model.Genders=new List<string>() { "Erkek", "Kadin" };

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

        #endregion

        #region Bank
        [HttpGet]
        public IActionResult Bank()
        {
            ViewData["PageTitle"] = "Banka Kartlarım";
            int userId=int.Parse(User.FindFirstValue("KullaniciId"));
            var list = _bankService.GetBanksByUserId(userId).Data;
            return View(list);
        }

        [HttpGet]
        public IActionResult BankCreate()
        {
            ViewData["PageTitle"] = "Yeni Kart Ekle";

            int kullaniciId = int.Parse(User.FindFirstValue("KullaniciId"));
            BankCreateViewModel model = new BankCreateViewModel();
            model.UserId = kullaniciId;

            return View(model);
        }
        [HttpPost]
        public IActionResult BankCreate(BankCreateViewModel model)
        {
            if (!ModelState.IsValid) 
            {
                ModelState.AddModelError(string.Empty, "Form alanlarını eksiksiz doldurunuz.");
                return View(model);
            }
            ServiceResponse<bool> response = _bankService.CreateBank(_mapper.Map<Bank>(model));
            
            if (!response.IsError)
            {
                //TempData["SuccessMessage"] = "Ödeme bilgisi başarıyla eklendi.";
                return RedirectToAction("Bank", "Profile");
            }

            foreach (string message in response.Errors)
                ModelState.AddModelError(string.Empty, message);

            return View(model);
        }
        
        
        [Route("[controller]/[action]/{id}")]
        [HttpGet]    
        public IActionResult BankUpdate(int id) 
        {
            ViewData["PageTitle"] = "Kart Bilgisi Güncelle";

            int kullaniciId = int.Parse(User.FindFirstValue("KullaniciId"));

            ServiceResponse<Bank> response = _bankService.GetBankById(kullaniciId, id);
            if (!response.IsError)
            {
                BankUpdateViewModel model = new BankUpdateViewModel();
                return View(_mapper.Map<BankUpdateViewModel>(response.Data));
            }

            foreach (string message in response.Errors)
                ModelState.AddModelError(string.Empty, message);

            return RedirectToAction("Bank");
        }

        [HttpPost]  
        public IActionResult BankUpdate(BankUpdateViewModel model)
        {
            

            ViewData["PageTitle"] = "Kart Bilgisi Güncelle";

            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Form alanlarını eksiksiz doldurunuz.");
                return View(model);
            }

            ServiceResponse<bool> response = _bankService.UpdateBank(_mapper.Map<Bank>(model));

            // Update işlemi başarılı ise.
            if (!response.IsError)
            {
                TempData["SuccessMessage"] = "Banka bilgileri başarıyla güncellendi.";
                return RedirectToAction("Bank", "Profile");
            }

            foreach (string message in response.Errors)
                ModelState.AddModelError(string.Empty, message);

            return View(model);
        }

        [HttpGet]
        public IActionResult BankRemove(int BankId) 
        {
           
            ServiceResponse<bool> response = _bankService.DeleteBankById(BankId);
            if (!response.IsError)
            {
                TempData["SuccessMessage"] = "Kart bilgileri başarıyla silindi.";
                return RedirectToAction("Bank", "Profile");
            }
            foreach (string message in response.Errors)
                ModelState.AddModelError(string.Empty, message);

            return RedirectToAction("Bank");
        }



        #endregion


        #region Adrress
        [HttpGet]
        public IActionResult Adrress()
        {
            ViewData["PageTitle"] = "Adreslerim";
            int userId = int.Parse(User.FindFirstValue("KullaniciId"));
            var list = _adressService.GetAdrressById(userId).Data;
            return View(list);
        }

        [HttpGet]
        public IActionResult AdrressCreate()
        {
            ViewData["PageTitle"] = "Yeni Adres Ekle";

            int kullaniciId = int.Parse(User.FindFirstValue("KullaniciId"));
            AdrressCreateViewModel model = new AdrressCreateViewModel();
            model.UserId = kullaniciId;

            return View(model);
        }

        [HttpPost]
        public IActionResult AdrressCreate(AdrressCreateViewModel model)
        {
            if (!ModelState.IsValid)
            {
                ModelState.AddModelError(string.Empty, "Form alanlarını eksiksiz doldurunuz.");
                return View(model);
            }
            ServiceResponse<bool> response = _adressService.AdrressCreate(_mapper.Map<Adrress>(model));

            if (!response.IsError)
            {
                TempData["SuccessMessage"] = "Adres bilgisi başarıyla eklendi.";
                return RedirectToAction("Adrress", "Profile");
            }

            foreach (string message in response.Errors)
                ModelState.AddModelError(string.Empty, message);

            return View(model);
        }

        [Route("[controller]/[action]/{id}")]
        [HttpGet]
        public IActionResult AdrressUpdate(int id)
        {
            ViewData["PageTitle"] = "Adres Güncelleme";
            int userId = int.Parse(User.FindFirstValue("KullaniciId"));
            ServiceResponse<Adrress> response = _adressService.GetAdrressByIdWillUpdate(userId, id);
            if (!response.IsError)
            {
                AdrressUpdateViewModel model = new AdrressUpdateViewModel();
                return View(_mapper.Map<AdrressUpdateViewModel>(response.Data));
            }
            foreach (string message in response.Errors)
                ModelState.AddModelError(string.Empty, message);

            return RedirectToAction("Adrress");
        }
        [HttpPost]
        public IActionResult AdrressUpdate(AdrressUpdateViewModel model)
        {
            if (!ModelState.IsValid) 
            {
                ModelState.AddModelError(string.Empty, "Form alanlarını eksiksiz doldurunuz.");
                return View(model);
            }
            else
            {
                ServiceResponse<bool> response = _adressService.UpdateAdrress(_mapper.Map<Adrress>(model));
                // Update işlemi başarılı ise.
                if (!response.IsError)
                {
                    TempData["SuccessMessage"] = "Adres bilgileri başarıyla güncellendi.";
                    return RedirectToAction("Adrress", "Profile");
                }

                foreach (string message in response.Errors)
                    ModelState.AddModelError(string.Empty, message);

                return View(model);
            }



        }


        [HttpGet]
        public IActionResult AdrressDelete(int id)
        {
            ServiceResponse<bool> response =  _adressService.RemoveAdrressById(id);
            if(!response.IsError)
            {
                TempData["SuccessMessage"] = "Kart bilgileri başarıyla silindi.";
                return RedirectToAction("Adrress", "Profile");
            }
            foreach (string message in response.Errors)
                ModelState.AddModelError(string.Empty, message);

            return RedirectToAction("Adrress");
        }

        #endregion
    }
}
