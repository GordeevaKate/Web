using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace DatabaseImplement.Models
{
    public class Service
    {
        public int? Id { get; set; }
        [Required]
        public string Name { get; set; }
        [Required]
        public double Cena { get; set; }
        [Required]
        public ServiceStatus Status { get; set; }
        public virtual List<HealingServise> HealingServises { get; set; }
        public virtual List<DiagnosisService> DiagnosisServices { get; set; }
    }
}
