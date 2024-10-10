using BusinessCardInformation.Core.Entities;
using BusinessCardInformation.Core.Entities.FilterEntities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardInformation.Core.IRepository
{
    public interface IBusinessCardRepository
    {
        Task<IEnumerable<BusinessCard>> GetAllAsync(BusinessCardFilter baseFilter);
        Task<bool> EmailExistsAsync(string email, int cardId);
        Task<bool> PhoneExistsAsync(string phone, int cardId);
    }
}
