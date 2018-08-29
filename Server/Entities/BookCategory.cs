using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    /// <summary>
    /// Name - the name of this category.
    /// Books - the list of books related to this category.
    /// </summary>
    public class BookCategory : EntityTable
    {
        [DataMember, Required]
        public string Name { get; set; }

        [DataMember]
        public virtual List<Book> Books { get; set; }

        public BookCategory()
        {
            Books = new List<Book>();
        }
    }
}