using FrostFlow.Data;
using FrostFlow.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace FrostFlow.Controllers
{
  public class RoomController : Controller
  {
    private readonly ApplicationDbContext _context;

    public RoomController(ApplicationDbContext context)
    {
      _context = context;
    }

    public IActionResult Index()
    {
      return View();
    }

    [HttpPost]
    public async Task<IActionResult> Calculate(Room room)
    {
      // Calculate room volume (m³)
      double roomVolume = room.Length * room.Width * room.Height;

      // Base BTU calculation (141 BTU per cubic meter)
      room.BTU = roomVolume * 141;

      // Adjust for room usage (if applicable)
      if (room.Usage == "kitchen")
      {
        room.BTU += 4000;
      }
      else if (room.Usage == "living-room")
      {
        room.BTU += 2000;
      }

      // Round to the nearest 1000 BTU
      room.BTU = Math.Round(room.BTU / 1000) * 1000;

      // Find the best Air Conditioner based on the calculated BTU
      AirConditioner bestAC = await FindBestAC(room.BTU);

      ViewData["BestAC"] = bestAC;

      return View("Result", room);
    }

    private async Task<AirConditioner> FindBestAC(double requiredBTU)
    {
      return await _context.AirConditioners
                           .OrderBy(ac => Math.Abs(ac.CoolingCapacity - requiredBTU))
                           .FirstOrDefaultAsync();
    }
  }
}
