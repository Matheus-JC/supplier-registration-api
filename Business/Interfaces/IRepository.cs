using System.Linq.Expressions;
using SupplierRegServer.Business.Models;

namespace SupplierRegServer.Business.Interfaces;

public interface IRepository<TEntity> : IDisposable where TEntity : Entity
{
    Task<List<TEntity>> GetAll();
    Task<TEntity> GetById(Guid id);
    Task<IEnumerable<TEntity>> Search(Expression<Func<TEntity, bool>> predicate);
    Task Create(TEntity entity);
    Task Update(TEntity entity);
    Task Delete(Guid id);
    Task<int> SaveChanges();
}