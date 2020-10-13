using System.Threading.Tasks;
using MameToppleApi.Models;
using MameToppleApi.Models.ViewModels;

namespace MameToppleApi.Interfaces
{
    public interface IUserService
    {
        void Create(User user);
        User GetById(string account);
        Task<bool> SignInCheck(LoginViewModel loginVM);
    }
}