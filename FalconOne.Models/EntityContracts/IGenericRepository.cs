using System.Linq.Expressions;

namespace FalconOne.Models.EntityContracts
{
    public interface IGenericRepository<T> where T : class
    {
        Task<T> AddAsync(T entity, CancellationToken cancellationToken);
        Task<List<T>> AddRangeAsync(List<T> entities, CancellationToken cancellationToken);
        Task<T> FindAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
        Task<T> FindAsync(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IQueryable<T>> includeProperties, CancellationToken cancellationToken);
        Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken);
        Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken);
        Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken);
        void Remove(T entity);
        void RemoveRange(List<T> entities);
        void UpdateAsync(T entity);
        Task UpdateRangeAsync(List<T> entities);
    }
}
