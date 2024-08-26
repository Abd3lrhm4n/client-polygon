using Domain.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection.Emit;
using System.Text;
using System.Threading.Tasks;

namespace Infrastructure.Context
{
    public class ApplicationDbContext : DbContext
    {
        public DbSet<Client> Clients { get; set; }
        public DbSet<Polygon> Polygons { get; set; }
        public DbSet<EmailQueue> EmailQueues { get; set; }

        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Client>(c =>
            {
                c.HasKey(c => c.Id);
                c.Property(e => e.Id).ValueGeneratedOnAdd();
            });

            modelBuilder.Entity<Client>()
                .HasIndex(c => c.Email)
                .IsUnique();

            modelBuilder.Entity<Polygon>(c =>
            {
                c.HasKey(c => c.Id);
            });

            modelBuilder.Entity<EmailQueue>(e =>
            {
                e.HasKey(e => e.Id);
            });

        }
    }
}
