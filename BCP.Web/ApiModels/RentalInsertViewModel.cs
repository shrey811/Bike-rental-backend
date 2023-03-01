using System;

namespace BCP.Core.Dtos
{
    public class RentalInsertViewModel
    {
        public int Id { get; set; }
        public int UserId { get; set; }
        public int BikeId { get; set; }
        public DateTime DateTime { get; set; }
        public DateTime RentedUntil { get; set; }
        public string Remarks { get; set; }
        public string BikeModel { get; set; }
        public string UserName { get; set; }
        public string RentalStatus { get; set; }
    }
}