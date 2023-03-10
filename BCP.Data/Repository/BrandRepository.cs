using BCP.Core.Entities;
using BCP.Core.Repository;

namespace BCP.Infrastructure.Repository;

public class BrandRepository :BaseRepository<Brand>, IBrandRepository
{
    private readonly AppDbContext _context;
    public BrandRepository(AppDbContext context) : base(context)
    {
        
        _context = context;
    }
}