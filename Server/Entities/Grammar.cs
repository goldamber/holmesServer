using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    /// <summary>
    /// Title - the topic of grammar.
    /// Description - rules description.
    /// Examples - a list of examples for this topic.
    /// Exceptions - a list of exceptions for this topic.
    /// </summary>
    public class Grammar : EntityTable
    {
        [DataMember, Required]
        public string Title { get; set; }
        [DataMember]
        public string Description { get; set; } = null;

        [DataMember]
        public virtual List<Rule> Rules { get; set; }
        [DataMember]
        public virtual List<GrammarExample> Examples { get; set; }
        [DataMember]
        public virtual List<GrammarException> Exceptions { get; set; }

        public Grammar()
        {
            Rules = new List<Rule>();
            Examples = new List<GrammarExample>();
            Exceptions = new List<GrammarException>();
        }
    }
}