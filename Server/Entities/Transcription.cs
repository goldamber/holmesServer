using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    public class Transcription : EntityTable
    {
        [DataMember, Required]
        public string British { get; set; }
        [DataMember, Required]
        public string American { get; set; }
        [DataMember, Required]
        public string Canadian { get; set; }
        [DataMember, Required]
        public string Australian { get; set; }
    }
}