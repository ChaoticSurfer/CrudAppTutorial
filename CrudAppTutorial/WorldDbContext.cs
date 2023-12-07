using CrudAppTutorial.Models;
using Microsoft.EntityFrameworkCore;

namespace CrudAppTutorial
{
    public class WorldDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }
        public DbSet<Pet> Pets { get; set; }

        public WorldDbContext(DbContextOptions<WorldDbContext> options)
              : base(options)
        {
        }
    }
}
