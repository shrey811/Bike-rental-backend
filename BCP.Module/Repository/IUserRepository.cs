using BCP.Core.Entities.user;

namespace BCP.Core.Repository;

public interface IUserRepository
{
    Task<User> InsertAsync(User user);
    Task<User?> GetByCredentialsAsync(string identifier);
    Task<User?> GetByIdAsync(int userId);
    Task<User> UpdateAsync(User user);
    Task<ICollection<User>> GetAllAsync();
}