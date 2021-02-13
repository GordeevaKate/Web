using System.Runtime.Serialization;
namespace BusinessLogic.BindingModel
{
    [DataContract]
    public   class WardBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public int Number { get; set; }
        [DataMember]
        public int Mesto { get; set; }
    }
}
