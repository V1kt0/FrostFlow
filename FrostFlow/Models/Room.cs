namespace FrostFlow.Models
{
  public class Room
  {
    public int Id { get; set; }  // Primary Key
    public double Length { get; set; } // Length in meters
    public double Width { get; set; }  // Width in meters
    public double Height { get; set; } // Height in meters
    public double BTU { get; set; } // Calculated BTU for the air conditioner

    // Added property for room usage
    public string Usage { get; set; } // "bedroom", "living-room", "kitchen"
  }
}
