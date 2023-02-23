using BCP.Core.Dtos;
using BCP.Core.Entities.user;
using BCP.Core.Exceptions;
using BCP.Core.Helper;
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
        var user = new User()
        {
            FirstName = dto.FirstName,
            LastName = dto.LastName,
            Password = PasswordHelper.HashPassword(dto.Password,dto.Email),
            PhoneNumber = dto.Phone,
            Email = dto.Email,
            Document = new UserDocument()
        };
        await _userRepository.InsertAsync(user);
        return user;
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