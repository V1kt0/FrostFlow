using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrostFlow.Models
{
  public class Order
  {

    [ForeignKey("AirConditionerId")]
    public AirConditioner AirConditioner { get; set; }
    public Customer Customer { get; set; } // Navigation property

    [Required]
    public int CustomerId { get; set; }

    [Required]
    public int AirConditionerId { get; set; }
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }  // Updated to refer to IdentityUser instead of CustomerId
    public IdentityUser User { get; set; } // Navigation property to IdentityUser (Customer)

    [Required]
    public DateTime OrderDate { get; set; } = DateTime.Now;

    // Additional fields for shipping and payment
    [Required]
    public string Address { get; set; }

    [Required]
    public string PaymentMethod { get; set; }

    // Navigation property for the order items (multiple air conditioners)
    public ICollection<OrderItem> OrderItems { get; set; } // One-to-many relationship with OrderItem

    // Calculated property for total amount
    public decimal TotalAmount => OrderItems != null ? OrderItems.Sum(item => item.Price * item.Quantity) : 0;
  }
}
