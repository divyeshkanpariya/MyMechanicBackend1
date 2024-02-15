using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Internal;
using MyMechanic.Entities.Models;
using MyMechanic.Repositories.Interface;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.Repositories.Repository
{
    public class CommonRepo<T>: ICommonRepo<T> where T : class
    {
        private readonly MyMechanicDbContext _context;

        public DbSet<T> dbSet;

        public CommonRepo(MyMechanicDbContext context)
        {
            _context = context;
            this.dbSet = _context.Set<T>();
        }

        public void AddNew(T entity)
        {
            dbSet.Add(entity);
        }

        public void Save()
        {
            _context.SaveChanges();
        }

        public bool ExistUser(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.Any(filter);
        }
        
        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            // Apply the filter
            query = query.Where(filter);
            return query.FirstOrDefault()!;
        }
        public void Update(T entity)
        {
            dbSet.Update(entity);
        }

        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }
            return query.ToList();
        }

        public IEnumerable<T> GetRecordsWhere(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes)
        {
            IQueryable<T> query = dbSet;
            foreach (var include in includes)
            {
                query = query.Include(include);
            }

            query = query.Where(filter);
            return query.ToList();
        }
        public void DeleteField(T entity)
        {
            dbSet.Remove(entity);
        }
    }
}
