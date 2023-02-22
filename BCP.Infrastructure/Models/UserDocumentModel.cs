using Microsoft.AspNetCore.Http;

namespace BCP.Infrastructure.Models;

public class UserDocumentModel
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public IFormFile License { get; set; }
    public IFormFile Citizenship { get; set; }
}