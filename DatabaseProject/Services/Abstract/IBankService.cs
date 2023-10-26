using DatabaseProject.Entities;
using DatabaseProject.Models;

namespace DatabaseProject.Services.Abstract
{
    public interface IBankService
    {
        ServiceResponse<IEnumerable<Bank>> GetBanksByUserId(int userId);

        ServiceResponse<bool> CreateBank(Bank model);
        ServiceResponse<bool> UpdateBank(Bank model);

        ServiceResponse<Bank>GetBankById(int kullaniciId,int bankId);
        ServiceResponse<bool>DeleteBankById(int bankId);
    }
}
