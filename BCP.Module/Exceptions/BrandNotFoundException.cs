namespace BCP.Core.Exceptions;

public class BrandNotFoundException : Exception
{
    public BrandNotFoundException(string? message = "The brand cannot be found") : base(message)
    {
    }
}