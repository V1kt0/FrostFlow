using FrostFlow.Data;
using FrostFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Linq;
using System.Threading.Tasks;

namespace FrostFlow.Controllers
{
  public class HomeController : Controller
  {
    private readonly ApplicationDbContext _context;
    private const int PageSize = 9;  // Define how many items per page (adjust based on your design)

    public HomeController(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<IActionResult> Index(int page = 1)
    {
      // If user is not logged in, redirect to Login page
      if (!User.Identity.IsAuthenticated)
      {
        return RedirectToPage("/Account/Login", new { area = "Identity" });
      }

      // Get the total count of air conditioners
      var totalCount = await _context.AirConditioners.CountAsync();

      // Calculate the total number of pages
      var totalPages = (int)Math.Ceiling(totalCount / (double)PageSize);

      // Get the list of air conditioners for the current page
      var airConditioners = await _context.AirConditioners
          .Skip((page - 1) * PageSize)
          .Take(PageSize)
          .ToListAsync();

      // Create a view model to pass to the view
      var viewModel = new AirConditionerIndexViewModel
      {
        AirConditioners = airConditioners,
        CurrentPage = page,
        TotalPages = totalPages
      };  

      return View(viewModel);
    }
  }
}
