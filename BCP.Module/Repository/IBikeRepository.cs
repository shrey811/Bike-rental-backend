using BCP.Core.Dtos;
using BCP.Core.Entities;

namespace BCP.Core.Repository;

public interface IBikeRepository
{
    Task<Bike?> GetByIdAsync(int id);
    Task<Bike> InsertAsync(Bike bike);
    Task<Bike> UpdateAsync(Bike bike);
    // Task<List<BikeInsertDto>> GetAvailableBikesAsync();
    Task<ICollection<Bike>> GetAllAsync();

}
