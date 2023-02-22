using Microsoft.AspNetCore.Http;

namespace BCP.Infrastructure.Models;

public class UserDocumentModel
{
    public int UserId { get; set; }
    public string License { get; set; }
    public string Citizenship { get; set; }
}