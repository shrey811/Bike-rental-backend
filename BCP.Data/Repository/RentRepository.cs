// using BCP.Core.Entities;
// using Microsoft.EntityFrameworkCore;
// using System.Collections.Generic;
// using System.Linq;
// using System.Threading.Tasks;
// using BCP.Core.Repository;
//
// namespace BCP.Infrastructure.Repository
// {
//     public class RentRepository : BaseRepository<RentEntry>, IRentRepository
//     {
//         private readonly AppDbContext _context;
//
//         public RentRepository(AppDbContext context) : base(context)
//         {
//             _context = context;
//         }
//
//         public async Task<ICollection<RentEntry>> GetAllRentedAsync()
//         {
//             return await _context.RentEntries
//                 .Include(r => r.RentedBy)
//                 .Include(r => r.ApprovedBy)
//                 .Where(r => r.Status == BikeRentalStatus.Rented)
//                 .ToListAsync();
//         }
//     }
// }