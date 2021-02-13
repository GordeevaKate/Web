using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace DatabaseImplement.Models
{
    public class HealingServise
    {
        public int? Id { get; set; }
        public int HealingId { get; set; }
        public int ServiseId { get; set; }
        [Required]
        public OutStatus Status { get; set; }
        public virtual Healing Healings { get; set; }
        public virtual Service Services { get; set; }
    }
}
