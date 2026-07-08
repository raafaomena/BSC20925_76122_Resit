using Xunit;
using System.ComponentModel.DataAnnotations;
using BSC20925_76122_Resit.Web.Models;
using BSC20925_76122_Resit.Web.Models.Enums;

namespace BSC20925_76122_Resit.Tests;

public class InsuranceClaimTests
{
    [Fact]
    public void InsuranceClaim_WithValidData_ShouldPassValidation()
    {
        var claim = new InsuranceClaim
        {
            ClaimReference = "CLM-2026-001",
            CustomerName = "John Doe",
            CustomerEmail = "john@example.com",
            PolicyNumber = "POL-12345",
            ClaimType = ClaimType.Motor,
            ClaimStatus = ClaimStatus.Submitted,
            ClaimDate = new DateTime(2026, 7, 1),
            IncidentDate = new DateTime(2026, 6, 28),
            EstimatedAmount = 1250.00m,
            Description = "Valid claim description"
        };

        var validationContext = new ValidationContext(claim);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(claim, validationContext, validationResults, true);

        Assert.True(isValid);
        Assert.Empty(validationResults);
    }

    [Fact]
    public void CustomerName_Empty_ShouldFailValidation()
    {
        var claim = new InsuranceClaim
        {
            ClaimReference = "CLM-2026-001",
            CustomerEmail = "john@example.com",
            PolicyNumber = "POL-12345",
            ClaimType = ClaimType.Motor,
            ClaimStatus = ClaimStatus.Submitted,
            ClaimDate = new DateTime(2026, 7, 1),
            IncidentDate = new DateTime(2026, 6, 28),
            EstimatedAmount = 1000.00m
        };

        var validationContext = new ValidationContext(claim);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(claim, validationContext, validationResults, true);

        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage != null && v.ErrorMessage.Contains("Customer name is required"));
    }

    [Fact]
    public void CustomerEmail_Invalid_ShouldFailValidation()
    {
        var claim = new InsuranceClaim
        {
            ClaimReference = "CLM-2026-001",
            CustomerName = "John Doe",
            CustomerEmail = "invalid-email",
            PolicyNumber = "POL-12345",
            ClaimType = ClaimType.Motor,
            ClaimStatus = ClaimStatus.Submitted,
            ClaimDate = new DateTime(2026, 7, 1),
            IncidentDate = new DateTime(2026, 6, 28),
            EstimatedAmount = 1000.00m
        };

        var validationContext = new ValidationContext(claim);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(claim, validationContext, validationResults, true);

        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.ErrorMessage != null && v.ErrorMessage.Contains("Invalid email address"));
    }

    [Fact]
    public void IncidentDate_AfterClaimDate_ShouldFailValidation()
    {
        var claim = new InsuranceClaim
        {
            ClaimReference = "CLM-2026-001",
            CustomerName = "John Doe",
            CustomerEmail = "john@example.com",
            PolicyNumber = "POL-12345",
            ClaimType = ClaimType.Motor,
            ClaimStatus = ClaimStatus.Submitted,
            ClaimDate = new DateTime(2026, 7, 1),
            IncidentDate = new DateTime(2026, 7, 5),
            EstimatedAmount = 1000.00m,
            Description = "Test description"
        };

        // Forçar a validação personalizada
        var validationContext = new ValidationContext(claim);
        var validationResults = new List<ValidationResult>();
        
        // Validar
        var isValid = Validator.TryValidateObject(claim, validationContext, validationResults, true);

        // Verificar se a validação personalizada foi chamada
        var incidentDateValidation = validationResults.FirstOrDefault(v => 
            v.ErrorMessage != null && v.ErrorMessage.Contains("Incident date cannot be after the claim date"));

        Assert.False(isValid);
        Assert.NotNull(incidentDateValidation);
    }

    [Fact]
    public void EstimatedAmount_Negative_ShouldFailValidation()
    {
        var claim = new InsuranceClaim
        {
            ClaimReference = "CLM-2026-001",
            CustomerName = "John Doe",
            CustomerEmail = "john@example.com",
            PolicyNumber = "POL-12345",
            ClaimType = ClaimType.Motor,
            ClaimStatus = ClaimStatus.Submitted,
            ClaimDate = new DateTime(2026, 7, 1),
            IncidentDate = new DateTime(2026, 6, 28),
            EstimatedAmount = -100.00m
        };

        var validationContext = new ValidationContext(claim);
        var validationResults = new List<ValidationResult>();
        var isValid = Validator.TryValidateObject(claim, validationContext, validationResults, true);

        Assert.False(isValid);
        Assert.Contains(validationResults, v => v.MemberNames.Contains("EstimatedAmount"));
    }
}