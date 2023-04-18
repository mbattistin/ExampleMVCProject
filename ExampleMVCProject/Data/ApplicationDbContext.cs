using ExampleMVCProject.Models;
using Microsoft.EntityFrameworkCore;

namespace ExampleMVCProject.Data
{
    // To migrate this database to your local database, run the commands:
    // add-migration <migration name>
    // update-database
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        public DbSet<Item> Items { get; set; }
        public DbSet<Expense> Expenses { get; set; }
    }
}
