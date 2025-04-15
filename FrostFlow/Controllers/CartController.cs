using FrostFlow.Data;
using FrostFlow.Models;
using FrostFlow.Models.ViewModels;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Security.Claims;

namespace FrostFlow.Controllers
{
  [Authorize]
  public class CartController : Controller
  {
    private readonly ApplicationDbContext _context;
    private readonly UserManager<IdentityUser> _userManager;

    public CartController(ApplicationDbContext context, UserManager<IdentityUser> userManager)
    {
      _context = context;
      _userManager = userManager;
    }

    public async Task<IActionResult> Index()
    {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      var items = await _context.CartItems
          .Include(c => c.AirConditioner)
          .Where(c => c.UserId == userId)
          .ToListAsync();

      return View(items);
    }

    public async Task<IActionResult> Add(int id)
    {
      var userId = User.FindFirstValue(ClaimTypes.NameIdentifier);
      var existingItem = await _context.CartItems
          .FirstOrDefaultAsync(c => c.UserId == userId && c.AirConditionerId == id);

      if (existingItem != null)
      {
        existingItem.Quantity++;
      }
      else
      {
        var item = new CartItem
        {
          UserId = userId,
          AirConditionerId = id,
          Quantity = 1
        };
        _context.CartItems.Add(item);
      }

      await _context.SaveChangesAsync();
      return RedirectToAction("Index", "Home");
    }

    public async Task<IActionResult> Remove(int id)
    {
      var item = await _context.CartItems.FindAsync(id);
      if (item != null)
      {
        _context.CartItems.Remove(item);
        await _context.SaveChangesAsync();
      }
      return RedirectToAction("Index");
    }

    [HttpPost]
    public async Task<IActionResult> AddToCart(int airConditionerId, int quantity)
    {
      var userId = _userManager.GetUserId(User);

      var existingItem = await _context.CartItems
        .FirstOrDefaultAsync(ci => ci.UserId == userId && ci.AirConditionerId == airConditionerId);

      if (existingItem != null)
      {
        existingItem.Quantity += quantity;
      }
      else
      {
        var cartItem = new CartItem
        {
          UserId = userId,
          AirConditionerId = airConditionerId,
          Quantity = quantity
        };
        _context.CartItems.Add(cartItem);
      }

      await _context.SaveChangesAsync();
      return RedirectToAction("Index", "Cart");
    }

    [HttpGet]
    public async Task<IActionResult> Checkout()
    {
      var userId = _userManager.GetUserId(User);
      var cartItems = await _context.CartItems
          .Include(c => c.AirConditioner)
          .Where(c => c.UserId == userId)
          .ToListAsync();

      if (!cartItems.Any())
        return RedirectToAction("Index");

      var total = cartItems.Sum(item => item.AirConditioner.Price * item.Quantity);

      var model = new CheckoutViewModel
      {
        TotalAmount = total
      };

      return View(model);
    }

    [HttpPost]
    [ValidateAntiForgeryToken]
    public async Task<IActionResult> Checkout(CheckoutViewModel model)
    {
      if (!ModelState.IsValid)
        return View(model);

      var userId = _userManager.GetUserId(User);
      var cartItems = await _context.CartItems
          .Where(c => c.UserId == userId)
          .ToListAsync();

      var order = new Order
      {
        UserId = userId,
        OrderDate = DateTime.Now,
        Address = model.Address,
        PaymentMethod = model.PaymentMethod,
      };

    
      _context.Orders.Add(order);
      _context.CartItems.RemoveRange(cartItems);
      await _context.SaveChangesAsync();

      return RedirectToAction("Success");
    }


    public IActionResult Success()
    {
      return View();
    }

  }
}
