using BCP.Core.Entities;
using BCP.Core.Repository;

namespace BCP.Infrastructure.Repository;

public class BikeRepository : IBikeRepository
{
    private readonly AppDbContext _context;

    public BikeRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<Bike> InsertAsync(Bike bike)
    {
        await _context.Set<Bike>().AddAsync(bike);
        await _context.SaveChangesAsync();
        return bike;
    }
}