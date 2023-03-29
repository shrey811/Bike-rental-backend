
    using BCP.ApiModels;
    using BCP.Core.Services;
    using Microsoft.AspNetCore.Mvc;
    using BCP.ApiModels.User;
    using BCP.Core.Repository;

    namespace BCP.Controllers
    {
        [ApiController]
        [Route("api/[controller]")]
        public class OtpController : ControllerBase
        {
            private readonly OtpService _otpService;
             private readonly IOtpRepository _otpRepository;

            public OtpController(OtpService otpService, IOtpRepository otpRepository)
            {
                _otpService = otpService;
                 _otpRepository = otpRepository;
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
            [HttpGet("{otpcode}")]
            public async Task<ActionResult<bool>> GenerateOtp(string otpcode)
            {
                return await _otpRepository.CheckOtp(otpcode);
            }
        }
    }
    

