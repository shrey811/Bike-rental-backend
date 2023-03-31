namespace BCP.ApiModels.Rent;

public class ReturnApiModel
{
    public int BikeId { get; set; }
    public DateTime ReturnedOn { get; set; }
    public string ImageUrl { get; set; }
}