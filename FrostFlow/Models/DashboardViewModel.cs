namespace FrostFlow.Models
{
  public class DashboardViewModel
  {
    // Total number of air conditioners available
    public int TotalAirConditioners { get; set; }

    // Average price of air conditioners
    public decimal AveragePrice { get; set; }

    // Price of the cheapest air conditioner
    public decimal CheapestAirConditioner { get; set; }

    // The brand with the highest number of air conditioners available
    public string TopBrand { get; set; }
  }
}
