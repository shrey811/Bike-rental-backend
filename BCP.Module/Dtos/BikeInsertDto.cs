namespace BCP.Core.Dtos;

public class BikeInsertDto
{
    public int Id { get; set; }
    public string Name { get; set; }
    public string NumberPlate { get; set; }
    public int brandId { get; set; }

    public decimal Rating { get; set; }
    public decimal KmRun { get; set; }
    public string Description { get; set; }
    public decimal Milage { get; set; }
    public string ImageUrl { get; set; }
}