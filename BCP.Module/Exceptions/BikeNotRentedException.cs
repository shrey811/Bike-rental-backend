namespace BCP.Core.Exceptions;

public class BikeNotRentedException : Exception
{
    public BikeNotRentedException(string message = "The brand cannot be found") : base(message)
    {
    }
}