using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardInformation.Core.Entities.FilterEntities
{
    public abstract class BaseFilter
    {
        public string? Name { get; set; }
        public string? Email { get; set; }


    }
}
