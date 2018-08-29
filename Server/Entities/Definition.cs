using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    /// <summary>
    /// Name - content of this definition.
    /// Words - a list of the words related to this definition.
    /// </summary>
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