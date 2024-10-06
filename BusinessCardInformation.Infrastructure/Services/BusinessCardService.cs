using BusinessCardInformation.Core.Entities;
using BusinessCardInformation.Core.IRepository;
using BusinessCardInformation.Core.IServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardInformation.Infrastructure.Services
{
    public class BusinessCardService :  Service<BusinessCard>, IBusinessCardService
    {
        
        public BusinessCardService(IRepository<BusinessCard> repository) : base(repository)
        {
           
        }

        
    }
}
