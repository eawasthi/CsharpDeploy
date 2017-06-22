using Microsoft.EntityFrameworkCore;
 
namespace WeddingPlanner.Models
{
    public class WeddingContext : DbContext
    {
        // base() calls the parent class' constructor passing the "options" parameter along
        public WeddingContext(DbContextOptions<WeddingContext> options) : base(options) { }

        public DbSet<User> Users {get; set;}
        public DbSet<WeddingCreator> Weddings {get; set;}
        public DbSet<Guest> Guests {get; set;}

    }
}