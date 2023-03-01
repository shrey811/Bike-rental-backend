namespace BCP.Core.Dtos
{
    public class RentDto
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BikeId { get; set; }
        public int ApproverId { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime RentedUntil { get; set; }
        public string Remarks { get; set; }
        public BikeInsertDto Bike { get; set; }
        public UserRegisterDto RentedBy { get; set; }
        public UserRegisterDto ApprovedBy { get; set; }
        public string RentalStatus { get; set; }
    }
}