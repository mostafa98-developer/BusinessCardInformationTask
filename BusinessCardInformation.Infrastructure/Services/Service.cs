using BusinessCardInformation.Core.Common;
using BusinessCardInformation.Core.Common.Enums;
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
            {
                return new ServiceResult<T>(ResultCode.NotFound)
                {
                    Errors = new List<Error> { new Error("Entity not found.") }
                };
            }

            return new ServiceResult<T>(ResultCode.Ok) { Data = entity };
        }

        public async Task<ServiceResult<IEnumerable<T>>> GetAllAsync(BaseFilter filter)
        {
            var entities = await _repository.GetAllAsync(filter);
            if (entities == null || !entities.Any())
            {
                return new ServiceResult<IEnumerable<T>>(ResultCode.NotFound)
                {
                    Errors = new List<Error> { new Error("No entities found.") }
                };
            }

            return new ServiceResult<IEnumerable<T>>(ResultCode.Ok) { Data = entities };
        }

        public async Task<ServiceResult<T>> AddAsync(T entity)
        {
            if (entity == null)
            {
                return new ServiceResult<T>(ResultCode.BadRequest)
                {
                    Errors = new List<Error> { new Error("Invalid input.") }
                };
            }

            await _repository.AddAsync(entity);
            return new ServiceResult<T>(ResultCode.Created) { Data = entity };
        }

        public async Task<ServiceResult<T>> UpdateAsync(T entity)
        {
            if (entity == null)
            {
                return new ServiceResult<T>(ResultCode.BadRequest)
                {
                    Errors = new List<Error> { new Error("Invalid input.") }
                };
            }

            var existingEntity = await _repository.GetByIdAsync(entity.Id);
            if (existingEntity == null) 
            {
                return new ServiceResult<T>(ResultCode.NotFound)
                {
                    Errors = new List<Error> { new Error("Entity not found.") }
                };
            }

            await _repository.UpdateAsync(entity);
            return new ServiceResult<T>(ResultCode.Ok) { Data = entity };
        }

        public async Task<ServiceResult<T>> DeleteAsync(int id)
        {
            var existingEntity = await _repository.GetByIdAsync(id);
            if (existingEntity == null)
            {
                return new ServiceResult<T>(ResultCode.NotFound)
                {
                    Errors = new List<Error> { new Error("Entity not found.") }
                };
            }

            await _repository.DeleteAsync(id);
            return new ServiceResult<T>(ResultCode.Ok) { Data = existingEntity };
        }

        public async Task<ServiceResult<IEnumerable<T>>> AddBulkAsync(IEnumerable<T> entities)
        {
            if (entities == null || !entities.Any())
            {
                return new ServiceResult<IEnumerable<T>>(ResultCode.BadRequest)
                {
                    Errors = new List<Error> { new Error("Invalid input.") }
                };
            }

            var addedEntities = new List<T>();
            foreach (var entity in entities)
            {
                if (entity == null)
                {
                    return new ServiceResult<IEnumerable<T>>(ResultCode.BadRequest)
                    {
                        Errors = new List<Error> { new Error("Invalid input.") }
                    };
                }

                await _repository.AddAsync(entity);
                addedEntities.Add(entity);
            }

            return new ServiceResult<IEnumerable<T>>(ResultCode.Created) { Data = addedEntities };
        }
    }

}
