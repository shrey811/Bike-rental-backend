using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCP.Core.Entities.user;
using BCP.Core.Models;
using BCP.Core.Repository;
using BCP.Core.ViewModels;
using BCP.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BCP.Controllers;
[Route("api/[Controller]")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _webApplication;
    private readonly IWebHostEnvironment _environment;
    public UserController(
        IUserRepository userRepository, IConfiguration webApplication, IWebHostEnvironment environment)
    {
        _userRepository = userRepository;
        _webApplication = webApplication;
        _environment = environment;
    }
    
    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(RegisterModel registerModel)
    {
        return Ok(await _userRepository.Register(registerModel));
    }
    
    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginModel loginModel)
    {
        var verifyUser = await _userRepository.Login(loginModel);
        if (verifyUser == true)
        {
            var issuer = _webApplication.GetValue<string>("Jwt:Issuer");
            var audience = _webApplication.GetValue<string>("Jwt:Audience");
            var key = Encoding.ASCII.GetBytes(_webApplication.GetValue<string>("Jwt:Key"));
            var tokenDescriptor = new SecurityTokenDescriptor()
            {
                Subject = new ClaimsIdentity(new[]
                {
                    new Claim("Id",Guid.NewGuid().ToString()),
                    new Claim(JwtRegisteredClaimNames.Email, loginModel.Email),
                    new Claim(JwtRegisteredClaimNames.Jti,
                        Guid.NewGuid().ToString())
                }),
                Expires = DateTime.UtcNow.AddMinutes(5),
                Issuer = issuer,
                Audience = audience,
                SigningCredentials = new SigningCredentials
                (new SymmetricSecurityKey(key),
                    SecurityAlgorithms.HmacSha512Signature)
            };
            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);
            var stringToken = tokenHandler.WriteToken(token);
            return Ok(stringToken);
        }
        else
        {
            return Unauthorized("Email or Password Doesn't Match");
        }
    }

    [Authorize]
    [HttpPost("userDocument")]
    public async Task<IActionResult> CreateUserDocument([FromForm] UserDocumentModel documentModel)
    {
        var user = new User()
        {
            FirstName = documentModel.FirstName,
            LastName = documentModel.LastName,
            PhoneNumber = documentModel.PhoneNumber
        };
        await _userRepository.CreateUser(user);
        var lastUserId = user.Id.ToString();
        var citizenshipFileName = Guid.NewGuid().ToString() + documentModel.Citizenship.FileName;
        var citizenshipPath = Path.Combine(_environment.WebRootPath, "temp/", citizenshipFileName);
        await documentModel.Citizenship.CopyToAsync(new FileStream(citizenshipPath, FileMode.Create));
        var licenseFileName = Guid.NewGuid().ToString() + documentModel.License.FileName;
        var licensePath = Path.Combine(_environment.WebRootPath, "temp/", licenseFileName);
        await documentModel.License.CopyToAsync(new FileStream(licensePath, FileMode.Create));
        var userDocument = new UserDocument()
        {
            UserId = lastUserId,
            Citizenship = citizenshipFileName,
            License = licenseFileName
        };
        await _userRepository.CreateDocument(userDocument);
        var userDocumentViewModel = new UserDocumentViewModel()
        {
            FirstName = documentModel.FirstName,
            LastName = documentModel.LastName,
            PhoneNumber = documentModel.PhoneNumber,
            Citizenship = documentModel.Citizenship.FileName,
            License = documentModel.License.FileName
        };
        return Ok(userDocumentViewModel);
    }
}