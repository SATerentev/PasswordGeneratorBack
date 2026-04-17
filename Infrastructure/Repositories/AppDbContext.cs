using Microsoft.EntityFrameworkCore;
using PassGeneratorService.Domain.Entity;

namespace PassGeneratorService.Infrastructure.Repositories
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
            
        }

        public DbSet<Password> Passwords { get; set; }
    }
}
