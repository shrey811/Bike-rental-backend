using BCP.Core.Entities;
using BCP.Core.Repository;

namespace BCP.Infrastructure.Repository;

public class BrandRepository :BaseRepository<Brand>, IBrandRepository
{
    public BrandRepository(AppDbContext context) : base(context)
    {
    }
}