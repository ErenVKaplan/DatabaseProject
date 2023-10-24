using DatabaseProject.Entities;
using DatabaseProject.Services.Abstract;

namespace DatabaseProject.Services.Concrete
{
    public class BankService : IBankService
    {
        private readonly AppDbContext _context;

        public BankService(AppDbContext context)
        {
            _context = context;
        }
        public ServiceResponse<IEnumerable<Bank>> GetBanksByUserId(int userId)
        {
           ServiceResponse<IEnumerable<Bank>> response = new ServiceResponse<IEnumerable<Bank>>();

            var banks = _context.Banks.Where(b => b.UserId == userId).ToList();
            response.Data = banks;
            return response;
        }
    }
}
