using System;
using System.Collections.Generic;
using System.Linq.Expressions;

namespace DAL.Interfaces
{
    public interface IRepository<T>
     where T : class
    {
        IEnumerable<T> GetAll();

        T GetById(int id);

        IEnumerable<T> Find(Func<T, Boolean> predicate);

        T GetOneByPredicate(Expression<Func<T, bool>> predicate);

        IEnumerable<T> GetAllByPredicate(Expression<Func<T, bool>> predicate);

        void Create(T item);

        void Update(T item);

        void Delete(T item);
    }
}
