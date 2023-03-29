using BCP.Core.Entities;

namespace BCP.Core.Repository;

public interface IBrandRepository
{
    Task<Brand?> GetByIdAsync(int brandId);
    Task<ICollection<Brand>> GetAllAsync();
    
    Task<Brand> InsertAsync(Brand brand);
}