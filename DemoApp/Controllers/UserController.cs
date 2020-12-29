using DemoApp.Data;
using DemoApp.Data.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace DemoApp.Controllers
{
    public class UserController : Controller
    {
        private readonly DemoDbContext dbContext;

        public UserController(DemoDbContext dbContext)
        {
            this.dbContext = dbContext;
        }

        public IActionResult Index()
        {
            return Ok();
        }

        public async Task<ActionResult> Add()
        {
            var newUser = new User()
            {
                Name = "New User",
                Email = "new_user@mail.com"
            };

            dbContext.Users.Add(newUser);

            await dbContext.SaveChangesAsync();

            return Ok("Added New User");
        }

        public async Task<ActionResult> Update(int id)
        {
            var user = await dbContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

            if(user == null)
            {
                return BadRequest("User not found");
            }

            user.Name += "_Updated";

            dbContext.Users.Update(user);

            await dbContext.SaveChangesAsync();

            return Ok("The user record is updated");
        }

        public async Task<ActionResult> Remove(int id)
        {
            var user = await dbContext.Users.Where(u => u.Id == id).FirstOrDefaultAsync();

            dbContext.Remove(user);

            await dbContext.SaveChangesAsync();

            return Ok("Last added user is removed");
        }

        public async Task<ActionResult> GetAll()
        {
            var result = await dbContext.Users.ToListAsync();

            return Ok(result);
        }

        public async Task<ActionResult> GetById(int id)
        {
            var user = await dbContext.Users.FindAsync(id);

            if(user == null)
            {
                return BadRequest("User not found");
            }

            return Ok(user);
        }

        public async Task<ActionResult> Get()
        {
            //get updated user records with even id number
            var sortedUsers = await dbContext.Users
                .Where(u => u.Name.EndsWith("_Updated") && u.Id % 2 == 0)
                .ToListAsync();

            return Ok(sortedUsers);
        }

    }
}
