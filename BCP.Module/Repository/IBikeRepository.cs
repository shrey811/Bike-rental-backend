using BCP.Core.Entities;

namespace BCP.Core.Repository;

public interface IBikeRepository
{
    Task<Bike> InsertAsync(Bike bike);
}