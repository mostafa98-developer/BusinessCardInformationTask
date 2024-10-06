using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardInformation.Core.Entities.FilterEntities
{
    public class BusinessCardFilter: BaseFilter
    {
        public DateTime? Dob { get; set; }
        public string? Phone { get; set; }
        public string? Gender { get; set; }
    }
}
