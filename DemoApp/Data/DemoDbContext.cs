using DemoApp.Data.Models;
using Microsoft.EntityFrameworkCore;

namespace DemoApp.Data
{
    public class DemoDbContext : DbContext
    {

        public DemoDbContext(DbContextOptions<DemoDbContext> options)
            :base(options)
        { }

        public DbSet<User> Users { get; set; }
        protected override void OnModelCreating(ModelBuilder builder)
        {

            builder
                .Entity<User>()
                .HasKey(e => e.Id);

            base.OnModelCreating(builder);
        }


    }
}
