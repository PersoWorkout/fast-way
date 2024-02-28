using Domain.Models;
using Microsoft.EntityFrameworkCore;

namespace Infrastructure.Data
{
    public class AuthDbContext: DbContext
    {
        public AuthDbContext(DbContextOptions<AuthDbContext> options) : 
            base(options) { }

        public DbSet<Session> Sessions { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<Session>()
                .HasKey(x => new { x.UserId, x.Token });
        }
    }
}
