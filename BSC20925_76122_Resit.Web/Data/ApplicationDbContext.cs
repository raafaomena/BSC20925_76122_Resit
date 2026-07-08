using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using BSC20925_76122_Resit.Web.Models;
using BSC20925_76122_Resit.Web.Models.Enums;

namespace BSC20925_76122_Resit.Web.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {
    }

    public DbSet<InsuranceClaim> InsuranceClaims { get; set; }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // Configure InsuranceClaim entity
        modelBuilder.Entity<InsuranceClaim>(entity =>
        {
            // Indexes for performance
            entity.HasIndex(e => e.ClaimReference).IsUnique();
            entity.HasIndex(e => e.CustomerName);
            entity.HasIndex(e => e.PolicyNumber);
            entity.HasIndex(e => e.ClaimStatus);
            entity.HasIndex(e => e.ClaimType);
            entity.HasIndex(e => e.ClaimDate);
            entity.HasIndex(e => e.CreatedAt);

            // Property configurations
            entity.Property(e => e.ClaimReference)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(e => e.CustomerName)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.CustomerEmail)
                .HasMaxLength(100)
                .IsRequired();

            entity.Property(e => e.PolicyNumber)
                .HasMaxLength(20)
                .IsRequired();

            entity.Property(e => e.ClaimType)
                .HasConversion<int>()
                .IsRequired();

            entity.Property(e => e.ClaimStatus)
                .HasConversion<int>()
                .IsRequired()
                .HasDefaultValue(ClaimStatus.Submitted);

            entity.Property(e => e.Description)
                .HasMaxLength(1000);

            entity.Property(e => e.EstimatedAmount)
                .HasPrecision(18, 2);
        });

        // Seed data
        SeedData(modelBuilder);
    }

    private void SeedData(ModelBuilder modelBuilder)
    {
        var now = DateTime.UtcNow;

        modelBuilder.Entity<InsuranceClaim>().HasData(
            new InsuranceClaim
            {
                Id = 1,
                ClaimReference = "CLM-2026-001",
                CustomerName = "Mary O'Brien",
                CustomerEmail = "mary.obrien@example.com",
                PolicyNumber = "POL-45678",
                ClaimType = ClaimType.Motor,
                ClaimStatus = ClaimStatus.Submitted,
                ClaimDate = new DateTime(2026, 7, 1),
                IncidentDate = new DateTime(2026, 6, 28),
                EstimatedAmount = 1250.00m,
                Description = "Rear bumper damaged in minor accident at supermarket parking lot",
                CreatedAt = now,
                UpdatedAt = null,
                CreatedBy = "System"
            },
            new InsuranceClaim
            {
                Id = 2,
                ClaimReference = "CLM-2026-002",
                CustomerName = "John Murphy",
                CustomerEmail = "john.murphy@example.com",
                PolicyNumber = "POL-12345",
                ClaimType = ClaimType.Home,
                ClaimStatus = ClaimStatus.InReview,
                ClaimDate = new DateTime(2026, 6, 25),
                IncidentDate = new DateTime(2026, 6, 20),
                EstimatedAmount = 3500.00m,
                Description = "Water damage to kitchen ceiling due to burst pipe",
                CreatedAt = now.AddDays(-5),
                UpdatedAt = now.AddDays(-2),
                CreatedBy = "System"
            },
            new InsuranceClaim
            {
                Id = 3,
                ClaimReference = "CLM-2026-003",
                CustomerName = "Sarah Kelly",
                CustomerEmail = "sarah.kelly@example.com",
                PolicyNumber = "POL-78901",
                ClaimType = ClaimType.Travel,
                ClaimStatus = ClaimStatus.Approved,
                ClaimDate = new DateTime(2026, 6, 15),
                IncidentDate = new DateTime(2026, 6, 10),
                EstimatedAmount = 850.50m,
                Description = "Lost luggage during international flight with delayed delivery",
                CreatedAt = now.AddDays(-10),
                UpdatedAt = now.AddDays(-3),
                CreatedBy = "System"
            }
        );
    }
}