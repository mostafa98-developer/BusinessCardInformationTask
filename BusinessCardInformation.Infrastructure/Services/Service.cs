using BusinessCardInformation.Core.Entities;
using BusinessCardInformation.Core.Entities.FilterEntities;
using BusinessCardInformation.Core.IRepository;
using BusinessCardInformation.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardInformation.Infrastructure.Services
{
    public class Service<T> : IService<T> where T : BaseEntity
    {
        private readonly IRepository<T> _repository;


        public Service(IRepository<T> repository)
        {
            _repository = repository;
        }

        public async Task<ServiceResult<T>> GetByIdAsync(int id)
        {
               var entity = await _repository.GetByIdAsync(id);
            if (entity == null)
                return new ServiceResult<T>("Entity not found.");

            return new ServiceResult<T>(entity);
        }

        public async Task<ServiceResult<IEnumerable<T>>> GetAllAsync(BaseFilter filter)
        {
            var entities = await _repository.GetAllAsync(filter);
            if (entities == null)
                return new ServiceResult<IEnumerable<T>>("Entity not found.");

            return new ServiceResult<IEnumerable<T>>(entities);
        }

        public async Task<ServiceResult<T>> AddAsync(T entity)
        {
            if (entity == null)
                return new ServiceResult<T>("Invalid input.");

            await _repository.AddAsync(entity);
            return new ServiceResult<T>(entity);
        }

        public async Task<ServiceResult<T>> UpdateAsync(T entity)
        {
            if (entity == null)
                return new ServiceResult<T>("Invalid input.");

            var existingCard = await _repository.GetByIdAsync(entity.Id);
            if (existingCard == null)
                return new ServiceResult<T>("Entity not found.");

            await _repository.UpdateAsync(entity);
            return new ServiceResult<T>(entity);
        }

        public async Task<ServiceResult<T>> DeleteAsync(int id)
        {
            var existingCard = await _repository.GetByIdAsync(id);
            if (existingCard == null)
                return new ServiceResult<T>("Entity not found.");

            await _repository.DeleteAsync(id);
            return new ServiceResult<T>(existingCard);
        }

        public async Task<ServiceResult<IEnumerable<T>>> AddBulkAsync(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                return new ServiceResult<IEnumerable<T>>("Invalid input.");
            }

            var addedEntities = new List<T>();
            foreach (var entity in entities)
            {
                if (entity == null)
                {
                    return new ServiceResult<IEnumerable<T>>("Invalid input.");
                }

                await _repository.AddAsync(entity);
                addedEntities.Add(entity);
            }

            return new ServiceResult<IEnumerable<T>>(addedEntities);
        }

    }
}
