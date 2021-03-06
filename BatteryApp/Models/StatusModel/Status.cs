using BatteryApp.Models.BatteryModel;
using BatteryApp.Models.ChargeModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models.StatusModel
{
    public class Status : BaseEntity
    {
        [Key]
        public int Id { get; set; }

        [Required]
        [MaxLength(20)]
        public string Name { get; set; }

        [Required]
        [Range(1, int.MaxValue, ErrorMessage = "Value should be greater than or equal to 1")]
        public int Order { get; set; }

        [Required]
        [MaxLength(450)]
        public string OwnerId { get; set; }

        [Required]
        public int BatteryId { get; set; }
        public virtual Battery Battery { get; set; }

        public ICollection<Charge> Charges { get; set; }

        [MaxLength(20)]
        public string BgColor { get; set; }

        [MaxLength(20)]
        public string FontColor { get; set; }
    }
}
