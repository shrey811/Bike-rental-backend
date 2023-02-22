using BCP.Core.Entities.user;
using BCP.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace BCP.Infrastructure.Repository;

public class UserRepository :BaseRepository<User>, IUserRepository
{
    public UserRepository(AppDbContext context) : base(context)
    {
    }
    
    public async Task<User?> GetByCredentialsAsync(string identifier)
    {
        var user = await GetQueryable().FirstOrDefaultAsync(u => u.Email.ToLower().Equals(identifier.ToLower()));
        return user;
    }


}