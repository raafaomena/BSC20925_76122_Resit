using BSC20925_76122_Resit.Web.Models;

namespace BSC20925_76122_Resit.Web.Services
{
    public interface IClaimService
    {
        Task<IEnumerable<InsuranceClaim>> GetAllClaimsAsync();
        Task<IEnumerable<InsuranceClaim>> GetFilteredClaimsAsync(string searchTerm, string status, string claimType);
        Task<InsuranceClaim> GetClaimByIdAsync(int id);
        Task<InsuranceClaim> CreateClaimAsync(InsuranceClaim claim);
        Task<InsuranceClaim> UpdateClaimAsync(InsuranceClaim claim);
        Task DeleteClaimAsync(int id);
        Task<Dictionary<string, int>> GetClaimStatusCountsAsync();
        Task<int> GetTotalClaimsCountAsync();
        Task<IEnumerable<InsuranceClaim>> GetRecentClaimsAsync(int count);
        Task<IEnumerable<InsuranceClaim>> GetClaimsByStatusAsync(string status);
    }
}