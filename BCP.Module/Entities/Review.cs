namespace BCP.Core.Entities;

public class Review
{
    public int Id { get; set; }
    public string Message { get; set; }
    public int Rating { get; set; }
    public int BikeId { get; set; }
    public  virtual  Bike Bike { get; set; }
}