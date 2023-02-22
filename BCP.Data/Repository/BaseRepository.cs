using Microsoft.EntityFrameworkCore;

namespace BCP.Infrastructure.Repository;

public class BaseRepository<TEntity> where TEntity:class
{
    private AppDbContext _context;
    private DbSet<TEntity> _set;

    protected BaseRepository(AppDbContext context)
    {
        _context = context;
    }

    public async Task<TEntity?> GetByIdAsync(int id)
    {
        var entity = await _set.FindAsync(id);
        return entity;
    }

    public async Task<TEntity> InsertAsync(TEntity entity)
    {
        await _set.AddAsync(entity);
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
}