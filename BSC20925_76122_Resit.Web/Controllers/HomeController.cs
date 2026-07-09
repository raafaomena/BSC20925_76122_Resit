using BSC20925_76122_Resit.Web.Models;
using Microsoft.AspNetCore.Mvc;

namespace BSC20925_76122_Resit.Web.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            return RedirectToAction("Dashboard", "Claims");
        }

        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = System.Diagnostics.Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}