using BSC20925_76122_Resit.Web.Data;
using BSC20925_76122_Resit.Web.Models;
using BSC20925_76122_Resit.Web.Services;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Moq;
using Xunit;

namespace BSC20925_76122_Resit.Tests
{
    public class ClaimServiceTests
    {
        private ApplicationDbContext GetDbContext()
        {
            var options = new DbContextOptionsBuilder<ApplicationDbContext>()
                .UseInMemoryDatabase(databaseName: Guid.NewGuid().ToString())
                .Options;

            return new ApplicationDbContext(options);
        }

        [Fact]
        public async Task CreateClaimAsync_ShouldCreateClaim()
        {
            var context = GetDbContext();
            var loggerMock = new Mock<ILogger<ClaimService>>();
            var service = new ClaimService(context, loggerMock.Object);

            var claim = new InsuranceClaim
            {
                ClaimReference = "CLM-TEST-001",
                CustomerName = "Test Customer",
                CustomerEmail = "test@example.com",
                PolicyNumber = "POL-TEST-001",
                ClaimType = "Motor",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-7),
                EstimatedAmount = 1000.00m,
                Description = "Test claim",
                ClaimStatus = "Submitted"
            };

            var result = await service.CreateClaimAsync(claim);

            Assert.NotNull(result);
            Assert.Equal(claim.ClaimReference, result.ClaimReference);
            Assert.Equal(claim.CustomerName, result.CustomerName);
        }

        [Fact]
        public async Task GetClaimByIdAsync_ShouldReturnClaim_WhenExists()
        {
            var context = GetDbContext();
            var loggerMock = new Mock<ILogger<ClaimService>>();
            var service = new ClaimService(context, loggerMock.Object);

            var claim = new InsuranceClaim
            {
                ClaimReference = "CLM-TEST-002",
                CustomerName = "Test Customer 2",
                CustomerEmail = "test2@example.com",
                PolicyNumber = "POL-TEST-002",
                ClaimType = "Home",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-5),
                EstimatedAmount = 2000.00m,
                Description = "Test claim 2",
                ClaimStatus = "Submitted"
            };

            await service.CreateClaimAsync(claim);
            var result = await service.GetClaimByIdAsync(claim.Id);

