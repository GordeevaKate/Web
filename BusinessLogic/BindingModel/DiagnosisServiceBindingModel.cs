using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.BindingModel
{
    [DataContract]
    public  class DiagnosisServiceBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int DiagnosisId { get; set; }
        [DataMember]
        public int ServiceId { get; set; }
    }
}
