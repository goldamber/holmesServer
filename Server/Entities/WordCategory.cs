using System.Collections.Generic;
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
        [DataMember]
        public string Name { get; set; }
        [DataMember]
        public string Abbreviation { get; set; }

        [DataMember]
        public List<Word> Words { get; set; }

        public WordCategory()
        {
            Words = new List<Word>();
        }
    }
}