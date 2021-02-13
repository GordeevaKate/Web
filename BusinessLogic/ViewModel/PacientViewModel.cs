using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.ViewModel
{
    [DataContract]
    public class PacientViewModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        [DisplayName("ФИО")]
        public string FIO { get; set; }
        [DataMember]
        [DisplayName("Страховой полис")]
        public int Polis { get; set; }
        [DataMember]
        [DisplayName("Адресс")]
        public string Adress { get; set; }
    }
}
