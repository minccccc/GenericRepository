using GenericRepository.Repositories;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections;
using System.Threading.Tasks;

namespace GenericRepository.UnitOfWorks
{
    internal class UnitOfWork : IUnitOfWork
    {
        private Hashtable repositories;
        private readonly DbContext dbContext;

        public UnitOfWork(DbContext dbContext)
        {
            this.dbContext = dbContext;
            repositories = new Hashtable();
        }


        public IRepository<TEntity> Repository<TEntity>() where TEntity : class
        {
            string type = typeof(TEntity).Name;

            if (!repositories.ContainsKey(type))
            {
                Type repositoryType = typeof(Repository<>);

                object repositoryInstance =
                    Activator.CreateInstance(repositoryType.MakeGenericType(typeof(TEntity)), dbContext);

                repositories.Add(type, repositoryInstance);
            }

            return (IRepository<TEntity>)repositories[type];
        }

        public void SaveChanges()
        {
            dbContext.SaveChanges();
        }

        public async Task SaveChangesAsync()
        {
            await dbContext.SaveChangesAsync();
        }

    }
}
