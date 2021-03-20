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

        [MaxLength(20)]
        public string Icon { get; set; }

        [MaxLength(20)]
        public string IconColor { get; set; }
    }
}
