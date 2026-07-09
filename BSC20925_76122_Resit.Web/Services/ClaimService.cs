using BSC20925_76122_Resit.Web.Data;
using BSC20925_76122_Resit.Web.Models;
using Microsoft.EntityFrameworkCore;

namespace BSC20925_76122_Resit.Web.Services
{
    public class ClaimService : IClaimService
    {
        private readonly ApplicationDbContext _context;
        private readonly ILogger<ClaimService> _logger;

        public ClaimService(ApplicationDbContext context, ILogger<ClaimService> logger)
        {
            _context = context;
            _logger = logger;
        }

        public async Task<IEnumerable<InsuranceClaim>> GetAllClaimsAsync()
        {
            try
            {
                return await _context.InsuranceClaims
                    .OrderByDescending(c => c.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting all claims");
                throw;
            }
        }

        public async Task<IEnumerable<InsuranceClaim>> GetFilteredClaimsAsync(string searchTerm, string status, string claimType)
        {
            try
            {
                var query = _context.InsuranceClaims.AsQueryable();

                if (!string.IsNullOrEmpty(searchTerm))
                {
                    query = query.Where(i => 
                        i.CustomerName.Contains(searchTerm) || 
                        i.PolicyNumber.Contains(searchTerm));
                }

                if (!string.IsNullOrEmpty(status))
                {
                    query = query.Where(i => i.ClaimStatus == status);
                }

                if (!string.IsNullOrEmpty(claimType))
                {
                    query = query.Where(i => i.ClaimType == claimType);
                }

                return await query
                    .OrderByDescending(c => c.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error retrieving filtered claims");
                throw;
            }
        }

        public async Task<InsuranceClaim> GetClaimByIdAsync(int id)
        {
            try
            {
                var claim = await _context.InsuranceClaims.FindAsync(id);
                if (claim == null)
                {
                    _logger.LogWarning("Claim with ID {Id} not found", id);
                    throw new KeyNotFoundException($"Claim with ID {id} not found");
                }
                return claim;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting claim with ID {Id}", id);
                throw;
            }
        }

        public async Task<InsuranceClaim> CreateClaimAsync(InsuranceClaim claim)
        {
            try
            {
                claim.CreatedAt = DateTime.UtcNow;
                claim.CreatedBy = "System";
                claim.UpdatedAt = DateTime.UtcNow;

                _context.InsuranceClaims.Add(claim);
                await _context.SaveChangesAsync();
                return claim;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error creating claim");
                throw;
            }
        }

        public async Task<InsuranceClaim> UpdateClaimAsync(InsuranceClaim claim)
        {
            try
            {
                var existingClaim = await _context.InsuranceClaims.FindAsync(claim.Id);
                if (existingClaim == null)
                {
                    _logger.LogWarning("Claim with ID {Id} not found for update", claim.Id);
                    throw new KeyNotFoundException($"Claim with ID {claim.Id} not found");
                }

                existingClaim.ClaimReference = claim.ClaimReference;
                existingClaim.CustomerName = claim.CustomerName;
                existingClaim.CustomerEmail = claim.CustomerEmail;
                existingClaim.PolicyNumber = claim.PolicyNumber;
                existingClaim.ClaimType = claim.ClaimType;
                existingClaim.ClaimDate = claim.ClaimDate;
                existingClaim.IncidentDate = claim.IncidentDate;
                existingClaim.EstimatedAmount = claim.EstimatedAmount;
                existingClaim.Description = claim.Description;
                existingClaim.ClaimStatus = claim.ClaimStatus;
                existingClaim.UpdatedAt = DateTime.UtcNow;

                await _context.SaveChangesAsync();
                return existingClaim;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error updating claim with ID {Id}", claim.Id);
                throw;
            }
        }

        public async Task DeleteClaimAsync(int id)
        {
            try
            {
                var claim = await _context.InsuranceClaims.FindAsync(id);
                if (claim == null)
                {
                    _logger.LogWarning("Claim with ID {Id} not found for deletion", id);
                    throw new KeyNotFoundException($"Claim with ID {id} not found");
                }

                _context.InsuranceClaims.Remove(claim);
                await _context.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting claim with ID {Id}", id);
                throw;
            }
        }

        public async Task<Dictionary<string, int>> GetClaimStatusCountsAsync()
        {
            try
            {
                var claims = await _context.InsuranceClaims.ToListAsync();
                var statusCounts = claims
                    .GroupBy(i => i.ClaimStatus)
                    .ToDictionary(g => g.Key, g => g.Count());

                return statusCounts;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting claim status counts");
                throw;
            }
        }

        public async Task<int> GetTotalClaimsCountAsync()
        {
            try
            {
                return await _context.InsuranceClaims.CountAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting total claims count");
                throw;
            }
        }

        public async Task<IEnumerable<InsuranceClaim>> GetRecentClaimsAsync(int count)
        {
            try
            {
                return await _context.InsuranceClaims
                    .OrderByDescending(c => c.CreatedAt)
                    .Take(count)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting recent claims");
                throw;
            }
        }

        public async Task<IEnumerable<InsuranceClaim>> GetClaimsByStatusAsync(string status)
        {
            try
            {
                return await _context.InsuranceClaims
                    .Where(c => c.ClaimStatus == status)
                    .OrderByDescending(c => c.CreatedAt)
                    .ToListAsync();
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error getting claims by status {Status}", status);
                throw;
            }
        }
    }
}