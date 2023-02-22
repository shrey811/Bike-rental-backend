using BCP.Core.Dtos;
using BCP.Core.Entities.user;
using BCP.Core.Exceptions;
using BCP.Core.Repository;

namespace BCP.Core.Services;

public class UserService
{
    private readonly IUserRepository _userRepository;

    public UserService(IUserRepository userRepository)
    {
        _userRepository = userRepository;
    }

    public async Task<User> RegisterAsync(UserRegisterDto dto)
    {
        throw new NotImplementedException();
    }

    public async Task<UserDocument> AddUserDocument(UserDocumentDto dto)
    {
        var user = await _userRepository.GetByIdAsync(dto.UserId) ?? throw new UserNotFoundException();
        var userDocument = new UserDocument()
        {
            Citizenship = dto.Citizenship,
            License = dto.License
        };
        user.Document = userDocument;
        await _userRepository.UpdateAsync(user);
        return userDocument;
    }
}