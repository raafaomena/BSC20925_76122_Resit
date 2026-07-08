using Microsoft.AspNetCore.Mvc;
using BSC20925_76122_Resit.Web.Models;
using BSC20925_76122_Resit.Web.Models.Enums;
using BSC20925_76122_Resit.Web.Services;

namespace BSC20925_76122_Resit.Web.Controllers;

public class ClaimsController : Controller
{
    private readonly IClaimService _claimService;
    private readonly ILogger<ClaimsController> _logger;

    public ClaimsController(IClaimService claimService, ILogger<ClaimsController> logger)
    {
        _claimService = claimService;
        _logger = logger;
    }

    public async Task<IActionResult> Index(string? searchTerm, string? status, string? claimType)
    {
        try
        {
            var claims = await _claimService.GetFilteredClaimsAsync(searchTerm, status, claimType);
            
            ViewData["SelectedStatus"] = status;
            ViewData["SelectedClaimType"] = claimType;
            ViewData["SearchTerm"] = searchTerm;
            
            return View(claims);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Index action");
            return View("Error");
        }
    }

    public async Task<IActionResult> Details(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            var claim = await _claimService.GetClaimByIdAsync(id.Value);
            
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in Details action for ID {id}");
            return View("Error");
        }
    }

    public IActionResult Create()
    {
        ViewBag.ClaimTypes = Enum.GetNames(typeof(ClaimType)).ToList();
        ViewBag.ClaimStatuses = Enum.GetNames(typeof(ClaimStatus)).ToList();
        return View();
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ClaimReference,CustomerName,CustomerEmail,PolicyNumber,ClaimType,ClaimStatus,ClaimDate,IncidentDate,EstimatedAmount,Description")] InsuranceClaim claim)
    {
        try
        {
            if (await _claimService.ClaimReferenceExistsAsync(claim.ClaimReference))
            {
                ModelState.AddModelError("ClaimReference", "A claim with this reference already exists.");
            }

            if (ModelState.IsValid)
            {
                await _claimService.CreateClaimAsync(claim);
                TempData["SuccessMessage"] = "Claim created successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ClaimTypes = Enum.GetNames(typeof(ClaimType)).ToList();
            ViewBag.ClaimStatuses = Enum.GetNames(typeof(ClaimStatus)).ToList();
            return View(claim);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Create POST action");
            ModelState.AddModelError("", "An error occurred while creating the claim.");
            ViewBag.ClaimTypes = Enum.GetNames(typeof(ClaimType)).ToList();
            ViewBag.ClaimStatuses = Enum.GetNames(typeof(ClaimStatus)).ToList();
            return View(claim);
        }
    }

    public async Task<IActionResult> Edit(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            var claim = await _claimService.GetClaimByIdAsync(id.Value);
            
            if (claim == null)
            {
                return NotFound();
            }

            ViewBag.ClaimTypes = Enum.GetNames(typeof(ClaimType)).ToList();
            ViewBag.ClaimStatuses = Enum.GetNames(typeof(ClaimStatus)).ToList();
            return View(claim);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in Edit GET action for ID {id}");
            return View("Error");
        }
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,ClaimReference,CustomerName,CustomerEmail,PolicyNumber,ClaimType,ClaimStatus,ClaimDate,IncidentDate,EstimatedAmount,Description")] InsuranceClaim claim)
    {
        if (id != claim.Id)
        {
            return NotFound();
        }

        try
        {
            var existingClaim = await _claimService.GetClaimByIdAsync(id);
            if (existingClaim != null && existingClaim.ClaimReference != claim.ClaimReference)
            {
                if (await _claimService.ClaimReferenceExistsAsync(claim.ClaimReference))
                {
                    ModelState.AddModelError("ClaimReference", "A claim with this reference already exists.");
                }
            }

            if (ModelState.IsValid)
            {
                var success = await _claimService.UpdateClaimAsync(claim);
                
                if (!success)
                {
                    return NotFound();
                }
                
                TempData["SuccessMessage"] = "Claim updated successfully!";
                return RedirectToAction(nameof(Index));
            }

            ViewBag.ClaimTypes = Enum.GetNames(typeof(ClaimType)).ToList();
            ViewBag.ClaimStatuses = Enum.GetNames(typeof(ClaimStatus)).ToList();
            return View(claim);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in Edit POST action for ID {id}");
            ModelState.AddModelError("", "An error occurred while updating the claim.");
            ViewBag.ClaimTypes = Enum.GetNames(typeof(ClaimType)).ToList();
            ViewBag.ClaimStatuses = Enum.GetNames(typeof(ClaimStatus)).ToList();
            return View(claim);
        }
    }

    public async Task<IActionResult> Delete(int? id)
    {
        if (id == null)
        {
            return NotFound();
        }

        try
        {
            var claim = await _claimService.GetClaimByIdAsync(id.Value);
            
            if (claim == null)
            {
                return NotFound();
            }

            return View(claim);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in Delete GET action for ID {id}");
            return View("Error");
        }
    }

    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
        try
        {
            var success = await _claimService.DeleteClaimAsync(id);
            
            if (!success)
            {
                return NotFound();
            }

            TempData["SuccessMessage"] = "Claim deleted successfully!";
            return RedirectToAction(nameof(Index));
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, $"Error in DeleteConfirmed action for ID {id}");
            TempData["ErrorMessage"] = "An error occurred while deleting the claim.";
            return RedirectToAction(nameof(Index));
        }
    }

    public async Task<IActionResult> Dashboard()
    {
        try
        {
            var statusCounts = await _claimService.GetClaimStatusCountsAsync();
            var allClaims = await _claimService.GetAllClaimsAsync();
            
            ViewBag.StatusCounts = statusCounts;
            ViewBag.TotalClaims = allClaims.Count();
            ViewBag.TotalValue = allClaims.Sum(c => c.EstimatedAmount);
            
            return View();
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Error in Dashboard action");
            return View("Error");
        }
    }
}