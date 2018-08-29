using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    public class Example : EntityTable
    {
        [DataMember, Required]
        public string Name { get; set; }

        [DataMember]
        public int WordID { get; set; }
        [DataMember]
        public Word Words { get; set; }
    }
}