using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;

namespace GenericRepository.Repositories
{
    public class RepositoryBase<TEntity> : Repository<TEntity> where TEntity : class
    {
        private readonly DbContext dbContext;

        public RepositoryBase(DbContext dbContext)
            :base(dbContext)
        {
            this.dbContext = dbContext;
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
