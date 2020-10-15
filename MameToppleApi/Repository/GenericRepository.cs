﻿using MameToppleApi.Models;
using MameToppleApi.Utility;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace MameToppleApi.Repository
{
    [DependencyInjection]
    public class GenericRepository<TEntity> : IRepository<TEntity> where TEntity : class
    {
        private ToppleDBContext _context;
        protected ToppleDBContext Context { get { return _context; } }
        public GenericRepository(ToppleDBContext context)
        {
            if (context == null)
            {
                _context = new ToppleDBContext();
            }
            _context = context;
        }
        public void Create(TEntity entity)
        {
            if (entity == null)
            {
                throw new NotImplementedException();
            }
            _context.Entry(entity).State = EntityState.Added;
            _context.SaveChanges();
        }

        public void Delete(TEntity entity)
        {
            if (entity == null)
            {
                throw new NotImplementedException();
            }
            _context.Entry(entity).State = EntityState.Deleted;
        }

        public void Dispose()
        {
            throw new NotImplementedException();
        }

        public async Task<IEnumerable<TEntity>> GetAllAsync()
        {
            return await _context.Set<TEntity>().ToListAsync();
        }

        public TEntity GetOne(Expression<Func<TEntity, bool>> expression)
        {
            return _context.Set<TEntity>().Single(expression);
        }

        public void Update(TEntity entity)
        {
            if (entity == null)
            {
                throw new NotImplementedException();
            }
            _context.Entry(entity).State = EntityState.Modified;
        }
    }
}
