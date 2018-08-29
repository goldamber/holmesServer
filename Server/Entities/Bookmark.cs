using System.Runtime.Serialization;

namespace Server.Entities
{
    public class Bookmark : EntityTable
    {
        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public User Users { get; set; }

        [DataMember]
        public int BookID { get; set; }
        [DataMember]
        public Book Books { get; set; }
    }
}
