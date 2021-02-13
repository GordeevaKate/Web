using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.ViewModel
{
    [DataContract]
    public class DiagnosisServiceViewModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int DiagnosisId { get; set; }
        [DataMember]
        public int ServiceId { get; set; }
    }
}
