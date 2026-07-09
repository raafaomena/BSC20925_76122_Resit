using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace BSC20925_76122_Resit.Web.Models
{
    public class InsuranceClaim
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Claim reference is required")]
        [StringLength(50, ErrorMessage = "Claim reference cannot exceed 50 characters")]
        public string ClaimReference { get; set; } = string.Empty;

        [Required(ErrorMessage = "Customer name is required")]
        [StringLength(100, ErrorMessage = "Customer name cannot exceed 100 characters")]
        public string CustomerName { get; set; } = string.Empty;

        [Required(ErrorMessage = "Customer email is required")]
        [EmailAddress(ErrorMessage = "Invalid email address")]
        [StringLength(100, ErrorMessage = "Email cannot exceed 100 characters")]
        public string CustomerEmail { get; set; } = string.Empty;

        [Required(ErrorMessage = "Policy number is required")]
        [StringLength(50, ErrorMessage = "Policy number cannot exceed 50 characters")]
        public string PolicyNumber { get; set; } = string.Empty;

        [Required(ErrorMessage = "Claim type is required")]
        [StringLength(50, ErrorMessage = "Claim type cannot exceed 50 characters")]
        public string ClaimType { get; set; } = string.Empty;

        [Required(ErrorMessage = "Claim date is required")]
        public DateTime ClaimDate { get; set; }

        [Required(ErrorMessage = "Incident date is required")]
        public DateTime IncidentDate { get; set; }

        [Required(ErrorMessage = "Estimated amount is required")]
        [Range(0, double.MaxValue, ErrorMessage = "Estimated amount must be greater than or equal to zero")]
        public decimal EstimatedAmount { get; set; }

        [StringLength(500, ErrorMessage = "Description cannot exceed 500 characters")]
        public string Description { get; set; } = string.Empty;

        [Required(ErrorMessage = "Claim status is required")]
        [StringLength(50, ErrorMessage = "Claim status cannot exceed 50 characters")]
        public string ClaimStatus { get; set; } = "Submitted";

        public DateTime CreatedAt { get; set; } = DateTime.UtcNow;

        public string CreatedBy { get; set; } = "System";

        public DateTime UpdatedAt { get; set; } = DateTime.UtcNow;

        [NotMapped]
        public bool IsValidIncidentDate => IncidentDate <= ClaimDate;
    }
}