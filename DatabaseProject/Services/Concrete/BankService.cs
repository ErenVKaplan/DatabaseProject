using Azure;
using DatabaseProject.Entities;
using DatabaseProject.Models;
using DatabaseProject.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DatabaseProject.Services.Concrete
{
    public class BankService : IBankService
    {
        private readonly AppDbContext _context;

        public BankService(AppDbContext context)
        {
            _context = context;
        }

        public ServiceResponse<bool> CreateBank(Bank model)
        {
           ServiceResponse<bool> response = new ServiceResponse<bool>();
            User? user = _context.Users
                .Include(b => b.Banks)
                .FirstOrDefault(u => u.Id == model.UserId);

            user.Banks.Add(model);
            if (_context.SaveChanges() == 0)
            {
                response.AddError("Kart bilgileri eklenirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
                response.Data = false;
                return response;
            }

            response.AddSuccessMessage("Kart bilgisi başarıyla eklendi.");
            response.Data = true;
            return response;
        }

        public ServiceResponse<bool> DeleteBankById(int BankId)
        {
            ServiceResponse<bool> response= new ServiceResponse<bool>(); 
            var bank = _context.Banks.Where(b=>b.BankId == BankId).FirstOrDefault();
            _context.Banks.Remove(bank);
            
            if (bank == null)
            {
                response.AddError("Silinmek istenen kart bilgisine erişilemedi.");
                response.Data = false;
                return response;
            }
            if (_context.SaveChanges() == 0)
            {
                response.AddError("Kart bilgisi silinirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
                response.Data = false;
                return response;
            }

            response.AddSuccessMessage("Kart bilgisi başarıyla silindi.");
            response.Data = true;
            return response;
        }

        public ServiceResponse<Bank> GetBankById(int kullaniciId, int bankId)
        {
            ServiceResponse<Bank> response = new ServiceResponse<Bank>();
            var bank = _context.Banks.Where(u => u.UserId == kullaniciId).FirstOrDefault(b => b.BankId == bankId);
            if(bank!=null)
            {
                response.Data = bank;
            }
            else
            {
                response.IsError = true;
                response.AddError("Istenilen Kart Bilgileri Bulunamadı.");
            }

            return response;
        }

        public ServiceResponse<IEnumerable<Bank>> GetBanksByUserId(int userId)
        {
           ServiceResponse<IEnumerable<Bank>> response = new ServiceResponse<IEnumerable<Bank>>();

            var banks = _context.Banks.Where(b => b.UserId == userId).ToList();
            response.Data = banks;
            return response;
        }

        public ServiceResponse<bool> UpdateBank(Bank model)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            
            var bank=_context.Banks.Where(b=>b.BankId == model.BankId).FirstOrDefault();
            
            if (bank == null)
            {
                response.AddError("Güncellenmek istenen banka bilgisi bulunamadı. Lütfen daha sonra tekrar deneyin.");
                response.Data = false;
                return response;
            }

            bank.BankName = model.BankName;
            bank.CardNo= model.CardNo;


            if (_context.SaveChanges() == 0)
            {
                response.AddError("Kart bilgileri güncellenirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
                response.Data = false;
                return response;
            }

            response.AddSuccessMessage("Kart bilgisi başarıyla güncellendi.");
            response.Data = true;
            return response;
        }
    }
}
