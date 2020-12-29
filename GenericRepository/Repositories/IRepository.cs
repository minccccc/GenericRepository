using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Threading.Tasks;

namespace GenericRepository.Repositories
{
    public interface IRepository<TEntity> where TEntity : class
    {
        void Add(TEntity entity);

        Task AddAsync(TEntity entity);


        void Update(TEntity entity);
        
        void Remove(TEntity entity);


        IEnumerable<TEntity> GetAll();

        Task<IEnumerable<TEntity>> GetAllAsync();


        TEntity GetById(object id);

        Task<TEntity> GetByIdAsync(object id);


        IEnumerable<TEntity> Get(Expression<Func<TEntity, bool>> predicate);

        Task<IEnumerable<TEntity>> GetAsync(Expression<Func<TEntity, bool>> predicate);
    }
}
