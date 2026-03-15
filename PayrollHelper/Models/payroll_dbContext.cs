using System;
using System.Collections.Generic;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace PayrollHelper.Models
{
    public partial class payroll_dbContext : DbContext
    {
        public payroll_dbContext()
        {
        }

        public payroll_dbContext(DbContextOptions<payroll_dbContext> options)
            : base(options)
        {
        }

        public virtual DbSet<Employee> Employees { get; set; } = null!;
        public virtual DbSet<Payment> Payments { get; set; } = null!;
        public virtual DbSet<Position> Positions { get; set; } = null!;
        public virtual DbSet<SalaryAndBonus> SalaryAndBonuses { get; set; } = null!;
        public virtual DbSet<Taxation> Taxations { get; set; } = null!;

        /*
        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            if (!optionsBuilder.IsConfigured)
            {
#warning To protect potentially sensitive information in your connection string, you should move it out of source code. You can avoid scaffolding the connection string by using the Name= syntax to read it from configuration - see https://go.microsoft.com/fwlink/?linkid=2131148. For more guidance on storing connection strings, see http://go.microsoft.com/fwlink/?LinkId=723263.
                optionsBuilder.UseNpgsql("Host=localhost;Database=payroll_db;Username=payroll_user;Password=123");
            }
        }
        */

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>(entity =>
            {
                entity.ToTable("employees");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.Address)
                    .HasMaxLength(150)
                    .HasColumnName("address");

                entity.Property(e => e.EmployeeName)
                    .HasMaxLength(100)
                    .HasColumnName("employee_name");

                entity.Property(e => e.Insurance)
                    .HasColumnName("insurance")
                    .HasDefaultValueSql("false");

                entity.Property(e => e.PhoneNumber)
                    .HasMaxLength(15)
                    .HasColumnName("phone_number");

                entity.Property(e => e.PostNumber).HasColumnName("post_number");

                entity.HasOne(d => d.PostNumberNavigation)
                    .WithMany(p => p.Employees)
                    .HasForeignKey(d => d.PostNumber)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_employees_position");
            });

            modelBuilder.Entity<Payment>(entity =>
            {
                entity.ToTable("payments");

                entity.Property(e => e.PaymentId).HasColumnName("payment_id");

                entity.Property(e => e.EmployeeId).HasColumnName("employee_id");

                entity.Property(e => e.PaymentAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("payment_amount");

                entity.Property(e => e.PaymentDate).HasColumnName("payment_date");

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(50)
                    .HasColumnName("payment_type");

                entity.HasOne(d => d.Employee)
                    .WithMany(p => p.Payments)
                    .HasForeignKey(d => d.EmployeeId)
                    .OnDelete(DeleteBehavior.ClientSetNull)
                    .HasConstraintName("fk_payments_employee");
            });

            modelBuilder.Entity<Position>(entity =>
            {
                entity.ToTable("positions");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.CreatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("created_at")
                    .HasDefaultValueSql("now()");

                entity.Property(e => e.Description)
                    .HasMaxLength(200)
                    .HasColumnName("description");

                entity.Property(e => e.Name)
                    .HasMaxLength(50)
                    .HasColumnName("name");

                entity.Property(e => e.Status)
                    .HasMaxLength(20)
                    .HasColumnName("status")
                    .HasDefaultValueSql("'active'::character varying");

                entity.Property(e => e.UpdatedAt)
                    .HasColumnType("timestamp without time zone")
                    .HasColumnName("updated_at")
                    .HasDefaultValueSql("now()");
            });

            modelBuilder.Entity<SalaryAndBonus>(entity =>
            {
                entity.ToTable("salary_and_bonuses");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Amount)
                    .HasPrecision(10, 2)
                    .HasColumnName("amount");

                entity.Property(e => e.DefaultAmount)
                    .HasPrecision(10, 2)
                    .HasColumnName("default_amount");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.PaymentType)
                    .HasMaxLength(50)
                    .HasColumnName("payment_type");

                entity.HasMany(d => d.Taxations)
                    .WithMany(p => p.SalaryAndBonuses)
                    .UsingEntity<Dictionary<string, object>>(
                        "SalaryAndBonusesTaxation",
                        l => l.HasOne<Taxation>().WithMany().HasForeignKey("TaxationId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_sbt_taxation"),
                        r => r.HasOne<SalaryAndBonus>().WithMany().HasForeignKey("SalaryAndBonusesId").OnDelete(DeleteBehavior.ClientSetNull).HasConstraintName("fk_sbt_salary"),
                        j =>
                        {
                            j.HasKey("SalaryAndBonusesId", "TaxationId").HasName("salary_and_bonuses_taxation_pkey");

                            j.ToTable("salary_and_bonuses_taxation");

                            j.IndexerProperty<int>("SalaryAndBonusesId").HasColumnName("salary_and_bonuses_id");

                            j.IndexerProperty<int>("TaxationId").HasColumnName("taxation_id");
                        });
            });

            modelBuilder.Entity<Taxation>(entity =>
            {
                entity.ToTable("taxation");

                entity.Property(e => e.Id).HasColumnName("id");

                entity.Property(e => e.Description)
                    .HasMaxLength(255)
                    .HasColumnName("description");

                entity.Property(e => e.TaxRate)
                    .HasPrecision(5, 2)
                    .HasColumnName("tax_rate");

                entity.Property(e => e.TaxType)
                    .HasMaxLength(50)
                    .HasColumnName("tax_type");
            });

            OnModelCreatingPartial(modelBuilder);
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
