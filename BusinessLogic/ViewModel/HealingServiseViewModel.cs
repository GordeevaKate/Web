using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.ViewModel
{
    [DataContract]
    public class HealingServiseViewModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int HealingId { get; set; }
        [DataMember]
        public int ServiseId { get; set; }
        [DataMember]
        [DisplayName("Статус приёма")]
        public OutStatus Status { get; set; }
    }
}
