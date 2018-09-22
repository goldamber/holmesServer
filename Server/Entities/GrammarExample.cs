using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    public class GrammarExample : EntityTable
    {
        [DataMember, Required]
        public string Name { get; set; }

        [DataMember]
        public virtual List<Grammar> Grammars { get; set; }

        public GrammarExample()
        {
            Grammars = new List<Grammar>();
        }
    }
}