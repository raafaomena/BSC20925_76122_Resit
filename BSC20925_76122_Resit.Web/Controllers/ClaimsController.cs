using BSC20925_76122_Resit.Web.Models;
using BSC20925_76122_Resit.Web.Services;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace BSC20925_76122_Resit.Web.Controllers
{
    public class ClaimsController : Controller
    {
        private readonly IClaimService _claimService;
        private readonly ILogger<ClaimsController> _logger;

        public ClaimsController(IClaimService claimService, ILogger<ClaimsController> logger)
        {
            _claimService = claimService;
            _logger = logger;
        }

        public async Task<IActionResult> Index(string searchTerm, string status, string claimType)
        {
            try
            {
                var claims = await _claimService.GetFilteredClaimsAsync(searchTerm, status, claimType);
                
                ViewBag.SearchTerm = searchTerm;
                ViewBag.Status = status;
                ViewBag.ClaimType = claimType;
                
                ViewBag.StatusList = new SelectList(new[]
                {
                    new { Value = "", Text = "All Statuses" },
                    new { Value = "Submitted", Text = "Submitted" },
                    new { Value = "Under Review", Text = "Under Review" },
                    new { Value = "Approved", Text = "Approved" },
                    new { Value = "Rejected", Text = "Rejected" }
                }, "Value", "Text", status);
                
                ViewBag.ClaimTypeList = new SelectList(new[]
                {
                    new { Value = "", Text = "All Types" },
                    new { Value = "Motor", Text = "Motor" },
                    new { Value = "Home", Text = "Home" },
                    new { Value = "Travel", Text = "Travel" },
                    new { Value = "Health", Text = "Health" }
                }, "Value", "Text", claimType);

                return View(claims);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Index action");
                return View("Error", new ErrorViewModel 
                { 
                    ErrorMessage = "An error occurred while retrieving claims. Please try again." 
                });
            }
        }

        public async Task<IActionResult> Details(int id)
        {
            try
            {
                var claim = await _claimService.GetClaimByIdAsync(id);
                return View(claim);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning("Claim with ID {Id} not found", id);
                return View("Error", new ErrorViewModel 
                { 
                    ErrorMessage = "The requested claim was not found." 
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Details action for ID {Id}", id);
                return View("Error", new ErrorViewModel 
                { 
                    ErrorMessage = "An error occurred while retrieving the claim details." 
                });
            }
        }

        public IActionResult Create()
        {
            var claim = new InsuranceClaim
            {
                ClaimDate = DateTime.Today,
                IncidentDate = DateTime.Today.AddDays(-7),
                ClaimStatus = "Submitted"
            };

            PopulateDropDownLists();
            return View(claim);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(InsuranceClaim claim)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _claimService.CreateClaimAsync(claim);
                    TempData["SuccessMessage"] = "Claim created successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error creating claim");
                    ModelState.AddModelError("", "An error occurred while creating the claim.");
                }
            }

            PopulateDropDownLists();
            return View(claim);
        }

        public async Task<IActionResult> Edit(int id)
        {
            try
            {
                var claim = await _claimService.GetClaimByIdAsync(id);
                PopulateDropDownLists();
                return View(claim);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning("Claim with ID {Id} not found for edit", id);
                return View("Error", new ErrorViewModel 
                { 
                    ErrorMessage = "The requested claim was not found." 
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Edit action for ID {Id}", id);
                return View("Error", new ErrorViewModel 
                { 
                    ErrorMessage = "An error occurred while retrieving the claim." 
                });
            }
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, InsuranceClaim claim)
        {
            if (id != claim.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    await _claimService.UpdateClaimAsync(claim);
                    TempData["SuccessMessage"] = "Claim updated successfully!";
                    return RedirectToAction(nameof(Index));
                }
                catch (KeyNotFoundException)
                {
                    _logger.LogWarning("Claim with ID {Id} not found for update", id);
                    return View("Error", new ErrorViewModel 
                    { 
                        ErrorMessage = "The requested claim was not found." 
                    });
                }
                catch (Exception ex)
                {
                    _logger.LogError(ex, "Error updating claim with ID {Id}", id);
                    ModelState.AddModelError("", "An error occurred while updating the claim.");
                }
            }

            PopulateDropDownLists();
            return View(claim);
        }

        public async Task<IActionResult> Delete(int id)
        {
            try
            {
                var claim = await _claimService.GetClaimByIdAsync(id);
                return View(claim);
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning("Claim with ID {Id} not found for delete", id);
                return View("Error", new ErrorViewModel 
                { 
                    ErrorMessage = "The requested claim was not found." 
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Delete action for ID {Id}", id);
                return View("Error", new ErrorViewModel 
                { 
                    ErrorMessage = "An error occurred while retrieving the claim." 
                });
            }
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            try
            {
                await _claimService.DeleteClaimAsync(id);
                TempData["SuccessMessage"] = "Claim deleted successfully!";
                return RedirectToAction(nameof(Index));
            }
            catch (KeyNotFoundException)
            {
                _logger.LogWarning("Claim with ID {Id} not found for deletion", id);
                return View("Error", new ErrorViewModel 
                { 
                    ErrorMessage = "The requested claim was not found." 
                });
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error deleting claim with ID {Id}", id);
                return View("Error", new ErrorViewModel 
                { 
                    ErrorMessage = "An error occurred while deleting the claim." 
                });
            }
        }

        public async Task<IActionResult> Dashboard()
        {
            try
            {
                var statusCounts = await _claimService.GetClaimStatusCountsAsync();
                var totalClaims = await _claimService.GetTotalClaimsCountAsync();
                var recentClaims = await _claimService.GetRecentClaimsAsync(5);

                ViewBag.StatusCounts = statusCounts;
                ViewBag.TotalClaims = totalClaims;

                return View(recentClaims);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, "Error in Dashboard action");
                return View("Error", new ErrorViewModel 
                { 
                    ErrorMessage = "An error occurred while loading the dashboard." 
                });
            }
        }

        private void PopulateDropDownLists()
        {
            ViewBag.ClaimTypeList = new SelectList(new[]
            {
                new { Value = "Motor", Text = "Motor" },
                new { Value = "Home", Text = "Home" },
                new { Value = "Travel", Text = "Travel" },
                new { Value = "Health", Text = "Health" }
            }, "Value", "Text");

            ViewBag.StatusList = new SelectList(new[]
            {
                new { Value = "Submitted", Text = "Submitted" },
                new { Value = "Under Review", Text = "Under Review" },
                new { Value = "Approved", Text = "Approved" },
                new { Value = "Rejected", Text = "Rejected" }
            }, "Value", "Text");
        }
    }
}