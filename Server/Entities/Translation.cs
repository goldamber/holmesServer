using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    /// <summary>
    /// Name - content of this translation.
    /// Words - a list of the words related to this translation.
    /// </summary>
    public class Translation : EntityTable
    {
        [DataMember, Required]
        public string Name { get; set; }

        [DataMember]
        public virtual List<Word> Words { get; set; }

        public Translation()
        {
            Words = new List<Word>();
        }
    }
}