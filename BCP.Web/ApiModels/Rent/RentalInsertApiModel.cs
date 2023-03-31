namespace BCP.ApiModels.Rent
{
    public class RentalInsertApiModel
    {
        public int UserId { get; set; }
        public int BikeId { get; set; }
        public DateTime? RentedOn { get; set; }
        public int Price { get; set; }
        public DateTime RentedUntil { get; set; }
        public string? Remarks { get; set; }
        public string ImageUrl { get; set; }
    }
}