            Assert.NotNull(result);
            Assert.Equal(claim.Id, result.Id);
            Assert.Equal(claim.ClaimReference, result.ClaimReference);
        }

        [Fact]
        public async Task GetClaimByIdAsync_ShouldThrowKeyNotFoundException_WhenNotExists()
        {
            var context = GetDbContext();
            var loggerMock = new Mock<ILogger<ClaimService>>();
            var service = new ClaimService(context, loggerMock.Object);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.GetClaimByIdAsync(999));
        }

        [Fact]
        public async Task UpdateClaimAsync_ShouldUpdateClaim()
        {
            var context = GetDbContext();
            var loggerMock = new Mock<ILogger<ClaimService>>();
            var service = new ClaimService(context, loggerMock.Object);

            var claim = new InsuranceClaim
            {
                ClaimReference = "CLM-TEST-003",
                CustomerName = "Test Customer 3",
                CustomerEmail = "test3@example.com",
                PolicyNumber = "POL-TEST-003",
                ClaimType = "Travel",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-3),
                EstimatedAmount = 500.00m,
                Description = "Test claim 3",
                ClaimStatus = "Submitted"
            };

            await service.CreateClaimAsync(claim);

            claim.CustomerName = "Updated Name";
            claim.ClaimStatus = "Approved";
            claim.EstimatedAmount = 750.00m;

            var result = await service.UpdateClaimAsync(claim);

            Assert.NotNull(result);
            Assert.Equal("Updated Name", result.CustomerName);
            Assert.Equal("Approved", result.ClaimStatus);
            Assert.Equal(750.00m, result.EstimatedAmount);
        }

        [Fact]
        public async Task DeleteClaimAsync_ShouldDeleteClaim()
        {
            var context = GetDbContext();
            var loggerMock = new Mock<ILogger<ClaimService>>();
            var service = new ClaimService(context, loggerMock.Object);

            var claim = new InsuranceClaim
            {
                ClaimReference = "CLM-TEST-004",
                CustomerName = "Test Customer 4",
                CustomerEmail = "test4@example.com",
                PolicyNumber = "POL-TEST-004",
                ClaimType = "Health",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-2),
                EstimatedAmount = 3000.00m,
                Description = "Test claim 4",
                ClaimStatus = "Submitted"
            };

            await service.CreateClaimAsync(claim);
            await service.DeleteClaimAsync(claim.Id);

            await Assert.ThrowsAsync<KeyNotFoundException>(() => service.GetClaimByIdAsync(claim.Id));
        }

        [Fact]
        public async Task GetFilteredClaimsAsync_ShouldFilterByStatus()
        {
            var context = GetDbContext();
            var loggerMock = new Mock<ILogger<ClaimService>>();
            var service = new ClaimService(context, loggerMock.Object);

            var claim1 = new InsuranceClaim
            {
                ClaimReference = "CLM-TEST-005",
                CustomerName = "Test Customer 5",
                CustomerEmail = "test5@example.com",
                PolicyNumber = "POL-TEST-005",
                ClaimType = "Motor",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-7),
                EstimatedAmount = 1000.00m,
                Description = "Test claim 5",
                ClaimStatus = "Submitted"
            };

            var claim2 = new InsuranceClaim
            {
                ClaimReference = "CLM-TEST-006",
                CustomerName = "Test Customer 6",
                CustomerEmail = "test6@example.com",
                PolicyNumber = "POL-TEST-006",
                ClaimType = "Motor",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-7),
                EstimatedAmount = 1500.00m,
                Description = "Test claim 6",
                ClaimStatus = "Approved"
            };

            await service.CreateClaimAsync(claim1);
            await service.CreateClaimAsync(claim2);

            var results = await service.GetFilteredClaimsAsync(null, "Approved", null);

            Assert.Single(results);
            Assert.Equal("Approved", results.First().ClaimStatus);
        }

        [Fact]
        public async Task GetFilteredClaimsAsync_ShouldFilterBySearchTerm()
        {
            var context = GetDbContext();
            var loggerMock = new Mock<ILogger<ClaimService>>();
            var service = new ClaimService(context, loggerMock.Object);

            var claim1 = new InsuranceClaim
            {
                ClaimReference = "CLM-TEST-007",
                CustomerName = "John Smith",
                CustomerEmail = "john@example.com",
                PolicyNumber = "POL-TEST-007",
                ClaimType = "Home",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-7),
                EstimatedAmount = 2000.00m,
                Description = "Test claim 7",
                ClaimStatus = "Submitted"
            };

            var claim2 = new InsuranceClaim
            {
                ClaimReference = "CLM-TEST-008",
                CustomerName = "Mary Johnson",
                CustomerEmail = "mary@example.com",
                PolicyNumber = "POL-TEST-008",
                ClaimType = "Home",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-7),
                EstimatedAmount = 2500.00m,
                Description = "Test claim 8",
                ClaimStatus = "Submitted"
            };

            await service.CreateClaimAsync(claim1);
            await service.CreateClaimAsync(claim2);

            var results = await service.GetFilteredClaimsAsync("Smith", null, null);
            var resultList = results.ToList();

            Assert.Single(resultList);
            Assert.Equal("John Smith", resultList.First().CustomerName);
        }

        [Fact]
        public async Task GetClaimStatusCountsAsync_ShouldReturnCounts()
        {
            var context = GetDbContext();
            var loggerMock = new Mock<ILogger<ClaimService>>();
            var service = new ClaimService(context, loggerMock.Object);

            var claim1 = new InsuranceClaim
            {
                ClaimReference = "CLM-TEST-009",
                CustomerName = "Test Customer 9",
                CustomerEmail = "test9@example.com",
                PolicyNumber = "POL-TEST-009",
                ClaimType = "Motor",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-7),
                EstimatedAmount = 1000.00m,
                Description = "Test claim 9",
                ClaimStatus = "Submitted"
            };

            var claim2 = new InsuranceClaim
            {
                ClaimReference = "CLM-TEST-010",
                CustomerName = "Test Customer 10",
                CustomerEmail = "test10@example.com",
                PolicyNumber = "POL-TEST-010",
                ClaimType = "Motor",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-7),
                EstimatedAmount = 1500.00m,
                Description = "Test claim 10",
                ClaimStatus = "Approved"
            };

            var claim3 = new InsuranceClaim
            {
                ClaimReference = "CLM-TEST-011",
                CustomerName = "Test Customer 11",
                CustomerEmail = "test11@example.com",
                PolicyNumber = "POL-TEST-011",
                ClaimType = "Motor",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-7),
                EstimatedAmount = 2000.00m,
                Description = "Test claim 11",
                ClaimStatus = "Submitted"
            };

            await service.CreateClaimAsync(claim1);
            await service.CreateClaimAsync(claim2);
            await service.CreateClaimAsync(claim3);

            var results = await service.GetClaimStatusCountsAsync();

            Assert.Equal(2, results["Submitted"]);
            Assert.Equal(1, results["Approved"]);
        }
    }
}
