using System.Threading.Tasks;
using BCP.Core.Entities.user;
using BCP.Core.Models;

namespace BCP.Core.Repository;

public interface IUserRepository
{
    Task<string> Register(RegisterModel register);
    Task<User> CreateUser(User user);
    Task<UserDocument> CreateDocument(UserDocument document);
    Task<bool> Login(LoginModel login);
    Task<User> GetByCredentialsAsync(string identifier);
}