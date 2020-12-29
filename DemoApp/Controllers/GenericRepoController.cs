using DemoApp.Data.Models;
using GenericRepository.Repositories;
using GenericRepository.UnitOfWorks;
using Microsoft.AspNetCore.Mvc;
using System.Threading.Tasks;

namespace DemoApp.Controllers
{
    public class GenericRepoController : Controller
    {

        private readonly IUnitOfWork unitOfWork;
        private readonly IRepository<User> userRepository;

        public GenericRepoController(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;
            this.userRepository = unitOfWork.Repository<User>();
        }

        public async Task<ActionResult> Add()
        {
            var newUser = new User()
            {
                Name = "New User",
                Email = "new_user@mail.com"
            };

            this.userRepository.Add(newUser);

            await unitOfWork.SaveChangesAsync();

            return Ok("Added New User");
        }

        public async Task<ActionResult> Update(int id)
        {
            var user = await userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            user.Name += "_Updated";

            userRepository.Update(user);

            await unitOfWork.SaveChangesAsync();

            return Ok("The user record is updated");
        }

        public async Task<ActionResult> Remove(int id)
        {
            var user = await userRepository.GetByIdAsync(id);
            
            userRepository.Remove(user);

            await unitOfWork.SaveChangesAsync();

            return Ok("The user is removed");
        }

        public async Task<ActionResult> GetAll()
        {
            var result = await userRepository.GetAllAsync();

            return Ok(result);
        }

        public async Task<ActionResult> GetById(int id)
        {
            var user = await this.userRepository.GetByIdAsync(id);

            if (user == null)
            {
                return BadRequest("User not found");
            }

            return Ok(user);
        }

        public async Task<ActionResult> Get()
        {
            //get updated user records with even id number
            var sortedUsers = await userRepository
                .GetAsync(u => u.Name.EndsWith("_Updated") && u.Id % 2 == 0);

            return Ok(sortedUsers);
        }

    }
}
