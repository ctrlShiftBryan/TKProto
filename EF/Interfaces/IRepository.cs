using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq.Expressions;

namespace ACRL.EFData.Interfaces
{
    public interface IRepository<T> where T : class
    {
        void Add(T entity);
        void Update(T entity);
        void Delete(T entity);
        void Delete(Expression<Func<T, bool>> where);
      
        T GetById(string Id);
        T GetById(Guid Id);

        T Get(Expression<Func<T, bool>> where);
        IEnumerable<T> GetAll();
        IEnumerable<T> GetMany(Expression<Func<T, bool>> where);


        DbContext DbContext { get; }
        DbEntityEntry<T> Entry(T entity);
        IDbSet<T> DbSet { get; set; }
        void Save();
    }
}
