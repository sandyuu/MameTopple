using System;
using System.Linq;
using System.Threading.Tasks;
using MameToppleApi.Interfaces;
using MameToppleApi.Models;
using MameToppleApi.Models.ViewModels;
using MameToppleApi.Repository;
using MameToppleApi.Utility;
using Microsoft.EntityFrameworkCore;

namespace MameToppleApi.Service
{
    [DependencyInjection]
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
            if (UserExists(user.Account).Result)
                throw new ArgumentException($"Email:{user.Account} 已經被註冊");
            _genericRepository.Create(user);
        }

        /// <summary>
        /// 以Id取得資料
        /// </summary>
        /// <param name="account">主鍵值：帳號</param>
        /// <returns>找到的實體 或 null</returns>
        public UserViewModel GetById(string account)
        {
            var User = _genericRepository.GetOne(x => x.Account == account);
            var userVM = new UserViewModel
            {
                Account = User.Account,
                NickName = User.NickName,
                Avatar = User.Avatar,
                Win = User.Win,
                Lose = User.Lose
            };

            return userVM;
        }

        /// <summary>
        /// 使用者確認
        /// </summary>
        /// <param name="loginVM"></param>
        /// <returns>true or false</returns>
        public async Task<bool> LoginVerify(LoginViewModel loginVM)
        {
            var Users = await _genericRepository.GetAllAsync();
            var userExist = Users.Any(x => x.Account == loginVM.Account && x.Password == loginVM.Password);
            if (userExist)
            {
                return true;
            }
            return false;
        }

        private async Task<bool> UserExists(string accound)
        {
            var allUsers = await _genericRepository.GetAllAsync();
            var userExists = allUsers.Any(x => x.Account == accound);
            if (userExists)
                return true;
            return false;
        }
    }
}