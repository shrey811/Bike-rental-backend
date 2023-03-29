
using BCP.Core.Entities;
using BCP.Core.Entities.user;

namespace BCP.Core.Repository;

public interface IRentRepository
{
    Task<RentEntry> GetByIdAsync(int id);
    Task<RentEntry> InsertAsync(RentEntry rent);

    Task<ICollection<RentEntry>> GetAllAsync();
    Task<ICollection<RentEntry>> GetAllRentedAsync();
    Task<RentEntry?> GetRentedEntryByBikeId(int dtoBikeId);
    Task<RentEntry> UpdateAsync(RentEntry rentEntry);
    // Task<RentEntry> RentBikeAsync(RentEntry rent);
    // Task<RentEntry> SendOtpEmailAsync(RentEntry rent);
    Task SendRentalEmailAsync(User user, Bike bike);

}
