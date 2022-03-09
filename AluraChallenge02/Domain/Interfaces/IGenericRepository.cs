using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace Challenge02.Domain.Interfaces
{
    public interface IGenericRepository<T> where T : class

    {
        Task Add(T entity);
        Task AddRange(IEnumerable<T> entities);
        Task<IEnumerable<T>> Find(Expression<Func<T, bool>> expression);
        Task<IEnumerable<T>> GetAll();
        Task<T> GetById(int id);
        void Remove(T entity);
        void RemoveRange(IEnumerable<T> entities);
        void Update(T entity);
        void UpdateRange(IEnumerable<T> entities);
    }
}