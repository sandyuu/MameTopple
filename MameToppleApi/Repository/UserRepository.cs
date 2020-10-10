using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;
using MameToppleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MameToppleApi.Repository
{
    public class UserRepository : IRepository<User, string>
    {
        private readonly ToppleDBContext _context;
        public UserRepository(ToppleDBContext context)
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
        public async Task<List<User>> GetAllAsync()
        {
            return await _context.Users.ToListAsync();
        }

        /// <summary>
        /// 取得單筆
        /// </summary>
        /// <param name="id">主鍵值</param>
        /// <returns>Models.User</returns>
        public async Task<User> GetByIdAsync(string account)
        {
            return await _context.Users.FindAsync(account);
        }
    }
}