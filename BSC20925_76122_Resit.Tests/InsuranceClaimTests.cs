using BSC20925_76122_Resit.Web.Models;
using System.ComponentModel.DataAnnotations;
using Xunit;

namespace BSC20925_76122_Resit.Tests
{
    public class InsurrClaimTests
    {
        private bool ValidateModel(object model, out List<ValidationResult> results)
        {
            var context = new ValidationContext(model, serviceProvider: null, items: null);
            results = new List<ValidationResult>();
            return Validator.TryValidateObject(model, context, results, true);
        }

        [Fact]
        public void InsuranceClaim_ShouldBeValid_WhenAllPropertiesAreValid()
        {
            var claim = new InsuranceClaim
            {
                ClaimReference = "CLM-1001",
                CustomerName = "John Doe",
                CustomerEmail = "john@example.com",
                PolicyNumber = "POL-12345",
                ClaimType = "Motor",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-7),
                EstimatedAmount = 1000.00m,
                Description = "Valid description",
                ClaimStatus = "Submitted"
            };

            var isValid = ValidateModel(claim, out var results);

            Assert.True(isValid);
            Assert.Empty(results);
        }

        [Fact]
        public void InsuranceClaim_ShouldBeInvalid_WhenCustomerNameIsEmpty()
        {
            var claim = new InsuranceClaim
            {
                ClaimReference = "CLM-1001",
                CustomerName = "",
                CustomerEmail = "john@example.com",
                PolicyNumber = "POL-12345",
                ClaimType = "Motor",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-7),
                EstimatedAmount = 1000.00m,
                Description = "Valid description",
                ClaimStatus = "Submitted"
            };

            var isValid = ValidateModel(claim, out var results);

            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains("CustomerName"));
        }

        [Fact]
        public void InsuranceClaim_ShouldBeInvalid_WhenPolicyNumberIsEmpty()
        {
            var claim = new InsuranceClaim
            {
                ClaimReference = "CLM-1001",
                CustomerName = "John Doe",
                CustomerEmail = "john@example.com",
                PolicyNumber = "",
                ClaimType = "Motor",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-7),
                EstimatedAmount = 1000.00m,
                Description = "Valid description",
                ClaimStatus = "Submitted"
            };

            var isValid = ValidateModel(claim, out var results);

            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains("PolicyNumber"));
        }

        [Fact]
        public void InsuranceClaim_ShouldBeInvalid_WhenClaimTypeIsEmpty()
        {
            var claim = new InsuranceClaim
            {
                ClaimReference = "CLM-1001",
                CustomerName = "John Doe",
                CustomerEmail = "john@example.com",
                PolicyNumber = "POL-12345",
                ClaimType = "",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-7),
                EstimatedAmount = 1000.00m,
                Description = "Valid description",
                ClaimStatus = "Submitted"
            };

            var isValid = ValidateModel(claim, out var results);

            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains("ClaimType"));
        }

        [Fact]
        public void InsuranceClaim_ShouldBeInvalid_WhenEmailIsInvalid()
        {
            var claim = new InsuranceClaim
            {
                ClaimReference = "CLM-1001",
                CustomerName = "John Doe",
                CustomerEmail = "invalid-email",
                PolicyNumber = "POL-12345",
                ClaimType = "Motor",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-7),
                EstimatedAmount = 1000.00m,
                Description = "Valid description",
                ClaimStatus = "Submitted"
            };

            var isValid = ValidateModel(claim, out var results);

            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains("CustomerEmail"));
        }

        [Fact]
        public void InsuranceClaim_ShouldBeInvalid_WhenEstimatedAmountIsNegative()
        {
            var claim = new InsuranceClaim
            {
                ClaimReference = "CLM-1001",
                CustomerName = "John Doe",
                CustomerEmail = "john@example.com",
                PolicyNumber = "POL-12345",
                ClaimType = "Motor",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-7),
                EstimatedAmount = -100.00m,
                Description = "Valid description",
                ClaimStatus = "Submitted"
            };

            var isValid = ValidateModel(claim, out var results);

            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains("EstimatedAmount"));
        }

        [Fact]
        public void InsuranceClaim_ShouldBeInvalid_WhenIncidentDateIsAfterClaimDate()
        {
            var claim = new InsuranceClaim
            {
                ClaimReference = "CLM-1001",
                CustomerName = "John Doe",
                CustomerEmail = "john@example.com",
                PolicyNumber = "POL-12345",
                ClaimType = "Motor",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(7),
                EstimatedAmount = 1000.00m,
                Description = "Valid description",
                ClaimStatus = "Submitted"
            };

            Assert.False(claim.IsValidIncidentDate);
        }

        [Fact]
        public void InsuranceClaim_ShouldBeInvalid_WhenClaimReferenceIsEmpty()
        {
            var claim = new InsuranceClaim
            {
                ClaimReference = "",
                CustomerName = "John Doe",
                CustomerEmail = "john@example.com",
                PolicyNumber = "POL-12345",
                ClaimType = "Motor",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-7),
                EstimatedAmount = 1000.00m,
                Description = "Valid description",
                ClaimStatus = "Submitted"
            };

            var isValid = ValidateModel(claim, out var results);

            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains("ClaimReference"));
        }

        [Fact]
        public void InsuranceClaim_ShouldBeInvalid_WhenClaimStatusIsEmpty()
        {
            var claim = new InsuranceClaim
            {
                ClaimReference = "CLM-1001",
                CustomerName = "John Doe",
                CustomerEmail = "john@example.com",
                PolicyNumber = "POL-12345",
                ClaimType = "Motor",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-7),
                EstimatedAmount = 1000.00m,
                Description = "Valid description",
                ClaimStatus = ""
            };

            var isValid = ValidateModel(claim, out var results);

            Assert.False(isValid);
            Assert.Contains(results, r => r.MemberNames.Contains("ClaimStatus"));
        }

        [Fact]
        public void InsuranceClaim_ShouldBeValid_WhenDescriptionIsEmpty()
        {
            var claim = new InsuranceClaim
            {
                ClaimReference = "CLM-1001",
                CustomerName = "John Doe",
                CustomerEmail = "john@example.com",
                PolicyNumber = "POL-12345",
                ClaimType = "Motor",
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-7),
                EstimatedAmount = 1000.00m,
                Description = "",
                ClaimStatus = "Submitted"
            };

            var isValid = ValidateModel(claim, out var results);

            Assert.True(isValid);
            Assert.Empty(results);
        }

        [Fact]
        public void InsuranceClaim_ShouldHaveSubmittedStatus_WhenCreated()
        {
            var claim = new InsuranceClaim();

            Assert.Equal("Submitted", claim.ClaimStatus);
        }

        [Fact]
        public void InsuranceClaim_ShouldHaveCreatedAt_WhenCreated()
        {
            var beforeCreation = DateTime.UtcNow.AddSeconds(-1);
            var claim = new InsuranceClaim();
            var afterCreation = DateTime.UtcNow.AddSeconds(1);

            Assert.True(claim.CreatedAt >= beforeCreation && claim.CreatedAt <= afterCreation);
        }

        [Fact]
        public void InsuranceClaim_ShouldHaveCreatedBySystem_WhenCreated()
        {
            var claim = new InsuranceClaim();

            Assert.Equal("System", claim.CreatedBy);
        }
    }
}
