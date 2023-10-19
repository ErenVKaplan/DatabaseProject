using DatabaseProject.Entities;

namespace DatabaseProject.Services.Abstract
{
    public interface IKullaniciService
    {
        public ServiceResponse<User> GetKullaniciByEmail(string email);
        public ServiceResponse<User> UpdateUser(User user);
        public ServiceResponse<bool>DeleteUserById(int id);

    }
}
