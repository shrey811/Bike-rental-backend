using BCP.Core.Entities;
using BCP.Core.Repository;

namespace BCP.Infrastructure.Repository;

public class ReviewRepository : BaseRepository<Review>, IReviewRepository
{
    private readonly AppDbContext _context;

    public ReviewRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}