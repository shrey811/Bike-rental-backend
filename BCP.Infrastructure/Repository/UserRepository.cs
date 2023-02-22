using BCP.Core.Entities.user;
using BCP.Core.Models;
using BCP.Core.Repository;
using BCP.Infrastructure.Helper;
using Microsoft.Extensions.Configuration;

namespace BCP.Infrastructure.Repository;

public class UserRepository : IUserRepository
{
    private readonly AppDbContext _context;
    private readonly IConfiguration _webApplication;

    public UserRepository(AppDbContext context, 
        IConfiguration webApplication)
    {
        _context = context;
        _webApplication = webApplication;
    }
    
    public async Task<string> Register(RegisterModel register)
    {
        var password = register.Password;
        var confirmPassword = register.ConfirmPassword;
        if (!confirmPassword.Equals(password))
        {
            return "Password doesn't Match!";
        }
        var passwordHelper = PasswordHelper.HashPassword(register.Password, register.Email);
        var registration = new Registration()
        {
            Email = register.Email,
            Password = passwordHelper,
        };
        await _context.AddAsync(registration);
        await _context.SaveChangesAsync();
        return "Account is Created Successfully!";
    }

    public async Task<User> CreateUser(User user)
    {
        await _context.Users.AddAsync(user);
        await _context.SaveChangesAsync();
        return user;
    }

    public async Task<UserDocument> CreateDocument(UserDocument document)
    {
        await _context.UserDocuments.AddAsync(document);
        await _context.SaveChangesAsync();
        return document;
    }

    public async Task<bool> Login(LoginModel login)
    {
        var registration = _context.Registrations.First(acc => acc.Email == login.Email);
        var verifyAccount = PasswordHelper.VerifyPassword(login.Password, registration.Password, login.Email);
        return verifyAccount;
    }

    public Task<User> GetByCredentialsAsync(string identifier)
    {
        throw new NotImplementedException();
        // var user = _context.Users.FirstOrDefaultAsync(u=>u.)
    }
}