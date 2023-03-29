namespace BCP.Core.Entities;
using BCP.Core.Enums;



public class Bike
{
    public Bike()
    {
        RentalStatus = BikeRentalStatus.Available;
        Reviews =new HashSet<Review>();
    }
    public int Id { get; set; }
    public string Name { get; set; }
    public string NumberPlate { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
     public BikeRentalStatus RentalStatus { get; set; }
    
     
     public ICollection<Review> Reviews { get; set; }
    public decimal Rating { get; set; }
    public decimal KmRun { get; set; }
    public string Description { get; set; }
    public decimal Milage { get; set; }
    public string ImageUrl { get; set; }
    
    public decimal Price { get; set; }
    
    
    // public bool RentalStatus { get; set; }
}