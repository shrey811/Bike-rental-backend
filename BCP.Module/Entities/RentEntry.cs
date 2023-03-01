using BCP.Core.Entities.user;
using BCP.Core.Enums;

namespace BCP.Core.Entities;

public class RentEntry
{
    public int Id { get; set; }
    public int UserId { get; set; }
    public int ApproverId { get; set; }
    public DateTime DateTime { get; set; }
    public DateTime RentedUntil { get; set; }
    public string? Remarks { get; set; }
    public BikeRentalStatus Status { get; set; }  // new property

    public User RentedBy { get; set; }
    public User ApprovedBy { get; set; }
}