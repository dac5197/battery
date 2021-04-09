using BatteryApp.Models.BatteryModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models.CategoryModel
{
    public class Category : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(10)]
        public string Name { get; set; }

        [Required]
        [MaxLength(450)]
        public string OwnerId { get; set; }

        [Required]
        public int BatteryId { get; set; }
        public virtual Battery Battery { get; set; }

        [MaxLength(20)]
        public string Icon { get; set; }

        [MaxLength(20)]
        public string IconColor { get; set; }

        public bool IsDefaultChildCategory { get; set; }

        public bool IsDefaultChargeCategory { get; set; }
    }
}
