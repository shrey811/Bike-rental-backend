using BCP.Core.Entities;
using BCP.Core.Repository;
using Microsoft.Extensions.Configuration;

namespace BCP.Core.Services
{
    public class OtpService 
    {
        private readonly IOtpRepository _otpRepository;
        private readonly IConfiguration _configuration;

        public OtpService(IOtpRepository otpRepository, IConfiguration configuration)
        {
            _otpRepository = otpRepository;
            _configuration = configuration;
        }

        public async Task<string> GenerateOtpAsync(string email)
        {
            // var email = _configuration.GetValue<string>("AppSettings:DefaultEmail");
            var otpCode = GenerateOtpCode();
            var otpEntity = new OtpCode
            {
                Email = email,
                Code = otpCode,
               
              
            };
            await _otpRepository.InsertAsync(otpEntity);
            await _otpRepository.SendOtpEmailAsync(email, otpCode);
            return otpCode;
        }

        private string GenerateOtpCode()
        {
            var random = new Random();
            var otpCode = random.Next(100000, 999999).ToString();
            return otpCode;
        }
    }
}