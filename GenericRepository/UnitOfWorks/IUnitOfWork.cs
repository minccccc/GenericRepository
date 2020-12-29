using GenericRepository.Repositories;
using System.Threading.Tasks;

namespace GenericRepository.UnitOfWorks
{
    public interface IUnitOfWork
    {
        /// <summary>
        /// Create a new respository of type "TEntity"
        /// </summary>
        /// <typeparam name="TEntity">Db Set Model</typeparam>
        /// <returns>New Repository of type "TEntity"</returns>
        IRepository<TEntity> Repository<TEntity>() where TEntity : class;

        void SaveChanges();

        Task SaveChangesAsync();
    }
}
