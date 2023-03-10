namespace BCP.Core.Exceptions;

public class BikeNotFoundException : Exception
{
    public BikeNotFoundException(string? message = "Bike is not available") : base(message)
    {
    }
}