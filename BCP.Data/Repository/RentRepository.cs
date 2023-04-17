    using BCP.Core.Entities;
    using Microsoft.EntityFrameworkCore;
    using BCP.Core.Entities.user;
    using BCP.Core.Enums;
    using BCP.Core.Repository;
    using MimeKit;
    using SmtpClient = MailKit.Net.Smtp.SmtpClient;


    namespace BCP.Infrastructure.Repository
    {
        public class RentRepository : BaseRepository<RentEntry>, IRentRepository
        {
            private readonly AppDbContext _context;

            public RentRepository(AppDbContext context) : base(context)
            {
                _context = context;
            }



            public async Task<ICollection<RentEntry>> GetAllRentedAsync()
            {
                return await _context.Rent
                    .Include(r => r.RentedBy)
                    // .Include(r => r.ApprovedBy)
                    .Where(r => r.Status == BikeRentalStatus.Rented)
                    .ToListAsync();
            }

            public async Task<RentEntry?> GetRentedEntryByBikeId(int bikeId)
            {
                return await _context.Rent
                    .Include(r => r.RentedBy)
                    // .Include(r => r.ApprovedBy)
                    .FirstOrDefaultAsync(r => r.BikeId == bikeId && r.Status == BikeRentalStatus.Rented);
            }

            public async Task SendRentalEmailAsync(User user, Bike bike)
            {
                var message = new MimeMessage();
                message.From.Add(new MailboxAddress("Bikers Choice", "shreychettri7@gmail.com"));
                message.To.Add(new MailboxAddress("check", "shreyasbudhathoki2015@gmail.com"));
                message.Subject = "Your bike rental details";

                // Create a HTML message body with the desired design
                var builder = new BodyBuilder();
                builder.HtmlBody = $@"
        <div style='background-color: #2D3E50; padding: 20px; border-radius: 20px 20px 0px 0px;'>
            <h1 style='color: #fff; text-align: center;'>Bike has been rented</h1>
        </div>
        <div style='background-color: #fff; padding: 20px; border-radius: 0px 0px 20px 20px;'>
   <p style='font-size: 18px; line-height: 1.5em; margin-bottom: 20px;'>Bikers Choice,</p>
            <p style='font-size: 18px; line-height: 1.5em; margin-bottom: 20px;'>A Bike has been rented by {user.FirstName} {user.LastName}</p>
            <p style='font-size: 18px; line-height: 1.5em; margin-bottom: 20px;'><strong>{bike.Name}</strong> has been rented. Please check the admin panel for more details of the rent.</p>
            <a href='http://localhost:3000/' style='background-color: #2D3E50; color: #fff; text-decoration: none; font-size: 16px; padding: 10px 20px; border-radius: 5px; display: inline-block;'>View Rental Details</a>
        </div>";

                message.Body = builder.ToMessageBody();

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync("shreychettri7@gmail.com", "nbqpyidztuxoesgq");
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }
            

        }
    }

