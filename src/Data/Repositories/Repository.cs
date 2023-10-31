using System.Linq.Expressions;
using Microsoft.EntityFrameworkCore;
using SupplierRegServer.Business.Interfaces;
using SupplierRegServer.Business.Models;
using SupplierRegServer.Data.Context;

namespace SupplierRegServer.Data.Repositories;

public abstract class Repository<TEntity> : IRepository<TEntity> where TEntity : Entity
{
    protected readonly DbContext _context;
    protected readonly DbSet<TEntity> _dbSet;

    protected Repository(DbContext context)
    {
        _context = context;
        _dbSet = _context.Set<TEntity>();
    }

    public virtual async Task Create(TEntity entity)
    {
        _dbSet.Add(entity);
        await SaveChanges();
    }


    public virtual async Task Update(TEntity entity)
    {
        _dbSet.Update(entity);
        await SaveChanges();
    }

    public virtual async Task<List<TEntity>> GetAll()
    {
        return await _dbSet.ToListAsync();
    }

    public virtual async Task<TEntity?> GetById(Guid id)
    {
        return await _dbSet.FindAsync(id);
    }

    public async Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate)
    {
        return await _dbSet.AsNoTracking().Where(predicate).ToListAsync();
    }

    public virtual async Task Delete(Guid id)
    {
        var entity = await GetById(id);

        if (entity != null)
        {
            _dbSet.Remove(entity);
            await SaveChanges();
        }
    }

    public async Task<int> SaveChanges()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
        GC.SuppressFinalize(this);
    }
}
