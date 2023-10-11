using DatabaseProject.Entities;
using DatabaseProject.Models;

namespace DatabaseProject.Services.Abstract
{
    public interface ILoginService
    {
        ServiceResponse<User> Login(LoginViewModel model);
        ServiceResponse<bool> Register(RegisterViewModel model);
    }
}
