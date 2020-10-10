using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;
/// <summary>
/// Repository的interface
/// </summary>
namespace MameToppleApi.Repository
{
    public interface IRepository<TEntity, TKey> where TEntity : class
    {
        /// <summary>
        /// 新增
        /// </summary>
        /// <param name="entity">實體</param>
        void Create(TEntity entity); //void可改TKey


        /// <summary>
        /// 取得全部
        /// </summary>
        /// <returns></returns>
        Task<ICollection<TEntity>> GetAllAsync();

        /// <summary>
        /// 取得單筆
        /// </summary>
        /// <param name="expression">查詢條件</param>
        /// <returns></returns>
        Task<TEntity> GetAsync(Expression<Func<TEntity, bool>> expression);

        /// <summary>
        /// 刪除
        /// </summary>
        /// <param name="id">主鍵值</param>
        void Delete(TKey id);

        /// <summary>
        /// 更新
        /// </summary>
        /// <param name="entity">實體</param>
        void Update(TEntity entity);
    }
}