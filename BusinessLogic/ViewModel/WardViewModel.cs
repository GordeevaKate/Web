using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.ViewModel
{
    [DataContract]
    public class WardViewModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        [DisplayName("Номер")]
        public int Number { get; set; }
        [DataMember]
        [DisplayName("Вместимость")]
        public int Mesto { get; set; }
    }
}
