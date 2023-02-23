using System.ComponentModel.DataAnnotations.Schema;

namespace BCP.Core.Entities.user;

public class User
{
    public int Id { get; set; }
    public string? FirstName { get; set; }
    public string? LastName { get; set; }
    public string? PhoneNumber { get; set; }
    public string Email { get; set; }
    public string Password { get; set; }
    
    [Column(TypeName = "jsonb")]
    public UserDocument Document { get; set; }
}