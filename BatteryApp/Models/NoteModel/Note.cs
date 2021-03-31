using BatteryApp.Models.ChargeModel;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace BatteryApp.Models.NoteModel
{
    public class Note
    {
        [Key]
        public int Id { get; set; }

        public DateTime Timestamp { get; set; } = DateTime.UtcNow;

        [MaxLength(450)]
        public string OwnerId { get; set; }

        [Required]
        public int ChargeId { get; set; }
        public virtual Charge Charge { get; set; }

        public string Description { get; set; }

        public IList<HistoryJson> History { get; set; }
    }
}
