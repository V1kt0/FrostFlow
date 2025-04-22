using FrostFlow.Data;
using FrostFlow.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FrostFlow.Controllers
{
  [Authorize(Roles = "Admin")]
  [Route("Admin")]
  public class AdminController : Controller
  {
    private readonly ApplicationDbContext _context;

    public AdminController(ApplicationDbContext context)
    {
      _context = context;
    }

    // Redirect /Admin to /Admin/Dashboard
    [HttpGet("")]
    public IActionResult Index()
    {
      return RedirectToAction("Dashboard");
    }

    // Render the Dashboard view
    [HttpGet("Dashboard")]
    public async Task<IActionResult> Dashboard()
    {
      var total = await _context.AirConditioners.CountAsync();
      var avg = await _context.AirConditioners.AverageAsync(a => (decimal?)a.Price);
      var min = await _context.AirConditioners.MinAsync(a => (decimal?)a.Price);
      var top = await _context.AirConditioners
          .GroupBy(a => a.Brand)
          .OrderByDescending(g => g.Count())
          .Select(g => g.Key)
          .FirstOrDefaultAsync();

      var vm = new DashboardViewModel
      {
        TotalAirConditioners = total,
        AveragePrice = avg.GetValueOrDefault(),
        CheapestAirConditioner = min.GetValueOrDefault(),
        TopBrand = top
      };

      return View(vm); // This renders Views/Admin/Dashboard.cshtml
    }

    // Provide chart data for visualization
    [HttpGet("ChartData")]
    public async Task<IActionResult> ChartData()
    {
      var brandCounts = await _context.AirConditioners
          .GroupBy(a => a.Brand)
          .Select(g => new { label = g.Key, value = g.Count() })
          .ToListAsync();

      return Json(brandCounts);
    }
  }
}
