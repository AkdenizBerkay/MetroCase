using MetroCase.DataAccessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace MetroCase.BusinessLayer
{
    public class Manager<T> : IRepository<T> where T : class
    {
        private EFRepository<T> cef = new EFRepository<T>();

        public virtual void Add(T entity)
        {
            cef.Add(entity);
        }

        public void Delete(T entity)
        {
            cef.Delete(entity);
        }

        public void Delete(int id)
        {
            cef.Delete(id);
        }

        public T Get(Expression<Func<T, bool>> predicate)
        {
            return cef.Get(predicate);
        }


        public IQueryable<T> GetAll()
        {
            return cef.GetAll();
        }

        public IQueryable<T> GetAll(Expression<Func<T, bool>> predicate)
        {
            return cef.GetAll(predicate);
        }

        public T GetById(int id)
        {
            return cef.GetById(id);
        }

        public void Update(T entity)
        {
            cef.Update(entity);
        }
    }
}
