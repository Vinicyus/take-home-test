using Microsoft.EntityFrameworkCore;
using LoanManagement.Domain.Entities;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace LoanManagement.Infrastructure.Data
{
    public class LoanDbContext : DbContext
    {
        public LoanDbContext(DbContextOptions<LoanDbContext> options) : base(options)
        {
        }
        public DbSet<Loan> Loans { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed data
            modelBuilder.Entity<Loan>().HasData(
                new Loan { Id = 1, Amount = 1500.00m, CurrentBalance = 500.00m, ApplicantName = "Maria Silva", Status = "active" },
                new Loan { Id = 2, Amount = 2000.00m, CurrentBalance = 0.00m, ApplicantName = "João Santos", Status = "paid" },
                new Loan { Id = 3, Amount = 3500.00m, CurrentBalance = 1200.00m, ApplicantName = "Ana Costa", Status = "active" }
            );
        }
    }
}