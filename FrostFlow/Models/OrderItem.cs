using System.ComponentModel.DataAnnotations;

namespace FrostFlow.Models
{
  public class OrderItem
  {
    public int Id { get; set; }

    [Required]
    public int AirConditionerId { get; set; }
    public AirConditioner AirConditioner { get; set; } // Navigation property to AirConditioner

    [Required]
    public int Quantity { get; set; } // Number of this air conditioner ordered

    [Required]
    public decimal Price { get; set; } // Price of the air conditioner at the time of the order

    [Required]
    public int OrderId { get; set; } // Foreign key to Order
    public Order Order { get; set; } // Navigation property to Order
  }
}
