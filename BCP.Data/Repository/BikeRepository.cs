using BCP.Core.Dtos;
using BCP.Core.Entities;
using BCP.Core.Repository;
using Microsoft.EntityFrameworkCore;

namespace BCP.Infrastructure.Repository;

public class BikeRepository : BaseRepository<Bike>, IBikeRepository
{
    private readonly AppDbContext _context;

    public BikeRepository(AppDbContext context) : base(context)
    {
        _context = context;
    }
}

