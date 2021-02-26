using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.ViewModel
{
    [DataContract]
    public class HealingViewModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int DoctorId { get; set; }
        [DataMember]
        public int PacientId { get; set; }
        [DataMember]
        public int DiagnosisId { get; set; }
        [DataMember]
        [DisplayName("Дата")]
        public DateTime Data { get; set; }
        [DataMember]
        [DisplayName("Температура больного")]
        public decimal Temperatura { get; set; }
    }
}
