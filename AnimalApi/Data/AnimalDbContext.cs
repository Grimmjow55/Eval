using AnimalApi.Models;
using Microsoft.EntityFrameworkCore;

namespace AnimalApi.Data
{
    public class AnimalDbContext : DbContext
    {
        public AnimalDbContext(DbContextOptions<AnimalDbContext> options) : base(options) { }

        public DbSet<Animal> Animals { get; set; }
        public DbSet<Race> Races { get; set; }
        public DbSet<User> Users { get; set; }
    }
}
