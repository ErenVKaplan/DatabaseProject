using DatabaseProject.Entities;
using DatabaseProject.Services.Abstract;

namespace DatabaseProject.Services.Concrete
{
    public class KullaniciService : IKullaniciService
    {
        private readonly AppDbContext _context;
        
        public KullaniciService(AppDbContext context)
        {
            _context = context;
        }

        public ServiceResponse<bool> DeleteUserById(int id)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            var user = _context.Users.FirstOrDefault(x => x.Id == id);
            if (user != null)
            {
                _context.Users.Remove(user);
                if (_context.SaveChanges() == 0)
                {
                    response.AddError("Kullanıcı bilgileri silinirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
                    response.Data = false;
                }
                else
                {
                    response.AddSuccessMessage("Kullanıcı bilgileri başarıyla güncellendi.");
                    response.Data = true;
                }
            }
            else
            {
                response.AddError("Kullanıcı bulunamadı.");
                response.Data = false;
            }

            return response;
        }

        public ServiceResponse<User> GetKullaniciByEmail(string email)
        {
            ServiceResponse<User> response = new ServiceResponse<User>();

            User user=_context.Users.FirstOrDefault(u=> u.Email == email);
            if (user == null)
            {
                response.AddError("E-Posta veya şifreniz yanlış!");
                return response;
            }
            response.Data = user;
            return response;    
        }

        public ServiceResponse<User> UpdateUser(User user)
        {
            ServiceResponse<User> response = new ServiceResponse<User>();
            User user1 = _context.Users.FirstOrDefault(u=>u.Id == user.Id);

            if (user1 == null) 
            {
                response.AddError("Güncellenecek Kullanıcı Bulunamadı!");
                response.Data = user1;
                return response;
            }
            user1.Name = user.Name;
            user1.CellPhone = user.CellPhone;
            user1.BornDate = user.BornDate;
            user1.Surname = user.Surname;
            user1.TC=user.TC;
            _context.Users.Update(user1);
            if (_context.SaveChanges() == 0)
            {
                response.AddError("Kullanıcı bilgileri güncellenirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
                response.Data = user1;
                return response;
            }

            response.AddSuccessMessage("Kullanıcı bilgileri başarıyla güncellendi.");
            response.Data = user;
            return response;
        }
    }
}
