using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Linq.Expressions;
using DAL.Interfaces;

namespace DAL.Repositories
{
    public abstract class BaseRepository<T> : IRepository<T>
         where T : IEntity
    {
        protected UserContext db;

        public BaseRepository(UserContext context)
        {
            this.db = context;
        }

        public IEnumerable<T> GetAll()
        {
            return db.Set<T>();
        }

        public T GetById(int id)
        {
            return db.Set<T>().Find(id);
        }

        public void Create(T book)
        {
            db.Set<T>().Add(book);
        }

        public void Update(T book)
        {
            db.Entry(book).State = EntityState.Modified;
        }

        public IEnumerable<T> Find(Func<T, bool> predicate)
        {
            return db.Set<T>().Where(predicate).ToList();
        }

        public void Delete(int id)
        {
            T book = db.Set<T>().Find(id);
            if (book != null) db.Set<T>().Remove(book);
        }

        public IEnumerable<T> GetAllByPredicate(Expression<Func<T, bool>> predicate)
        {
            var visitor =
                new PredicateExpressionVisitor<T, T>(Expression.Parameter(typeof(T), predicate.Parameters[0].Name));
            var express = Expression.Lambda<Func<T, bool>>(visitor.Visit(predicate.Body), visitor.NewParameter);
            var final = db.Set<T>().Where(express).ToList();
            return final;
        }

        public T GetOneByPredicate(Expression<Func<T, bool>> predicate)
        {
            return GetAllByPredicate(predicate).FirstOrDefault();
        }

        public void Delete(T item)
        {
            db.Set<T>().Remove(item);
        }
    }
}