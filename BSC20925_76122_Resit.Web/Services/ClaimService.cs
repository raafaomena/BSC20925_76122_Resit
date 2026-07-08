using Microsoft.EntityFrameworkCore;
using BSC20925_76122_Resit.Web.Data;
using BSC20925_76122_Resit.Web.Models;

namespace BSC20925_76122_Resit.Web.Services;

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
            _logger.LogError(ex, "Error retrieving all claims");
            throw;
        }
    }

    public async Task<IEnumerable<InsuranceClaim>> GetFilteredClaimsAsync(string? searchTerm, string? status, string? claimType)
    {
        try
        {
            var query = _context.InsuranceClaims.AsQueryable();

            if (!string.IsNullOrWhiteSpace(searchTerm))
            {
                query = query.Where(c => 
                    c.CustomerName.Contains(searchTerm) || 
                    c.PolicyNumber.Contains(searchTerm));
            }

            if (!string.IsNullOrWhiteSpace(status))
            {
                query = query.Where(c => c.ClaimStatus.ToString() == status);
            }

            if (!string.IsNullOrWhiteSpace(claimType))
            {
                query = query.Where(c => c.ClaimType.ToString() == claimType);
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

    public async Task<InsuranceClaim?> GetClaimByIdAsync(int id)
    {
        try
        {
            return await _context.InsuranceClaims.FindAsync(id);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error retrieving claim with ID {id}");
            throw;
        }
    }

    public async Task<InsuranceClaim> CreateClaimAsync(InsuranceClaim claim)
    {
        try
        {
            claim.CreatedAt = DateTime.UtcNow;
            claim.UpdatedAt = null;
            
            _context.InsuranceClaims.Add(claim);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation($"Claim {claim.ClaimReference} created successfully");
            return claim;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error creating claim {claim.ClaimReference}");
            throw;
        }
    }

    public async Task<bool> UpdateClaimAsync(InsuranceClaim claim)
    {
        try
        {
            var existingClaim = await _context.InsuranceClaims.FindAsync(claim.Id);
            
            if (existingClaim == null)
            {
                _logger.LogWarning($"Claim with ID {claim.Id} not found for update");
                return false;
            }

            existingClaim.ClaimReference = claim.ClaimReference;
            existingClaim.CustomerName = claim.CustomerName;
            existingClaim.CustomerEmail = claim.CustomerEmail;
            existingClaim.PolicyNumber = claim.PolicyNumber;
            existingClaim.ClaimType = claim.ClaimType;
            existingClaim.ClaimStatus = claim.ClaimStatus;
            existingClaim.ClaimDate = claim.ClaimDate;
            existingClaim.IncidentDate = claim.IncidentDate;
            existingClaim.EstimatedAmount = claim.EstimatedAmount;
            existingClaim.Description = claim.Description;
            existingClaim.UpdatedAt = DateTime.UtcNow;

            await _context.SaveChangesAsync();
            
            _logger.LogInformation($"Claim {claim.ClaimReference} updated successfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error updating claim {claim.ClaimReference}");
            throw;
        }
    }

    public async Task<bool> DeleteClaimAsync(int id)
    {
        try
        {
            var claim = await _context.InsuranceClaims.FindAsync(id);
            
            if (claim == null)
            {
                _logger.LogWarning($"Claim with ID {id} not found for deletion");
                return false;
            }

            _context.InsuranceClaims.Remove(claim);
            await _context.SaveChangesAsync();
            
            _logger.LogInformation($"Claim {claim.ClaimReference} deleted successfully");
            return true;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error deleting claim with ID {id}");
            throw;
        }
    }

    public async Task<bool> ClaimReferenceExistsAsync(string claimReference)
    {
        try
        {
            return await _context.InsuranceClaims
                .AnyAsync(c => c.ClaimReference == claimReference);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error checking claim reference {claimReference}");
            throw;
        }
    }

    public async Task<Dictionary<string, int>> GetClaimStatusCountsAsync()
    {
        try
        {
            var counts = await _context.InsuranceClaims
                .GroupBy(c => c.ClaimStatus.ToString())
                .Select(g => new { Status = g.Key, Count = g.Count() })
                .ToDictionaryAsync(g => g.Status, g => g.Count);
            
            return counts;
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error getting claim status counts");
            throw;
        }
    }

    public bool ClaimExists(int id)
    {
        return _context.InsuranceClaims.Any(e => e.Id == id);
    }
}