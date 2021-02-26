using System.Runtime.Serialization;

namespace BusinessLogic.BindingModel
{
    [DataContract]
    public class PacientBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string  FIO{ get; set; }
        [DataMember]
        public string Polis { get; set; }
        [DataMember]
        public string Adress { get; set; }
    }
}
