using DatabaseProject.Entities;

namespace DatabaseProject.Services.Abstract
{
    public interface IKullaniciService
    {
        public ServiceResponse<User> GetKullaniciByEmail(string email);
    }
}
