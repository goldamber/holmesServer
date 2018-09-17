using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    /// <summary>
    /// Name - the name of this category.
    /// Abbreviation - an abridged form of this category.
    /// Words - the list of words related to this category.
    /// </summary>
    public class WordCategory : EntityTable
    {
        [DataMember, Required]
        public string Name { get; set; }
        [DataMember]
        public string Abbreviation { get; set; }

        [DataMember]
        public virtual List<Word> Words { get; set; }

        public WordCategory()
        {
            Words = new List<Word>();
        }
    }
}