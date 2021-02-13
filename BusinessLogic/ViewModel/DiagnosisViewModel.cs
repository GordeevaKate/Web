using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.ViewModel
{
    [DataContract]
  public  class DiagnosisViewModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        [DisplayName("Диагноз")]
        public string Name { get; set; }
    }
}
