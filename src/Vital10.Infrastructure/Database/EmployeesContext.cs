using System;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

#nullable disable

namespace Vital10.Infrastructure.Database
{
    public partial class EmployeesContext : DbContext
    {
        private string _connectionString;
        public EmployeesContext(string connectionString)
        {
            _connectionString = connectionString;
        }

        public EmployeesContext(DbContextOptions<EmployeesContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; }
        public virtual DbSet<Partner> Partners { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseSqlServer(_connectionString);
            }
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.HasAnnotation("Relational:Collation", "SQL_Latin1_General_CP1_CI_AS");

            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("Employee");

                entity.Property(e => e.Name).IsRequired();
            });

            modelBuilder.Entity<Partner>(entity =>
            {
                entity.ToTable("Partner");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.PartnerEmployees)
                    .HasForeignKey(d => d.EmployeeId)
                    .HasConstraintName("FK__Partner__Employe__5441852A");

                entity.HasOne(d => d.PartnerEmployee)
                    .WithMany(p => p.PartnerPartnerEmployees)
                    .HasForeignKey(d => d.PartnerEmployeeId)
                    .HasConstraintName("FK__Partner__Partner__5535A963");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
