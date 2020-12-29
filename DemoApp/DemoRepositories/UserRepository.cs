using DemoApp.Data;
using DemoApp.Data.Models;
using GenericRepository.Repositories;
using System.Linq;

namespace DemoApp.DemoRepositories
{
    public class UserRepository : RepositoryBase<User>, IUserRepository
    {
        public UserRepository(DemoDbContext dbContext)
            : base(dbContext)
        { }

        public User ChangeEmail(int id, string email)
        {
            var user = this.GetById(id);
            user.Email = email;

            this.Update(user);

            this.SaveChanges();

            return user;
        }

        public int GetTotalUsersCount()
        {
            return this.GetAll().Count();
        }
    }
}
