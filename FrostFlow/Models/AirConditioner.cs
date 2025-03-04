using System.ComponentModel.DataAnnotations;

namespace FrostFlow.Models
{
  public class AirConditioner
  {
    public int Id { get; set; }

    [Required(ErrorMessage = "Model Name is required")]
    public string ModelName { get; set; }

    [Required(ErrorMessage = "Brand is required")]
    public string Brand { get; set; }

    [Required(ErrorMessage = "Price is required")]
    [Range(1, double.MaxValue, ErrorMessage = "Price must be greater than 0")]
    public decimal Price { get; set; }

    public string Description { get; set; }

    [Required(ErrorMessage = "Type is required")]
    public string Type { get; set; }

    [Required(ErrorMessage = "Cooling Capacity is required")]
    [Range(1, int.MaxValue, ErrorMessage = "Cooling Capacity must be greater than 0")]
    public int CoolingCapacity { get; set; }
  }
}
