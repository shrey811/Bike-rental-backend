namespace BCP.Core.Exceptions;

public class BikeAlreadyInUseException : Exception
{
    public BikeAlreadyInUseException(string message = "Bike is already rented"):base(message)
    {
        
    }
}