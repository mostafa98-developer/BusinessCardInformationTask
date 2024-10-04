using BusinessCardInformation.Core.Entities;
using BusinessCardInformation.Core.IRepository;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardInformation.Infrastructure.Services
{
    public class BusinessCardService : Service<BusinessCard>
    {
        public BusinessCardService(IRepository<BusinessCard> repository) : base(repository)
        {
        }

        // Add any additional methods specific to BusinessCard if needed
    }
}
