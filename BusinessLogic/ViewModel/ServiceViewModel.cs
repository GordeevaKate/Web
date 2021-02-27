using BusinessLogic.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Runtime.Serialization;
using System.Text;

namespace BusinessLogic.ViewModel
{
    [DataContract]
    public class ServiceViewModel
    {
        [DataMember]
        public int Id { get; set; }
        [DataMember]
        [DisplayName("Название")]
        public string Name { get; set; }
        [DataMember]
        [DisplayName("Стоимость")]
        public double Cena { get; set; }
        [DataMember]
        [DisplayName("Тип услуги")]
        public ServiceStatus Status { get; set; }

    }
}
