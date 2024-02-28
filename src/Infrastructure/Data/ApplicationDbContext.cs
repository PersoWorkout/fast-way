using Domain.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Data
{
    public class ApplicationDbContext: DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) :
            base(options) { }

        public DbSet<User> Users { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder
                .Entity<User>()
                .HasKey(x => x.Id);

            modelBuilder
                .Entity<User>()
                .ComplexProperty(x => x.Email)
                .Property(x => x.Value)
                .HasColumnName("email");

            modelBuilder
                .Entity<User>()
                .ComplexProperty(x => x.Password)
                .Property(x => x.Value)
                .HasColumnName("password");
        }
    }
}
