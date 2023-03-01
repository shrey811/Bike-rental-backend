using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using BCP.ApiModels;
using BCP.Core.Dtos;
using BCP.Core.Entities.user;
using BCP.Core.Helper;
using BCP.Core.Models;
using BCP.Core.Repository;
using BCP.Core.Services;
using BCP.Core.ViewModels;
using BCP.Infrastructure.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace BCP.Controllers;
[Route("api/user")]
[ApiController]
public class UserController : ControllerBase
{
    private readonly IUserRepository _userRepository;
    private readonly IConfiguration _webApplication;
    private readonly IWebHostEnvironment _environment;
    private readonly UserService _userService;

    public UserController(
        IUserRepository userRepository, IConfiguration webApplication, IWebHostEnvironment environment, UserService userService)
    {
        _userRepository = userRepository;
        _webApplication = webApplication;
        _environment = environment;
        _userService = userService;
    }

    [AllowAnonymous]
    [HttpPost("register")]
    public async Task<IActionResult> RegisterUser(UserRegisterApiModel model)
    {
        var dto = new UserRegisterDto
        {
            FirstName = model.FirstName,
            LastName = model.LastName,
            Email = model.Email,
            Password = model.Password,
            Phone = model.Phone
        };
        var user = await _userService.RegisterAsync(dto);
        return Ok(user);
    }

    [AllowAnonymous]
    [HttpPost("login")]
    public async Task<IActionResult> LoginUser(LoginModel loginModel)
    {
        var verifyUser = await _userRepository.GetByCredentialsAsync(loginModel.Email);
        if (verifyUser == null)
        {
            return BadRequest("Invalid Email");
        }

        if (!PasswordHelper.VerifyPassword(loginModel.Password, verifyUser.Password, verifyUser.Email))
        {
            return BadRequest("Invalid Password");
        }
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
    [Authorize]
    [HttpGet("users")]
    public async Task<IActionResult> GetAllUsers()
    {
        var users = await _userRepository.GetAllAsync();
        return Ok(users);
    }
}