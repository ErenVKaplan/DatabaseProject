using DatabaseProject.Entities;
using DatabaseProject.Models;

namespace DatabaseProject.Services.Abstract
{
    public interface IAdrressService
    {
        public ServiceResponse<IEnumerable<Adrress>> GetAdrressById(int Id);
        public ServiceResponse<bool> AdrressCreate(Adrress model);

        public ServiceResponse<Adrress> GetAdrressByIdWillUpdate(int kullaniciId, int adresid);

        public ServiceResponse<bool> UpdateAdrress(Adrress model);

        public ServiceResponse<bool> RemoveAdrressById(int id);
    }
}
