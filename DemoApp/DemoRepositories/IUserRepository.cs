using DemoApp.Data.Models;
using GenericRepository.Repositories;

namespace DemoApp.DemoRepositories
{
    public interface IUserRepository : IRepository<User>
    {

        User ChangeEmail(int id, string email);

        int GetTotalUsersCount();
    }
}
