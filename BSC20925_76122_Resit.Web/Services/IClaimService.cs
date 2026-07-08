using BSC20925_76122_Resit.Web.Models;

namespace BSC20925_76122_Resit.Web.Services;

public interface IClaimService
{
    Task<IEnumerable<InsuranceClaim>> GetAllClaimsAsync();
    Task<IEnumerable<InsuranceClaim>> GetFilteredClaimsAsync(string? searchTerm, string? status, string? claimType);
    Task<InsuranceClaim?> GetClaimByIdAsync(int id);
    Task<InsuranceClaim> CreateClaimAsync(InsuranceClaim claim);
    Task<bool> UpdateClaimAsync(InsuranceClaim claim);
    Task<bool> DeleteClaimAsync(int id);
    Task<bool> ClaimReferenceExistsAsync(string claimReference);
    Task<Dictionary<string, int>> GetClaimStatusCountsAsync();
    bool ClaimExists(int id);
}