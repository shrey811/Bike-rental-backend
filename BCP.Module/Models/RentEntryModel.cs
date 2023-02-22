using BCP.Core.Enums;

namespace BCP.Core.Models;

public class RentEntryModel
{
    public int UserId { get; set; }
    public int ApproverId { get; set; }
    public DateTime RentedOn { get; set; }
    public DateTime RentedUntil { get; set; }
    public BikeRentalStatus Status { get; set; }
    public string? Remarks { get; set; }
}