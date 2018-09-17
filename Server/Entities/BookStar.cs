using System.ComponentModel.DataAnnotations;
using System.Runtime.Serialization;

namespace Server.Entities
{
    /// <summary>
    /// MarkCount - the quantity of marking stars for this book (1 - 5).
    /// VideoID - videos id.
    /// VideoName - the video.
    /// BookID - users id.
    /// BookName - the user.
    /// </summary>
    public class BookStar : EntityTable
    {
        [DataMember, Required]
        public int MarkCount { get; set; }

        [DataMember]
        public int BookID { get; set; }
        [DataMember]
        public virtual Book BookName { get; set; }
        [DataMember]
        public int UserID { get; set; }
        [DataMember]
        public virtual User UserName { get; set; }
    }
}