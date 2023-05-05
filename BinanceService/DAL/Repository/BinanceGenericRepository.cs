using DAL.Context;
using Domain.Entity;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;

namespace DAL.Repository
{
    public class BinanceGenericRepository<T>: IGenericRepository<T> where T : BaseEntity
    {
        private readonly ApplicationContext _context;
        private DbSet<T> _dbSet;

        public BinanceGenericRepository(ApplicationContext context)
        {
            _context = context;
            _dbSet = context.Set<T>();
        }

        public async Task<T> Create(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Сущность не может быть Null");
            await _dbSet.AddAsync(entity);
            await _context.SaveChangesAsync();
            return entity;
        }

        public async Task<T> FindById(int id) => await _dbSet.FindAsync(id);

        public async Task<IEnumerable<T>> Get() => await _dbSet.ToListAsync();

        public async Task<IEnumerable<T>> Get(Expression<Func<T, bool>> predicate, int limit, int offset) =>
                 await _dbSet.Where(predicate).Take(limit).Skip(offset).ToListAsync();

        public async Task Remove(int id)
        {
            var entity = await _dbSet.FindAsync(id);

            _dbSet.Remove(entity ?? throw new NullReferenceException("Сущность не найдена")); ;
            await _context.SaveChangesAsync();
        }

        public async Task<T> Update(T entity)
        {
            if (entity == null)
                throw new ArgumentNullException("Обновляемая сущность не может быть пустой");
            _dbSet.Update(entity);
            await _context.SaveChangesAsync();
            return entity;
        }
        

        private IQueryable<T> Include(params Expression<Func<T, object>>[] includeProperties)
        {
            IQueryable<T> query = _dbSet.AsNoTracking();
            return includeProperties
                .Aggregate(query, (current, includeProperty) => current.Include(includeProperty));
        }
    }
}
