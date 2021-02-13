using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace DatabaseImplement.Models
{
    public class DiagnosisService
    {
        public int? Id { get; set; }
        public int DiagnosisId { get; set; }
        public int ServiceId { get; set; }
        public virtual Diagnosis Diagnosiss { get; set; }
        public virtual Service Services { get; set; }
    }
}
