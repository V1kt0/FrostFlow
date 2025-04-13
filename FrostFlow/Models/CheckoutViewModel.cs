// Models/ViewModels/CheckoutViewModel.cs
namespace FrostFlow.Models.ViewModels
{
  public class CheckoutViewModel
  {
    public string FullName { get; set; }
    public string Address { get; set; }
    public string PaymentMethod { get; set; } // Example: "Credit Card", "PayPal"
    public decimal TotalAmount { get; set; }
  }
}
