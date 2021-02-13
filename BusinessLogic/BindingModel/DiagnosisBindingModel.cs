using System.Runtime.Serialization;

namespace BusinessLogic.BindingModel
{
    [DataContract]
   public class DiagnosisBindingModel
    {
        [DataMember]
        public int? Id { get; set; }
        [DataMember]
        public string Name { get; set; }
        // Public string Mesto{get; set;} область заболевания : голова, легкие, глаза
    }
}
