using BusinessCardInformation.Core.Common;
using BusinessCardInformation.Core.Common.Enums;
using BusinessCardInformation.Core.Entities;
using BusinessCardInformation.Core.Entities.FilterEntities;
using BusinessCardInformation.Core.IRepository;
using BusinessCardInformation.Core.IServices;
using BusinessCardInformation.Infrastructure.Repositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardInformation.Infrastructure.Services
{
    public class BusinessCardService : Service<BusinessCard>, IBusinessCardService
    {
        private readonly IBusinessCardRepository _businessCardRepository;
        public BusinessCardService(IRepository<BusinessCard> repository, IBusinessCardRepository businessCardRepository) : base(repository)
        {
            _businessCardRepository = businessCardRepository;
        }


        public async Task<ServiceResult<IEnumerable<BusinessCard>>> GetAllAsync(BusinessCardFilter businessCardFilter)
        {
            var entities = await _businessCardRepository.GetAllAsync(businessCardFilter);
            if (entities == null)
                return new ServiceResult<IEnumerable<BusinessCard>> { Status = ResultCode.NotFound, Errors = new List<Error> { new Error("\"BusinessCard not found.") } };


            return new ServiceResult<IEnumerable<BusinessCard>>{ Status = ResultCode.Ok, Data = entities };
        }

    }
}
