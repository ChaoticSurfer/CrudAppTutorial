using Microsoft.EntityFrameworkCore;

namespace CrudAppTutorial
{
    public class PersonDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        public PersonDbContext(DbContextOptions<PersonDbContext> options)
              : base(options)
        {
        }
    }
}
