using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    abstract public class EntityTable
    {
        [DataMember, Key]
        public int Id { get; set; }
    }
}