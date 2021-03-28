using BatteryApp.Models.ChargeModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models.TagModel
{
    public class ChargeTagRelation
    {
        [Key]
        public int Id { get; set; }

        [Required]
        public int ChargeId { get; set; }
        public virtual Charge Charge { get; set; }

        [Required]
        public int TagId { get; set; }
        public virtual Tag Tag { get; set; }
    }
}
