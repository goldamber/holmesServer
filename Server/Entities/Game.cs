using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    public class Game : EntityTable
    {
        [DataMember, Required]
        public string Name { get; set; }
        [DataMember]
        public string Description { get; set; }
    }
}