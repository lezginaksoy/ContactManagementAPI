using ContactManagementAPI.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System;

namespace ContactManagementAPI.Data
{

    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Fund> Funds { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<FundContact> FundContacts { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);
            modelBuilder.Entity<FundContact>()
                .HasKey(fc => fc.Id);  

            // 🌱 **Seeding Funds Data**
            modelBuilder.Entity<Fund>().HasData(
                new Fund { Id = 1, Name = "investorflow X Credit Fund" },
                new Fund { Id = 2, Name = "London 1X Credit Fund" },
                new Fund { Id = 3, Name = "London 2X Credit Fund" },
                new Fund { Id = 4, Name = "London 3X Credit Fund" },
                new Fund { Id = 5, Name = "ABC Hedge Fund" },
                new Fund { Id = 6, Name = "BAC Hedge Fund" },
                new Fund { Id = 7, Name = "Google Startup Fund" },
                new Fund { Id = 8, Name = "IBM Startup Fund" },
                new Fund { Id = 9, Name = "London XX Investment Fund" },
                new Fund { Id = 10, Name = "Bristol XX Investment Fund" }
            );
        }
    }

}

