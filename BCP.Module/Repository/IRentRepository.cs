
using BCP.Core.Entities;

namespace BCP.Core.Repository;

public interface IRentRepository
{
    Task<RentEntry> GetByIdAsync(int id);
    Task<RentEntry> InsertAsync(RentEntry rent);

    Task<ICollection<RentEntry>> GetAllAsync();
    Task<ICollection<RentEntry>> GetAllRentedAsync();
}
