using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    public class Author : EntityTable
    {
        [DataMember, Required]
        public string Name { get; set; }
        [DataMember]
        public string Surname { get; set; }

        [DataMember]
        public virtual List<Book> Books { get; set; }

        public Author()
        {
            Books = new List<Book>();
        }
    }
}