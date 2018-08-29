using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    /// <summary>
    /// Name - a title o the group.
    /// Words - a list of words, to whom this group is connected.
    /// </summary>
    public class WordsGroup : EntityTable
    {
        [DataMember, Required]
        public string Name { get; set; }
        [DataMember]
        public virtual List<Word> Words { get; set; }

        public WordsGroup()
        {
            Words = new List<Word>();
        }
    }
}