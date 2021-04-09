using BatteryApp.Models.BatteryModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models.PriorityModel
{
    public class Priority : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Name { get; set; }

        [Required]
        [Range(0, int.MaxValue, ErrorMessage = "Only positive number allowed")]
        public int Severity { get; set; }

        [Required]
        [MaxLength(450)]
        public string OwnerId { get; set; }

        [Required]
        public int BatteryId { get; set; }
        public virtual Battery Battery { get; set; }

        public bool IsDefault { get; set; }

        [MaxLength(20)]
        public string BgColor { get; set; }

        [MaxLength(20)]
        public string FontColor { get; set; }

        public string DisplayName
        {
            get
            {
                return $"{Severity} - {Name}";
            }
        }
    }
}
