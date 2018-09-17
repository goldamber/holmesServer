using System.Runtime.Serialization;

namespace Server.Entities
{
    /// <summary>
    /// Position - the number of a word, where a user stopped reading.
    /// UserID - the id of a reader.
    /// Users - the reader.
    /// BookID - the id of a book.
    /// Books - the book.
    /// </summary>
    public class Bookmark : EntityTable
    {
        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public virtual User Users { get; set; }

        [DataMember]
        public int BookID { get; set; }
        [DataMember]
        public virtual Book Books { get; set; }
    }
}
