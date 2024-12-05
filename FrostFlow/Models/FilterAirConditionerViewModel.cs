namespace FrostFlow.Models
{
    public class FilterAirConditionerViewModel
    {
        public string? SelectedBrand { get; set; }
        public List<string> Brands { get; set; } = new();
        public List<AirConditioner> AirConditioners { get; set; } = new();
        public List<string> Types { get; set; } // List of distinct types
        public string? SelectedType { get; set; } // Selected type

    }
}
