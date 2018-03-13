using Microsoft.EntityFrameworkCore;
using PersonRegistration.Domain.Persons;

namespace PersonRegistration.Infrastructure.Data
{
    public class PersonDbContext : DbContext
    {
        public PersonDbContext(DbContextOptions<PersonDbContext> options)
            : base(options)
        { }

        public virtual DbSet<Person> Persons { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>()
                .Ignore(b => b.Errors);
        }
    }
}
