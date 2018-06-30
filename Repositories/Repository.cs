using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;

namespace MJIot.Storage.Models.Repositiories
{
    public interface IRepository<T> where T : class
    {
        T Get(int id);
        IEnumerable<T> GetAll();

        void Remove(int id);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void RemoveAll();

        void Add(T entity);
        void AddRange(IEnumerable<T> entities);

        T Find(Expression<Func<T, bool>> predicate);
    }

    public abstract class Repository<T> : IRepository<T> where T : class
    {
        protected readonly DbContext _context;

        public Repository(DbContext context)
        {
            _context = context;
        }

        public void Add(T entity)
        {
            _context.Set<T>().Add(entity);
        }

        public void AddRange(IEnumerable<T> entities)
        {
            _context.Set<T>().AddRange(entities);
        }

        public T Find(Expression<Func<T, bool>> predicate)
        {
            return _context.Set<T>().Find(predicate);
        }

        public T Get(int id)
        {
            return _context.Set<T>().Find(id);

            //I wanted to add includes option, so that Navigation Properties could be also fetced, but it's problematic
            //IQueryable<T> query = _context.Set<T>();
            //if (includes != null)
            //    foreach (Expression<Func<T, object>> include in includes)
            //        query = query.Include(include);

            //return ((DbSet<T>)query).Find(id);
        }

        public IEnumerable<T> GetAll()
        {
            return _context.Set<T>().ToList();
        }

        public void Remove(int id)
        {
            _context.Set<T>().Remove(Get(id));
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(IEnumerable<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public void RemoveAll()
        {
            var entities = GetAll();
            RemoveRange(entities);
        }
    }
}
