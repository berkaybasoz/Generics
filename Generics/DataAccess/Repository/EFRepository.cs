using DataAccess.Context;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DataAccess.Repository
{
    public class EFRepository<T> : IRepository<T> where T : class
    {
        private readonly BaseContext _dbContext;
        private readonly DbSet<T> _dbSet;

        public EFRepository(BaseContext dbContext)
        {
            if (dbContext == null)
                throw new ArgumentNullException("dbContext can not be null");

            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }


        public IQueryable<T> GetAll()
        {
            return _dbSet;
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Where(predicate);
        }

        public T GetById(int id)
        {
            return _dbSet.Find(id);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return _dbSet.Find(predicate);
        }

        public void Add(T entity)
        {
            _dbSet.Add(entity);
        }

        public void Delete(int id)
        {
            var entity = GetById(id);
            if (entity == null)
                return;

            Delete(entity);
        }

        public void Delete(T entity)
        {
            DbEntityEntry dbEntityEntry = _dbContext.Entry(entity);

            if (dbEntityEntry.State != EntityState.Deleted)
            {
                dbEntityEntry.State = EntityState.Deleted;
            }
            else
            {
                _dbSet.Attach(entity);
                _dbSet.Remove(entity);
            }
        }

        public void Update(T entity)
        {
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;

        }
    }
}
