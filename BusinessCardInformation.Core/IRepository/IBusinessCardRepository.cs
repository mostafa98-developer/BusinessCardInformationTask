using BusinessCardInformation.Core.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardInformation.Core.IRepository
{
    internal interface IBusinessCardRepository
    {
        Task<BusinessCard> AddAsync(BusinessCard card);
        Task<IEnumerable<BusinessCard>> GetAllAsync();
    }
}
