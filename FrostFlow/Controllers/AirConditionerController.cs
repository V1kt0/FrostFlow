using FrostFlow.Data;
using FrostFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.IO;
using System;
using System.Threading.Tasks;
using FrostFlow.Models;

namespace FrostFlow.Controllers
{
  public class AirConditionerController : Controller
  {
    private readonly ApplicationDbContext _context;
    private const int PageSize = 10;  // Define how many items per page

    public AirConditionerController(ApplicationDbContext context)
    {
      _context = context;
    }

    // GET: AirConditioner
    public async Task<IActionResult> Index(int page = 1)
    {
      // Total number of records
      var totalItems = await _context.AirConditioners.CountAsync();

      // Calculate total pages
      var totalPages = (int)Math.Ceiling(totalItems / (double)PageSize);

      // Fetch the records for the current page
      var airConditioners = await _context.AirConditioners
          .Skip((page - 1) * PageSize)
          .Take(PageSize)
          .ToListAsync();

      // Pass the data to the view
      var viewModel = new AirConditionerIndexViewModel
      {
        AirConditioners = airConditioners,
        CurrentPage = page,
        TotalPages = totalPages
      };

      return View(viewModel);
    }

    // GET: AirConditioner/Details/5
    public async Task<IActionResult> Details(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var airConditioner = await _context.AirConditioners
          .FirstOrDefaultAsync(m => m.Id == id);
      if (airConditioner == null)
      {
        return NotFound();
      }

      return View(airConditioner);
    }

    // GET: AirConditioner/Create
    public IActionResult Create()
    {
      return View();
    }

    // POST: AirConditioner/Create
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Create([Bind("ModelName,Brand,Price,Description,Type,CoolingCapacity")] AirConditioner airConditioner)
    {
      if (!ModelState.IsValid)
      {
        Console.WriteLine("ModelState is invalid!");
        var errors = ModelState.Values.SelectMany(v => v.Errors);
        foreach (var error in errors)
        {
          Console.WriteLine(error.ErrorMessage);
        }
        return View(airConditioner);
      }

      try
      {
        _context.Add(airConditioner);
        await _context.SaveChangesAsync();
        Console.WriteLine("AC successfully added!");
        return RedirectToAction(nameof(Index));
      }
      catch (Exception ex)
      {
        Console.WriteLine($"Error saving AC: {ex.Message}");
        return View(airConditioner);
      }
    }

    // GET: AirConditioner/Edit/5
    public async Task<IActionResult> Edit(int? id)
    {
      if (id == null)
      {
        return NotFound();
      }

      var airConditioner = await _context.AirConditioners.FindAsync(id);
      if (airConditioner == null)
      {
        return NotFound();
      }
      return View(airConditioner);
    }

    // POST: AirConditioner/Edit/5
    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Edit(int id, [Bind("Id,ModelName,Brand,Price,Description,Type,CoolingCapacity")] AirConditioner airConditioner)
    {
      if (id != airConditioner.Id)
      {
        return NotFound();
      }

      if (ModelState.IsValid)
      {
        try
        {
          _context.Update(airConditioner);
          await _context.SaveChangesAsync();
          Console.WriteLine("AC successfully updated!");
        }
        catch (DbUpdateConcurrencyException)
        {
          if (!AirConditionerExists(airConditioner.Id))
          {
            return NotFound();
          }
          else
          {
            throw;
          }
        }
        return RedirectToAction(nameof(Index));
      }
      return View(airConditioner);
    }

    // POST: AirConditioner/Delete/5
    [HttpPost, ActionName("Delete")]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> DeleteConfirmed(int id)
    {
      var airConditioner = await _context.AirConditioners.FindAsync(id);
      if (airConditioner != null)
      {
        _context.AirConditioners.Remove(airConditioner);
        await _context.SaveChangesAsync();
      }

      return RedirectToAction(nameof(Index));
    }

    private bool AirConditionerExists(int id)
    {
      return _context.AirConditioners.Any(e => e.Id == id);
    }

    public IActionResult Filter(string selectedType)
    {
      var viewModel = new FilterAirConditionerViewModel
      {
        AirConditioners = string.IsNullOrEmpty(selectedType) ?
              _context.AirConditioners.ToList() :
              _context.AirConditioners.Where(ac => ac.Type == selectedType).ToList(),

        Types = _context.AirConditioners
              .Select(ac => ac.Type)
              .Distinct()
              .ToList(),

        SelectedType = selectedType
      };

      return View(viewModel);
    }
  }
}
