namespace BCP.Core.Exceptions;

public class UserNotFoundException : Exception
{
    public UserNotFoundException(string? message = "Cannot find the user") : base(message)
    {
    }
}