using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    public class Rule : EntityTable
    {
        [DataMember, Required]
        public string Name { get; set; }

        [DataMember]
        public virtual List<Grammar> Grammars { get; set; }

        public Rule()
        {
            Grammars = new List<Grammar>();
        }
    }
}