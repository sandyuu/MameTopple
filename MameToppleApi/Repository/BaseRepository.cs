using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using MameToppleApi.Interfaces;
using MameToppleApi.Models;
using Microsoft.EntityFrameworkCore;

namespace MameToppleApi.Repository
{
    public abstract class BaseRepository<TEntity, TKey> : IRepository<TEntity, TKey> where TEntity : class
    {
        private readonly ToppleDBContext _context;
        public BaseRepository(ToppleDBContext context)
        {
            _context = context;
        }

        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">實體</param>
        public void Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Set<TEntity>().Add(entity); ;
            _context.SaveChanges();
        }

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">實體</param>
        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Entry(entity).State = EntityState.Modified;
            _context.SaveChanges();
        }

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="account">主鍵值</param>
        public void Delete(TKey id)
        {
            if (id == null)
            {
                throw new ArgumentNullException("entity");
            }
            _context.Set<TEntity>().Remove(_context.Set<TEntity>().Find(id));
            _context.SaveChanges();
        }

        /// <summary>
        /// 取得全部
        /// </summary>
        /// <returns>List<Models.User></returns>
        public List<TEntity> GetAll()
        {
            return _context.Set<TEntity>().ToList();
        }

        /// <summary>
        /// 取得單筆
        /// </summary>
        /// <param name="id">主鍵值</param>
        /// <returns>Models.User</returns>
        public TEntity GetById(TKey id)
        {
            return _context.Set<TEntity>().Find(id);
        }
    }
}