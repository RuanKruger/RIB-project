using AcmeSoft.Core.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;
using System.Reflection.Emit;

namespace AcmeSoft.Infrastructure.Data
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options) { }

        public DbSet<Person> Person { get; set; }
        public DbSet<Employee> Employee { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Person>(entity =>
            {
                entity.HasKey(e => e.PersonId);
                entity.Property(e => e.LastName).IsRequired().HasMaxLength(128);
                entity.Property(e => e.FirstName).IsRequired().HasMaxLength(128);
                entity.Property(e => e.BirthDate).IsRequired();

                entity.HasMany(p => p.Employees)
                    .WithOne(e => e.Person)
                    .HasForeignKey(e => e.PersonId);
            });

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.HasKey(e => e.EmployeeId);
                entity.Property(e => e.EmployeeNum).IsRequired().HasMaxLength(16);
                entity.Property(e => e.EmployeeDate).IsRequired();

                entity.HasOne(e => e.Person)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(e => e.PersonId);
            });
        }
    }
}