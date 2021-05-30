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

        [Required]
        [MaxLength(100)]
        [DisplayName("First Name")]
        public string FirstName { get; set; }

        [Required]
        [MaxLength(100)]
        [DisplayName("Last Name")]
        public string LastName { get; set; }

        [MaxLength(20)]
        public string BgColor { get; set; }

        [MaxLength(20)]
        public string FontColor { get; set; }

        public int PaginationCount { get; set; }

        public string Initials 
        {
            get
            {
                return $"{GetInitial(FirstName).ToUpper()}{GetInitial(LastName).ToUpper()}";
            }
        }

        private static string GetInitial(string Name)
        {
            if (String.IsNullOrWhiteSpace(Name))
            {
                return "";
            }

            return Name.Substring(0, 1);
        }
    }
}
