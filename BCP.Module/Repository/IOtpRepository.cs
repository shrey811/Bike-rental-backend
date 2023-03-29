using BCP.Core.Entities;

namespace BCP.Core.Repository
{
    public interface IOtpRepository
    {
        Task InsertAsync(OtpCode otpCode);
        Task<OtpCode> GetByCodeAndEmailAsync(string code, string email);
        Task SendOtpEmailAsync(string email, string otpCode);
        Task<bool>CheckOtp(string otpCode);
    }
}