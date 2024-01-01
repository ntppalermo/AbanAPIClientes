using AbanChallenge.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;

namespace AbanChallenge_Test
{
    public class TestDbContext : AppDbContext
    {
        public TestDbContext(DbContextOptions<AppDbContext> options)
            : base(options)
        {
        }

        public TestDbContext(DbContextOptions options) : base(options)
        {
        }
    }


}