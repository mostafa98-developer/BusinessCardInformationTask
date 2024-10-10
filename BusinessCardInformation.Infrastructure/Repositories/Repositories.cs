using BusinessCardInformation.Core.Entities;
using BusinessCardInformation.Core.Entities.FilterEntities;
using BusinessCardInformation.Core.IRepository;
using BusinessCardInformation.Infrastructure.Data;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardInformation.Infrastructure.Repositories
{
    public class Repository<T> : IRepository<T> where T : BaseEntity
    {
        protected readonly BusinessCardDbContext _dbContext;
        protected readonly DbSet<T> _dbSet;

        public Repository(BusinessCardDbContext dbContext)
        {
            _dbContext = dbContext;
            _dbSet = _dbContext.Set<T>();
        }

        public async Task<T> GetByIdAsync(int id)
        {
            return await _dbSet.FindAsync(id);
        }

        public async Task<IEnumerable<T>> GetAllAsync(BaseFilter filter)
        {
            var query = _dbContext.BusinessCards.AsQueryable();

            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    query = query.Where(u => u.Name.Contains(filter.Name));
                }

                if (!string.IsNullOrEmpty(filter.Email))
                {
                    query = query.Where(u => u.Email.Contains(filter.Email));
                }
            }
            return (IEnumerable<T>)await query.ToListAsync();

        }

        public async Task<T> AddAsync(T entity)
        {
            _dbSet.Add(entity);
            await _dbContext.SaveChangesAsync();
            return entity;
        }

        public async Task UpdateAsync(T entity)
        {
            var trackedEntity = _dbContext.ChangeTracker.Entries<T>().FirstOrDefault(e => e.Entity.Id == entity.Id);

            if (trackedEntity != null)
            {
                // Detach the already tracked entity
                _dbContext.Entry(trackedEntity.Entity).State = EntityState.Detached;
            }

            // Attach the new entity and mark it as modified
            _dbSet.Attach(entity);
            _dbContext.Entry(entity).State = EntityState.Modified;
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(int id)
        {
            var entity = await _dbSet.FindAsync(id);
            if (entity != null)
            {
                _dbSet.Remove(entity);
                await _dbContext.SaveChangesAsync();
            }
        }
    }
}
