using System.Collections.Generic;
using System.Runtime.Serialization;

namespace Server.Entities
{
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