using Microsoft.EntityFrameworkCore;

namespace CrudAppTutorial
{
    public class PersonDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
