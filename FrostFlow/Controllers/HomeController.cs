using FrostFlow.Data;
using FrostFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using System.Diagnostics;

namespace FrostFlow.Controllers
{
  public class HomeController : Controller
  {
    private readonly ApplicationDbContext _context;

    public HomeController(ApplicationDbContext context)
    {
      _context = context;
    }

    public async Task<IActionResult> Index()
    {
      // If user is not logged in, redirect to Login page
      if (!User.Identity.IsAuthenticated)
      {
        return RedirectToPage("/Account/Login", new { area = "Identity" });
      }

      var airConditioners = await _context.AirConditioners.ToListAsync();
      return View(airConditioners);
    }
  }
}
