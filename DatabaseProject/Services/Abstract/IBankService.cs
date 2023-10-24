using DatabaseProject.Entities;

namespace DatabaseProject.Services.Abstract
{
    public interface IBankService
    {
        ServiceResponse<IEnumerable<Bank>> GetBanksByUserId(int userId);
    }
}
