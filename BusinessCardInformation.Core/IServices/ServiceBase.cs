using BusinessCardInformation.Core.Entities;
using BusinessCardInformation.Core.Entities.FilterEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardInformation.Core.IServices
{
    public interface IService<T> where T : BaseEntity
    {
        Task<ServiceResult<T>> GetByIdAsync(int id);
        Task<ServiceResult<IEnumerable<T>>> GetAllAsync(BaseFilter baseFilter);
        Task<ServiceResult<T>> AddAsync(T entity);
        Task<ServiceResult<T>> UpdateAsync(T entity);
        Task<ServiceResult<T>> DeleteAsync(int id);
    }
}
