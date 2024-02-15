using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MyMechanic.Repositories.Interface
{
    public interface ICommonRepo<T> where T : class
    {
        public bool ExistUser(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);

        public void AddNew(T entity);

        public IEnumerable<T> GetAll(params Expression<Func<T, object>>[] includes);

        public T GetFirstOrDefault(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);

        public IEnumerable<T> GetRecordsWhere(Expression<Func<T, bool>> filter, params Expression<Func<T, object>>[] includes);

        public void Update(T entity);

        public void Save();

        public void DeleteField(T entity);
    }
}
