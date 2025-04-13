using System.ComponentModel.DataAnnotations;

namespace FrostFlow.Models
{
    public class Payment
    {
        public int Id { get; set; }

        [Required]
        public int OrderId { get; set; }
        public Order Order { get; set; } // Navigation property

        [Required]
        public decimal Amount { get; set; }

        [Required]
        public DateTime PaymentDate { get; set; } = DateTime.Now;

        public string PaymentMethod { get; set; } // Optional: e.g., "Credit Card", "PayPal"
    }
}
