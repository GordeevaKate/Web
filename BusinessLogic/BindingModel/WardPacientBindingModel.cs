using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.BindingModel
{
    [DataContract]
  public  class WardPacientBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int PacientId { get; set; }
        [DataMember]
        public int WardId { get; set; }
    }
}
