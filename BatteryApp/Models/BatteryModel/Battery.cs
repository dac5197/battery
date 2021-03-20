using BatteryApp.Models.ChargeModel;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models.BatteryModel
{
    public class Battery : BaseEntity
    {

        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Name { get; set; }

        public string Description { get; set; }

        [Required]
        [MaxLength(450)]
        public string OwnerId { get; set; }

        [Required]
        [DisplayName("Active")]
        public bool IsActive { get; set; } = true;

    }
}
