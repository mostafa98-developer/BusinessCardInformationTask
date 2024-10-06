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
    public class BusinessCardRepository : Repository<BusinessCard>, IBusinessCardRepository
    {
        public BusinessCardRepository(BusinessCardDbContext dbContext) : base(dbContext)
        {
        }

        public async Task<IEnumerable<BusinessCard>> GetAllAsync(BusinessCardFilter filter)
        {
            var query = _dbContext.BusinessCards.AsQueryable();

            // Apply filters dynamically
            if (filter != null)
            {
                if (!string.IsNullOrEmpty(filter.Name))
                {
                    query = query.Where(u => u.Name.Contains(filter.Name));
                }

                if (filter.Dob.HasValue)
                {
                    query = query.Where(u => u.DateOfBirth.Date == filter.Dob.Value.Date);
                }

                if (!string.IsNullOrEmpty(filter.Phone))
                {
                    query = query.Where(u => u.Phone.Contains(filter.Phone));
                }

                if (!string.IsNullOrEmpty(filter.Gender))
                {
                    query = query.Where(u => u.Gender == filter.Gender);
                }

                if (!string.IsNullOrEmpty(filter.Email))
                {
                    query = query.Where(u => u.Email.Contains(filter.Email));
                }
            }

            // Execute the query and return results
            return await query.ToListAsync();
        }
    }
}
