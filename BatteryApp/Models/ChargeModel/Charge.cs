using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.CategoryModel;
using BatteryApp.Models.PriorityModel;
using BatteryApp.Models.StatusModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models.ChargeModel
{
    public class Charge : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(100)]
        public string Title { get; set; }

        public string Description { get; set; }

        [Required]
        public int StatusId { get; set; }
        public virtual Status Status { get; set; }

        [Required]
        public int BatteryId { get; set; }
        public virtual Battery Battery { get; set; }

        [MaxLength(450)]
        public string OwnerId { get; set; }

        [Required]
        public int CategoryId { get; set; }
        public virtual Category Category { get; set; }

        [Required]
        public int PriorityId { get; set; }
        public virtual Priority Priority { get; set; }
    }
}
