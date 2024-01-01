using AbanChallenge.Models;
using Microsoft.EntityFrameworkCore;

namespace AbanChallenge.Infrastructure.Data
{
    #pragma warning disable CS1591 // Missing XML comment for publicly visible type or member
    public class AppDbContext : DbContext , IAppDbContext
    {
        public AppDbContext()
        {
        }
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<Cliente> Clientes { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder) { }
    }
}
