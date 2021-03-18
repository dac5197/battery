using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models.UserProfileModel
{
    public class UserProfile : BaseEntity
    {
        [Key]
        public string Id { get; set; }
        
        [Required]
        [MaxLength(255)]
        [EmailAddress]
        public string Email { get; set; }

        [MaxLength(100)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [MaxLength(100)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        
    }
}
