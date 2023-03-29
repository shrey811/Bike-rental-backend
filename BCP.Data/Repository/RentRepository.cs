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
                message.From.Add(new MailboxAddress("TEST 1234", "shreychettri7@gmail.com"));
                message.To.Add(new MailboxAddress("check", "shreyasbudhathoki2015@gmail.com"));
                message.Subject = "Your bike rental details";
                message.Body = new TextPart("plain")
                {
                    Text =
                        $"The bike {bike.Name} has been rented by {user.FirstName} {user.LastName}.Please check the admin panel for more details of the rent."
                };

                using (var client = new SmtpClient())
                {
                    await client.ConnectAsync("smtp.gmail.com", 587, false);
                    await client.AuthenticateAsync("shreychettri7@gmail.com", "eywtdzkulybooakf");
                    await client.SendAsync(message);
                    await client.DisconnectAsync(true);
                }
            }

        }
    }

