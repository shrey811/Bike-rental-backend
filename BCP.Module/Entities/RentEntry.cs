using BCP.Core.Entities.user;
using BCP.Core.Enums;

namespace BCP.Core.Entities;

public class RentEntry
{
    public int Id { get; set; }
    public int BikeId { get; set; }
    public int UserId { get; set; }
    public DateTime RentedOn { get; set; }
    public DateTime RentedUntil { get; set; }
    public string? Remarks { get; set; }
    public BikeRentalStatus Status { get; set; }
    public Bike Bike { get; set; }
    public User RentedBy { get; set; }
}

