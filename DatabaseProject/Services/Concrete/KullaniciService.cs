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
    }
}
