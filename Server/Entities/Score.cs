using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    public class Score : EntityTable
    {
        [DataMember, Required]
        public int ScoreCount { get; set; }

        [DataMember, Required]
        public virtual Game GameName { get; set; }
        [DataMember, Required]
        public virtual User UserName { get; set; }
    }
}