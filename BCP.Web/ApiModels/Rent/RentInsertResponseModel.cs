namespace BCP.ApiModels.Rent;

public class RentInsertResponseModel
{
    public int Id { get; set; }
    public string BikeName { get; set; }
    public string BrandName { get; set; }
    public string User { get; set; }
    public DateTime RentedUntil { get; set; }
    public DateTime RentedOn { get; set; }
    
}