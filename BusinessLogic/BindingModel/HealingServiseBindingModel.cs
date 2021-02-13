using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.BindingModel
{
    [DataContract]
    public  class HealingServiseBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int HealingId { get; set; }
        [DataMember]
        public int ServiseId { get; set; }
        [DataMember]
        public OutStatus Status { get; set; }


    }
}
