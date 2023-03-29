using BCP.Core.Entities;
using BCP.Core.Repository;
using Microsoft.EntityFrameworkCore;
using MailKit.Net.Smtp;
using MimeKit;

namespace BCP.Infrastructure.Repository;


    public class OtpRepository : BaseRepository<OtpCode>, IOtpRepository
    {
        private readonly AppDbContext _context;

        public OtpRepository(AppDbContext context) 
            : base(context)
        {
            _context = context;
        }

        public async Task InsertAsync(OtpCode otpCode)
        {
            await base.InsertAsync(otpCode);
        }
        
        

        public async Task<OtpCode> GetByCodeAndEmailAsync(string code, string email)
        {
            return await _context.Set<OtpCode>()
                .SingleOrDefaultAsync(x => x.Email == email && x.Code == code);
        }
        
        
        public async Task SendOtpEmailAsync(string email, string otpCode)
        {
            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("TEST 1234", "shreychettri7@gmail.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Your OTP Code";

            message.Body = new TextPart("plain")
            {
                Text = $"Your OTP code is: {otpCode}"
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("shreychettri7@gmail.com", "eywtdzkulybooakf");
                await client.SendAsync(message);
                await client.DisconnectAsync(true);
            }
        }

        public async Task<bool> CheckOtp(string otpCode)
        {
          var datas= await _context.Otp.FirstOrDefaultAsync(x => x.Code == otpCode);
          if (datas == null)
          {
              return false;
          }
          return true;
        }

        // public async Task InsertAsync(OtpCode otpCode)
        // {
        //     await base.InsertAsync(otpCode);
        // }
        //
        // public async Task<OtpCode> GetByCodeAndEmailAsync(string code, string email)
        // {
        //     return await _dbContext.OtpCode
        //         .SingleOrDefaultAsync(x => x.Email == email && x.Code == code);
        // }
    }


