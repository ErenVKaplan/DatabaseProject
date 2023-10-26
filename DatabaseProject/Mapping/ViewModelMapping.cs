using AutoMapper;
using DatabaseProject.Entities;
using DatabaseProject.Models;

namespace DatabaseProject.Mapping
{
    public class ViewModelMapping : Profile
    {
        public ViewModelMapping()
        {
            // map işlemi yapılacak sınıflar burada ayarlanır.

            //CreateMap<Product, ProductViewModel>().ReverseMap();
            //CreateMap<Visitor, VisitorViewModel>().ReverseMap();
            
            CreateMap<User,LoginViewModel>().ReverseMap();
            CreateMap<User,RegisterViewModel>().ReverseMap();
            CreateMap<User,ProfileUpdateViewModel>().ReverseMap();



            CreateMap<Bank, BankCreateViewModel>().ReverseMap();
            CreateMap<Bank, BankUpdateViewModel>().ReverseMap();
        }
    }
}
