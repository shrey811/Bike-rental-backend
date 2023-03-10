namespace BCP.ApiModels.Rent
{
    public class RentalInsertApiModel
    {
        public int UserId { get; set; }
        public int BikeId { get; set; }
        public DateTime? DateTime { get; set; }
        public decimal Price { get; set; }
        public DateTime RentedUntil { get; set; }
        public string? Remarks { get; set; }
    }
}