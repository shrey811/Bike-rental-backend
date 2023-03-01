namespace BCP.Core.Entities;
using BCP.Core.Enums;

public class Bike
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string NumberPlate { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
    public decimal Rating { get; set; }
    public decimal KmRun { get; set; }
    public string Description { get; set; }
    public decimal Milage { get; set; }
    public string ImageUrl { get; set; }
    
 
    // public bool IsRented { get; set; }
    //     public DateTime? RentedAt { get; set; }
    //     public User RentedBy { get; set; }
  
}