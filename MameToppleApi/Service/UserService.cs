using System.Linq;
using System.Threading.Tasks;
using MameToppleApi.Interfaces;
using MameToppleApi.Models;
using MameToppleApi.Models.ViewModels;
using MameToppleApi.Repository;
using Microsoft.EntityFrameworkCore;

namespace MameToppleApi.Service
{
    public class UserService : IUserService
    {
        private readonly IRepository<User> _genericRepository;
        public UserService(IRepository<User> genericRepository)
        {
            _genericRepository = genericRepository;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="user">user資料表</param>
        public void Create(User user)
        {
            _genericRepository.Create(user);
        }

        /// <summary>
        /// 以Id取得資料
        /// </summary>
        /// <param name="account">主鍵值：帳號</param>
        /// <returns>找到的實體 或 null</returns>
        public User GetById(string account)
        {
            return _genericRepository.GetOne(x => x.Account == account);
        }

        /// <summary>
        /// 使用者確認
        /// </summary>
        /// <param name="loginVM"></param>
        /// <returns>true or false</returns>
        public async Task<bool> SignInCheck(LoginViewModel loginVM)
        {
            var Users = await _genericRepository.GetAllAsync();
            var hasUser = Users.Any(x => x.Account == loginVM.Username && x.Password == loginVM.Password);
            if (hasUser)
            {
                return true;
            }
            return false;
        }
    }
}