using System.ComponentModel.DataAnnotations;
using BSC20925_76122_Resit.Web.Models.Enums;

namespace BSC20925_76122_Resit.Web.Models;

public class InsuranceClaim
{
    [Key]
    public int Id { get; set; }

    [Required(ErrorMessage = "Claim reference is required")]
    [StringLength(20, MinimumLength = 4, ErrorMessage = "Claim reference must be between 4 and 20 characters")]
    [Display(Name = "Claim Reference")]
    [RegularExpression(@"^CLM-\d{4}-\d{3}$", ErrorMessage = "Format must be CLM-YYYY-XXX")]
    public string ClaimReference { get; set; } = string.Empty;

    [Required(ErrorMessage = "Customer name is required")]
    [StringLength(100, MinimumLength = 2, ErrorMessage = "Customer name must be between 2 and 100 characters")]
    [Display(Name = "Customer Name")]
    public string CustomerName { get; set; } = string.Empty;

    [Required(ErrorMessage = "Email is required")]
    [EmailAddress(ErrorMessage = "Invalid email address format")]
    [Display(Name = "Email Address")]
    [StringLength(100)]
    public string CustomerEmail { get; set; } = string.Empty;

    [Required(ErrorMessage = "Policy number is required")]
    [StringLength(20, MinimumLength = 5, ErrorMessage = "Policy number must be between 5 and 20 characters")]
    [Display(Name = "Policy Number")]
    [RegularExpression(@"^POL-\d{5}$", ErrorMessage = "Format must be POL-XXXXX")]
    public string PolicyNumber { get; set; } = string.Empty;

    [Required(ErrorMessage = "Claim type is required")]
    [Display(Name = "Claim Type")]
    public ClaimType ClaimType { get; set; }

    [Required(ErrorMessage = "Claim status is required")]
    [Display(Name = "Status")]
    public ClaimStatus ClaimStatus { get; set; } = ClaimStatus.Submitted;

    [Required(ErrorMessage = "Claim date is required")]
    [DataType(DataType.Date)]
    [Display(Name = "Claim Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    public DateTime ClaimDate { get; set; } = DateTime.UtcNow;

    [Required(ErrorMessage = "Incident date is required")]
    [DataType(DataType.Date)]
    [Display(Name = "Incident Date")]
    [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
    [CustomValidation(typeof(InsuranceClaim), nameof(ValidateIncidentDate))]
    public DateTime IncidentDate { get; set; }

    [Required(ErrorMessage = "Estimated amount is required")]
    [Range(0.01, 9999999.99, ErrorMessage = "Estimated amount must be between 0.01 and 9,999,999.99")]
    [DataType(DataType.Currency)]
    [Display(Name = "Estimated Amount")]
    [DisplayFormat(DataFormatString = "{0:C2}", ApplyFormatInEditMode = true)]
    public decimal EstimatedAmount { get; set; }

    [Display(Name = "Description")]
    [StringLength(1000, ErrorMessage = "Description cannot exceed 1000 characters")]
    public string Description { get; set; } = string.Empty;

    [Display(Name = "Created At")]
    public DateTime CreatedAt { get; set; }

    [Display(Name = "Last Updated")]
    public DateTime? UpdatedAt { get; set; }

    [Display(Name = "Created By")]
    public string? CreatedBy { get; set; }

    public string? UserId { get; set; }

    public static ValidationResult? ValidateIncidentDate(DateTime incidentDate, ValidationContext context)
    {
        var claim = (InsuranceClaim)context.ObjectInstance;
        
        if (incidentDate > claim.ClaimDate)
        {
            return new ValidationResult("Incident date cannot be after the claim date.");
        }

        if (incidentDate > DateTime.UtcNow)
        {
            return new ValidationResult("Incident date cannot be in the future.");
        }

        return ValidationResult.Success;
    }
}