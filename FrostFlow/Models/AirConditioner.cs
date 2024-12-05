using System.ComponentModel.DataAnnotations;

namespace FrostFlow.Models
{
    public class AirConditioner
    {
        public int Id { get; set; }

        [Required]
        [StringLength(100)]
        public string ModelName { get; set; }

        [Required]
        [StringLength(50)]
        public string Brand { get; set; }

        [Required]
        public decimal Price { get; set; }

        public string Description { get; set; }

        public string ImageUrl { get; set; } // Optional: URL for the air conditioner image

        public string Type { get; set; }
    }
}
