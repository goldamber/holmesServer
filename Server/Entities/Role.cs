using System.Runtime.Serialization;

namespace Server.Entities
{
    public class Role : EntityTable
    {
        [DataMember]
        public string Name { get; set; }
    }
}