using FrostFlow.Data;
using FrostFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrostFlow.Controllers
{
    public class AirConditionerController : Controller
    {
        private readonly ApplicationDbContext _context;

        public AirConditionerController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: AirConditioner
        public async Task<IActionResult> Index()
        {
            var airConditioners = await _context.AirConditioners.ToListAsync();
            Console.WriteLine($"Total AirConditioners: {airConditioners.Count}");  // Log count for verification
            return View(airConditioners);
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

        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("Id,ModelName,Brand,Price,Description,ImageUrl,Type")] AirConditioner airConditioner)
        {
            if (ModelState.IsValid)
            {
                _context.Add(airConditioner);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
            return View(airConditioner);
        }


        // GET: AirConditioner/Edit/5
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ModelName,Brand,Price,Description,ImageUrl,Type")] AirConditioner airConditioner)
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


        // POST: AirConditioner/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to.
        // For more details, see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Edit(int id, [Bind("Id,ModelName,Brand,Price,Description,ImageUrl,Type")] AirConditioner airConditioner, IFormFile image)
        {
            if (id != airConditioner.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    // Handle image upload if a new image is selected
                    if (image != null && image.Length > 0)
                    {
                        var fileName = Guid.NewGuid().ToString() + Path.GetExtension(image.FileName);
                        var filePath = Path.Combine(Directory.GetCurrentDirectory(), "wwwroot/images", fileName);
                        using (var stream = new FileStream(filePath, FileMode.Create))
                        {
                            await image.CopyToAsync(stream);
                        }
                        airConditioner.ImageUrl = $"/images/{fileName}";
                    }

                    _context.Update(airConditioner);
                    await _context.SaveChangesAsync();
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
            }

            await _context.SaveChangesAsync();
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
