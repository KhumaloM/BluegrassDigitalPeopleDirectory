using BluegrassDigitalPeopleDirectory.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace BluegrassDigitalPeopleDirectory.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }


        public DbSet<Person> People { get; set; }
        public DbSet<PersonProfilePicture> ProfilePictures { get; set; }
        public DbSet<Country> Countries { get; set; }
        public DbSet<City> Cities { get; set; }
        public DbSet<Gender> Genders { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Gender>().HasData(
                new Gender
                {
                    Id = 1,
                    Name = "Male"
                },
                new Gender
                {
                    Id = 2,
                    Name = "Female"
                }
            );

            modelBuilder.Entity<City>().HasData(
                new City { Id = 1, Name = "Johannesburg" }
            );

            modelBuilder.Entity<Country>().HasData(
                new Country { Id = 1, Name = "South Africa" }
            );

            base.OnModelCreating(modelBuilder);
        }
    }
}
