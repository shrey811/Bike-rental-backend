
    using BCP.ApiModels;
    using BCP.Core.Services;
    using Microsoft.AspNetCore.Mvc;
    using System.Threading.Tasks;
    using BCP.ApiModels.User;

    namespace BCP.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class OtpController : ControllerBase
        {
            private readonly OtpService _otpService;
            // private readonly IOtpRepository _otpRepository;

            public OtpController(OtpService otpService, OtpService otpRepository)
            {
                _otpService = otpService;
                // _otpRepository = otpRepository;
            }

            [HttpPost]
            public async Task<ActionResult<OtpCode>> GenerateOtp([FromBody] OtpRequest request)
            {
                var email = request.Email;
                var otpCode = await _otpService.GenerateOtpAsync(email);
                 // await _otpRepository.SendOtpEmailAsync(email, otpCode);
                var otpModel = new OtpCode
                {
                    Email = email,
                    Code = otpCode
                };

                return Ok(otpModel);
            }
        }
    }
    

