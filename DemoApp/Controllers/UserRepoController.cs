using DemoApp.DemoRepositories;
using Microsoft.AspNetCore.Mvc;

namespace DemoApp.Controllers
{
    public class UserRepoController : Controller
    {

        private readonly IUserRepository userRepository;

        public UserRepoController(IUserRepository userRepository)
        {
            this.userRepository = userRepository;
        }

        public ActionResult ChangeEmail(int id)
        {
            var user = this.userRepository.ChangeEmail(id, "changed_email@mail.com");

            return Ok(user);
        }


        public ActionResult GetTotalUsersCount()
        {
            var usersCount = this.userRepository.GetTotalUsersCount();

            return Ok(usersCount);
        }

    }
}
