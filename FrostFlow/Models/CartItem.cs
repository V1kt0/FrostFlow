using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace FrostFlow.Models
{
  public class CartItem
  {
    [Key]
    public int Id { get; set; }

    [Required]
    public string UserId { get; set; }

    [Required]
    public int AirConditionerId { get; set; }

    [ForeignKey("AirConditionerId")]
    public AirConditioner AirConditioner { get; set; }

    public int Quantity { get; set; } = 1;
  }
}
