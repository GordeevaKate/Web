using BusinessLogic.Enums;
using System.Runtime.Serialization;

namespace BusinessLogic.BindingModel
{
    [DataContract]
    public class DoctorBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string Login { get; set; }
        [DataMember]
        public DoctorStatus Status { get; set; }
        [DataMember]
        public string Password { get; set; }

    }
}
