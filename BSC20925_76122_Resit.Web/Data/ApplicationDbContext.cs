using BSC20925_76122_Resit.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace BSC20925_76122_Resit.Web.Data
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<InsuranceClaim> InsuranceClaims { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.Entity<InsuranceClaim>(entity =>
            {
                entity.HasKey(e => e.Id);

                entity.Property(e => e.ClaimReference)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.CustomerName)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.CustomerEmail)
                    .IsRequired()
                    .HasMaxLength(100);

                entity.Property(e => e.PolicyNumber)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ClaimType)
                    .IsRequired()
                    .HasMaxLength(50);

                entity.Property(e => e.ClaimStatus)
                    .IsRequired()
                    .HasMaxLength(50)
                    .HasDefaultValue("Submitted")
                    .HasSentinel(null);

                entity.Property(e => e.Description)
                    .HasMaxLength(500);

                entity.Property(e => e.EstimatedAmount)
                    .HasPrecision(18, 2);

                entity.Property(e => e.CreatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.Property(e => e.CreatedBy)
                    .HasMaxLength(100)
                    .HasDefaultValue("System");

                entity.Property(e => e.UpdatedAt)
                    .HasDefaultValueSql("CURRENT_TIMESTAMP");

                entity.HasIndex(e => e.ClaimReference)
                    .IsUnique();

                entity.HasIndex(e => e.CustomerName);
                entity.HasIndex(e => e.PolicyNumber);
                entity.HasIndex(e => e.ClaimStatus);
                entity.HasIndex(e => e.ClaimType);
            });

            SeedData(modelBuilder);
        }

        private void SeedData(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<InsuranceClaim>().HasData(
                new InsuranceClaim
                {
                    Id = 1,
                    ClaimReference = "CLM-1001",
                    CustomerName = "Mary O'Brien",
                    CustomerEmail = "mary@example.com",
                    PolicyNumber = "POL-45678",
                    ClaimType = "Motor",
                    ClaimDate = new DateTime(2026, 7, 1),
                    IncidentDate = new DateTime(2026, 6, 28),
                    EstimatedAmount = 1250.00m,
                    Description = "Rear bumper damaged in minor accident",
                    ClaimStatus = "Submitted",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedAt = DateTime.UtcNow
                },
                new InsuranceClaim
                {
                    Id = 2,
                    ClaimReference = "CLM-1002",
                    CustomerName = "John Smith",
                    CustomerEmail = "john@example.com",
                    PolicyNumber = "POL-12345",
                    ClaimType = "Home",
                    ClaimDate = new DateTime(2026, 7, 2),
                    IncidentDate = new DateTime(2026, 6, 30),
                    EstimatedAmount = 3500.00m,
                    Description = "Water damage from burst pipe",
                    ClaimStatus = "Under Review",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedAt = DateTime.UtcNow
                },
                new InsuranceClaim
                {
                    Id = 3,
                    ClaimReference = "CLM-1003",
                    CustomerName = "Sarah Johnson",
                    CustomerEmail = "sarah@example.com",
                    PolicyNumber = "POL-78901",
                    ClaimType = "Health",
                    ClaimDate = new DateTime(2026, 7, 3),
                    IncidentDate = new DateTime(2026, 6, 25),
                    EstimatedAmount = 5000.00m,
                    Description = "Hospital stay for surgery",
                    ClaimStatus = "Approved",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedAt = DateTime.UtcNow
                },
                new InsuranceClaim
                {
                    Id = 4,
                    ClaimReference = "CLM-1004",
                    CustomerName = "Michael Brown",
                    CustomerEmail = "michael@example.com",
                    PolicyNumber = "POL-24680",
                    ClaimType = "Travel",
                    ClaimDate = new DateTime(2026, 7, 4),
                    IncidentDate = new DateTime(2026, 7, 2),
                    EstimatedAmount = 750.00m,
                    Description = "Lost luggage during international flight",
                    ClaimStatus = "Rejected",
                    CreatedAt = DateTime.UtcNow,
                    CreatedBy = "System",
                    UpdatedAt = DateTime.UtcNow
                }
            );
        }
    }
}