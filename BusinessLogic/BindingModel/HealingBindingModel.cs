using System;
using System.Runtime.Serialization;

namespace BusinessLogic.BindingModel
{
    [DataContract]
    public class HealingBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int  DoctorId{ get; set; }
        [DataMember]
        public int PacientId { get; set; }
        [DataMember]
        public int DiagnosisId { get; set; }
        [DataMember]
        public DateTime Data { get; set; }
        [DataMember]
        public decimal Temperatura { get; set; }

    }
}
