namespace BCP.Core.Entities;

public class Bike
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string NumberPlate { get; set; }
    public int BrandId { get; set; }
    public Brand Brand { get; set; }
}