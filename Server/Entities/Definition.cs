using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    public class Definition : EntityTable
    {
        [DataMember, Required]
        public string Name { get; set; }

        [DataMember]
        public virtual List<Word> Words { get; set; }

        public Definition()
        {
            Words = new List<Word>();
        }
    }
}