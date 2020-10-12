using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MameToppleApi.Interfaces;
using MameToppleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MameToppleApi.Repository
{
    public class TestUserRepository : IRepository<User, string>
    {
        private readonly ToppleDBContext _context;
        public TestUserRepository(ToppleDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">實體</param>
        public void Create(User entity)
        {
            _context.Users.Add(entity);
            _context.SaveChanges();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">實體</param>
        public void Update(User entity)
        {
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="account">主鍵值</param>
        public void Delete(string account)
        {
            _context.Users.Remove(_context.Users.Single(x => x.Account == account));
            _context.SaveChanges();
        }

        /// <summary>
        /// 取得全部
        /// </summary>
        /// <returns>List<Models.User></returns>
        public List<User> GetAll()
        {
            return _context.Users.ToList();
        }

        /// <summary>
        /// 取得單筆
        /// </summary>
        /// <param name="id">主鍵值</param>
        /// <returns>Models.User</returns>
        public User GetById(string account)
        {
            return _context.Users.Find(account);
        }
    }
}