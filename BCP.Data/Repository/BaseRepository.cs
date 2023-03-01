using Microsoft.EntityFrameworkCore;

namespace BCP.Infrastructure.Repository;

public class BaseRepository<TEntity> where TEntity : class
{
    private readonly AppDbContext _context;
    private readonly DbSet<TEntity> _set;

    protected BaseRepository(AppDbContext context)
    {
        _context = context;
        _set = _context.Set<TEntity>();
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        var entity = await _set.FindAsync(id);
        return entity;
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        await _set.AddAsync(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public async Task<TEntity> UpdateAsync(TEntity entity)
    {
        _set.Update(entity);
        await _context.SaveChangesAsync();
        return entity;
    }

    public IQueryable<TEntity> GetQueryable()
    {
        return _set;
    }

    public async Task<ICollection<TEntity>> GetAllAsync()
    {
        return await _set.ToListAsync();
    }

    public async Task DeleteAsync(int id)
    {
        var entity = await GetByIdAsync(id);
        if (entity != null)
        {
            _set.Remove(entity);
            await _context.SaveChangesAsync();
        }
    }
}