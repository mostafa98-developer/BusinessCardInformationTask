using BusinessCardInformation.Core.Entities;
using BusinessCardInformation.Core.Entities.FilterEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardInformation.Core.IServices
{
    public interface IBusinessCardService: IService<BusinessCard>
    {
        Task<ServiceResult<IEnumerable<BusinessCard>>> GetAllAsync(BusinessCardFilter businessCardFilter);
    }
}
