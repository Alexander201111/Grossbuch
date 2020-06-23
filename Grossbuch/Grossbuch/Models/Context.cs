using Microsoft.EntityFrameworkCore;

namespace Grossbuch.Models
{
    public class Context : DbContext
    {
        private readonly string databasePath;

        public DbSet<Account> Accounts { get; set; }
        public DbSet<Category> Categories { get; set; }
        public DbSet<Currency> Currencies { get; set; }
        public DbSet<Operation> Operations { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Aim> Aims { get; set; }
        public DbSet<Debt> Debts { get; set; }

        public DbSet<Product> Products { get; set; }

        public Context(string _databasePath)
        {
            databasePath = _databasePath;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlite($"Filename={databasePath}");
        }
    }
}