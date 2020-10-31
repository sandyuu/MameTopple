using System.Threading.Tasks;
using MameToppleApi.Models;
using MameToppleApi.Models.ViewModels;

namespace MameToppleApi.Interfaces
{
    public interface IUserService
    {
        bool Create(User user);
        UserViewModel GetById(string account);
        Task<bool> LoginVerify(LoginViewModel loginVM);
    }
}