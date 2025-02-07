using FalconOne.Models.EntityContracts;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Caching.Memory;
using System.Linq.Expressions;

namespace FalconOne.DAL
{
    public class GenericRepository<T> : IGenericRepository<T> where T : class
    {
        protected readonly FalconOneContext _context;
        protected readonly IMemoryCache _memoryCache;

        public GenericRepository(FalconOneContext context, IMemoryCache cache)
        {
            _context = context;
            _memoryCache = cache;
        }

        public async Task<T> AddAsync(T entity, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddAsync(entity, cancellationToken);

            return entity;
        }

        public async Task<List<T>> AddRangeAsync(List<T> entities, CancellationToken cancellationToken)
        {
            await _context.Set<T>().AddRangeAsync(entities, cancellationToken);

            return entities;
        }

        public void Remove(T entity)
        {
            _context.Set<T>().Remove(entity);
        }

        public void RemoveRange(List<T> entities)
        {
            _context.Set<T>().RemoveRange(entities);
        }

        public async Task<T> GetByIdAsync(Guid id, CancellationToken cancellationToken)
        {
            var result = await _context.Set<T>().FindAsync(id, cancellationToken);

            return result;
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
        {
            var result = await _context.Set<T>().Where(expression)
                                                .FirstOrDefaultAsync(cancellationToken);

            return result;
        }

        public async Task<T> FindAsync(Expression<Func<T, bool>> expression, Func<IQueryable<T>, IQueryable<T>> includeProperties, CancellationToken cancellationToken)
        {
            var query = _context.Set<T>().Where(expression);

            if (includeProperties != null)
            {
                query = includeProperties(query);
            }

            var result = await query
                                    .AsSplitQuery()
                                    .FirstOrDefaultAsync(cancellationToken);

            return result;
        }

        public async Task<IEnumerable<T>> GetAllAsync(CancellationToken cancellationToken)
        {
            var result = await _context.Set<T>().ToListAsync(cancellationToken);

            return result;
        }

        public async void UpdateAsync(T entity)
        {
            await Task.Run(() =>
            {
                _context.Set<T>().Update(entity);
            });
        }

        public async Task UpdateRangeAsync(List<T> entities)
        {
            await Task.Run(() =>
            {
                _context.Set<T>().UpdateRange(entities);
            });
        }

        public async Task<List<T>> GetAllAsync(Expression<Func<T, bool>> expression, CancellationToken cancellationToken)
        {
            return await _context.Set<T>().Where(expression).ToListAsync(cancellationToken);
        }
    }
}
