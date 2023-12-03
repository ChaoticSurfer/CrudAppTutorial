using Microsoft.EntityFrameworkCore;

namespace CrudAppTutorial
{
    public class PersonDbContext : DbContext
    {
        public DbSet<Person> People { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite("Data Source = dataabse.db");
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
        }
    }
}
