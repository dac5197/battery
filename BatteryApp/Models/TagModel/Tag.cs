using BatteryApp.Models.BatteryModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models.TagModel
{
    public class Tag
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        public int BatteryId { get; set; }
        public virtual Battery Battery { get; set; }

        [MaxLength(450)]
        public string OwnerId { get; set; }
    }
}
