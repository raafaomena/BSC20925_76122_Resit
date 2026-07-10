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

        public IActionResult SetRole(string role)
        {
            if (role == "Manager" || role == "Student")
            {
                HttpContext.Session.SetString("UserRole", role);
                TempData["SuccessMessage"] = $"Switched to {role} mode";
            }
            return RedirectToAction(nameof(Dashboard));
        }

        private string GetCurrentRole()
        {
            return HttpContext.Session.GetString("UserRole") ?? "Student";
        }

        public async Task<IActionResult> Index(string searchTerm, string status, string claimType, int page = 1, int pageSize = 5, string sortBy = "ClaimDate", string sortOrder = "desc")
        {
            try
            {
                var currentRole = GetCurrentRole();
                var allClaims = await _claimService.GetFilteredClaimsAsync(searchTerm, status, claimType);
                var totalItems = allClaims.Count();
                
                if (sortBy == "ClaimDate")
                {
                    allClaims = sortOrder == "asc" ? allClaims.OrderBy(c => c.ClaimDate) : allClaims.OrderByDescending(c => c.ClaimDate);
                }
                else if (sortBy == "EstimatedAmount")
                {
                    allClaims = sortOrder == "asc" ? allClaims.OrderBy(c => c.EstimatedAmount) : allClaims.OrderByDescending(c => c.EstimatedAmount);
                }
                else
                {
                    allClaims = allClaims.OrderByDescending(c => c.CreatedAt);
                }
                
                var claims = allClaims.Skip((page - 1) * pageSize).Take(pageSize);
                
                ViewBag.SearchTerm = searchTerm;
                ViewBag.Status = status;
                ViewBag.ClaimType = claimType;
                ViewBag.CurrentPage = page;
                ViewBag.TotalPages = (int)Math.Ceiling((double)totalItems / pageSize);
                ViewBag.SortBy = sortBy;
                ViewBag.SortOrder = sortOrder;
                ViewBag.PageSize = pageSize;
                ViewBag.CurrentRole = currentRole;
                
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
            var currentRole = GetCurrentRole();
            if (currentRole != "Manager")
            {
                return View("AccessDenied");
            }

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
                    claim.CreatedBy = User.Identity?.Name ?? "System";
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
            var currentRole = GetCurrentRole();
            if (currentRole != "Manager")
            {
                return View("AccessDenied");
            }

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
            var currentRole = GetCurrentRole();
            if (currentRole != "Manager")
            {
                return View("AccessDenied");
            }

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
            var currentRole = GetCurrentRole();
            if (currentRole != "Manager")
            {
                return View("AccessDenied");
            }

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
            var currentRole = GetCurrentRole();
            if (currentRole != "Manager")
            {
                return View("AccessDenied");
            }

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
                var currentRole = GetCurrentRole();
                var statusCounts = await _claimService.GetClaimStatusCountsAsync();
                var totalClaims = await _claimService.GetTotalClaimsCountAsync();
                var recentClaims = await _claimService.GetRecentClaimsAsync(5);

                ViewBag.StatusCounts = statusCounts;
                ViewBag.TotalClaims = totalClaims;
                ViewBag.CurrentRole = currentRole;

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

        public IActionResult AccessDenied()
        {
            return View();
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