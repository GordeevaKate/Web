using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace DatabaseImplement.Models
{

    public class Healing
    {
        public int? Id { get; set; }
        public int DoctorId { get; set; }
        public int PacientId { get; set; }
        public int DiagnosisId { get; set; }
        [Required]
        public DateTime Data { get; set; }
        [Required]
        public decimal Temperatura { get; set; }
        public virtual List<HealingServise> HealingServises { get; set; }
    }
}
