using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using FrostFlow.Data;
using FrostFlow.Models;

namespace FrostFlow.Controllers
{
    public class PaymentController : Controller
    {
        private readonly ApplicationDbContext _context;

        public PaymentController(ApplicationDbContext context)
        {
            _context = context;
        }

        // GET: Payment/Create
        public IActionResult Create(int orderId)
        {
            var order = _context.Orders.Find(orderId);
            if (order == null)
            {
                return NotFound();
            }

            var payment = new Payment { OrderId = order.Id, Amount = order.AirConditioner.Price };
            return View(payment);
        }

        // POST: Payment/Create
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind("OrderId, Amount, PaymentDate")] Payment payment)
        {
            if (ModelState.IsValid)
            {
                _context.Add(payment);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Confirmation), "Payment", new { id = payment.OrderId });
            }
            return View(payment);
        }

        // GET: Payment/Confirmation
        public async Task<IActionResult> Confirmation(int id)
        {
            var order = await _context.Orders.FindAsync(id);
            if (order == null)
            {
                return NotFound();
            }

            return View(order);
        }
    }
}
