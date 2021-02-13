using System.Runtime.Serialization;

namespace BusinessLogic.BindingModel
{
    [DataContract]
    public class PacientWardBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int PacientId { get; set; }
        [DataMember]
        public int WardId { get; set; }
    }
}
