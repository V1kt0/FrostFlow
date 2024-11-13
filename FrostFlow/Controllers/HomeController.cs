using FrostFlow.Data;
using FrostFlow.Models;
using Microsoft.AspNetCore.Mvc;
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

        // Index page - shows all AC models initially
        public IActionResult Index()
        {
            var airConditioners = _context.AirConditioners.ToList();
            return View(airConditioners);
        }

        // Search page - handles search query and filters AC models
        public IActionResult Search(string query)
        {
            // If no query is provided, return all air conditioners
            if (string.IsNullOrWhiteSpace(query))
            {
                var airConditioners = _context.AirConditioners.ToList();
                return View("Index", airConditioners);
            }

            // Perform search based on model name or brand
            var searchResults = _context.AirConditioners
                .Where(ac => ac.ModelName.Contains(query) || ac.Brand.Contains(query))
                .ToList();

            return View("Index", searchResults);
        }
    }
}
