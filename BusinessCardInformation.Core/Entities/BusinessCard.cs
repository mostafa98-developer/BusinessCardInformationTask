using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BusinessCardInformation.Core.Entities
{
    public class BusinessCard : BaseEntity
    {

        [Required]  
        public string Name { get; set; }

        [Required]  
        public string Gender { get; set; }

        [Required]  
        public DateTime DateOfBirth { get; set; }

        [Required]  
        [EmailAddress]  
        public string Email { get; set; }

        [Required]  
        public string Phone { get; set; }

        [Required]  
        public string Address { get; set; }

        public string PhotoBase64 { get; set; } = " ";
    }
}
