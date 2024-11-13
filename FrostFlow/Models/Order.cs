using System.ComponentModel.DataAnnotations;

namespace FrostFlow.Models
{
    public class Order
    {
        public int Id { get; set; }

        [Required]
        public int AirConditionerId { get; set; }
        public AirConditioner AirConditioner { get; set; } // Navigation property

        [Required]
        public int CustomerId { get; set; }
        public Customer Customer { get; set; } // Navigation property

        [Required]
        public DateTime OrderDate { get; set; } = DateTime.Now;
    }
}
