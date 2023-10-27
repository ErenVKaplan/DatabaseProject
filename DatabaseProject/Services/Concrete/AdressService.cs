using Azure;
using DatabaseProject.Entities;
using DatabaseProject.Models;
using DatabaseProject.Services.Abstract;
using Microsoft.EntityFrameworkCore;

namespace DatabaseProject.Services.Concrete
{
    public class AdressService : IAdrressService
    {
        private readonly AppDbContext _context;

        public AdressService(AppDbContext context)
        {
            _context = context;
        }

        public ServiceResponse<bool> AdrressCreate(Adrress model)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            User? user = _context.Users
                .Include(b => b.Adrresses)
                .FirstOrDefault(u => u.Id == model.UserId);
           
            user.Adrresses.Add(model);

            if (_context.SaveChanges() == 0)
            {
                response.AddError("Adres bilgileri eklenirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
                response.Data = false;
                return response;
            }

            response.AddSuccessMessage("Adres  başarıyla eklendi.");
            response.Data = true;
            return response;
        }

        public ServiceResponse<IEnumerable<Adrress>> GetAdrressById(int Id)
        {
            ServiceResponse<IEnumerable<Adrress>> response = new ServiceResponse<IEnumerable<Adrress>>();
            var list=_context.Adrresses.Where(u=>u.UserId== Id).ToList();
            response.Data = list;
            return response;
        }

        public ServiceResponse<Adrress> GetAdrressByIdWillUpdate(int kullaniciId, int adresid)
        {
            ServiceResponse<Adrress> response=new ServiceResponse<Adrress>();
            var adrress=_context.Adrresses.Where(u=>u.UserId == kullaniciId).FirstOrDefault(a=>a.AdrressId==adresid);
            if (adrress != null)
            {
                response.Data = adrress;
            }
            else
            {
                response.IsError = true;
                response.AddError("Istenilen Adres Bilgileri Bulunamadı.");
            }

            return response;
        }

        public ServiceResponse<bool> RemoveAdrressById(int id)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            var adrress=_context.Adrresses.Where(a=>a.AdrressId==id).FirstOrDefault();

            if (adrress != null) 
            {
                _context.Adrresses.Remove(adrress);
                if (_context.SaveChanges() == 0)
                {
                    response.AddError("Adres bilgileri silinirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
                    response.Data = false;
                    return response;
                }
                response.AddSuccessMessage("Adres bilgisi başarıyla silindi.");
                response.Data = true;
                return response;

            }
            else
            {
                response.AddError("Silinmek istenen banka bilgisi bulunamadı. Lütfen daha sonra tekrar deneyin.");
                response.Data = false;
                return response;
            }
        }

        public ServiceResponse<bool> UpdateAdrress(Adrress model)
        {
            ServiceResponse<bool> response = new ServiceResponse<bool>();
            Adrress? adrress=_context.Adrresses.Where(a=>a.AdrressId == model.AdrressId).FirstOrDefault();

            if (adrress != null)
            {
                adrress.AdrressName = model.AdrressName;
                adrress.AdrressDescription = model.AdrressDescription;
                if (_context.SaveChanges() == 0)
                {
                    response.AddError("Adres bilgileri güncellenirken bir hata oluştu. Lütfen daha sonra tekrar deneyin.");
                    response.Data = false;
                    return response;
                }
                response.AddSuccessMessage("Adres bilgisi başarıyla güncellendi.");
                response.Data = true;
                return response;
            }
            else
            {
                response.AddError("Güncellenmek istenen Adres bilgisi bulunamadı. Lütfen daha sonra tekrar deneyin.");
                response.Data = false;
                return response;
            }
        }
    }
}
