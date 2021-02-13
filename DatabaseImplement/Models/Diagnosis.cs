using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;
using System.Text;

namespace DatabaseImplement.Models { 
  public  class Diagnosis
    {
    public int? Id { get; set; }
    [Required]
    public string Name { get; set; }
        public virtual List<DiagnosisService> DiagnosisServices { get; set; }

    }
}
