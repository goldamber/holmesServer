using System.Runtime.Serialization;

namespace Server.Entities
{
    public class VideoBookmark : EntityTable
    {
        [DataMember]
        public int Position { get; set; }

        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public User Users { get; set; }

        [DataMember]
        public int VideoID { get; set; }
        [DataMember]
        public Video Videos { get; set; }
    }
}