namespace BCP.Core.Enums;

public class BikeRentalStatus : BaseEnum
{
    private BikeRentalStatus(int id, string value) : base(id, value)
    {
    }

    public static BikeRentalStatus Returned = new(1, "Returned");
    public static BikeRentalStatus Rented = new(2, "Rented");
    public static BikeRentalStatus OverTime = new(3, "Overtime");
}