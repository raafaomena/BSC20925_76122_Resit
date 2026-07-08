using Xunit;
using Moq;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using BSC20925_76122_Resit.Web.Data;
using BSC20925_76122_Resit.Web.Models;
using BSC20925_76122_Resit.Web.Models.Enums;
using BSC20925_76122_Resit.Web.Services;

namespace BSC20925_76122_Resit.Tests;

public class ClaimServiceTests
{
    private readonly Mock<ILogger<ClaimService>> _loggerMock;
    private readonly ApplicationDbContext _context;
    private readonly ClaimService _service;

    public ClaimServiceTests()
    {
        _loggerMock = new Mock<ILogger<ClaimService>>();
        
        var options = new DbContextOptionsBuilder<ApplicationDbContext>()
            .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
            .Options;
        
        _context = new ApplicationDbContext(options);
        _service = new ClaimService(_context, _loggerMock.Object);
    }

    [Fact]
    public async Task CreateClaimAsync_ValidClaim_ShouldCreateClaim()
    {
        var claim = new InsuranceClaim
        {
            ClaimReference = "CLM-TEST-001",
            CustomerName = "Test Customer",
            CustomerEmail = "test@example.com",
            PolicyNumber = "POL-TEST-001",
            ClaimType = ClaimType.Motor,
            ClaimStatus = ClaimStatus.Submitted,
            ClaimDate = DateTime.UtcNow,
            IncidentDate = DateTime.UtcNow.AddDays(-5),
            EstimatedAmount = 1000.00m,
            Description = "Test claim"
        };

        var result = await _service.CreateClaimAsync(claim);

        Assert.NotNull(result);
        Assert.Equal(claim.ClaimReference, result.ClaimReference);
        Assert.Equal(1, _context.InsuranceClaims.Count());
    }

    [Fact]
    public async Task GetAllClaimsAsync_WhenClaimsExist_ShouldReturnAllClaims()
    {
        var claims = new List<InsuranceClaim>
        {
            new() { ClaimReference = "CLM-001", CustomerName = "John", CustomerEmail = "john@test.com", 
                    PolicyNumber = "POL-001", ClaimType = ClaimType.Motor, ClaimStatus = ClaimStatus.Submitted, 
                    ClaimDate = DateTime.UtcNow, IncidentDate = DateTime.UtcNow.AddDays(-5), EstimatedAmount = 100 },
            new() { ClaimReference = "CLM-002", CustomerName = "Jane", CustomerEmail = "jane@test.com", 
                    PolicyNumber = "POL-002", ClaimType = ClaimType.Home, ClaimStatus = ClaimStatus.InReview, 
                    ClaimDate = DateTime.UtcNow, IncidentDate = DateTime.UtcNow.AddDays(-5), EstimatedAmount = 200 }
        };

        foreach (var c in claims)
        {
            await _service.CreateClaimAsync(c);
        }

        var result = await _service.GetAllClaimsAsync();

        Assert.Equal(2, result.Count());
    }

    [Fact]
    public async Task GetClaimByIdAsync_WhenClaimExists_ShouldReturnClaim()
    {
        var claim = new InsuranceClaim
        {
            ClaimReference = "CLM-EXISTS-001",
            CustomerName = "Test",
            CustomerEmail = "test@test.com",
            PolicyNumber = "POL-001",
            ClaimType = ClaimType.Motor,
            ClaimStatus = ClaimStatus.Submitted,
            ClaimDate = DateTime.UtcNow,
            IncidentDate = DateTime.UtcNow.AddDays(-5),
            EstimatedAmount = 100
        };
        var created = await _service.CreateClaimAsync(claim);

        var result = await _service.GetClaimByIdAsync(created.Id);

        Assert.NotNull(result);
        Assert.Equal(created.Id, result.Id);
    }

    [Fact]
    public async Task DeleteClaimAsync_WhenClaimExists_ShouldDeleteClaim()
    {
        var claim = new InsuranceClaim
        {
            ClaimReference = "CLM-DELETE-001",
            CustomerName = "Test",
            CustomerEmail = "test@test.com",
            PolicyNumber = "POL-001",
            ClaimType = ClaimType.Motor,
            ClaimStatus = ClaimStatus.Submitted,
            ClaimDate = DateTime.UtcNow,
            IncidentDate = DateTime.UtcNow.AddDays(-5),
            EstimatedAmount = 100
        };
        var created = await _service.CreateClaimAsync(claim);

        var result = await _service.DeleteClaimAsync(created.Id);
        var deleted = await _service.GetClaimByIdAsync(created.Id);

        Assert.True(result);
        Assert.Null(deleted);
    }
}