using YatriiWorld.MVC.ViewModels.LoginRegister;
using YatriiWorld.MVC.ViewModels.Register;

namespace YatriiWorld.MVC.Services.Interfaces
{
    public interface IAccountClientService
    {
        Task RegisterAsync(RegisterVM model);
        Task<string> LoginAsync(LoginVM model);
        Task VerifyCodeAsync(string email, string code);
    }
}
