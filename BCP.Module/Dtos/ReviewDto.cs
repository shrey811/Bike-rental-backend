namespace BCP.Dtos.Review
{
    public class ReviewDto
    {
        public int Id { get; set; }
        public string Message { get; set; }
        public int Rating { get; set; }
        public int BikeId { get; set; }
    }
}