using DataAccess.Interfaces;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class Repository<T> : IRepository<T> where T : class
    {
        private readonly IUnitOfWork _unitOfWork;
        protected DbSet<T> dbSet;
        protected DbContext dbContext;

        public Repository(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;

            if (_unitOfWork.Context != null)
            {
                dbContext = _unitOfWork.Context;
                dbSet = dbContext.Set<T>();
            }
        }

        public async Task<IEnumerable<T>> Get()
        {
            return await dbSet.ToListAsync();
        }

        public async Task<T> GetById(int id)
        {
            return await dbSet.FindAsync(id);
        }

        public async Task Add(T entity)
        {
            await dbSet.AddAsync(entity);
        }

        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        //public void Delete(T entity)
        //{
        //    dbSet.Remove(entity);
        //}

        public int SaveChanges()
        {
            return _unitOfWork.Context.SaveChanges();
        }

        public async Task<int> SaveChangesAsync()
        {
            return await _unitOfWork.Context.SaveChangesAsync();
        }
    }

}
