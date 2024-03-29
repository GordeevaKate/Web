﻿using BusinessLogic.Enums;
using System.Runtime.Serialization;

namespace BusinessLogic.BindingModel
{
    [DataContract]
   public class ServiceBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public double Cena { get; set; }
        [DataMember]
        public ServiceStatus Status { get; set; }


    }
}
