using DatabaseProject.Entities;
using DatabaseProject.Models;
using DatabaseProject.Services.Abstract;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using System.Runtime.InteropServices;

namespace DatabaseProject.Services.Concrete
{
    public class LoginService : ILoginService
    { //DI Yapacagiz
        private readonly AppDbContext _context;
        public LoginService(AppDbContext context)
        {
            _context = context;
        }

        public ServiceResponse<User> Login(LoginViewModel model)
        {
            ServiceResponse<User> response = new ServiceResponse<User>();
            User? user;
            if (model != null)
            {
                user = _context.Users.Where(x => x.Email == model.Email && x.Password == model.Password).FirstOrDefault();
            }
            else
            {
                response.AddError("E-Posta veya şifrenizi kontrol ediniz!");
                return response;
            }
            response.Data = user;
            return response;
        }

        public ServiceResponse<bool> Register(RegisterViewModel model)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            if (_context.Users.Any(x => x.Email == model.Email))
            {
                response.AddError("Girdiğiniz E-Posta adresi zaten kullanılmakta.");
                response.Data = false;
                return response;
            }
            //Buraya daha yapilacak kosullar var ise eklenebílir 
            return Create(model);
        }
        public ServiceResponse<bool> Create(RegisterViewModel model)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            User user = new User()
            {
                Name = model.Name,
                Surname = model.Surname,
                Email = model.Email.Trim(),
                Password = model.Password,
                Role = "User",
                Adrress = model.Adrress,
                CellPhone = model.CellPhone.Trim(),
                TC = model.TC.Trim(),
                BornDate = model.BornDate,

            };
            _context.Users.Add(user);
            if(_context.SaveChanges()==0) 
            {
                response.AddError("Kayıt olurken bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
                response.Data = false;
                return response;
            }
            response.AddSuccessMessage("Kayıt işlemi başarıyla tamamlandı.");
            response.Data = true;
            return response;
        }
    }
}

