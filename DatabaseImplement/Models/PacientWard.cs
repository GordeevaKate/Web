using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace DatabaseImplement.Models
{
    public class PacientWard
    {

        public int? Id { get; set; }
        [Required]
        public int PacientId { get; set; }
        [Required]
        public int WardId { get; set; }
        public virtual Diagnosis Diagnosiss { get; set; }
        public virtual Service Services { get; set; }
    }
}
