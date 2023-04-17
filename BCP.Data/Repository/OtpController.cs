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
        
        
        // public async Task SendOtpEmailAsync(string email, string otpCode)
        // {
        //     var message = new MimeMessage();
        //     message.From.Add(new MailboxAddress("TEST 1234", "shreychettri7@gmail.com"));
        //     message.To.Add(new MailboxAddress("", email));
        //     message.Subject = "Your OTP Code";
        //
        //     // Create the HTML body of the email using a template
        //     string htmlBody = @"
        //  <html>
        //     <head>
        //         <link rel=""stylesheet"" href=""https://cdnjs.cloudflare.com/ajax/libs/uikit/3.7.4/css/uikit.min.css"" />
        //     </head>
        //     <body>
        //         <div class=""uk-card uk-card-default"">
        //             <div class=""uk-card-header uk-background-primary"">
        //                 <h2 class=""uk-card-title uk-text-center uk-text-white"">Your OTP Code</h2>
        //             </div>
        //             <div class=""uk-card-body uk-background-default"">
        //                 <div class=""uk-flex uk-flex-center"">
        //                     <h3>{0}</h3>
        //                 </div>
        //                 <p class=""uk-text-center"">Please enter this code to proceed.</p>
        //             </div>
        //             <div class=""uk-card-footer"">
        //                 <a class=""uk-button uk-button-primary uk-align-center"" href=""www.google.com"">Visit Website</a>
        //             </div>
        //         </div>
        //     </body>
        // </html>";
        //     htmlBody = string.Format(htmlBody, otpCode);
        //     // Add the HTML body to the message
        //     message.Body = new TextPart("html")
        //     {
        //         Text = htmlBody
        //     };
        //
        //     using (var client = new SmtpClient())
        //     {
        //         await client.ConnectAsync("smtp.gmail.com", 587, false);
        //         await client.AuthenticateAsync("shreychettri7@gmail.com", "nbqpyidztuxoesgq");
        //         await client.SendAsync(message);
        //         await client.DisconnectAsync(true);
        //     }
        // }

        public async Task SendOtpEmailAsync(string email, string otpCode)
        {
            string htmlBody = @"
       <div style=""background-color: #2196f3; padding: 20px; color: white; border-top-left-radius: 10px; border-top-right-radius: 10px;"">
                <h2 style=""margin-top: 0;"">Your OTP Code</h2>
                </div>
                <div style=""background-color: #f2f2f2; padding: 20px; border-bottom-left-radius: 10px; border-bottom-right-radius: 10px;"">
                <p style=""font-size: 18px; text-align: center;"">Here's your one-time password:</p>
                <h1 style=""font-size: 60px; text-align: center; margin: 40px 0;"">" + otpCode + @"</h1>
                <p style=""font-size: 18px; text-align: center;"">Use this code to log in to your account.</p>
                <div style=""text-align: center; margin-top: 40px;"">
                <a href=""http://localhost:3000/forget-password"" style=""background-color: #2196f3; color: white; padding: 10px 20px; border-radius: 5px; text-decoration: none;"">Click here to go back</a>
                
                </div>
                </div>";

            var message = new MimeMessage();
            message.From.Add(new MailboxAddress("Bikers Choice", "shreychettri7@gmail.com"));
            message.To.Add(new MailboxAddress("", email));
            message.Subject = "Your OTP Code";

            message.Body = new TextPart("html")
            {
                Text = htmlBody
            };

            using (var client = new SmtpClient())
            {
                await client.ConnectAsync("smtp.gmail.com", 587, false);
                await client.AuthenticateAsync("shreychettri7@gmail.com", "nbqpyidztuxoesgq");
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


