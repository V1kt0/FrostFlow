using Microsoft.AspNetCore.Mvc;
using FrostFlow.Models;

namespace FrostFlow.Controllers
{
    public class RoomController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }

        [HttpPost]
        public IActionResult Calculate(Room room)
        {
            room.BTU = CalculateBTU(room.Length, room.Width, room.Height);
            return View("Result", room);
        }

        private double CalculateBTU(double length, double width, double height)
        {
            double volume = length * width * height;
            return volume * 20; // Rough estimate: 20 BTUs per cubic foot
        }
    }
}