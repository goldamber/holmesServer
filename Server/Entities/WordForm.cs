using System.Runtime.Serialization;

namespace Server.Entities
{
    public class WordForm : EntityTable
    {
        [DataMember]
        public string PastForm { get; set; }
        [DataMember]
        public string PastThForm { get; set; }
        [DataMember]
        public string PluralForm { get; set; }
    }
}