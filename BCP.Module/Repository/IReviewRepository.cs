using BCP.Core.Entities;

namespace BCP.Core.Repository;

public interface IReviewRepository
{
    Task<Review?> GetByIdAsync(int bikeId);
    Task<Review> InsertAsync(Review review);
    Task<ICollection<Review>> GetAllAsync();
}