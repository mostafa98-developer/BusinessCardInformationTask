using BusinessCardInformation.Core.Entities;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardInformation.Infrastructure.Data
{
    public class BusinessCardDbContext : DbContext
    {
        public BusinessCardDbContext(DbContextOptions<BusinessCardDbContext> options)
            : base(options) { }

        public DbSet<BusinessCard> BusinessCards { get; set; }
    }
}
